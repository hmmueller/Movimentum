using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    abstract class ExpressionRewrite {
        public readonly string Name;

        protected ExpressionRewrite(string name, ICollection<ExpressionRewrite> collection) {
            Name = name;
            collection.Add(this);
        }

        public abstract IAbstractExpr SuccessfulMatch(IAbstractExpr expr);

        public override string ToString() {
            return "{" + GetType().Name + "}" + Name;
        }
    }

    class ExpressionRewrite<TMatchResult> : ExpressionRewrite where TMatchResult : class {
        private readonly Func<IAbstractExpr, TMatchResult> _tryMatch;
        private readonly Func<TMatchResult, bool> _isMatch;
        private readonly Func<TMatchResult, IAbstractExpr, IAbstractExpr> _onMatch;

        public ExpressionRewrite(string name,
                ICollection<ExpressionRewrite> collection,
                Func<IAbstractExpr, TMatchResult> tryMatch,
                Func<TMatchResult, bool> isMatch,
                Func<TMatchResult, IAbstractExpr, IAbstractExpr> onMatch)
            : base(name, collection) {
            _tryMatch = tryMatch;
            _isMatch = isMatch;
            _onMatch = onMatch;
        }

        public override IAbstractExpr SuccessfulMatch(IAbstractExpr expr) {
            TMatchResult matchResult = _tryMatch(expr);
            if (matchResult == null) {
                return null;
            } else if (_isMatch(matchResult)) {
                return _onMatch(matchResult, expr);
            } else {
                return null;
            }
        }
    }

    internal class StandardExpressionRewrite : ExpressionRewrite<ExpressionMatcher> {
        public StandardExpressionRewrite(string name,
                ICollection<ExpressionRewrite> collection,
                AbstractExpressionTemplate template,
                Func<ExpressionMatcher, bool> isMatch,
                Func<ExpressionMatcher, IAbstractExpr> onMatch) :
            base(name, collection,
            e => template.CreateMatcher().TryMatch(e),
            m => m != null && isMatch(m),
            (m, e) => onMatch(m)) { }

        public StandardExpressionRewrite(string name,
                ICollection<ExpressionRewrite> collection,
                AbstractExpressionTemplate template,
                Func<ExpressionMatcher, IAbstractExpr> onMatch) :
            this(name, collection, template, m => true, onMatch) { }
    }
}
