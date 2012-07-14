using System.Collections.Generic;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestRewritingVisitor {
        [Test]
        public void TestDontReplaceConstant() {
            IConstant input = Polynomial.CreateConstant(1.5);
            var visitor = new RewritingVisitor(new Dictionary<IAbstractExpr, IAbstractExpr> { { Polynomial.CreateConstant(1), Polynomial.CreateConstant(2) } });
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1.5), result);
            Assert.AreSame(input, result);
        }

        [Test]
        public void TestDoReplaceConstant() {
            IConstant input = Polynomial.CreateConstant(1.5);
            var visitor = new RewritingVisitor(new Dictionary<IAbstractExpr, IAbstractExpr> { { Polynomial.CreateConstant(1.5), Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(0.5) } });
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1).E + Polynomial.CreateConstant(0.5), result);
        }

        [Test]
        public void TestVariable() {
            INamedVariable input = Polynomial.CreateNamedVariable("a");
            var visitor = new RewritingVisitor(new Dictionary<IAbstractExpr, IAbstractExpr> { { Polynomial.CreateNamedVariable("b"), Polynomial.CreateConstant(2) }, { Polynomial.CreateNamedVariable("a"), Polynomial.CreateConstant(1) } });
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1), result);
        }

        [Test]
        public void TestVariableInExpr() {
            AbstractExpr input = Polynomial.CreateNamedVariable("a").E + Polynomial.CreateNamedVariable("c");
            var visitor = new RewritingVisitor(new Dictionary<IAbstractExpr, IAbstractExpr> { { Polynomial.CreateNamedVariable("b"), Polynomial.CreateConstant(2) }, { Polynomial.CreateNamedVariable("a"), Polynomial.CreateConstant(1) } });
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1).E + Polynomial.CreateNamedVariable("c"), result);
        }
    }
}
