//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Movimentum.Model;
//using Movimentum.SubstitutionSolver3;
//using NUnit.Framework;

//namespace Movimentum.Unittests {
//    [TestFixture]
//    class TestSolveConstraintSet {
//        [Test]
//        public void Test2SimpleConstraintsAndOneStep() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                               new ScalarEqualityConstraint("b", new ConstScalar(2)),
//                           };
//            SolverNode set = SolverNode.Create(constraints, 0, 0);
//            IEnumerable<SolverNode> setsAfterOneStep = set.Expand(new Dictionary<Variable, VariableRangeRestriction>());
//            Assert.AreEqual(new EqualsZeroConstraint(new Constant(0)), setsAfterOneStep.ElementAt(0).Constraints.ElementAt(0));
//            //Assert.AreEqual(new VariableInRangeKnowledge(new NamedVariable("a"), 1, 1), setsAfterOneStep.ElementAt(0).VariableInRangeKnowledges.ElementAt(0));
//            AssertVariables(setsAfterOneStep.ElementAt(0).VariableInRangeKnowledges, new Dictionary<string, double> { { "a", 1 } });
//        }

//        [Test]
//        public void Test2SimpleConstraintsAndManySteps() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                               new ScalarEqualityConstraint("b", new ConstScalar(2)),
//                           };
//            OLDSolverNode result = RunSolverLoop(constraints);
//            Assert.IsFalse(result.IsDeadEnd);

//            AssertVariables(result.VariableInRangeKnowledges, new Dictionary<string, double> { { "a", 1 }, { "b", 2 } });
//            //List<VariableInRangeKnowledge> variableInRanges = result.VariableInRangeKnowledges.ToList();
//            //variableInRanges.Sort((x, y) => x.Variable.Name.CompareTo(y.Variable.Name));
//            //Assert.AreEqual(new VariableInRangeKnowledge(new NamedVariable("a"), 1, 1), variableInRanges.ElementAt(0));
//            //Assert.AreEqual(new VariableInRangeKnowledge(new NamedVariable("b"), 2, 2), variableInRanges.ElementAt(1));
//        }

//        private static OLDSolverNode RunSolverLoop(IEnumerable<ScalarEqualityConstraint> constraints) {
//            SolverNode set = SolverNode.Create(constraints, 0, 0);
//            int stepCt = 0;
//            while (set.Constraints.Any()) {
//                IEnumerable<SolverNode> expandedSets = set.Expand(new Dictionary<Variable, VariableRangeRestriction>());
//                Assert.AreEqual(1, expandedSets.Count());
//                set = expandedSets.ElementAt(0);
//                stepCt++;
//                Assert.That(stepCt, Is.AtMost(10));
//            }
//            return set;
//        }

//        [Test]
//        public void Test2SimpleConstraintsAndManyStepsThatReturnFalse() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                               new ScalarEqualityConstraint("a", new ConstScalar(2)),
//                           };
//            OLDSolverNode result = RunSolverLoop(constraints);
//            Assert.IsTrue(result.IsDeadEnd);
//        }

//        [Test]
//        public void Test2SimpleConstraintsAndManyStepsThatReturnTrue() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                           };
//            OLDSolverNode result = RunSolverLoop(constraints);
//            Assert.IsFalse(result.IsDeadEnd);
//            //Assert.AreEqual(new VariableInRangeKnowledge(new NamedVariable("a"), 1, 1), result.VariableInRangeKnowledges.ElementAt(0));
//            AssertVariables(result.VariableInRangeKnowledges, new Dictionary<string, double> { { "a", 1 } });
//        }

//        [Test]
//        public void Test2DependentConstraints() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                               new ScalarEqualityConstraint("b", new ScalarVariable("a")),
//                           };
//            OLDSolverNode result = RunSolverLoop(constraints);
//            Assert.IsFalse(result.IsDeadEnd);

//            AssertVariables(result.VariableInRangeKnowledges, new Dictionary<string, double> { { "a", 1 }, { "b", 1 } });
//        }

//        private void AssertVariables(IDictionary<Variable, VariableRangeRestriction> variableInRanges, IEnumerable<KeyValuePair<string, double>> name2value) {
//            Assert.AreEqual(name2value.Count(), variableInRanges.Count());
//            foreach (var kvp in name2value) {
//                VariableRangeRestriction v = variableInRanges.First(k => k.Key.Name == kvp.Key).Value;
//                Assert.AreEqual(kvp.Value, v.GetSomeValue());
//            }
//        }

//        [Test]
//        public void Test2DependentConstraintsWithPlus() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("b", 
//                                   new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.PLUS, new ConstScalar(5))),
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                           };
//            OLDSolverNode result = RunSolverLoop(constraints);
//            Assert.IsFalse(result.IsDeadEnd);

//            AssertVariables(result.VariableInRangeKnowledges, new Dictionary<string, double> { { "a", 1 }, { "b", 6 } });
//        }

//        [Test]
//        public void Solve2DependentConstraints() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("b", 
//                                   new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.PLUS, new ConstScalar(5))),
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                           };
//            IDictionary<Variable, VariableRangeRestriction> result = SolverNode.Solve(constraints, 0, 0, 10, new Dictionary<Variable, VariableRangeRestriction>(), -1);
//            AssertVariables(result, new Dictionary<string, double> { { "a", 1 }, { "b", 6 } });
//        }

//        [Test]
//        public void Solve2DependentConstraintsWithContradiction() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new ConstScalar(5)),
//                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
//                           };
//            Assert.Throws<Exception>(() => SolverNode.Solve(constraints, 1, 1, 10, new Dictionary<Variable, VariableRangeRestriction>(), -1));
//        }

//        [Test]
//        public void Solve2DependentConstraintsWithFormalSquareroots() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT, new ConstScalar(9))),
//                               new ScalarEqualityConstraint("a", new ConstScalar(-3)),
//                           };
//            IDictionary<Variable, VariableRangeRestriction> result = SolverNode.Solve(constraints, 2, 2, 20, new Dictionary<Variable, VariableRangeRestriction>(), -1);
//            AssertVariables(result, new Dictionary<string, double> { { "a", -3 } });
//        }


//        [Test]
//        public void Solve4DependentConstraintsWithFormalSquareroots() {
//            ScalarEqualityConstraint[] constraints = new[] {
//                               new ScalarEqualityConstraint("a", new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT, new ConstScalar(81))),
//                               new ScalarEqualityConstraint("a", new ConstScalar(9)),
//                               new ScalarEqualityConstraint("b", new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT, new ScalarVariable("a"))),
//                               new ScalarEqualityConstraint("b", new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.MINUS, new ConstScalar(12))),
//                           };
//            IDictionary<Variable, VariableRangeRestriction> result = SolverNode.Solve(constraints, 3, 3, 20, new Dictionary<Variable, VariableRangeRestriction>(), -1);
//            AssertVariables(result, new Dictionary<string, double> { { "a", 9 }, { "b", -3 } });
//        }
//    }
//}
