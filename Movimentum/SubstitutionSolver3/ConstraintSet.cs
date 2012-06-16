using System;
using System.Collections.Generic;
using System.Linq;
using Movimentum.Model;

namespace Movimentum.SubstitutionSolver3 {
    public class ConstraintSet {
        private readonly List<AbstractConstraint> _constraints;
        private readonly ConstraintSet _origin;
        private readonly Dictionary<Variable, VariableRangeRestriction> _variableInRangeKnowledges;
        private bool _isDeadEnd;
        private double? _rank;

        private static int _sortId = 1;
        private static readonly ConstantFoldingVisitor CONSTANT_FOLDER = new ConstantFoldingVisitor();

        public IEnumerable<AbstractConstraint> Constraints {
            get { return _constraints; }
        }

        public IDictionary<Variable, VariableRangeRestriction> VariableInRangeKnowledges {
            get {
                return _variableInRangeKnowledges;
            }
        }

        public bool IsDeadEnd {
            get { return _isDeadEnd; }
        }

        public static ConstraintSet Create(IEnumerable<Constraint> modelConstraints, double t, double iv) {
            return new ConstraintSet(modelConstraints.SelectMany(c => c.CreateSolverConstraints(t, iv)), null);
        }

        public ConstraintSet(IEnumerable<AbstractConstraint> constraints, ConstraintSet origin) {
            _constraints = constraints.Select(c => c.Accept(CONSTANT_FOLDER, Ig.nore)).ToList();
            _origin = origin;
            _variableInRangeKnowledges = origin == null 
                ? new Dictionary<Variable, VariableRangeRestriction>()
                : new Dictionary<Variable, VariableRangeRestriction>(_origin._variableInRangeKnowledges);
        }

        public IEnumerable<ConstraintSet> Expand(IDictionary<Variable, VariableRangeRestriction> previousValues) {
            var result = new List<ConstraintSet>();
            RewriteSet rewrites = FindBestRewrite(previousValues);
            foreach (var r in rewrites.Rewrites) {
                var rewrittenConstraints = new List<AbstractConstraint>();
                foreach (var c in Constraints) {
                    AbstractConstraint newC = r.Rewrite(c);
                    if (newC != null) {
                        rewrittenConstraints.Add(newC);
                    }
                }
                ConstraintSet constraintSet = new ConstraintSet(rewrittenConstraints, this);
                r.Action(constraintSet);
                result.Add(constraintSet);
            }
            return result;
        }

        //private IEnumerable<AbstractRewrite> FindBestRewrite() {
        //    Dictionary<AbstractConstraint, Rank> ranks = Constraints.Distinct().ToDictionary(c => c, c => c.CreateRank(Constraints));
        //    Rank minRank = ranks.Values.Min();
        //    AbstractConstraint minConstraint = Constraints.First(c => ranks[c] == minRank);
        //    AbstractRewrite[] result = minConstraint.CreateRewritesFromSingleConstraint().ToArray();
        //    return result;
        //}

        private RewriteSet FindBestRewrite(IDictionary<Variable, VariableRangeRestriction> previousValues) {
            RewriteSet minRewriteSet = null;
            foreach (var c in Constraints) {
                int minRank = minRewriteSet == null ? int.MaxValue : minRewriteSet.Rank;
                RewriteSet rs = c.CreateRewritesFromSingleConstraint(Constraints.Except(c), previousValues, _variableInRangeKnowledges, minRank - 1);
                if (rs != null && rs.Rank < minRank) {
                    minRewriteSet = rs;
                }
            }
            return minRewriteSet;
        }

        public void AddVariableRestriction(VariableRangeRestriction restriction) {
            VariableRangeRestriction existing;
            if (!_variableInRangeKnowledges.TryGetValue(restriction.Variable, out existing)) {
                existing = new VariableRangeRestriction(restriction.Variable, double.NegativeInfinity, double.PositiveInfinity);
            }
            _variableInRangeKnowledges[restriction.Variable] = existing.Intersect(restriction.PossibleRanges);
        }

        public void MarkAsDeadEnd() {
            _isDeadEnd = true;
        }

        private double Rank {
            get {
                if (!_rank.HasValue) {
                    _rank = 1 + _sortId++ / 1000000.0;
                }
                return _rank.Value;
            }
        }

        public static IEnumerable<ConstraintSet> SolverStep(
                IEnumerable<ConstraintSet> open, 
                IDictionary<Variable, VariableRangeRestriction> previousValues, 
                out ConstraintSet solutionOrNull) {
            ////IEnumerable<ConstraintSet> open = new[] { ConstraintSet.Create(constraints, 0, 0) };
            double minRank = open.Min(cs => cs.Rank);
            ConstraintSet selected = open.First(cs => cs.Rank <= minRank);
            IEnumerable<ConstraintSet> expandedSets = selected.Expand(previousValues).Where(cs => !cs.IsDeadEnd).ToArray();

            solutionOrNull = expandedSets.FirstOrDefault(cs => !cs.Constraints.Any());
            IEnumerable<ConstraintSet> newOpen = open.Except(selected).Concat(expandedSets);
            return newOpen;
        }

        public static IDictionary<Variable, VariableRangeRestriction> Solve(IEnumerable<Constraint> constraints, double t, double iv, int loopLimit, IDictionary<Variable, VariableRangeRestriction> previousValues, int frameNo) {
            IEnumerable<ConstraintSet> open = new[] { Create(constraints, t, iv) };
            ConstraintSet solutionOrNull;
            do {
                if (!open.Any()) {
                    throw new Exception("No solution found for frame " + frameNo);
                }
                open = SolverStep(open, previousValues, out solutionOrNull).ToArray();
                if (loopLimit-- < 0) {
                    throw new Exception("Cannot find solution");
                }
            } while (solutionOrNull == null);
            return solutionOrNull.VariableInRangeKnowledges;
        }

        public void RemoveConstraint(AbstractConstraint constraint) {
            _constraints.Remove(constraint);
        }

        public void AddConstraint(AbstractConstraint constraint) {
            _constraints.Add(constraint);
        }
    }
}
