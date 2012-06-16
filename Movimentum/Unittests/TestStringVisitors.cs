using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestStringVisitors {
        private static readonly ToStringVisitor visitor = new ToStringVisitor();
        private const int p = -1;
        //private static readonly SimpleToStringVisitor visitor = new SimpleToStringVisitor();
        //private static readonly Ignore p = Ig.nore;

        // tsv.1
        [Test]
        public void TestConstant() {
            Constant input = new Constant(1.5);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1.5", result);
            Assert.AreEqual("{Constant}1.5", input.ToString());
            Assert.AreEqual("1.5", result);
            Assert.AreEqual("{Constant}" + "1.5", input.ToString());
        }

        [Test]
        public void TestVariable() {
            NamedVariable input = new NamedVariable("a");
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("a", result);
            Assert.AreEqual("{NamedVariable}a", input.ToString());
        }

        [Test]
        public void TestWithoutParentheses0() {
            AbstractExpr input = new Constant(1) + new NamedVariable("z");
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+z", result);
        }
        [Test]
        public void TestWithoutParentheses1() {
            AbstractExpr input = (new Constant(1) + new Constant(2)) + new Constant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2+4", result);
        }
        [Test]
        public void TestWithoutParentheses2() {
            AbstractExpr input = new Constant(1) + (new Constant(2) + new Constant(4));
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2+4", result);
        }

        [Test]
        public void TestWithoutParentheses3() {
            AbstractExpr input = new Constant(1) + new Constant(2) + new Constant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2+4", result);
        }

        [Test]
        public void TestWithoutParentheses4() {
            AbstractExpr input = new Constant(1) + new Constant(2) * new Constant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2*4", result);
        }
        [Test]
        public void TestWithParentheses1() {
            AbstractExpr input = (new Constant(1) + new Constant(2)) * new Constant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(1+2)*4", result);
        }

        [Test]
        public void TestComplex() {
            AbstractExpr input = ((new Constant(1) + new Constant(2)) + -new Constant(4)) / new Constant(8) + new NamedVariable("x") + -new Constant(16);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(1+2+-4)/8+x+-16", result);
        }

        [Test]
        public void TestComplexExpression() {
            AbstractExpr input = ((new Constant(1) + new Constant(2)) + -new Constant(4))
                                / new Constant(8) + new NamedVariable("x") + -new Constant(16);
            Assert.AreEqual("{BinaryExpression}(1+2+-4)/8+x+-16", input.ToString());
        }
        [Test]
        public void TestComplexConstraint() {
            var input = new EqualsZeroConstraint(((new Constant(1) + new Constant(2)) + -new Constant(4)) 
                        / new Constant(8) + new NamedVariable("x") + -new Constant(16));
            //Assert.AreEqual("{EqualsZeroConstraint}0 = (1+2+-4)/8+x+-16", input.ToString());
            Assert.AreEqual("{EqualsZeroConstraint}0 = -0.125+x+-16", input.ToString());
        }

        [Test]
        public void TestUnaryMinusOfConstant() {
            AbstractExpr input = -new Constant(1);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-1", result);
        }
        [Test]
        public void TestUnaryMinusOfUnaryMinus() {
            AbstractExpr input = -(-new Constant(1));
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("--1", result);
        }

        [Test]
        public void TestUnaryMinusOfSin() {
            AbstractExpr input = -new UnaryExpression(new Constant(1), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-sin 1", result);
        }
        [Test]
        public void TestSinOfUnaryMinus() {
            AbstractExpr input = new UnaryExpression(-new Constant(1), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("sin (-1)", result);
        }

        [Test]
        public void TestUnaryMinusOfUnaryMinusOfSin() {
            AbstractExpr input = -(-new UnaryExpression(new Constant(1), new Sin()));
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("--sin 1", result);
        }

        [Test]
        public void TestSinOfSum() {
            AbstractExpr input = new UnaryExpression(new NamedVariable("a") + new Constant(1), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("sin (a+1)", result);
        }

        [Test]
        public void TestSquareOfVariable() {
            AbstractExpr input = new UnaryExpression(new NamedVariable("a"), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("a²", result);
        }
        [Test]
        public void TestSquareOfUnaryMinus() {
            AbstractExpr input = new UnaryExpression(-new NamedVariable("a"), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(-a)²", result);
        }
        [Test]
        public void TestUnaryMinusOfSquare() {
            AbstractExpr input = -new UnaryExpression(new NamedVariable("a"), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-a²", result);
        }
        [Test]
        public void TestSquareOfSin() {
            AbstractExpr input = new UnaryExpression(new UnaryExpression(new NamedVariable("a"), new Sin()), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(sin a)²", result);
        }
        [Test]
        public void TestSinOfSquare() {
            AbstractExpr input = new UnaryExpression(new UnaryExpression(new NamedVariable("a"), new Square()), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("sin a²", result);
        }
        [Test]
        public void TestSquareOfSum() {
            AbstractExpr input = new UnaryExpression(new NamedVariable("a") + new Constant(1), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(a+1)²", result);
        }

        [Test]
        public void TestUnaryMinusOfSum() {
            AbstractExpr input = new UnaryExpression(new NamedVariable("a") + new Constant(1), new UnaryMinus());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-(a+1)", result);
        }
    }
}
