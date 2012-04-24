using System.Collections.Generic;
using System.Linq;

namespace Movimentum.Model {
    public partial class Script {

        #region Rigid body and 2D constraints

        private const string ZERO_VARIABLE = "ZERO";

        /// <summary>
        /// Add rigid body constraints and 2-d constraints to the first step where a thing appears.
        /// </summary>
        public void AddRigidBodyAnd2DConstraints() {
            // We look for the first step that mentions a thing. This step gets the rigid body constraints.
            var rigidThings = new HashSet<string>();
            bool zeroConstraintAdded = false;

            foreach (var st in _steps) {
                // We will add possibly constraints to this step's constraint list - so the list changes.
                // Therefore, we must create a copy of the list before we iterate over it.
                VectorEqualityConstraint[] vectorConstraints = st.Constraints.OfType<VectorEqualityConstraint>().ToArray();
                foreach (var c in vectorConstraints) {
                    string thingName = c.Variable.Thing;
                    if (!rigidThings.Contains(thingName)) {
                        // first time that this thing appears -> we create rigid body constraints
                        // and maybe 2d constraints.

                        if (!zeroConstraintAdded) {
                            // At the first step where any 2D constraint might be necessary, we add
                            // the constraint ZERO = 0.
                            st.AddConstraint(new ScalarEqualityConstraint(ZERO_VARIABLE, new Constant(0)));
                            zeroConstraintAdded = true;
                        }

                        AddRigidBodyAnd2DConstraintsFor(st, _things.First(th => th.Name == thingName));
                        rigidThings.Add(thingName);
                    }
                }
            }
        }

        private static void AddRigidBodyAnd2DConstraintsFor(Step st, Thing th) {
            string[] anchorNames = th.Anchors.Keys.ToArray();
            // For each pair of different Anchors, we create a rigid body
            // constraint. For 5 or more anchors, this will create
            // redundant constraints - I don't care, as most things will
            // have only two anchors.
            for (int i = 0; i < anchorNames.Length; i++) {
                string anchorName1 = anchorNames[i];
                ConstVector anchor1 = th.Anchors[anchorName1];

                for (int j = i + 1; j < anchorNames.Length; j++) {
                    string anchorName2 = anchorNames[j];
                    AddRigidBodyConstraint(st, th.Name,
                        anchorName1, anchorName2,
                        anchor1, th.Anchors[anchorName2]);
                }

                // For 2D anchors, we create an additional "2d constraint", which
                // fixes the z coordinate at 0. A constraint that equals
                // ZERO_VARIABLE to 0 has been created in the outermost loop once.
                if (anchor1.Is2D()) {
                    st.AddConstraint(new ScalarEqualityConstraint(
                        ZERO_VARIABLE,
                        new UnaryScalarVectorExpr(new Anchor(th.Name, anchorName1), UnaryScalarVectorOperator.Z)));
                }
            }
        }

        /// <summary>
        /// Add auxVar = (P.x - Q.x)² + (P.y - Q.y)² + (P.z - Q.z)² and auxVar = constant (of value |P - Q|²) to step <c>st</c>.
        /// </summary>
        private static void AddRigidBodyConstraint(Step st, string thing, string anchorName1, string anchorName2, ConstVector cv1, ConstVector cv2) {
            string auxVar = thing + "_" + anchorName1 + "_" + anchorName2;
            ScalarEqualityConstraint constraint1, constraint2;
            {
                // Create constraint auxVar = (P.x - Q.x)² + (P.y - Q.y)² + (P.z - Q.z)²
                var anchor1 = new Anchor(thing, anchorName1);
                var anchor2 = new Anchor(thing, anchorName2);
                UnaryScalarExpr xSquared = SquareCoord(UnaryScalarVectorOperator.X, anchor1, anchor2);
                UnaryScalarExpr ySquared = SquareCoord(UnaryScalarVectorOperator.Y, anchor1, anchor2);
                BinaryScalarExpr squaredSum = new BinaryScalarExpr(xSquared, BinaryScalarOperator.PLUS, ySquared);

                // If both vectors are 2d, we drop the square of the z distance. This makes life a little easier
                // for the constraint solver.
                if (!cv1.Is2D() || !cv2.Is2D()) {
                    UnaryScalarExpr zSquared = SquareCoord(UnaryScalarVectorOperator.Z, anchor1, anchor2);
                    squaredSum = new BinaryScalarExpr(squaredSum, BinaryScalarOperator.PLUS, zSquared);
                }

                constraint1 = new ScalarEqualityConstraint(auxVar, squaredSum);
            }
            {
                // Create constraint auxVar = |P - Q|²
                double squaredDistance = Square(cv1.X - cv2.X)
                                       + Square(cv1.Y - cv2.Y)
                                       + Square(cv1.Z - cv2.Z);
                constraint2 = new ScalarEqualityConstraint(auxVar, new Constant(squaredDistance));
            }
            st.AddConstraint(constraint1);
            st.AddConstraint(constraint2);
        }

        private static double Square(double v) {
            return v * v;
        }

        /// <summary>
        /// Return (anchor1.c - anchor2.c)², where .c is .x or .y or .z
        /// </summary>
        private static UnaryScalarExpr SquareCoord(UnaryScalarVectorOperator coordOp,
                                                   Anchor anchor1, Anchor anchor2) {
            return new UnaryScalarExpr(UnaryScalarOperator.SQUARED,
                new BinaryScalarExpr(
                    new UnaryScalarVectorExpr(anchor1, coordOp),
                    BinaryScalarOperator.MINUS,
                    new UnaryScalarVectorExpr(anchor2, coordOp))
                );
        }

        #endregion Rigid body and 2D constraints

        #region Create frames

        public IEnumerable<Frame> CreateFrames() {
            var result = new List<Frame>();
            IEnumerator<Step> stepEnumerator = _steps.GetEnumerator();
            if (stepEnumerator.MoveNext()) {
                Step nextStep = stepEnumerator.Current;
                var activeConstraints = new Dictionary<string, List<Constraint>>();
                Step currentStep = nextStep;
                nextStep = UpdateActiveConstraintsAndGetNextStep(currentStep, activeConstraints, stepEnumerator);
                for (double t = _steps.First().Time; nextStep != null; t += 1.0 / _config.FramesPerTimeunit) {
                    result.Add(new Frame(t, t - currentStep.Time, nextStep.Time - currentStep.Time, activeConstraints.Values.SelectMany(c => c)));
                    if (t >= nextStep.Time) {
                        currentStep = nextStep;
                        nextStep = UpdateActiveConstraintsAndGetNextStep(currentStep, activeConstraints, stepEnumerator);
                    }
                }
            }
            return result;
        }

        private Step UpdateActiveConstraintsAndGetNextStep(Step step,
                Dictionary<string, List<Constraint>> currentConstraints,
                IEnumerator<Step> stepEnumerator) {
            foreach (var c in step.Constraints) {
                currentConstraints.Remove(c.Key);
                currentConstraints.Add(c.Key, new List<Constraint>());
            }
            foreach (var c in step.Constraints) {
                currentConstraints[c.Key].Add(c);
            }
            return stepEnumerator.MoveNext() ? stepEnumerator.Current : null;
        }

        #endregion Create frames
    }
}
