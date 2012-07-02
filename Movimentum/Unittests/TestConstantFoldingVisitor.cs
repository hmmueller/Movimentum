using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestConstantFoldingVisitor {
        private readonly ConstantFoldingVisitor visitor = new ConstantFoldingVisitor();

        [Test]
        public void TestConstant() {
            Constant input = new Constant(1.5);
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(1.5), result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestVariable() {
            NamedVariable input = new NamedVariable("a");
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoFoldConstant() {
            AbstractExpr input = (new Constant(1) + new Constant(2)) * new Constant(4);
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(12), result);
        }

        [Test]
        public void TestDontFoldConstant() {
            AbstractExpr input = (new Constant(1) + new NamedVariable("b")) * new Constant(4);
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(input, result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoFoldConstantLeftAndRightOfBinaryOp() {
            AbstractExpr input = (new Constant(1) + new Constant(2)) / (new Constant(3) + new Constant(4));
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant((1.0 + 2.0) / (3.0 + 4.0)), result);
        }

        [Test]
        public void TestDoFoldConstantOnlyOnLeftSideOfBinaryOp() {
            AbstractExpr input = (new Constant(1) + new Constant(2)) * new NamedVariable("c");
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(3) * new NamedVariable("c"), result);
        }

        [Test]
        public void TestDoFoldConstantOnlyOnRightSideOfBinaryOp() {
            AbstractExpr input = new NamedVariable("a") * (new Constant(2) + new Constant(3));
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new NamedVariable("a") * new Constant(5), result);
        }

        [Test]
        public void TestDontFoldConstantInBinaryOp() {
            AbstractExpr input = new NamedVariable("a") * (new Constant(2) + new NamedVariable("c"));
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(input, result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDontFoldConstantInFormalSquareRoot() {
            AbstractExpr input = new UnaryExpression(new Constant(4), new FormalSquareroot());
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(input, result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoFoldConstantInPositiveSquareRoot() {
            AbstractExpr input = new UnaryExpression(new Constant(4), new PositiveSquareroot());
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreNotEqual(input, result);
            Assert.AreEqual(new Constant(2), result);
        }

        [Test]
        public void TestDoFoldConstantInUnaryMinus() {
            AbstractExpr input = -new Constant(4);
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreNotEqual(input, result);
            Assert.AreEqual(new Constant(-4), result);
        }

        [Test]
        public void TestFoldingsInSimpleSums() {
            AbstractExpr input1 = new NamedVariable("a") + new Constant(1) + new Constant(2);
            AbstractExpr input2 = new Constant(1) + new NamedVariable("a") + new Constant(2);
            AbstractExpr input3 = new Constant(1) + new Constant(2) + new NamedVariable("a");
            AbstractExpr input4 = new NamedVariable("a") + (new Constant(1) + new Constant(2));
            AbstractExpr result1 = input1.Accept(visitor, Ig.nore);
            AbstractExpr result2 = input2.Accept(visitor, Ig.nore);
            AbstractExpr result3 = input3.Accept(visitor, Ig.nore);
            AbstractExpr result4 = input4.Accept(visitor, Ig.nore);
            Assert.AreSame(input1, result1);
            Assert.AreSame(input2, result2);
            Assert.AreEqual(new Constant(3) + new NamedVariable("a"), result3);
            Assert.AreEqual(new NamedVariable("a") + new Constant(3), result4);
        }

    }
}
