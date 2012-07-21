using System;

namespace Movimentum.SubstitutionSolver3 {
    internal class RewritingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<IAbstractExpr> {
        private readonly IAbstractExpr _from;
        private readonly IAbstractExpr _to;
        public RewritingVisitor(IAbstractExpr from, IAbstractExpr to) {
            if (from is IConstant) {
                throw new ArgumentException("Cannot replace constants - they might already have been folded");
            }
            _from = from;
            _to = to;
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
            return expr.Equals(_from) ? _to : expr;
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
            if (unaryExpression.Equals(_from)) {
                return _to;
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
            if (binaryExpression.Equals(_from)) {
                return _to;
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

        public IAbstractExpr Visit(IGeneralPolynomial polynomial, Ignore parameter) {
            if (polynomial.Var.Equals(_from)) {
                if (_from is IConstant) {
                    // For efficiency, handle this case by direct evaluation, instead
                    // of building an expression tree that is later constant-folded.
                    double from = ((IConstant)_from).Value;
                    var visitor = new EvaluationVisitor(polynomial.Var, from);
                    return Polynomial.CreateConstant(polynomial.Accept(visitor, Ig.nore));
                } else {
                    // Evaluate by Horner's rule.
                    IAbstractExpr result = Polynomial.CreateConstant(polynomial.Coefficient(polynomial.Degree));
                    for (int i = polynomial.Degree - 1; i >= 0; i--) {
                        result = _to.E*result + Polynomial.CreateConstant(polynomial.Coefficient(i));
                    }
                    return result;
                }
            } else {
                return polynomial;
            }
        }

        #endregion
    }
}
