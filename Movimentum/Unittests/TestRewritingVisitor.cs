using System.Collections.Generic;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestRewritingVisitor {
        [Test]
        public void TestDontReplaceConstant() {
            Constant input = new Constant(1.5);
            var visitor = new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> { { new Constant(1), new Constant(2) } });
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(1.5), result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoReplaceConstant() {
            Constant input = new Constant(1.5);
            var visitor = new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> { { new Constant(1.5), new Constant(1) + new Constant(0.5) } });
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(1) + new Constant(0.5), result);
        }

        [Test]
        public void TestVariable() {
            NamedVariable input = new NamedVariable("a");
            var visitor = new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> { { new NamedVariable("b"), new Constant(2) }, { new NamedVariable("a"), new Constant(1) } });
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(1), result);
        }

        [Test]
        public void TestVariableInExpr() {
            AbstractExpr input = new NamedVariable("a") + new NamedVariable("c");
            var visitor = new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> { { new NamedVariable("b"), new Constant(2) }, { new NamedVariable("a"), new Constant(1) } });
            AbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(new Constant(1) + new NamedVariable("c"), result);
        }
    }
}
