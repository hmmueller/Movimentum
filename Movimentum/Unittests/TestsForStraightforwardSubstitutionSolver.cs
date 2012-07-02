using System;
using System.Collections.Generic;
using System.Linq;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    public class TestsForStraightforwardSubstitutionSolver {
        [Test]
        public void TestOnlyConstantAssignment() {
            var a = new NamedVariable("a");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] { new EqualsZeroConstraint(new BinaryExpression(a, new Plus(), new Constant(6))) });
            Assert.AreEqual(-6, result[a]);
        }

        [Test]
        public void TestOnlyConstantAssignments() {
            var a = new NamedVariable("a");
            var b = new NamedVariable("bb");
            var c = new NamedVariable("ccc");
            var d = new NamedVariable("dd_dd");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                    new EqualsZeroConstraint(new BinaryExpression(a, new Plus(), new Constant(-6))),
                    new EqualsZeroConstraint(new BinaryExpression(b, new Plus(),
                        new UnaryExpression(
                            new BinaryExpression(new Constant(3), new Times(), new Constant(2)),
                            new UnaryMinus())
                        )
                    ),
                    new EqualsZeroConstraint(new BinaryExpression(c, new Plus(), new UnaryExpression(new Constant(6), new UnaryMinus()))),
                    new EqualsZeroConstraint(new BinaryExpression(d, new Plus(), new UnaryExpression(new UnaryExpression(new Constant(36), new Squareroot()), new UnaryMinus()))),
                });
            Assert.AreEqual(6, result[a]);
            Assert.AreEqual(6, result[b]);
            Assert.AreEqual(6, result[c]);
            Assert.AreEqual(6, result[d]);
        }

        [Test]
        public void TestSimpleReversedChain() {
            var a = new NamedVariable("a");
            var b = new NamedVariable("b");
            var c = new NamedVariable("c");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                          new EqualsZeroConstraint(new BinaryExpression(a, new Plus(), new UnaryExpression(b, new UnaryMinus()))),
                          new EqualsZeroConstraint(new BinaryExpression(b, new Plus(), new UnaryExpression(c, new UnaryMinus()))),
                          new EqualsZeroConstraint(new BinaryExpression(c, new Plus(), new UnaryExpression(new Constant(6), new UnaryMinus())))
                      });
            Assert.AreEqual(6, result[a]);
            Assert.AreEqual(6, result[b]);
            Assert.AreEqual(6, result[c]);
        }

        [Test]
        public void TestSimpleReversedTree() {
            var a = new NamedVariable("a");
            var b = new NamedVariable("b");
            var c = new NamedVariable("c");
            var d = new NamedVariable("d");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                          new EqualsZeroConstraint(new BinaryExpression(a, new Plus(), new UnaryExpression(c, new Squareroot()))),
                          new EqualsZeroConstraint(new BinaryExpression(b, new Plus(), new UnaryExpression(c, new Squareroot()))),
                          new EqualsZeroConstraint(new BinaryExpression(c, new Plus(), new UnaryExpression(new UnaryExpression(d, new Squareroot()), new UnaryMinus()))),
                          new EqualsZeroConstraint(new BinaryExpression(d, new Plus(), new UnaryExpression(new Constant(81), new UnaryMinus())))
                      });
            Assert.AreEqual(-3, result[a]);
            Assert.AreEqual(-3, result[b]);
            Assert.AreEqual(9, result[c]);
            Assert.AreEqual(81, result[d]);
        }

        [Test]
        public void TestSimpleLinearEquation() {
            var a = new NamedVariable("a");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] { new EqualsZeroConstraint(new BinaryExpression(a, new Plus(), a)) });
            Assert.AreEqual(0, result[a]);
        }

        [Test]
        public void TestSimpleLinearEquation1() {
            var a = new NamedVariable("a");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                    new EqualsZeroConstraint(
                        new BinaryExpression(
                            a, 
                            new Plus(), 
                            new UnaryExpression(
                                new BinaryExpression(
                                    new Constant(4), 
                                    new Plus(), 
                                    new UnaryExpression(a, new UnaryMinus())
                                ), 
                                new UnaryMinus()
                            )
                        )
                    )
                });
            Assert.AreEqual(2, result[a]);
        }

        [Test]
        public void TestSimpleCubicEquationWithSingleSolution() {
            // 0 = a³-a+6 or 0 = a(a²-1)+6 --> a = -2.
            var a = new NamedVariable("a");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                    (a*(a*a+(-new Constant(1)))+new Constant(6)).EqualsZero()
                });
            Assert.AreEqual(-2.0, result[a], 1e-8);
        }

        [Test]
        public void TestSimpleCubicEquationAndDependents() {
            // 0 = a³-a+6 or 0 = a(a²-1)+6 --> a = -2.
            var a = new NamedVariable("a");
            var b = new NamedVariable("b");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                    (b+-(-a+-new Constant(4))).EqualsZero(),
                    (a*(a*a+(-new Constant(1)))+new Constant(6)).EqualsZero()
                });
            Assert.AreEqual(-2.0, result[a], 1e-8);
            Assert.AreEqual(-2.0, result[b], 1e-8);
        }

        [Test]
        public void TestToStringPrecedence() {
            var a = new NamedVariable("a");
            var constraint = (a * (a * a + (-new Constant(1))) + new Constant(6)).EqualsZero();
            string[] result = constraint.ToString().Replace(" ", "").Split('[');

            Assert.AreEqual("0=a*(a*a+-1)+6", result[0]);
        }

        [Test]
        public void TestSimpleCubicEquationAndDependents2() {
            // 0 = a³-a+6 or 0 = a(a²-1)+6 --> a = -2.
            var a = new NamedVariable("a");
            var b = new NamedVariable("b");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] {
                    (-b+-(-a+-new Constant(4))).EqualsZero(),
                    (a*(a*a+(-new Constant(1)))+new Constant(6)).EqualsZero()
                });
            Assert.AreEqual(-2.0, result[a], 1e-8);
            Assert.AreEqual(2.0, result[b], 1e-8);
        }

        [Test]
        public void TestSquareRootEquation() {
            var a = new NamedVariable("a");
            IDictionary<Variable, double> result = StraightforwardSubstitutionSolver.Solve(-100, 100,
                new[] { new EqualsZeroConstraint(
                    new BinaryExpression(a, 
                        new Plus(), 
                        new UnaryExpression(
                            new UnaryExpression(
                                a, 
                                new Squareroot()), 
                            new UnaryMinus()))) });
            Assert.IsTrue(result[a].Near(0) || result[a].Near(1));
        }

        [Test]
        public void TestFoldConstants() {
            var a = new NamedVariable("a");
            AbstractExpr constSubExpr = new Constant(3) + new UnaryExpression(new Constant(1) * new Constant(2), new Cos());
            var expr = a * constSubExpr;
            var result = expr.Accept(new ConstantFoldingVisitor(), Ig.nore);
            double value = EvaluationVisitor.Evaluate(constSubExpr, DictionaryUtils.Empty<Variable, double>());
            AbstractExpr rhs = ((BinaryExpression)result).Rhs;
            Assert.IsInstanceOf<Constant>(rhs);
            Assert.AreEqual(value, ((Constant)rhs).Value, 1e-8);
        }

        [Test]
        public void TestIllinois2() {
            IEnumerable<double> result = Illinois2SolvingStrategy.SolveOuter(-10000, 10000, x => (x - 2) * (x - 2) - 1, 1, 0);
            AreSolutions(new[] { 1.0, 3 }, result, 1e-5);
        }


        [Test]
        public void TestIllinois2_1() {
            IEnumerable<double> result = Illinois2SolvingStrategy.SolveOuter(-100000, 100000, x => 500 + -(Square(100 + -(100 + x * 0.156434465195377 + 0)) + 0), 0.1, 0);
            AreSolutions(new[] { 142.0, -142.0 }, result, 1);
        }

        private void AreSolutions(double[] expected, IEnumerable<double> result, double delta) {
            Assert.AreEqual(expected.Length, result.Count());
            foreach (var e in expected) {
                Assert.IsTrue(result.Any(r => Math.Abs(e - r) <= delta));
            }
        }

        private static double Square(double x) {
            return x * x;
        }
    }
}
