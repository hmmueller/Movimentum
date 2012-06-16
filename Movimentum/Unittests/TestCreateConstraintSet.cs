using Movimentum.Model;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestCreateConstraintSet {
        [Test]
        public void Test2SimpleConstraints() {
            ScalarEqualityConstraint[] constraints = new[] {
                               new ScalarEqualityConstraint("a", new ConstScalar(1)),
                               new ScalarEqualityConstraint("b", new ConstScalar(2)),
                           };
            ConstraintSet.Create(constraints, 0, 0);
        }
    }
}
