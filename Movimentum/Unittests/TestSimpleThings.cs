using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestSimpleThings {
        [Test]
        public void TestEqualityOfAtLeastZeroConstraint() {
            var z1 = new AtLeastZeroConstraint(Polynomial.CreateConstant(0));
            var z2 = new AtLeastZeroConstraint(Polynomial.CreateConstant(0));
            Assert.AreEqual(z1, z2);
        }

        [Test]
        public void TestConstantExpressions() {
            //var c1 = new Constant(1) + new Constant(2);
            var c2 = Polynomial.CreateConstant(1).C + Polynomial.CreateConstant(2).C;
            Assert.IsNotNull(c2);
        }
    }
}
