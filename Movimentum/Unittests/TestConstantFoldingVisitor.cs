using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestConstantFoldingVisitor {
        private readonly ConstantFoldingVisitor visitor = new ConstantFoldingVisitor();

        [Test]
        public void TestConstant() {
            IConstant input = Polynomial.CreateConstant(1.5);
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1.5), result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestVariable() {
            INamedVariable input = Polynomial.CreateNamedVariable("a");
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoFoldConstant() {
            AbstractExpr input = (Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(2)) * Polynomial.CreateConstant(4);
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(12), result);
        }

        [Test]
        public void TestDontFoldConstant() {
            AbstractExpr input = (Polynomial.CreateConstant(1).E + Polynomial.CreateNamedVariable("b")) * Polynomial.CreateConstant(4);
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(input, result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoFoldConstantLeftAndRightOfBinaryOp() {
            AbstractExpr input = (Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(2)) / (Polynomial.CreateConstant(3).E + Polynomial.CreateConstant(4));
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant((1.0 + 2.0) / (3.0 + 4.0)), result);
        }

        [Test]
        public void TestDoFoldConstantOnlyOnLeftSideOfBinaryOp() {
            AbstractExpr input = (Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(2)) * Polynomial.CreateNamedVariable("c");
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(3).E * Polynomial.CreateNamedVariable("c"), result);
        }

        [Test]
        public void TestDoFoldConstantOnlyOnRightSideOfBinaryOp() {
            AbstractExpr input = Polynomial.CreateNamedVariable("a").E * (Polynomial.CreateConstant(2).E + Polynomial.CreateConstant(3));
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateNamedVariable("a").E * Polynomial.CreateConstant(5), result);
        }

        [Test]
        public void TestDontFoldConstantInBinaryOp() {
            AbstractExpr input = Polynomial.CreateNamedVariable("a").E * (Polynomial.CreateConstant(2).E + Polynomial.CreateNamedVariable("c"));
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(input, result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDontFoldConstantInFormalSquareRoot() {
            AbstractExpr input = new UnaryExpression(Polynomial.CreateConstant(4), new FormalSquareroot());
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(input, result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoFoldConstantInPositiveSquareRoot() {
            AbstractExpr input = new UnaryExpression(Polynomial.CreateConstant(4), new PositiveSquareroot());
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreNotEqual(input, result);
            Assert.AreEqual(Polynomial.CreateConstant(2), result);
        }

        [Test]
        public void TestDoFoldConstantInUnaryMinus() {
            AbstractExpr input = -Polynomial.CreateConstant(4).E;
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreNotEqual(input, result);
            Assert.AreEqual(Polynomial.CreateConstant(-4), result);
        }

        [Test]
        public void TestFoldingsInSimpleSums() {
            AbstractExpr input1 = Polynomial.CreateNamedVariable("a").E + Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(2);
            AbstractExpr input2 = Polynomial.CreateConstant(1).E + Polynomial.CreateNamedVariable("a") + Polynomial.CreateConstant(2);
            AbstractExpr input3 = Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(2).E + Polynomial.CreateNamedVariable("a");
            AbstractExpr input4 = Polynomial.CreateNamedVariable("a").E + (Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(2));
            IAbstractExpr result1 = input1.Accept(visitor, Ig.nore);
            IAbstractExpr result2 = input2.Accept(visitor, Ig.nore);
            IAbstractExpr result3 = input3.Accept(visitor, Ig.nore);
            IAbstractExpr result4 = input4.Accept(visitor, Ig.nore);
            Assert.AreSame(input1, result1);
            Assert.AreSame(input2, result2);
            Assert.AreEqual(Polynomial.CreateConstant(3).E + Polynomial.CreateNamedVariable("a"), result3);
            Assert.AreEqual(Polynomial.CreateNamedVariable("a").E + Polynomial.CreateConstant(3), result4);
        }

    }
}
