using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movimentum.Model {
    public partial class Script {
        public void AddRigidBodyConstraints() {
            if (Steps.Any()) {
                foreach (var th in _things) {
                    string[] anchorNames = th.Anchors.Keys.ToArray();
                    for (int i = 0; i < anchorNames.Length; i++) {
                        for (int j = i + 1; j < anchorNames.Length; j++) {
                            string anchorName1 = anchorNames[i];
                            string anchorName2 = anchorNames[j];
                            AddRigidBodyConstraint(th.Name, anchorName1, anchorName2, th.Anchors[anchorName1], th.Anchors[anchorName2]);
                        }
                    }
                }
            }
        }

        private void AddRigidBodyConstraint(string thing, string anchorName1, string anchorName2, ConstVector cv1, ConstVector cv2) {
            string auxVar = thing + "_" + anchorName1 + "_" + anchorName2;
            ScalarEqualityConstraint constraint1, constraint2;
            {
                var anchor1 = new Anchor(thing, anchorName1);
                var anchor2 = new Anchor(thing, anchorName2);
                UnaryScalarExpr xSquared = SquareCoord(UnaryScalarVectorOperator.X, anchor1, anchor2);
                UnaryScalarExpr ySquared = SquareCoord(UnaryScalarVectorOperator.Y, anchor1, anchor2);
                UnaryScalarExpr zSquared = SquareCoord(UnaryScalarVectorOperator.Z, anchor1, anchor2);
                BinaryScalarExpr squaredSum = new BinaryScalarExpr(xSquared, BinaryScalarOperator.PLUS, new BinaryScalarExpr(ySquared, BinaryScalarOperator.PLUS, zSquared));
                constraint1 = new ScalarEqualityConstraint(auxVar, squaredSum);
            }
            {
                double squaredDistance = Square(cv1.X - cv2.X) + Square(cv1.Y - cv2.Y) + Square(cv1.Z - cv2.Z);
                constraint2 = new ScalarEqualityConstraint(auxVar, new Constant(squaredDistance));
            }
            // AddRigidBodyConstraint is nly called when Steps.Any() is true - therefore Steps.First() is safe.
            Steps.First().AddConstraint(constraint1);
            Steps.First().AddConstraint(constraint2);
        }

        private double Square(double v) {
            return v * v;
        }

        private static UnaryScalarExpr SquareCoord(UnaryScalarVectorOperator coordOp, Anchor anchor1, Anchor anchor2) {
            return new UnaryScalarExpr(UnaryScalarOperator.SQUARED,
                new BinaryScalarExpr(
                    new UnaryScalarVectorExpr(anchor1, coordOp),
                    BinaryScalarOperator.MINUS,
                    new UnaryScalarVectorExpr(anchor2, coordOp))
                );
        }
    }
}
