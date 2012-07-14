using System;

namespace Movimentum.SubstitutionSolver3 {
    internal abstract class ScalarConstraintTemplate {
        private readonly AbstractExpressionTemplate _expr;
        protected ScalarConstraintTemplate(AbstractExpressionTemplate expr) {
            _expr = expr;
        }
        public AbstractExpressionTemplate Expr { get { return _expr; } }

        public abstract bool TypeMatches(ScalarConstraint abstractConstraint);

        public Func<ScalarConstraint, ScalarConstraintMatcher> GetMatchDelegate() {
            return constraint => new ScalarConstraintMatcher(this).TryMatch(constraint);
        }
    }

    internal class EqualsZeroConstraintTemplate : ScalarConstraintTemplate {
        public EqualsZeroConstraintTemplate(AbstractExpressionTemplate expr)
            : base(expr) { }
        public override bool TypeMatches(ScalarConstraint constraint) {
            return constraint is EqualsZeroConstraint;
        }
    }

    internal class AtLeastZeroConstraintTemplate : ScalarConstraintTemplate {
        public AtLeastZeroConstraintTemplate(AbstractExpressionTemplate expr)
            : base(expr) { }
        public override bool TypeMatches(ScalarConstraint constraint) {
            return constraint is AtLeastZeroConstraint;
        }
    }

    internal class MoreThanZeroConstraintTemplate : ScalarConstraintTemplate {
        public MoreThanZeroConstraintTemplate(AbstractExpressionTemplate expr)
            : base(expr) { }
        public override bool TypeMatches(ScalarConstraint constraint) {
            return constraint is MoreThanZeroConstraint;
        }
    }

    //internal class BoolMatcher : Matcher {
    //    private readonly Func<ScalarConstraint, bool> _match;
    //    private readonly Func<SolverNode, BoolMatcher, AbstractConstraint, IEnumerable<SolverNode>> _onMatch;

    //    public BoolMatcher(Func<ScalarConstraint, bool> match, Func<SolverNode, BoolMatcher, AbstractConstraint, IEnumerable<SolverNode>> onMatch) {
    //        _match = match;
    //        _onMatch = onMatch;
    //    }

    //    public override bool TryMatch(ScalarConstraint constraint) {
    //        return _match(constraint);
    //    }

    //    public override IEnumerable<SolverNode> OnMatch(SolverNode solverNode, ScalarConstraint constraint) {
    //        return _onMatch(solverNode, this, constraint);
    //    }
    //}

    internal class ScalarConstraintMatcher {
        private readonly ExpressionMatcher _expressionMatcher;
        private readonly ScalarConstraintTemplate _template;

        internal ScalarConstraintMatcher(ScalarConstraintTemplate template) {
            _template = template;
            _expressionMatcher = new ExpressionMatcher(_template.Expr);
        }

        public ScalarConstraintMatcher TryMatch(ScalarConstraint constraint) {
            bool isMatch = _template.TypeMatches(constraint)
                           && _expressionMatcher.TryMatch(constraint.Expr);
            return isMatch ? this : null;
        }

        public IAbstractExpr Match(FixedExpressionTemplate t) {
            return _expressionMatcher.Match(t);
        }

        public BinaryExpression Match(BinaryExpressionTemplate t) {
            return _expressionMatcher.Match(t);
        }

        public UnaryExpression Match(UnaryExpressionTemplate t) {
            return _expressionMatcher.Match(t);
        }

        public T Match<T>(TypeMatchTemplate<T> t) where T : class, IAbstractExpr {
            return _expressionMatcher.Match(t);
        }
    }
}