using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        private readonly IEnumerable<AbstractConstraint> _constraints;
        private readonly Dictionary<Variable, VariableRangeRestriction> _variableInRangeKnowledges;

        public IEnumerable<AbstractConstraint> Constraints {
            get { return _constraints; }
        }

        public IDictionary<Variable, VariableRangeRestriction> VariableInRangeKnowledges {
            get { return _variableInRangeKnowledges; }
        }

        public SolverNode(IEnumerable<AbstractConstraint> constraints, SolverNode origin) {
            _constraints = constraints.Select(c => c.Accept(new ConstantFoldingVisitor(), Ig.nore));
            _variableInRangeKnowledges = origin == null
                ? new Dictionary<Variable, VariableRangeRestriction>()
                : new Dictionary<Variable, VariableRangeRestriction>(origin._variableInRangeKnowledges);
        }

        public static IDictionary<Variable, VariableRangeRestriction>
            Solve(IEnumerable<AbstractConstraint> solverConstraints,
                    int loopLimit,
                    IDictionary<Variable, VariableRangeRestriction> previousValues,
                    int frameNo) {
            // Create initial open set
            IEnumerable<SolverNode> open = new[] { new SolverNode(solverConstraints, null) };

            // Solver loop
            SolverNode solutionOrNull;
            do {
                if (!open.Any()) {
                    throw new Exception("No solution found for frame " + frameNo);
                }
                open = SolverStep(open, previousValues, out solutionOrNull);
                if (--loopLimit < 0) {
                    throw new Exception("Cannot find solution");
                }
            } while (solutionOrNull == null);
            return solutionOrNull.VariableInRangeKnowledges;
        }

        public static IEnumerable<SolverNode> SolverStep(
                IEnumerable<SolverNode> open,
                IDictionary<Variable, VariableRangeRestriction> previousValues,
                out SolverNode solutionOrNull) {
            double minRank = open.Min(cs => cs.Rank);
            SolverNode selected = open.First(cs => cs.Rank <= minRank);
            IEnumerable<SolverNode> expandedSets = selected.Expand(previousValues).ToArray();

            //IEnumerable<SolverNode> newOpen = open.Except(selected).Concat(expandedSets);
            IEnumerable<SolverNode> newOpen = expandedSets.Concat(open.Except(selected));

            // Not really correct: We should also check whether all anchor variables have
            // a single value. For the moment, in our tests, we live with this rough check.
            solutionOrNull = expandedSets.FirstOrDefault(cs => cs.IsSolved());

            return newOpen;
        }

        public IEnumerable<SolverNode> Expand(IDictionary<Variable, VariableRangeRestriction> previousValues) {
            foreach (var ra in _ruleActions) {
                ScalarConstraintMatcher matcher = new ScalarConstraintMatcher(ra.Template);
                foreach (var c in Constraints.OfType<ScalarConstraint>()) {
                    if (matcher.TryMatch(c)) {
                        return ra.OnMatch(this, matcher, c);
                    }
                }
            }
            throw new NotSupportedException("I found no matching constraint");
        }

        protected int Rank {
            get { return 1; }
        }

        private bool IsSolved() {
            return !_constraints.Any();
        }

        private static readonly List<MatcherRuleAction> _ruleActions = new List<MatcherRuleAction>();

        //private abstract class RuleAction {}

        //private class SimpleRuleAction : RuleAction {

        //    public readonly Func<SolverNode, ScalarConstraintMatcher, AbstractConstraint, IEnumerable<SolverNode>> OnMatch;
        //    public SimpleRuleAction(ScalarConstraintTemplate template, Func<SolverNode, ScalarConstraintMatcher, AbstractConstraint, IEnumerable<SolverNode>> onMatch) {
        //        OnMatch = onMatch;
        //        _ruleActions.Add(this);
        //    }
        //}

        //private class MatcherRuleAction : RuleAction {
        private class MatcherRuleAction {
            public readonly ScalarConstraintTemplate Template;
            public readonly Func<SolverNode, ScalarConstraintMatcher, AbstractConstraint, IEnumerable<SolverNode>> OnMatch;
            public MatcherRuleAction(ScalarConstraintTemplate template, Func<SolverNode, ScalarConstraintMatcher, AbstractConstraint, IEnumerable<SolverNode>> onMatch) {
                Template = template;
                OnMatch = onMatch;
                _ruleActions.Add(this);
            }
            public MatcherRuleAction(ScalarConstraintTemplate template, Func<SolverNode, ScalarConstraintMatcher, AbstractConstraint, SolverNode> match)
                : this(template, (n, m, c) => {
                    SolverNode v = match(n, m, c);
                    return v == null ? new SolverNode[0] : new[] { v };
                }) { }
        }

        private static IEnumerable<SolverNode> RememberAndSubstituteVariable(SolverNode currNode, Variable variable, double value) {
            currNode._variableInRangeKnowledges.Add(variable, new VariableValueRestriction(variable, value));
            return SubstituteVariable(currNode, variable, new Constant(value));
        }

        private static IEnumerable<SolverNode> SubstituteVariable(SolverNode currNode, Variable variable, AbstractExpr expression) {
            var rewriter = new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> { { variable, expression } });
            IEnumerable<AbstractConstraint> rewrittenConstraints = currNode.Constraints.Select(c2 => c2.Accept(rewriter, Ig.nore));
            return new[] { new SolverNode(rewrittenConstraints, currNode) };
        }
    }
}