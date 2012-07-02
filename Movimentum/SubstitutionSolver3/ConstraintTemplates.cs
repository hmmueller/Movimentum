namespace Movimentum.SubstitutionSolver3 {
internal abstract class ScalarConstraintTemplate {
    private readonly AbstractExpressionTemplate _expr;
    protected ScalarConstraintTemplate(AbstractExpressionTemplate expr) { 
        _expr = expr;
    }
    public AbstractExpressionTemplate Expr { get { return _expr; } }

    public abstract bool TypeMatches(ScalarConstraint abstractConstraint);
}

internal class EqualsZeroConstraintTemplate : ScalarConstraintTemplate {
    public EqualsZeroConstraintTemplate(AbstractExpressionTemplate expr) 
        : base(expr) {}
    public override bool TypeMatches(ScalarConstraint constraint) {
        return constraint is EqualsZeroConstraint;
    }
}

internal class AtLeastZeroConstraintTemplate : ScalarConstraintTemplate {
    public AtLeastZeroConstraintTemplate(AbstractExpressionTemplate expr) 
        : base(expr) {}
    public override bool TypeMatches(ScalarConstraint constraint) {
        return constraint is AtLeastZeroConstraint;
    }
}

internal class MoreThanZeroConstraintTemplate : ScalarConstraintTemplate {
    public MoreThanZeroConstraintTemplate(AbstractExpressionTemplate expr) 
        : base(expr) {}
    public override bool TypeMatches(ScalarConstraint constraint) {
        return constraint is MoreThanZeroConstraint;
    }
}

    internal abstract class Matcher {}

    internal class BoolMatcher : Matcher {}

    internal class ScalarConstraintMatcher : Matcher {
    private readonly ScalarConstraintTemplate _template;
    private ExpressionMatcher _expressionMatcher;

    public ScalarConstraintMatcher(ScalarConstraintTemplate template) {
        _template = template;
    }

    public bool TryMatch(ScalarConstraint constraint) {
        _expressionMatcher = new ExpressionMatcher(_template.Expr);
        return _template.TypeMatches(constraint) 
            && _expressionMatcher.TryMatch(constraint.Expr);
    }

    public AbstractExpr Match(FixedExpressionTemplate t) {
        return _expressionMatcher.Match(t);
    }

    public BinaryExpression Match(BinaryExpressionTemplate t) {
        return _expressionMatcher.Match(t);
    }

    public UnaryExpression Match(UnaryExpressionTemplate t) {
        return _expressionMatcher.Match(t);
    }

    public T Match<T>(TypeMatchTemplate<T> t) where T : AbstractExpr {
        return _expressionMatcher.Match(t);
    }
}
}