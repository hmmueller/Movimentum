using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestStringVisitors {
        [Test]
        public void TestConstant() {
            IConstant input = Polynomial.CreateConstant(1.5);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1.5", result);
            Assert.AreEqual("{Constant}1.5", input.ToString());
        }

        [Test]
        public void TestVariable() {
            INamedVariable input = Polynomial.CreateNamedVariable("a");
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("a", result);
            Assert.AreEqual("{NamedVariable}a", input.ToString());
        }

        [Test]
        public void TestWithoutParentheses0() {
            AbstractExpr input = Polynomial.CreateConstant(1).C + Polynomial.CreateNamedVariable("z");
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+z", result);
        }
        [Test]
        public void TestWithoutParentheses1() {
            AbstractExpr input = (Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2)) + Polynomial.CreateConstant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2+4", result);
        }
        [Test]
        public void TestWithoutParentheses2() {
            AbstractExpr input = Polynomial.CreateConstant(1).C + (Polynomial.CreateConstant(2).C + Polynomial.CreateConstant(4));
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2+4", result);
        }

        [Test]
        public void TestWithoutParentheses3() {
            AbstractExpr input = Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2) + Polynomial.CreateConstant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2+4", result);
        }

        [Test]
        public void TestWithoutParentheses4() {
            AbstractExpr input = Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2).C * Polynomial.CreateConstant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("1+2*4", result);
        }
        [Test]
        public void TestWithParentheses1() {
            AbstractExpr input = (Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2)) * Polynomial.CreateConstant(4);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(1+2)*4", result);
        }

        [Test]
        public void TestComplex() {
            AbstractExpr input = ((Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2)) + -Polynomial.CreateConstant(4).C) / Polynomial.CreateConstant(8) + Polynomial.CreateNamedVariable("x") + -Polynomial.CreateConstant(16).C;
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(1+2+-4)/8+x+-16", result);
        }

        [Test]
        public void TestComplexExpression() {
            AbstractExpr input = ((Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2)) + -Polynomial.CreateConstant(4).C)
                                / Polynomial.CreateConstant(8) + Polynomial.CreateNamedVariable("x") + -Polynomial.CreateConstant(16).C;
            Assert.AreEqual("{BinaryExpression}(1+2+-4)/8+x+-16", input.ToString());
        }
        [Test]
        public void TestComplexConstraint() {
            var input = new EqualsZeroConstraint(((Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2)) + -Polynomial.CreateConstant(4).C)
                        / Polynomial.CreateConstant(8) + Polynomial.CreateNamedVariable("x") + -Polynomial.CreateConstant(16).C);
            Assert.AreEqual("{EqualsZeroConstraint}0 = (1+2+-4)/8+x+-16", input.ToString());
            //Assert.AreEqual("{EqualsZeroConstraint}0 = -0.125+x+-16", input.ToString());
        }

        [Test]
        public void TestUnaryMinusOfConstant() {
            AbstractExpr input = -Polynomial.CreateConstant(1).C;
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-1", result);
        }
        [Test]
        public void TestUnaryMinusOfUnaryMinus() {
            AbstractExpr input = -(-Polynomial.CreateConstant(1).C);
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("--1", result);
        }

        [Test]
        public void TestUnaryMinusOfSin() {
            AbstractExpr input = -new UnaryExpression(Polynomial.CreateConstant(1), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-sin 1", result);
        }
        [Test]
        public void TestSinOfUnaryMinus() {
            AbstractExpr input = new UnaryExpression(-Polynomial.CreateConstant(1).C, new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("sin (-1)", result);
        }

        [Test]
        public void TestUnaryMinusOfUnaryMinusOfSin() {
            AbstractExpr input = -(-new UnaryExpression(Polynomial.CreateConstant(1), new Sin()));
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("--sin 1", result);
        }

        [Test]
        public void TestSinOfSum() {
            AbstractExpr input = new UnaryExpression(Polynomial.CreateNamedVariable("a").C + Polynomial.CreateConstant(1), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("sin (a+1)", result);
        }

        [Test]
        public void TestSquareOfVariable() {
            AbstractExpr input = new UnaryExpression(Polynomial.CreateNamedVariable("a"), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("a²", result);
        }
        [Test]
        public void TestSquareOfUnaryMinus() {
            AbstractExpr input = new UnaryExpression(-Polynomial.CreateNamedVariable("a").C, new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(-a)²", result);
        }
        [Test]
        public void TestUnaryMinusOfSquare() {
            AbstractExpr input = -new UnaryExpression(Polynomial.CreateNamedVariable("a"), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-a²", result);
        }
        [Test]
        public void TestSquareOfSin() {
            AbstractExpr input = new UnaryExpression(new UnaryExpression(Polynomial.CreateNamedVariable("a"), new Sin()), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(sin a)²", result);
        }
        [Test]
        public void TestSinOfSquare() {
            AbstractExpr input = new UnaryExpression(new UnaryExpression(Polynomial.CreateNamedVariable("a"), new Square()), new Sin());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("sin a²", result);
        }
        [Test]
        public void TestSquareOfSum() {
            AbstractExpr input = new UnaryExpression(Polynomial.CreateNamedVariable("a").C + Polynomial.CreateConstant(1), new Square());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("(a+1)²", result);
        }

        [Test]
        public void TestUnaryMinusOfSum() {
            AbstractExpr input = new UnaryExpression(Polynomial.CreateNamedVariable("a").C + Polynomial.CreateConstant(1), new UnaryMinus());
            string result = input.Accept(new ToStringVisitor(), 0);
            Assert.AreEqual("-(a+1)", result);
        }
    }
}
