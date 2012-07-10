using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        private readonly IEnumerable<AbstractConstraint> _constraints;
        //private readonly Dictionary<Variable, VariableRangeRestriction> _variableInRangeKnowledges;
        private readonly Dictionary<Variable, VariableValueRestriction> _variableValues;
        private readonly IDictionary<Variable, AbstractExpr> _backsubstitutions;

        private static int _debugIdCount = 1000;
        public readonly int DebugId = _debugIdCount++;

        public static readonly List<SolverNode> DebugDeadNodes = new List<SolverNode>();

        public IEnumerable<AbstractConstraint> Constraints {
            get { return _constraints; }
        }
        // TODO: Auch Rule des Übergangs reincodieren; und gleiche Rules zusammenfassen und mit Count versehen!

        private readonly SolverNode _debugOrigin;
        private int _debugSuccessors;
        private string DebugCreationRule;

        private string DebugOriginChain(int depth) {
            return
                (_debugSuccessors == 0 ? "" : _debugSuccessors == 1 ? "-" : "*") +
                DebugId + " < " + DebugCreationRule +
                (depth > 3 ? "..."
                 : _debugOrigin == null ? ""
                 : _debugOrigin.DebugOriginChain(depth + 1));
        }

        public override string ToString() {
            return "SolverNode " + DebugOriginChain(0) +
                    _constraints.Aggregate("", (s, c) => s + "\r\n  ! " + c) +
                    _variableValues.Aggregate("", (s, k) => s + "\r\n  " + k.Key.Name + " = " + k.Value.Value);
        }

        public string GetDebugHistory(int count) {
            var sb = new StringBuilder();
            for (var node = this; node != null && count > 0; node = node._debugOrigin, count--) {
                sb.AppendLine(node.ToString());
            }
            return sb.ToString();
        }

        public static string GetDebugHistoryOfDeadNodes(int depth) {
            var sb = new StringBuilder();
            foreach (var n in DebugDeadNodes) {
                sb.AppendLine("-------------");
                sb.Append(n.GetDebugHistory(depth));
            }
            return sb.ToString();
        }

        //public IDictionary<Variable, VariableRangeRestriction> VariableInRangeKnowledges {
        //    get { return _variableInRangeKnowledges; }
        //}
        public IDictionary<Variable, VariableValueRestriction> VariableValues {
            get { return _variableValues; }
        }

        public SolverNode(IEnumerable<AbstractConstraint> constraints,
                            IDictionary<Variable, AbstractExpr> backsubstitutions,
                            SolverNode origin) {
            _debugOrigin = origin;
            if (origin != null) {
                origin._debugSuccessors++;
            }
            _constraints = constraints.Select(c => c.Accept(new ConstantFoldingVisitor(), Ig.nore));
            ////_constraints = constraints.Select(c => c.Accept(new PolynomialFoldingVisitor(), Ig.nore)).ToArray();
            _backsubstitutions = backsubstitutions;
            foreach (var c in _constraints) {
                c.SetDebugCreatingNodeIdIfMissing(DebugId);
            }
            _variableValues = origin == null
                ? new Dictionary<Variable, VariableValueRestriction>()
                : new Dictionary<Variable, VariableValueRestriction>(origin._variableValues);
        }

        public static IDictionary<Variable, VariableValueRestriction>
            Solve(IEnumerable<AbstractConstraint> solverConstraints,
                    int loopLimit,
                    IDictionary<Variable, VariableValueRestriction> previousValues,
                    int frameNo) {
            // Create initial open set
            IEnumerable<SolverNode> open = new[] { new SolverNode(solverConstraints, new Dictionary<Variable, AbstractExpr>(), null) };

            // Solver loop
            SolverNode solutionOrNull;
            do {
                if (!open.Any()) {
                    Debug.WriteLine("---- " + DebugDeadNodes.Count + " dead nodes ----");
                    foreach (var deadNode in DebugDeadNodes) {
                        Debug.WriteLine(deadNode);
                    }
                    throw new Exception("No solution found for frame " + frameNo + " - no more open nodes. Look into DebugDeadNodes to see nodes that did not mathc a rule.");
                }
                open = SolverStep(open, previousValues, out solutionOrNull);
                if (--loopLimit < 0) {
                    throw new Exception("Cannot find solution for frame " + frameNo + " - loop limit exhausted");
                }
            } while (solutionOrNull == null);
            Debug.WriteLine("++++ solution node ++++");
            Debug.WriteLine(solutionOrNull);
            return solutionOrNull.VariableValues;
        }

        public static IEnumerable<SolverNode> SolverStep(
            IEnumerable<SolverNode> open,
            out SolverNode solutionOrNull) {
            return SolverStep(open,
                new Dictionary<Variable, VariableValueRestriction>(),
                out solutionOrNull);
        }

        public static IEnumerable<SolverNode> SolverStep(
            IEnumerable<SolverNode> open,
            IDictionary<Variable, VariableValueRestriction> previousValues,
            out SolverNode solutionOrNull) {
            double minRank = open.Min(cs => cs.Rank);
            SolverNode selected = open.First(cs => cs.Rank <= minRank);
            IEnumerable<SolverNode> expandedSets = selected.Expand(/*previousValues*/).ToArray();

            if (!expandedSets.Any()) {
                // Dead node - register in dead nodes.
                DebugDeadNodes.Add(selected);
            }

            //IEnumerable<SolverNode> newOpen = open.Except(selected).Concat(expandedSets);
            IEnumerable<SolverNode> newOpen = expandedSets.Concat(open.Except(selected));

            // Not really correct: We should also check whether all anchor variables have
            // a single value. For the moment, in our tests, we live with this rough check.
            solutionOrNull = expandedSets.FirstOrDefault(cs => cs.IsSolved());

            return newOpen;
        }

        public IEnumerable<SolverNode> Expand(/*IDictionary<Variable, VariableRangeRestriction> previousValues*/) {
            foreach (var ra in _ruleActions) {
                foreach (var c in Constraints.OfType<ScalarConstraint>()) {
                    IEnumerable<SolverNode> resultOrNull = ra.SuccessfulMatch(this, c);
                    if (resultOrNull != null) {
                        foreach (var node in resultOrNull) {
                            node.DebugSetCreationRule(ra.Name);
                        }
                        return resultOrNull;
                    }
                }
            }
            return Enumerable.Empty<SolverNode>();
        }

        private void DebugSetCreationRule(string ruleActionName) {
            DebugCreationRule = ruleActionName;
        }

        protected int Rank {
            get { return 1; }
        }

        public IDictionary<Variable, AbstractExpr> Backsubstitutions {
            get { return _backsubstitutions; }
        }

        private bool IsSolved() {
            return !_constraints.Any();
        }

        private static readonly List<RuleAction> _ruleActions =
            new List<RuleAction>();

        private abstract class RuleAction {
            public readonly string Name;

            protected RuleAction(string name) {
                Name = name;
            }

            public abstract IEnumerable<SolverNode> SuccessfulMatch(SolverNode current, ScalarConstraint constraint);
        }

        private class RuleAction<TMatchResult> : RuleAction where TMatchResult : class {
            private readonly Func<ScalarConstraint, TMatchResult> _tryMatch;
            private readonly Func<TMatchResult, bool> _isMatch;
            private readonly Func<SolverNode, TMatchResult, AbstractConstraint, IEnumerable<SolverNode>> _onMatch;

            public RuleAction(string name,
                Func<ScalarConstraint, TMatchResult> tryMatch,
                Func<TMatchResult, bool> isMatch,
                Func<SolverNode, TMatchResult, AbstractConstraint, IEnumerable<SolverNode>> onMatch)
                : base(name) {
                _tryMatch = tryMatch;
                _isMatch = isMatch;
                _onMatch = onMatch;
                _ruleActions.Add(this);
            }

            public RuleAction(string name,
                Func<ScalarConstraint, TMatchResult> tryMatch,
                Func<TMatchResult, bool> isMatch,
                Func<SolverNode, TMatchResult, AbstractConstraint, SolverNode> onMatch) :
                this(name, tryMatch, isMatch, (node, matchResult, constraint) => {
                    var result = onMatch(node, matchResult, constraint);
                    return result == null ? Enumerable.Empty<SolverNode>() : new[] { result };
                }) { }

            public override IEnumerable<SolverNode> SuccessfulMatch(SolverNode current, ScalarConstraint constraint) {
                TMatchResult matchResult = _tryMatch(constraint);
                if (matchResult == null) {
                    return null;
                } else if (_isMatch(matchResult)) {
                    return _onMatch(current, matchResult, constraint);
                } else {
                    return null;
                }
            }
        }

        //private class MatcherRuleAction : RuleAction<ScalarConstraintMatcher> {
        //    public MatcherRuleAction(string name,
        //        ScalarConstraintTemplate template,
        //            Func<SolverNode,
        //                 ScalarConstraintMatcher,
        //                 AbstractConstraint,
        //                 SolverNode> onMatch)
        //        : base(name,
        //        constraint => new ScalarConstraintMatcher(template).TryMatch(constraint),
        //        matcher => matcher.IsMatch,
        //        (node, matcher, constraint) => {
        //            SolverNode result = onMatch(node, matcher, constraint);
        //            return result == null ? new SolverNode[0] : new[] { result };
        //        }) {
        //    }
        //}

        // Factored out RememberAndSubstituteVariable
        public SolverNode RememberAndSubstituteVariable(Variable variable,
                double value,
                AbstractConstraint sourceConstraintToBeRemoved) {
            _variableValues.Add(variable, new VariableValueRestriction(variable, value));
            var constantFolder = new ConstantFoldingVisitor();
            IDictionary<Variable, double> workList = new Dictionary<Variable, double> { { variable, value } };
            IDictionary<Variable, AbstractExpr> backsubstitutions = new Dictionary<Variable, AbstractExpr>(_backsubstitutions);
            
            var rewriter = new RewritingVisitor(
                new Dictionary<AbstractExpr, AbstractExpr> { { variable, new Constant(value) } });
            IEnumerable<AbstractConstraint> rewrittenConstraints =
                Constraints.Except(sourceConstraintToBeRemoved)
                            .Select(c2 => c2.Accept(rewriter, Ig.nore));

            while (workList.Any()) {
                var current = workList.First();
                workList.Remove(current.Key);
                var rewriterForBacksubstitutions = new RewritingVisitor(
                    new Dictionary<AbstractExpr, AbstractExpr> { { current.Key, new Constant(current.Value) } });
                IDictionary<Variable, AbstractExpr> newBacksubstitutions = new Dictionary<Variable, AbstractExpr>();
                foreach (var b in backsubstitutions) {
                    var e = b.Value.Accept(rewriterForBacksubstitutions, Ig.nore).Accept(constantFolder, Ig.nore);
                    if (e is Constant) {
                        var d = (e as Constant).Value;
                        workList.Add(b.Key, d);
                        _variableValues.Add(b.Key, new VariableValueRestriction(b.Key, d));
                    } else {
                        newBacksubstitutions.Add(b.Key, e);
                    }
                }
                backsubstitutions = newBacksubstitutions;
            }

            return new SolverNode(rewrittenConstraints, backsubstitutions, this);
        }

        private SolverNode SubstituteVariable(Variable variable,
                AbstractExpr expression,
                AbstractConstraint sourceConstraintToBeRemoved) {
            var rewriter = new RewritingVisitor(
                new Dictionary<AbstractExpr, AbstractExpr> { { variable, expression } });
            IEnumerable<AbstractConstraint> rewrittenConstraints =
                Constraints.Except(sourceConstraintToBeRemoved)
                            .Select(c2 => c2.Accept(rewriter, Ig.nore));
            var newBacksubstitutions = Backsubstitutions;
            if (!(expression is Constant)) {
                newBacksubstitutions.Add(variable, expression);
            }
            return new SolverNode(rewrittenConstraints, newBacksubstitutions, this);
        }
    }
}