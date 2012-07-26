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

        public IDictionary<string, IDictionary<string, ConstVector>> SolveConstraints(double range, 
                    ref IDictionary<IVariable, VariableWithValue> result, 
                    IDictionary<string, double> debugExpectedResults = null) {
            IEnumerable<Constraint> modelConstraints = Constraints;

            var solverConstraints = modelConstraints.SelectMany(c => c.CreateSolverConstraints(_absoluteTime, _iv));
            
            EvaluationVisitor evaluationVisitor = debugExpectedResults == null 
                ? null 
                : new EvaluationVisitor(debugExpectedResults.ToDictionary(kvp => CreateVariable(kvp.Key), kvp => kvp.Value));

            result = SolverNode.Solve(solverConstraints, 1000 * Constraints.Count(), result, FrameNo, evaluationVisitor);

            return ConvertResultToAnchorLocations(result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value));
        }

        private IVariable CreateVariable(string variableName) {
            return Polynomial.CreateNamedVariable(variableName);
        }

        private static Dictionary<string, IDictionary<string, ConstVector>> ConvertResultToAnchorLocations(IDictionary<IVariable, double> result) {
            var anchorLocations = new Dictionary<string, IDictionary<string, ConstVector>>();

            foreach (var anchorXVariable in result.Keys.OfType<IAnchorVariable>().Where(v => v.Coordinate == Anchor.Coordinate.X)) {
                Anchor anchor = anchorXVariable.Anchor;
                ConstVector resultVector = new ConstVector(
                    result[anchorXVariable],
                    result[Polynomial.CreateAnchorVariable(anchor, Anchor.Coordinate.Y)],
                    result[Polynomial.CreateAnchorVariable(anchor, Anchor.Coordinate.Z)]
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