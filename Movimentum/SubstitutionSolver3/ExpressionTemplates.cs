using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    abstract class AbstractExpressionTemplate {
        /// <returns><c>null</c> if not successful; or a dictionary of (template,expression) pairs if successful.</returns>
        public IDictionary<AbstractExpressionTemplate, IAbstractExpr> TryMatch(IAbstractExpr e) {
            var collector = new Dictionary<AbstractExpressionTemplate, IAbstractExpr>();
            return TryMatchAndRemember(e, collector) ? collector : null;
        }

        internal bool TryMatchAndRemember(IAbstractExpr e, IDictionary<AbstractExpressionTemplate, IAbstractExpr> matches) {
            if (matches.ContainsKey(this)) {
                return matches[this].Equals(e);
            } else if (TryMatch(e, matches)) {
                matches.Add(this, e);
                return true;
            } else {
                return false;
            }
        }
        protected abstract bool TryMatch(IAbstractExpr e, IDictionary<AbstractExpressionTemplate, IAbstractExpr> matches);

        public static BinaryExpressionTemplate operator +(AbstractExpressionTemplate lhs, AbstractExpressionTemplate rhs) {
            return new BinaryExpressionTemplate(lhs, new Plus(), rhs);
        }

        public static BinaryExpressionTemplate operator *(AbstractExpressionTemplate lhs, AbstractExpressionTemplate rhs) {
            return new BinaryExpressionTemplate(lhs, new Times(), rhs);
        }

        public static BinaryExpressionTemplate operator /(AbstractExpressionTemplate lhs, AbstractExpressionTemplate rhs) {
            return new BinaryExpressionTemplate(lhs, new Divide(), rhs);
        }

        public static UnaryExpressionTemplate operator -(AbstractExpressionTemplate inner) {
            return new UnaryExpressionTemplate(new UnaryMinus(), inner);
        }

        public ExpressionMatcher CreateMatcher() {
            return new ExpressionMatcher(this);
        }
    }

    class TypeMatchTemplate<T> : AbstractExpressionTemplate where T : IAbstractExpr {
        protected override bool TryMatch(IAbstractExpr e, IDictionary<AbstractExpressionTemplate, IAbstractExpr> matches) {
            return e is T;
        }
    }

    class FixedExpressionTemplate : AbstractExpressionTemplate {
        private readonly AbstractExpr _expr;
        public FixedExpressionTemplate(AbstractExpr expr) {
            _expr = expr;
        }

        public AbstractExpr Expr {
            get { return _expr; }
        }

        protected override bool TryMatch(IAbstractExpr e, IDictionary<AbstractExpressionTemplate, IAbstractExpr> matches) {
            return _expr.Equals(e);
        }
    }

    class UnaryExpressionTemplate : AbstractExpressionTemplate {
        private readonly UnaryOperator _op;
        private readonly AbstractExpressionTemplate _inner;
        public UnaryExpressionTemplate(UnaryOperator op, AbstractExpressionTemplate inner) {
            _op = op;
            _inner = inner;
        }

        public UnaryOperator Op {
            get { return _op; }
        }

        public AbstractExpressionTemplate Inner {
            get { return _inner; }
        }
        protected override bool TryMatch(IAbstractExpr e, IDictionary<AbstractExpressionTemplate, IAbstractExpr> matches) {
            var ue = e as UnaryExpression;
            return ue != null && IsSameOperatorAs(ue.Op, _op) && _inner.TryMatchAndRemember(ue.Inner, matches);
        }

        private bool IsSameOperatorAs(UnaryOperator op1, UnaryOperator op2) {
            return op1.GetType() == op2.GetType();
        }
    }

    class BinaryExpressionTemplate : AbstractExpressionTemplate {
        private readonly AbstractExpressionTemplate _lhs;
        private readonly BinaryOperator _op;
        private readonly AbstractExpressionTemplate _rhs;
        public BinaryExpressionTemplate(AbstractExpressionTemplate lhs, BinaryOperator op, AbstractExpressionTemplate rhs) {
            _lhs = lhs;
            _op = op;
            _rhs = rhs;
        }

        public AbstractExpressionTemplate Lhs {
            get { return _lhs; }
        }

        public BinaryOperator Op {
            get { return _op; }
        }

        public AbstractExpressionTemplate Rhs {
            get { return _rhs; }
        }
        protected override bool TryMatch(IAbstractExpr e, IDictionary<AbstractExpressionTemplate, IAbstractExpr> matches) {
            var be = e as BinaryExpression;
            return be != null && _lhs.TryMatchAndRemember(be.Lhs, matches) && IsSameOperatorAs(be.Op, _op) && _rhs.TryMatchAndRemember(be.Rhs, matches);
        }

        private bool IsSameOperatorAs(BinaryOperator op1, BinaryOperator op2) {
            return op1.GetType() == op2.GetType();
        }
    }

    class ExpressionMatcher {
        private readonly AbstractExpressionTemplate _template;
        private IDictionary<AbstractExpressionTemplate, IAbstractExpr> _matches;

        public ExpressionMatcher(AbstractExpressionTemplate template) {
            _template = template;
        }

        public bool TryMatch(IAbstractExpr e) {
            _matches = _template.TryMatch(e);
            return _matches != null;
        }

        private IAbstractExpr GetMatch(AbstractExpressionTemplate t) {
            IAbstractExpr result;
            _matches.TryGetValue(t, out result);
            return result;
        }

        public IAbstractExpr Match(FixedExpressionTemplate t) {
            return GetMatch(t);
        }

        public BinaryExpression Match(BinaryExpressionTemplate t) {
            return GetMatch(t) as BinaryExpression;
        }

        public UnaryExpression Match(UnaryExpressionTemplate t) {
            return GetMatch(t) as UnaryExpression;
        }

        public T Match<T>(TypeMatchTemplate<T> t) where T : class, IAbstractExpr {
            return GetMatch(t) as T;
        }
    }
}
