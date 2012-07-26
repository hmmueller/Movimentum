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
            IAbstractExpr result = equalsZero.Expr.Accept(this);
            return result != equalsZero.Expr ? new EqualsZeroConstraint(result) : equalsZero;
        }

        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            IAbstractExpr result = moreThanZero.Expr.Accept(this);
            return result != moreThanZero.Expr ? new MoreThanZeroConstraint(result) : moreThanZero;
        }

        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            IAbstractExpr result = atLeastZero.Expr.Accept(this);
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

        public IAbstractExpr Visit(INamedVariable namedVar, Ignore p) {
            return Rewrite(namedVar);
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVar, Ignore p) {
            return Rewrite(anchorVar);
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpr, Ignore p) {
            if (unaryExpr.Equals(_from)) {
                return _to;
            } else {
                IAbstractExpr oldInner = unaryExpr.Inner;
                IAbstractExpr newInner = oldInner.Accept(this);
                if (newInner != oldInner) {
                    return new UnaryExpression(newInner, unaryExpr.Op);
                } else {
                    return unaryExpr;
                }
            }
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpr, Ignore p) {
            if (binaryExpr.Equals(_from)) {
                return _to;
            } else {
                IAbstractExpr oldLhs = binaryExpr.Lhs;
                IAbstractExpr oldRhs = binaryExpr.Rhs;
                IAbstractExpr newLhs = oldLhs.Accept(this);
                IAbstractExpr newRhs = oldRhs.Accept(this);
                if (newLhs != oldLhs | newRhs != oldRhs) {
                    return new BinaryExpression(newLhs, binaryExpr.Op, newRhs);
                } else {
                    return binaryExpr;
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
                    return Polynomial.CreateConstant(polynomial.Accept(visitor));
                } else {
                    // Evaluate by Horner's rule.
                    IAbstractExpr result = Polynomial.CreateConstant(polynomial.Coefficient(polynomial.Degree));
                    for (int i = polynomial.Degree - 1; i >= 0; i--) {
                        result = _to.C * result + Polynomial.CreateConstant(polynomial.Coefficient(i));
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
