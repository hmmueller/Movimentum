using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    internal class RewritingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<AbstractExpr> {
        private readonly IDictionary<AbstractExpr, AbstractExpr> _rewrites;
        public RewritingVisitor(IDictionary<AbstractExpr, AbstractExpr> rewrites) {
            _rewrites = rewrites;
        }

        public IDictionary<AbstractExpr, AbstractExpr> Rewrites {
            get { return _rewrites; }
        }

        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out ScalarConstraint>

        public AbstractConstraint Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            AbstractExpr result = equalsZero.Expr.Accept(this, Ig.nore);
            return result != equalsZero.Expr ? new EqualsZeroConstraint(result) : equalsZero;
        }

        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            AbstractExpr result = moreThanZero.Expr.Accept(this, Ig.nore);
            return result != moreThanZero.Expr ? new MoreThanZeroConstraint(result) : moreThanZero;
        }

        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            AbstractExpr result = atLeastZero.Expr.Accept(this, Ig.nore);
            return result != atLeastZero.Expr ? new AtLeastZeroConstraint(result) : atLeastZero;
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        private AbstractExpr Rewrite(AbstractExpr expr) {
            AbstractExpr result;
            return _rewrites.TryGetValue(expr, out result) ? result : expr;
        }

        public AbstractExpr Visit(Constant constant, Ignore p) {
            return Rewrite(constant);
        }

        public AbstractExpr Visit(NamedVariable namedVariable, Ignore p) {
            return Rewrite(namedVariable);
        }

        public AbstractExpr Visit(AnchorVariable anchorVariable, Ignore p) {
            return Rewrite(anchorVariable);
        }

        public AbstractExpr Visit(UnaryExpression unaryExpression, Ignore p) {
            AbstractExpr result;
            if (_rewrites.TryGetValue(unaryExpression, out result)) {
                return result;
            } else {
                AbstractExpr oldInner = unaryExpression.Inner;
                AbstractExpr newInner = oldInner.Accept(this, Ig.nore);
                if (newInner != oldInner) {
                    return new UnaryExpression(newInner, unaryExpression.Op);
                } else {
                    return unaryExpression;
                }
            }
        }

        public AbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
            AbstractExpr result;
            if (_rewrites.TryGetValue(binaryExpression, out result)) {
                return result;
            } else {
                AbstractExpr oldLhs = binaryExpression.Lhs;
                AbstractExpr oldRhs = binaryExpression.Rhs;
                AbstractExpr newLhs = oldLhs.Accept(this, Ig.nore);
                AbstractExpr newRhs = oldRhs.Accept(this, Ig.nore);
                if (newLhs != oldLhs | newRhs != oldRhs) {
                    return new BinaryExpression(newLhs, binaryExpression.Op, newRhs);
                } else {
                    return binaryExpression;
                }
            }
        }

        public AbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
        }

        ////public AbstractExpr Visit(SingleVariablePolynomial singleVariablePolynomial, Ignore p) {
        ////    // if we rewrite a -> b + c, we get a general expression!
        ////    var variableRewrite = Rewrite(singleVariablePolynomial.Var);
        ////    return variableRewrite == singleVariablePolynomial.Var
        ////        ? singleVariablePolynomial
        ////        : singleVariablePolynomial.EvaluateAt(variableRewrite);
        ////}

        #endregion
    }
}
