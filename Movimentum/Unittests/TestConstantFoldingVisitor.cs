using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestConstantFoldingVisitor {
        private ConstantFoldingVisitor visitor = new ConstantFoldingVisitor();

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
            Assert.AreEqual(new Constant((1.0+2.0)/(3.0+4.0)), result);
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
    }
}
