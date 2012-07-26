using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestRewritingVisitor {
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

        [Test]
        public void TestPolynomial() {
            var input = Polynomial.CreatePolynomial(Polynomial.CreateNamedVariable("a"), 1, 2, 3); // x²+2x+3
            var visitor = new RewritingVisitor(Polynomial.CreateNamedVariable("a"), Polynomial.CreateConstant(2));
            IAbstractExpr result = input.Accept(visitor, Ig.nore);
            // lambda x.x²+2x+3 (2) = 4+4+3 = 11
            Assert.AreEqual(Polynomial.CreateConstant(11), result);
        }

    }
}
