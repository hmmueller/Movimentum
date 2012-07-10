using System.Collections.Generic;
using System.Linq;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestSolver {
        [Test]
        public void Test2SimpleConstraintsAndOneStep() {
            // Try to solve:
            //   0 = y + 2
            //   0 = x + y
            EqualsZeroConstraint e1 = new EqualsZeroConstraint(NV("y") + new Constant(2));
            EqualsZeroConstraint e2 = new EqualsZeroConstraint(NV("x") + NV("y"));
            var current = new SolverNode(new[] { e1, e2 }, new Dictionary<Variable, AbstractExpr>(), null);
            SolverNode solutionOrNull;

            // First step: Find that y = -2, and substitute -2 for y everywhere.
            IEnumerable<SolverNode> expanded = SolverNode.SolverStep(new[] { current }, new Dictionary<Variable, VariableValueRestriction>(), out solutionOrNull);

            Assert.AreEqual(1, expanded.Count());
            Assert.AreEqual(new EqualsZeroConstraint(NV("x") + new Constant(-2)), expanded.ElementAt(0).Constraints.ElementAt(0));
            Assert.IsNull(solutionOrNull);
        }

        //expanded = SolverNode.SolverStep(expanded, new Dictionary<Variable, VariableValueRestriction>(), out solutionOrNull);
        //Assert.AreEqual(1, expanded.Count());
        //Assert.IsNotNull(solutionOrNull);

        [Test]
        public void Test2SimpleConstraintsAndTwoSteps() {
            // Try to solve:
            //   0 = y + 2
            //   0 = x + y
            EqualsZeroConstraint e1 =
                new EqualsZeroConstraint(
                    NV("y") + new Constant(2));
            EqualsZeroConstraint e2 =
                new EqualsZeroConstraint(
                    NV("x") + NV("y"));
            var current = new SolverNode(new[] { e1, e2 }, new Dictionary<Variable, AbstractExpr>(), null);
            SolverNode solutionOrNull;

            // First step
            var NoRestrictions = new Dictionary<Variable, VariableValueRestriction>();
            IEnumerable<SolverNode> expanded1 =
                SolverNode.SolverStep(new[] { current },
                    NoRestrictions,
                    out solutionOrNull);
            Assert.IsNull(solutionOrNull);

            // Second step: Find that x = 2, substitute 2 for x everywhere.
            IEnumerable<SolverNode> expanded2 =
                SolverNode.SolverStep(expanded1,
                    NoRestrictions,
                    out solutionOrNull);

            // Check that we found the right solution.
            Assert.IsNotNull(solutionOrNull);
            AssertVariable(solutionOrNull.VariableValues, -2, "y");
            AssertVariable(solutionOrNull.VariableValues, 2, "x");
        }

        private static void AssertVariable(
                IDictionary<Variable, VariableValueRestriction> solution,
                double expected, string variablename) {
            VariableValueRestriction varKnowledge =
                solution[new NamedVariable(variablename)];
            Assert.IsNotNull(varKnowledge);
            Assert.AreEqual(expected, varKnowledge.Value, 1e-10);
        }

        private static NamedVariable NV(string n) {
            return new NamedVariable(n);
        }

        private UnaryExpression UE<TOp>(AbstractExpr e) where TOp : UnaryOperator, new() {
            return new UnaryExpression(e, new TOp());
        }

        [Test]
        public void TestTriagonalSystem() {
            var constraints = new[] {
            new EqualsZeroConstraint(NV("f") + 
                UE<Square>(NV("e") + NV("d") + NV("c") + NV("b") + NV("a") + new Constant(2.5))),
            new EqualsZeroConstraint(NV("e") + 
                UE<Cos>(NV("d") + NV("c") + NV("b") + NV("a") + new Constant(11.5))),
            new EqualsZeroConstraint(NV("d") + 
                UE<Square>(NV("c") + NV("b") + NV("a") + new Constant(-0.5))),
            new EqualsZeroConstraint(NV("c") + 
                UE<Cos>(NV("b") + NV("a") + new Constant(-58))),
            new EqualsZeroConstraint(NV("b") + 
                UE<Cos>(NV("a") + new Constant(1))),
            new EqualsZeroConstraint(NV("a") + new Constant(1))
        };

            IDictionary<Variable, VariableValueRestriction> solution =
                SolverNode.Solve(constraints,
                                    12,
                                    new Dictionary<Variable, VariableValueRestriction>(),
                                    0);
            AssertVariable(solution, -1, "a");
            AssertVariable(solution, -1, "b");
            AssertVariable(solution, -0.5, "c");
            AssertVariable(solution, -9, "d");
            AssertVariable(solution, -1, "e");
            AssertVariable(solution, -100, "f");
        }

    [Test]
    public void Test0VERewritingOneStep() {
        // The 0=V+E constraint: 0 = x + y.
        EqualsZeroConstraint ve = new EqualsZeroConstraint(NV("x") + NV("y"));
        // Another constraint that contains x: 0 = y + (3x + y + 6)
        EqualsZeroConstraint other = new EqualsZeroConstraint(
            NV("y") + (new Constant(3) * NV("x") + NV("y") + new Constant(6)));

        var current = new SolverNode(new[] { other, ve }, new Dictionary<Variable, AbstractExpr>(), null);

        // Do a step
        SolverNode solutionOrNull;
        IEnumerable<SolverNode> result =
            SolverNode.SolverStep(new[] { current }, out solutionOrNull);

        // Afterwards, we have a new node, but not yet a solution.
        Assert.AreEqual(1, result.Count());
        Assert.IsNull(solutionOrNull);

        SolverNode resultNode = result.ElementAt(0);
        Assert.AreEqual(1, resultNode.Constraints.Count());

        // The single constraint is other, with x replaced with -y.
        Assert.AreEqual(new EqualsZeroConstraint(
            NV("y") + (new Constant(3) * -NV("y") + NV("y") + new Constant(6))),
            resultNode.Constraints.ElementAt(0));

        // Moreover, we have a backsubstitution.
        Assert.AreEqual(-NV("y"), resultNode.Backsubstitutions[NV("x")]);
    }

    [Test]
    public void Test0VERewritingTwoSteps() {
        EqualsZeroConstraint veConstraint1 = 
            new EqualsZeroConstraint(NV("z") + (NV("x") + new Constant(4)));
        EqualsZeroConstraint veConstraint2 = 
            new EqualsZeroConstraint(NV("x") + NV("y"));
        EqualsZeroConstraint constraint3 = new EqualsZeroConstraint(
            NV("y") + (new Constant(5) * NV("x") + NV("y") + NV("z")));

        var current = new SolverNode(new[] { 
            veConstraint1, veConstraint2, constraint3}, new Dictionary<Variable, AbstractExpr>(), null);

        // Do two steps that rewrite ve1 and ve2.
        SolverNode solutionOrNull;
        IEnumerable<SolverNode> expanded1 =
            SolverNode.SolverStep(new[] { current }, out solutionOrNull);
        Assert.AreEqual(1, expanded1.Count());
        Assert.IsNull(solutionOrNull);

        // After this first step, we should have the backsubstitution
        //   z &rarr; -(x+4)
        // and the constraints
        //   0 = x + y
        //   0 = y + (5x + y + -(x+4))
        IEnumerable<SolverNode> expanded2 =
            SolverNode.SolverStep(new[] { expanded1.First() }, out solutionOrNull);
        Assert.AreEqual(1, expanded2.Count());
        Assert.IsNull(solutionOrNull);

        // After this first step, we should have the two backsubstitutions
        //   z &rarr; -(x+4)
        //   x &rarr; -y
        // and the single constraints
        //   0 = y + (5(-y) + y + -((-y)+4))
        SolverNode expanded2Node = expanded2.ElementAt(0);
        Assert.AreEqual(1, expanded2Node.Constraints.Count());

        // Now, we must solve the last single constraint. It can be
        // simplified to
        //   0 = y - 5y + y + y - 4
        // or
        //   0 = -2y - 4
        // which gives us
        //   y = -2
        // We simulate this solving by creating a new node with the 
        // correct value for y:
        SolverNode result = 
            expanded2Node.RememberAndSubstituteVariable(NV("y"), -2, null);

        // This now should put all solutions into the variable knowledge:
        Assert.AreEqual(-2, result.VariableValues[NV("y")].Value);
        Assert.AreEqual(2, result.VariableValues[NV("x")].Value);
        Assert.AreEqual(-6, result.VariableValues[NV("z")].Value);
    }
    }
}
