using System.Collections.Generic;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    public class TestForEvaluateVisitor {
        [Test]
        public void TestConstant() {
            var c = new Constant(3.0);
            double? d = c.Accept(new EvaluationVisitor(new Dictionary<Variable, double>()), Ig.nore);
            Assert.AreEqual(3.0, d, 1e-8);
        }

        [Test]
        public void TestPlusAndVariable() {
            var c = new BinaryExpression(new NamedVariable("a"), new Plus(), new Constant(2.0));
            double? d = c.Accept(new EvaluationVisitor(new Dictionary<Variable, double> { { new NamedVariable("a"), 1.0 } }), Ig.nore);
            Assert.AreEqual(3.0, d, 1e-8);
        }

        [Test]
        public void TestMinusAndUnaryMinusAndTimesandSquareroot() {
            var c = new BinaryExpression(new UnaryExpression(new NamedVariable("a"), new UnaryMinus()), new Times(),
                new UnaryExpression(new Constant(16.0), new Squareroot()));
            double? d = c.Accept(new EvaluationVisitor(new Dictionary<Variable, double> { { new NamedVariable("a"), 1.0 } }), Ig.nore);
            Assert.AreEqual(-4.0, d, 1e-8);
        }
    }
}
