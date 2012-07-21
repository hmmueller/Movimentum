using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestRewritingVisitorSTEPC {
        [Test]
        public void TestVariable() {
            INamedVariable input = Polynomial.CreateNamedVariable("a");
            var visitor = new RewritingVisitor(Polynomial.CreateNamedVariable("a"), Polynomial.CreateConstant(1));
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1), result);
        }

        [Test]
        public void TestVariableInExpr() {
            AbstractExpr input = Polynomial.CreateNamedVariable("a").C + Polynomial.CreateNamedVariable("c");
            var visitor = new RewritingVisitor(Polynomial.CreateNamedVariable("a"), Polynomial.CreateConstant(1));
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            Assert.AreEqual(Polynomial.CreateConstant(1).C + Polynomial.CreateNamedVariable("c"), result);
        }
    }
}
