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
            var current = new SolverNode(new[] { e1, e2 }, null);
            SolverNode solutionOrNull;

            // First step: Find that y = -2, and substitute -2 for y everywhere.
            IEnumerable<SolverNode> expanded = SolverNode.SolverStep(new[] { current }, new Dictionary<Variable, VariableRangeRestriction>(), out solutionOrNull);

            Assert.AreEqual(1, expanded.Count());
            Assert.AreEqual(new EqualsZeroConstraint(new Constant(0)), expanded.ElementAt(0).Constraints.ElementAt(0));
            Assert.AreEqual(new EqualsZeroConstraint(NV("x") + new Constant(-2)), expanded.ElementAt(0).Constraints.ElementAt(1));
            Assert.IsNull(solutionOrNull);
        }

        //expanded = SolverNode.SolverStep(expanded, new Dictionary<Variable, VariableRangeRestriction>(), out solutionOrNull);
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
            var current = new SolverNode(new[] { e1, e2 }, null);
            SolverNode solutionOrNull;

            // First step
            var NoRestrictions = new Dictionary<Variable, VariableRangeRestriction>();
            IEnumerable<SolverNode> expanded1 =
                SolverNode.SolverStep(new[] { current },
                    NoRestrictions,
                    out solutionOrNull);
            Assert.IsNull(solutionOrNull);

            // Second step: Remove 0 = 0
            IEnumerable<SolverNode> expanded2 =
                SolverNode.SolverStep(expanded1,
                    NoRestrictions,
                    out solutionOrNull);
            Assert.IsNull(solutionOrNull);

            // Third step: Find that x = 2, substitute 2 for x everywhere.
            IEnumerable<SolverNode> expanded3 =
                SolverNode.SolverStep(expanded2,
                    NoRestrictions,
                    out solutionOrNull);
            Assert.IsNull(solutionOrNull);

            // Fourth step: Remove 0 = 0.
            IEnumerable<SolverNode> expanded4 =
                SolverNode.SolverStep(expanded3,
                    NoRestrictions,
                    out solutionOrNull);
            Assert.AreEqual(1, expanded4.Count());

            // Check that we found the right solution.
            Assert.IsNotNull(solutionOrNull);
            AssertVariable(solutionOrNull.VariableInRangeKnowledges, -2, "y");
            AssertVariable(solutionOrNull.VariableInRangeKnowledges, 2, "x");
        }

        private static void AssertVariable(
                IDictionary<Variable, VariableRangeRestriction> solution,
                double expected, string variablename) {
            VariableValueRestriction varKnowledge =
                solution[new NamedVariable(variablename)]
                as VariableValueRestriction;
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

            IDictionary<Variable, VariableRangeRestriction> solution =
                SolverNode.Solve(constraints,
                                    12,
                                    new Dictionary<Variable, VariableRangeRestriction>(),
                                    0);
            AssertVariable(solution, -1, "a");
            AssertVariable(solution, -1, "b");
            AssertVariable(solution, -0.5, "c");
            AssertVariable(solution, -9, "d");
            AssertVariable(solution, -1, "e");
            AssertVariable(solution, -100, "f");
        }
    }
}
