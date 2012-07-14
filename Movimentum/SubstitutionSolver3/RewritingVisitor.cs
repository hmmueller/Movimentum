﻿using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    internal class RewritingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<IAbstractExpr> {
        private readonly IDictionary<IAbstractExpr, IAbstractExpr> _rewrites;
        public RewritingVisitor(IDictionary<IAbstractExpr, IAbstractExpr> rewrites) {
            _rewrites = rewrites;
        }

        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out ScalarConstraint>

        public AbstractConstraint Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            IAbstractExpr result = equalsZero.Expr.Accept(this, Ig.nore);
            return result != equalsZero.Expr ? new EqualsZeroConstraint(result) : equalsZero;
        }

        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            IAbstractExpr result = moreThanZero.Expr.Accept(this, Ig.nore);
            return result != moreThanZero.Expr ? new MoreThanZeroConstraint(result) : moreThanZero;
        }

        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            IAbstractExpr result = atLeastZero.Expr.Accept(this, Ig.nore);
            return result != atLeastZero.Expr ? new AtLeastZeroConstraint(result) : atLeastZero;
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        private IAbstractExpr Rewrite(IAbstractExpr expr) {
            IAbstractExpr result;
            return _rewrites.TryGetValue(expr, out result) ? result : expr;
        }

        public IAbstractExpr Visit(IConstant constant, Ignore p) {
            return Rewrite(constant);
        }

        public IAbstractExpr Visit(INamedVariable namedVariable, Ignore p) {
            return Rewrite(namedVariable);
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVariable, Ignore p) {
            return Rewrite(anchorVariable);
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpression, Ignore p) {
            IAbstractExpr result;
            if (_rewrites.TryGetValue(unaryExpression, out result)) {
                return result;
            } else {
                IAbstractExpr oldInner = unaryExpression.Inner;
                IAbstractExpr newInner = oldInner.Accept(this, Ig.nore);
                if (newInner != oldInner) {
                    return new UnaryExpression(newInner, unaryExpression.Op);
                } else {
                    return unaryExpression;
                }
            }
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
            IAbstractExpr result;
            if (_rewrites.TryGetValue(binaryExpression, out result)) {
                return result;
            } else {
                IAbstractExpr oldLhs = binaryExpression.Lhs;
                IAbstractExpr oldRhs = binaryExpression.Rhs;
                IAbstractExpr newLhs = oldLhs.Accept(this, Ig.nore);
                IAbstractExpr newRhs = oldRhs.Accept(this, Ig.nore);
                if (newLhs != oldLhs | newRhs != oldRhs) {
                    return new BinaryExpression(newLhs, binaryExpression.Op, newRhs);
                } else {
                    return binaryExpression;
                }
            }
        }

        public IAbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
        }

        public IAbstractExpr VisitSTEPB(IGeneralPolynomialSTEPB polynomial, Ignore parameter) {
            // STEPC --> RewritingVIsitorSTEPC
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
