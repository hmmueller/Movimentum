using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        private readonly IEnumerable<AbstractConstraint> _constraints;
        private readonly IDictionary<Variable, AbstractClosedVariable> _closedVariables;

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
                (depth > 3
                     ? "..."
                     : _debugOrigin == null
                           ? ""
                           : _debugOrigin.DebugOriginChain(depth + 1));
        }

        public override string ToString() {
            return "SolverNode " + DebugOriginChain(0) +
                   _constraints.Aggregate("", (s, c) => s + "\r\n  ! " + c) +
                   _closedVariables.Aggregate("", (s, k) => s + "\r\n  " + k.Value);
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

        public double GetVariableValue(Variable var) {
            AbstractClosedVariable closedVariable = GetClosedVariable(var);
            VariableWithValue result = closedVariable as VariableWithValue;
            if (result == null) {
                throw new InvalidOperationException("Variable " + var + " not closed as value in this node, but as " + closedVariable);
            }
            return result.Value;
        }

        public AbstractClosedVariable GetClosedVariable(Variable var) {
            AbstractClosedVariable closedVariable;
            if (!_closedVariables.TryGetValue(var, out closedVariable)) {
                throw new InvalidOperationException("Variable " + var + " not closed in this node");
            }
            return closedVariable;
        }

        private SolverNode(IEnumerable<AbstractConstraint> constraints,
                           SolverNode origin)
            : this(constraints, origin._closedVariables, origin) { }

        private SolverNode(IEnumerable<AbstractConstraint> constraints,
                           IDictionary<Variable, AbstractClosedVariable> closedVariables,
                           SolverNode origin) {
            _debugOrigin = origin;
            if (origin != null) {
                origin._debugSuccessors++;
            }

            var constantFoldingVisitor = new ConstantFoldingVisitor();
            _constraints = constraints.Select(c => c.Accept(constantFoldingVisitor, Ig.nore));
            foreach (var c in _constraints) {
                c.SetDebugCreatingNodeIdIfMissing(DebugId);
            }

            _closedVariables = closedVariables;
        }

        public static IDictionary<Variable, VariableWithValue>
            Solve(IEnumerable<AbstractConstraint> solverConstraints,
                  int loopLimit,
                  IDictionary<Variable, VariableWithValue> previousValues,
                  int frameNo) {
            // Create initial open set
            IEnumerable<SolverNode> open = new[] { new SolverNode(solverConstraints, new Dictionary<Variable, AbstractClosedVariable>(), null) };

            // Solver loop
            SolverNode solutionOrNull;
            do {
                if (!open.Any()) {
                    var promisingNodes = DebugDeadNodes.Where(n => !n.DefinitelyDead);
                    Debug.WriteLine("---- " + promisingNodes.Count() + " Promising dead nodes ----");
                    foreach (var deadNode in promisingNodes) {
                        Debug.WriteLine(deadNode);
                    }
                    var definitelyDeadNodes = DebugDeadNodes.Where(n => n.DefinitelyDead);
                    Debug.WriteLine("---- " + definitelyDeadNodes.Count() + " Definitely dead nodes ----");
                    foreach (var deadNode in definitelyDeadNodes) {
                        Debug.WriteLine(deadNode);
                    }
                    throw new Exception("No solution found for frame " + frameNo + " - no more open nodes. Look into DebugDeadNodes to see nodes that did not mathc a rule.");
                }
                open = SolverStep(open, previousValues, out solutionOrNull);
                if (--loopLimit < 0) {
                    throw new Exception("Cannot find solution for frame " + frameNo + " - loop limit exhausted");
                }
            } while (solutionOrNull == null);

            solutionOrNull.ResolveBacksubstitutions();

            Debug.WriteLine("++++ solution node ++++");
            Debug.WriteLine(solutionOrNull);

            return solutionOrNull
                ._closedVariables
                .Where(kvp => kvp.Value is VariableWithValue)
                .ToDictionary(kvp => kvp.Key, kvp => (VariableWithValue)kvp.Value);
        }

        private void ResolveBacksubstitutions() {
            var constantFolder = new ConstantFoldingVisitor();

            // The "ToArrays" are necessary because we modify _closedVariables below. Without them,
            // we get InvalidOperationExceptions telling us that the "Collection was modified."
            var varsWithValue = _closedVariables.Values.OfType<VariableWithValue>().ToArray();
            var varsWithBacksubstitutions = _closedVariables.Values.OfType<VariableWithBacksubstitution>().ToArray();

            while (varsWithBacksubstitutions.Any()) {
                if (!varsWithValue.Any()) {
                    Debug.WriteLine("---- No complete backsubstitution possible ----");
                    foreach (var vb in varsWithBacksubstitutions) {
                        Debug.WriteLine(vb);
                    }
                    throw new InvalidOperationException("No solution found - " +
                              "there are more variables to substitute, but still open substitutions");
                }

                var newVarsWithValue = new List<VariableWithValue>();
                var newVarsWithBacksubstitutions = new List<VariableWithBacksubstitution>();
                foreach (var varWithValue in varsWithValue) {
                    // We substitute each variable into all open backsubstitutions.
                    var rewriter = new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> 
                                                    { { varWithValue.Variable, 
                                                        new Constant(varWithValue.Value) 
                                                    } });
                    foreach (var varWithBacksub in varsWithBacksubstitutions) {
                        AbstractExpr rewritten = varWithBacksub.Expr
                                                               .Accept(rewriter, Ig.nore)
                                                               .Accept(constantFolder, Ig.nore);
                        // If the result, after constant folding, is a constant, we have found a new solution value.
                        // Otherwise, we still have a - maybe smaller - backsubstitution for this variable.
                        if (rewritten is Constant) {
                            var result = new VariableWithValue(varWithBacksub.Variable, ((Constant)rewritten).Value);
                            newVarsWithValue.Add(result);
                            _closedVariables[varWithBacksub.Variable] = result;
                        } else {
                            var result = new VariableWithBacksubstitution(varWithBacksub.Variable, rewritten);
                            newVarsWithBacksubstitutions.Add(result);
                        }
                    }
                }
                varsWithValue = newVarsWithValue.ToArray();
                varsWithBacksubstitutions = newVarsWithBacksubstitutions.ToArray();
            }
        }

        public static IEnumerable<SolverNode> SolverStep(
            IEnumerable<SolverNode> open,
            out SolverNode solutionOrNull) {
            return SolverStep(open,
                              new Dictionary<Variable, VariableWithValue>(),
                              out solutionOrNull);
        }

        public static IEnumerable<SolverNode> SolverStep(
            IEnumerable<SolverNode> open,
            IDictionary<Variable, VariableWithValue> previousValues,
            out SolverNode solutionOrNull) {
            double minRank = open.Min(cs => cs.Rank);
            SolverNode selected = open.First(cs => cs.Rank <= minRank);
            IEnumerable<SolverNode> expandedSets = selected.Expand( /*previousValues*/).ToArray();

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

        public IEnumerable<SolverNode> Expand( /*IDictionary<Variable, VariableRangeRestriction> previousValues*/) {
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

        public bool DefinitelyDead {
            get { return _definitelyDead; }
        }

        private bool IsSolved() {
            return !_constraints.Any();
        }

        private static readonly List<RuleAction> _ruleActions =
            new List<RuleAction>();

        private bool _definitelyDead;

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

        private SolverNode CloseVariable(Variable variable,
                                         AbstractExpr expression,
                                         AbstractConstraint sourceConstraintToBeRemoved) {
            expression = expression.Accept(new ConstantFoldingVisitor(), Ig.nore);

            // Rewrite all constraints
            var rewriter = new RewritingVisitor(
                new Dictionary<AbstractExpr, AbstractExpr> { { variable, expression } });
            IEnumerable<AbstractConstraint> rewrittenConstraints =
                Constraints.Except(sourceConstraintToBeRemoved)
                    .Select(c => c.Accept(rewriter, Ig.nore));

            // Create new variable->value knowledge or new backsubstition.
            var newBacksubstitutions = new Dictionary<Variable, AbstractClosedVariable>(_closedVariables) {{
            variable, expression is Constant
                ? (AbstractClosedVariable)
                    new VariableWithValue(variable, ((Constant) expression).Value)
                : new VariableWithBacksubstitution(variable, expression)
            }};

            // Create new node.
            return new SolverNode(rewrittenConstraints, newBacksubstitutions, this);
        }

        public static SolverNode CreateForTest(IEnumerable<AbstractConstraint> constraints, IDictionary<Variable, AbstractClosedVariable> closedVariables) {
            return new SolverNode(constraints, closedVariables, null);
        }

        public SolverNode CloseVariableAndResolveBacksubstitutionsForTests(Variable variable,
                                               double value) {
            var closed = CloseVariable(variable, new Constant(value), null);
            closed.ResolveBacksubstitutions();
            return closed;
        }

        private SolverNode MarkDefinitelyDead() {
            _definitelyDead = true;
            return null;
        }
    }
}