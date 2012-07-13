using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestSimpleThings {
        [Test]
        public void TestEqualityOfAtLeastZeroConstraint() {
            var z1 = new AtLeastZeroConstraint(new Constant(0));
            var z2 = new AtLeastZeroConstraint(new Constant(0));
            Assert.AreEqual(z1, z2);
        }

    }
}
