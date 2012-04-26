using System.Collections.Generic;
using Movimentum.Model;

namespace Movimentum {
    public class Frame {
        private readonly double _absoluteTime;
        private readonly double _relativeTime;
        private readonly double _iv;
        private readonly IEnumerable<Constraint> _constraints;
        private readonly int _frameNo;

        public Frame(double absoluteTime, double relativeTime, double iv, IEnumerable<Constraint> constraints, int frameNo) {
            _absoluteTime = absoluteTime;
            _relativeTime = relativeTime;
            _iv = iv;
            _constraints = constraints;
            _frameNo = frameNo;
        }

        public double AbsoluteTime {
            get { return _absoluteTime; }
        }

        public double T {
            get { return _relativeTime; }
        }

        public double IV {
            get { return _iv; }
        }

        public IEnumerable<Constraint> Constraints {
            get { return _constraints; }
        }

        public int FrameNo {
            get { return _frameNo; }
        }

        public override string ToString() {
            return base.ToString() + "{T=" + _absoluteTime + " t=" + _relativeTime + " iv=" + _iv + "}: " + string.Join("; ", _constraints);
        }

        public IDictionary<string, IDictionary<string, ConstVector>> SolveConstraints() {
            var anchorLocations = new Dictionary<string, IDictionary<string, ConstVector>>();
            #region ----------- TEMPORARY CODE FOR TRYING OUT THE DRAWING MACHINE!! -----------
            foreach (var c in _constraints) {
                var constraint = c as VectorEqualityConstraint;
                if (constraint != null) {
                    Anchor anchor = constraint.Anchor;
                    Vector vector = (Vector)constraint.Rhs;
                    double x = Get(vector.X as BinaryScalarExpr);
                    double y = Get(vector.Y as BinaryScalarExpr);
                    var resultVector = new ConstVector(x, y);
                    IDictionary<string, ConstVector> anchorLocationsForThing;
                    if (!anchorLocations.TryGetValue(anchor.Thing, out anchorLocationsForThing)) {
                        anchorLocationsForThing = new Dictionary<string, ConstVector>();
                        anchorLocations.Add(anchor.Thing, anchorLocationsForThing);
                    }
                    anchorLocationsForThing.Add(anchor.Name, resultVector);
                } else {
                    // We ignore the rigid body constraints and the 2d constraints for our testing.
                }
            }
            #endregion -------- TEMPORARY CODE FOR TRYING OUT THE DRAWING MACHINE!! --------------
            return anchorLocations;
        }

        #region --------------- TEMPORARY CODE FOR TRYING OUT THE DRAWING MACHINE!! --------------
        private double Get(BinaryScalarExpr expr) {
                var lhs = (Constant)expr.Lhs;
                return lhs.Value + _relativeTime; // Expression MUST be (c + .t), with constant c.
        }
        #endregion ------------ TEMPORARY CODE FOR TRYING OUT THE DRAWING MACHINE!! --------------
    }
}