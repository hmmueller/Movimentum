using System.Collections.Generic;
using System.Linq;

namespace Movimentum.Model {
    public partial class Script {

        public IDictionary<int, Dictionary<string, double>> DebugExpectedResults = new Dictionary<int, Dictionary<string, double>>();

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
                // We will possibly add constraints to this step's constraint list - so the list changes.
                // Therefore, we must create a copy of the list before we iterate over it.
                VectorEqualityConstraint[] vectorConstraints = st.Constraints.OfType<VectorEqualityConstraint>().ToArray();
                foreach (var c in vectorConstraints) {
                    string thingName = c.Anchor.Thing;
                    if (!rigidThings.Contains(thingName)) {
                        // first time that this thing appears -> we create rigid body constraints
                        // and maybe 2d constraints.

                        if (!zeroConstraintAdded) {
                            // At the first step where any 2D constraint might be necessary, we add
                            // the constraint ZERO = 0.
                            st.AddConstraint(new ScalarEqualityConstraint(ZERO_VARIABLE, new ConstScalar(0)));
                            zeroConstraintAdded = true;
                        }

                        AddRigidBodyAnd2DConstraintsFor(st, _things.First(th => th.Name == thingName));
                        rigidThings.Add(thingName);
                    }
                }
            }
        }

        private static void AddRigidBodyAnd2DConstraintsFor(Step st, Thing th) {
            ConstAnchor[] anchors = th.Anchors.ToArray();
            // For each pair of different Anchors, we create a rigid body
            // constraint. For 5 or more anchors, this will create
            // redundant constraints - I don't care, as most things will
            // have only two anchors.
            for (int i = 0; i < anchors.Length; i++) {
                var anchor1 = anchors[i];
                for (int j = i + 1; j < anchors.Length; j++) {
                    AddAssignableRigidBodyConstraints(st, th.Name, anchor1, anchors[j]);
                }

                // For 2D anchors, we create an additional "2d constraint", which
                // fixes the z coordinate at 0. A constraint that equals
                // ZERO_VARIABLE to 0 has been created in the outermost loop once.
                if (anchor1.Location.Is2D()) {
                    st.AddConstraint(new ScalarEqualityConstraint(
                                         ZERO_VARIABLE,
                                         new UnaryVectorScalarExpr(new Anchor(th.Name, anchor1.Name), UnaryVectorScalarOperator.Z)));
                }
            }
        }

        /// <summary>
        /// Add 3 "solved" constraints |P - Q|² = (P.x - Q.x)² + (P.y - Q.y)² + (P.z - Q.z)² to step <c>st</c>.
        /// </summary>
        private static void AddAssignableRigidBodyConstraints(Step st, string thing, ConstAnchor constAnchor1, ConstAnchor constAnchor2) {
            double squaredDistance;
            {
                ConstVector cv1 = constAnchor1.Location;
                ConstVector cv2 = constAnchor2.Location;
                squaredDistance = Square(cv1.X - cv2.X) + Square(cv1.Y - cv2.Y) + Square(cv1.Z - cv2.Z);
            }

            var anchor1 = new Anchor(thing, constAnchor1.Name);
            var anchor2 = new Anchor(thing, constAnchor2.Name);
            UnaryScalarExpr dxSquared = SquareCoord(UnaryVectorScalarOperator.X, anchor1, anchor2);
            UnaryScalarExpr dySquared = SquareCoord(UnaryVectorScalarOperator.Y, anchor1, anchor2);
            UnaryScalarExpr dzSquared = SquareCoord(UnaryVectorScalarOperator.Z, anchor1, anchor2);

            st.AddConstraint(CreateRigidBodyConstraint(anchor1, anchor2, dxSquared, dySquared, dzSquared, new ConstScalar(squaredDistance)));
        }

        private static Constraint CreateRigidBodyConstraint(Anchor a1, Anchor a2, ScalarExpr dxSquared, ScalarExpr dySquared, ScalarExpr dzSquared, ConstScalar squaredDistance) {
            // a1.X = a2.X + root(squaredDistance - dySquared - dzSquared)
            //    a1.X - a2.X = root(squaredDistance - dySquared - dzSquared)
            //    (a1.X - a2.X)² = squaredDistance - dySquared - dzSquared
            // a1.Y = a2.Y + root(squaredDistance - dxSquared - dzSquared)
            // a1.Z = a2.Z + root(squaredDistance - dxSquared - dySquared)
            return new VectorEqualityConstraint(a1,
                                                new BinaryVectorExpr(a2, BinaryVectorOperator.PLUS, new Vector(
                                                                                                        SquareRootOfDMinusSquares(squaredDistance, dySquared, dzSquared),
                                                                                                        SquareRootOfDMinusSquares(squaredDistance, dxSquared, dzSquared),
                                                                                                        SquareRootOfDMinusSquares(squaredDistance, dxSquared, dySquared))));
        }

        private static ScalarExpr SquareRootOfDMinusSquares(ConstScalar squaredDistance, ScalarExpr square1, ScalarExpr square2) {
            // root(squaredDistance - square1 - square2)
            return new UnaryScalarExpr(UnaryScalarOperator.SQUAREROOT,
                                       new BinaryScalarExpr(squaredDistance, BinaryScalarOperator.MINUS,
                                                            new BinaryScalarExpr(square1, BinaryScalarOperator.PLUS, square2))
                );
        }

        private static double Square(double v) {
            return v * v;
        }

        /// <summary>
        /// Return (anchor1.c - anchor2.c)², where .c is .x or .y or .z
        /// </summary>
        private static UnaryScalarExpr SquareCoord(UnaryVectorScalarOperator coordOp,
                                                   Anchor anchor1, Anchor anchor2) {
            return new UnaryScalarExpr(UnaryScalarOperator.SQUARED,
                                       new BinaryScalarExpr(
                                           new UnaryVectorScalarExpr(anchor1, coordOp),
                                           BinaryScalarOperator.MINUS,
                                           new UnaryVectorScalarExpr(anchor2, coordOp))
                );
        }

        #endregion Rigid body and 2D constraints

        #region Create frames

        public IEnumerable<Frame> CreateFrames() {
            var result = new List<Frame>();
            IEnumerator<Step> stepEnumerator = _steps.GetEnumerator();

            if (stepEnumerator.MoveNext()) {
                var activeConstraints = new Dictionary<string, List<Constraint>>();

                Step currentStep = stepEnumerator.Current;
                Step nextStep = UpdateActiveConstraintsAndGetNextStep(currentStep, activeConstraints, stepEnumerator);

                // We do not want t to miss a step due to rounding, hence
                // we "push each frame a little bit too far" (< a nanosec ...).
                double deltaT = 1.0 / _config.FramesPerTimeunit + 1e-10;

                int seqNo = 1;
                for (var t = currentStep.Time; nextStep != null; t += deltaT) {

                    while (nextStep != null && t >= nextStep.Time) {
                        currentStep = nextStep;
                        nextStep = UpdateActiveConstraintsAndGetNextStep(currentStep, activeConstraints, stepEnumerator);
                    }

                    result.Add(new Frame(
                        absoluteTime: t,
                        relativeTime: t - currentStep.Time,

                        // In last step - when nextStep is null -, we use
                        // currentStep instead of nextStep -> iv is set to 0.
                        iv: (nextStep ?? currentStep).Time - currentStep.Time,

                        // ToArray() necessary to COPY result into Frame.
                        // Otherwise, the iterator will run on the constraints
                        // of the LAST frame when it is executed at some time.
                        constraints: activeConstraints.Values
                                        .SelectMany(c => c)
                                        .ToArray(),
                        frameNo: seqNo++)
                    );
                }
            }
            return result;
        }

        /// <summary>
        /// Concept: If there are constraints with key K in this step, we
        /// remove ALL earlier constraints with that key from the active
        /// constraints; and afterwards add all the constraints with this
        /// key from the step.
        /// </summary>
        /// <returns>next step; or null if there is none.</returns>
        private Step UpdateActiveConstraintsAndGetNextStep(Step step,
                            Dictionary<string, List<Constraint>> activeConstraints,
                            IEnumerator<Step> stepEnumerator) {

            foreach (var c in step.Constraints) {
                // For more than one constraint with same key, the following
                // will init c.Key to the empty list more than once. So what.
                activeConstraints[c.Key] = new List<Constraint>();
            }
            foreach (var c in step.Constraints) {
                activeConstraints[c.Key].Add(c);
            }
            return stepEnumerator.MoveNext() ? stepEnumerator.Current : null;
        }

        #endregion Create frames

        public void DebugAddExpectedResult(int frameNo, Dictionary<string, double> expected) {
            DebugExpectedResults.Add(frameNo, expected);
        }
    }
}
