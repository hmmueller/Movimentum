using System.Collections.Generic;
using System.Linq;
using Movimentum.Model;
using Movimentum.SubstitutionSolver3;

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
            return base.ToString() + "{#=" + _frameNo + " T=" + _absoluteTime + " t=" + _relativeTime + " iv=" + _iv + "}: " + string.Join("; ", _constraints);
        }

        public IDictionary<string, IDictionary<string, ConstVector>> SolveConstraints(double range, ref IDictionary<Variable, VariableValueRestriction> result) {
            IEnumerable<Constraint> modelConstraints = Constraints;

            var solverConstraints = modelConstraints.SelectMany(c => c.CreateSolverConstraints(_absoluteTime, _iv));
            result = SolverNode.Solve(solverConstraints, 200 * Constraints.Count(), result, FrameNo);

            return ConvertResultToAnchorLocations(result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.GetSomeValue()));
        }

        private static Dictionary<string, IDictionary<string, ConstVector>> ConvertResultToAnchorLocations(IDictionary<Variable, double> result) {
            var anchorLocations = new Dictionary<string, IDictionary<string, ConstVector>>();

            foreach (var anchorXVariable in result.Keys.OfType<AnchorVariable>().Where(v => v.Coordinate == Anchor.Coordinate.X)) {
                Anchor anchor = anchorXVariable.Anchor;
                ConstVector resultVector = new ConstVector(
                    result[anchorXVariable],
                    result[new AnchorVariable(anchor, Anchor.Coordinate.Y)],
                    result[new AnchorVariable(anchor, Anchor.Coordinate.Z)]
                    );
                IDictionary<string, ConstVector> anchorLocationsForThing;
                if (!anchorLocations.TryGetValue(anchor.Thing, out anchorLocationsForThing)) {
                    anchorLocationsForThing = new Dictionary<string, ConstVector>();
                    anchorLocations.Add(anchor.Thing, anchorLocationsForThing);
                }
                anchorLocationsForThing.Add(anchor.Name, resultVector);
            }
            return anchorLocations;
        }

    }
}