using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        private readonly IEnumerable<AbstractConstraint> _constraints;
        private readonly IDictionary<IVariable, AbstractClosedVariable> _closedVariables;

        private static int _debugIdCount = 1000;
        public readonly int DebugId = _debugIdCount++;

        public static readonly List<SolverNode> DebugDeadNodes = new List<SolverNode>();
        public static readonly HashSet<SolverNode> DebugWrittenNodes = new HashSet<SolverNode>();

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
                (depth < 0
                     ? "..."
                     : _debugOrigin == null
                           ? ""
                           : _debugOrigin.DebugOriginChain(depth - 1));
        }

        public override string ToString() {
            return "SolverNode " + DebugOriginChain(4) +
                   _constraints.Aggregate("", (s, c) => s + "\r\n  " + c) +
                   ClosedVariables.Aggregate("", (s, k) => s + "\r\n  " + k.Value);
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

        public double GetVariableValue(IVariable var) {
            AbstractClosedVariable closedVariable = GetClosedVariable(var);
            VariableWithValue result = closedVariable as VariableWithValue;
            if (result == null) {
                throw new InvalidOperationException("IVariable " + var + " not closed as value in this node, but as " + closedVariable);
            }
            return result.Value;
        }

        public AbstractClosedVariable GetClosedVariable(IVariable var) {
            AbstractClosedVariable closedVariable;
            if (!ClosedVariables.TryGetValue(var, out closedVariable)) {
                throw new InvalidOperationException("IVariable " + var + " not closed in this node");
            }
            return closedVariable;
        }

        private SolverNode(IEnumerable<AbstractConstraint> constraints,
                           SolverNode origin)
            : this(constraints, origin.ClosedVariables, origin) { }

        private SolverNode(IEnumerable<AbstractConstraint> constraints,
                           IDictionary<IVariable, AbstractClosedVariable> closedVariables,
                           SolverNode origin) {
            _debugOrigin = origin;
            if (origin != null) {
                origin._debugSuccessors++;
            }

            //var foldingVisitor = new ConstantFoldingVisitor();
            var foldingVisitor = new PolynomialFoldingVisitor();
            _constraints = constraints.Select(c => c.Accept(foldingVisitor, Ig.nore)).ToArray(); // !!!TOARRAY
            foreach (var c in _constraints) {
                c.SetDebugCreatingNodeIdIfMissing(DebugId);
            }

            _closedVariables = new SortedDictionary<IVariable, AbstractClosedVariable>(closedVariables);
        }

        public static IDictionary<IVariable, VariableWithValue> Solve(
                IEnumerable<AbstractConstraint> solverConstraints,
                int loopLimit,
                IDictionary<IVariable, VariableWithValue> previousValues,
                int frameNo,
                EvaluationVisitor debugExpectedResults = null) {
            // Create initial open set
            IEnumerable<SolverNode> open = new[] { new SolverNode(solverConstraints, new Dictionary<IVariable, AbstractClosedVariable>(), null) };

            DebugDeadNodes.Clear();

            // Solver loop
            SolverNode solutionOrNull;
            do {
                if (!open.Any()) {
                    DebugWriteDeadNodes();
                    throw new Exception("No solution found for frame " + frameNo + " - no more open nodes. Look into DebugDeadNodes to see nodes that did not mathc a rule.");
                }

                open = SolverStep(open, previousValues, out solutionOrNull, debugExpectedResults);

                //if (debugExpectedResults != null) {
                //    foreach (var node in open.Except(DebugWrittenNodes)) {
                //        string debugOutifDead = node.DebugToString(debugExpectedResults);
                //        Debug.WriteLine(debugOutifDead);
                //    }
                //    DebugWrittenNodes.UnionWith(open);
                //}

                if (--loopLimit < 0) {
                    DebugWriteDeadNodes();
                    throw new Exception("Cannot find solution for frame " + frameNo + " - loop limit exhausted");
                }
            } while (solutionOrNull == null);

            solutionOrNull.ResolveBacksubstitutions();

            Debug.WriteLine("++++ solution node ++++");
            Debug.WriteLine(solutionOrNull);

            return solutionOrNull.ClosedVariables
                .Where(kvp => kvp.Value is VariableWithValue)
                .ToDictionary(kvp => kvp.Key, kvp => (VariableWithValue)kvp.Value);
        }

        private static void DebugWriteDeadNodes() {
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
        }

        private string DebugToString(EvaluationVisitor debugExpectedResults) {
            var sb = new StringBuilder();
            bool allTrue = true;
            sb.AppendLine("SolverNode " + DebugOriginChain(4));
            foreach (ScalarConstraint c in Constraints) {
                string result;
                try {
                    bool isTrue = c.Accept(debugExpectedResults, Ig.nore);
                    result = isTrue ? "  Y " : "  N [" + c.Expr.Accept(debugExpectedResults, Ig.nore).ToString(CultureInfo.InvariantCulture) + "]";
                    allTrue &= isTrue;
                } catch (NotSupportedException) {
                    result = "  ? ";
                }
                sb.Append(result);
                sb.AppendLine(c.ToString().WithParDepth(3));
            }
            foreach (var c in ClosedVariables.Values) {
                sb.Append("  ");
                sb.AppendLine(c.ToString());
            }
            sb.AppendLine(debugExpectedResults.DebugVariableValuesAsString());
            return (allTrue ? "+" : "-") + sb;
        }

        private void ResolveBacksubstitutions() {
            //var foldingVisitor = new ConstantFoldingVisitor();
            var foldingVisitor = new PolynomialFoldingVisitor();

            // The "ToArrays" are necessary because we modify _closedVariables below. Without them,
            // we get InvalidOperationExceptions telling us that the "Collection was modified."
            var varsWithValue = ClosedVariables.Values.OfType<VariableWithValue>().ToArray();
            var varsWithBacksubstitutions = ClosedVariables.Values.OfType<VariableWithBacksubstitution>().ToArray();

            while (varsWithBacksubstitutions.Any()) {
                if (!varsWithValue.Any()) {
                    Debug.WriteLine("---- No complete backsubstitution possible ----");
                    foreach (var vb in varsWithBacksubstitutions) {
                        Debug.WriteLine(vb);
                    }
                    throw new InvalidOperationException("No solution found - " +
                              "there are more variables to substitute, but still open substitutions");
                }

                var newVarsWithValue = new Dictionary<IVariable, VariableWithValue>();
                foreach (var varWithValue in varsWithValue) {
                    var newVarsWithBacksubstitutions = new List<VariableWithBacksubstitution>();
                    // We substitute each variable into all open backsubstitutions.
                    //var rewriter = new RewritingVisitor(new Dictionary<IAbstractExpr, IAbstractExpr> 
                    //                                { { varWithValue.Variable, 
                    //                                    Polynomial.CreateConstant(varWithValue.Value) 
                    //                                } });
                    var rewriter = new RewritingVisitor(varWithValue.Variable,
                                                        Polynomial.CreateConstant(varWithValue.Value));
                    foreach (var varWithBacksub in varsWithBacksubstitutions) {
                        IAbstractExpr rewritten = varWithBacksub.Expr
                                                               .Accept(rewriter)
                                                               .Accept(foldingVisitor);
                        // If the result, after constant folding, is a constant, we have found a new solution value.
                        // Otherwise, we still have a - maybe smaller - backsubstitution for this variable.
                        if (rewritten is IConstant) {
                            var result = new VariableWithValue(varWithBacksub.Variable, ((IConstant)rewritten).Value);
                            newVarsWithValue.Add(varWithBacksub.Variable, result);
                            ClosedVariables[varWithBacksub.Variable] = result;
                        } else {
                            var result = new VariableWithBacksubstitution(varWithBacksub.Variable, rewritten);
                            newVarsWithBacksubstitutions.Add(result);
                        }
                    }
                    newVarsWithBacksubstitutions.RemoveAll(v => ClosedVariables.ContainsKey(v.Variable) && ClosedVariables[v.Variable] is VariableWithValue);
                    varsWithBacksubstitutions = newVarsWithBacksubstitutions.ToArray();
                }
                varsWithValue = newVarsWithValue.Values.ToArray();
            }
        }

        public static IEnumerable<SolverNode> SolverStep(IEnumerable<SolverNode> open, 
                IDictionary<IVariable, VariableWithValue> previousValues, 
                out SolverNode solutionOrNull, 
                EvaluationVisitor debugExpectedResults = null) {
            double minRank = open.Min(cs => cs.Rank);
            SolverNode selected;
            if (debugExpectedResults == null) {
                selected = open.First(cs => cs.Rank <= minRank);
            } else {
                // The debugExpectedResults are intended to steer the solver only on
                // nodes that match the expected result!
                selected = open.FirstOrDefault(cs =>
                    cs.Constraints.All(c => DebugIsTrueOrUnknown(c, debugExpectedResults))
                );
                if (selected == null) {
                    Debug.WriteLine("---- All open nodes do not match expected results:");
                    foreach (var node in open) {
                        Debug.WriteLine(node.DebugToString(debugExpectedResults));
                    }

                    throw new InvalidOperationException("No node matches expected results");
                }
            }
            IEnumerable<SolverNode> expanded = selected.Expand( /*previousValues*/).ToArray();

            if (!expanded.Any()) {
                // Dead node - register in dead nodes.
                DebugDeadNodes.Add(selected);

                // ????
                if (debugExpectedResults != null) {
                    Debug.WriteLine(".... Constraint state " + selected.DebugToString(debugExpectedResults));
                }
            }

            //IEnumerable<SolverNode> newOpen = open.Except(selected).Concat(expandedSets);
            IEnumerable<SolverNode> newOpen = expanded.Concat(open.Except(selected)).ToArray();

            // Not really correct: We should also check whether all anchor variables have
            // a single value. For the moment, in our tests, we live with this rough check.
            solutionOrNull = expanded.FirstOrDefault(cs => cs.IsSolved());

            return newOpen;
        }

        private static bool DebugIsTrueOrUnknown(AbstractConstraint c, EvaluationVisitor debugExpectedResults) {
            try {
                return c.Accept(debugExpectedResults, Ig.nore);
            } catch (NotSupportedException) {
                return true;
            }
        }

        public IEnumerable<SolverNode> Expand( /*IDictionary<IVariable, VariableRangeRestriction> previousValues*/) {
            foreach (var ra in _ruleActions) {
                foreach (var c in Constraints.OfType<ScalarConstraint>()) {
                    IEnumerable<SolverNode> resultOrNull = ra.SuccessfulMatch(this, c);
                    if (resultOrNull != null) {
                        resultOrNull = resultOrNull.ToArray();
                        foreach (var node in resultOrNull) {
                            node.DebugSetCreationRule(ra.Name);
                        }
                        return resultOrNull;
                    }
                }
            }
            Debug.WriteLine("No RuleAction matches " + this);
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

        public IDictionary<IVariable, AbstractClosedVariable> ClosedVariables {
            get { return _closedVariables; }
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

            public override string ToString() {
                return "{" + GetType().Name + "}" + Name;
            }
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

        private SolverNode CloseVariable(IVariable variable,
                                         IAbstractExpr expression,
                                         AbstractConstraint sourceConstraintToBeRemoved) {
            //var foldingVisitor = new ConstantFoldingVisitor();
            var foldingVisitor = new PolynomialFoldingVisitor();
            expression = expression.Accept(foldingVisitor);

            // Rewrite all constraints
            //var rewriter = new RewritingVisitor(
            //    new Dictionary<IAbstractExpr, IAbstractExpr> { { variable, expression } });
            var rewriter = new RewritingVisitor(variable, expression);
            IEnumerable<AbstractConstraint> rewrittenConstraints =
                Constraints.Except(sourceConstraintToBeRemoved)
                    .Select(c => c.Accept(rewriter, Ig.nore));

            // Create new variable->value knowledge or new backsubstition.
            var newBacksubstitutions = new Dictionary<IVariable, AbstractClosedVariable>(ClosedVariables) {{
                variable, expression is IConstant
                    ? (AbstractClosedVariable)
                        new VariableWithValue(variable, ((IConstant) expression).Value)
                    : new VariableWithBacksubstitution(variable, expression)
                }};

            // Create new node.
            return new SolverNode(rewrittenConstraints, newBacksubstitutions, this);
        }

        public static SolverNode CreateForTest(IEnumerable<AbstractConstraint> constraints, IDictionary<IVariable, AbstractClosedVariable> closedVariables) {
            return new SolverNode(constraints, closedVariables, null);
        }

        public SolverNode CloseVariableAndResolveBacksubstitutionsForTests(IVariable variable,
                                               double value) {
            var closed = CloseVariable(variable, Polynomial.CreateConstant(value), null);
            closed.ResolveBacksubstitutions();
            return closed;
        }

        private SolverNode MarkDefinitelyDead() {
            _definitelyDead = true;
            return null;
        }
    }
}