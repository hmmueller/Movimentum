using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    class PolynomialFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<IAbstractExpr>
                                          , ISolverModelUnaryOpVisitor<IPolynomial, Ignore, IAbstractExpr>
                                          , ISolverModelBinaryOpVisitor<IPolynomial, Ignore, IAbstractExpr> {
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

        public IAbstractExpr Visit(IConstant constant, Ignore p) {
            return constant;
        }

        public IAbstractExpr Visit(INamedVariable namedVariable, Ignore p) {
            return namedVariable;
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVariable, Ignore p) {
            return anchorVariable;
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpression, Ignore p) {
            IAbstractExpr oldInner = unaryExpression.Inner;
            IAbstractExpr newInner = oldInner.Accept(this, Ig.nore);
            if (newInner is IPolynomial && !(unaryExpression.Op is FormalSquareroot)) {
                return unaryExpression.Op.Accept(this, (IPolynomial)newInner, Ig.nore);
            } else if (newInner != oldInner) {
                return new UnaryExpression(newInner, unaryExpression.Op);
            } else {
                return unaryExpression;
            }
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
            IAbstractExpr oldLhs = binaryExpression.Lhs;
            IAbstractExpr oldRhs = binaryExpression.Rhs;
            IAbstractExpr newLhs = oldLhs.Accept(this, Ig.nore);
            IAbstractExpr newRhs = oldRhs.Accept(this, Ig.nore);
            if (newLhs is IPolynomial & newRhs is IPolynomial) {
                return binaryExpression.Op.Accept(this, (IPolynomial)newLhs, (IPolynomial)newRhs, Ig.nore);
            } else if (newLhs != oldLhs | newRhs != oldRhs) {
                return new BinaryExpression(newLhs, binaryExpression.Op, newRhs);
            } else {
                return binaryExpression;
            }
        }

        public IAbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
        }

        public IAbstractExpr VisitSTEPB(IGeneralPolynomialSTEPB polynomial, Ignore parameter) {
            return polynomial;
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(UnaryMinus op, IPolynomial inner, Ignore p) {
            return Polynomial.CreatePolynomial(inner.Var, inner.Coefficients.Select(c => -c));
        }

        public IAbstractExpr Visit(Square op, IPolynomial inner, Ignore p) {
            return (inner.E * inner).Accept(this, p);
        }

        public IAbstractExpr Visit(FormalSquareroot op, IPolynomial inner, Ignore p) {
            return new UnaryExpression(inner, new FormalSquareroot());
        }

        private static IAbstractExpr Fold(UnaryOperator op, IPolynomial inner, Func<double, double> value, IConstant innerAsConstant) {
            return innerAsConstant == null
                       ? (IAbstractExpr)new UnaryExpression(inner, op)
                       : Polynomial.CreateConstant(value(innerAsConstant.Value));
        }

        public IAbstractExpr Visit(PositiveSquareroot op, IPolynomial inner, Ignore p) {
            return Fold(op, inner, x => Math.Sqrt(x), inner as IConstant);
        }

        public IAbstractExpr Visit(Sin op, IPolynomial inner, Ignore p) {
            return Fold(op, inner, x => Math.Sin(x / 180 * Math.PI), inner as IConstant);
        }

        public IAbstractExpr Visit(Cos op, IPolynomial inner, Ignore p) {
            return Fold(op, inner, x => Math.Cos(x / 180 * Math.PI), inner as IConstant);
        }

        #endregion Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

        #region Implementation of ISolverModelBinaryOpVisitor<in IPolynomial,in IPolynomial,in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(Plus op, IPolynomial lhs, IPolynomial rhs, Ignore p) {
            if (lhs.Var.Equals(rhs.Var) 
                | lhs is IConstant     // constants have a funny variable in them that does not compare well.
                | rhs is IConstant) {
                var deg = Math.Max(lhs.Degree, rhs.Degree);
                double[] lhsCoeff = new double[deg - lhs.Degree].Concat(lhs.Coefficients).ToArray();
                double[] rhsCoeff = new double[deg - rhs.Degree].Concat(rhs.Coefficients).ToArray();
                var coeffs = new double[deg + 1];
                for (int i = 0; i <= deg; i++) {
                    coeffs[i] = lhsCoeff[i] + rhsCoeff[i];
                }
                IVariable newVar = lhs is IConstant ? rhs.Var : lhs.Var;
                return Polynomial.CreatePolynomial(newVar, coeffs);
            } else {
                return new BinaryExpression((AbstractExpr)lhs, new Plus(), (AbstractExpr)rhs);
            }
        }

        public IAbstractExpr Visit(Times op, IPolynomial lhs, IPolynomial rhs, Ignore p) {
            if (lhs.Var.Equals(rhs.Var) | lhs is IConstant | rhs is IConstant) {
                var deg = lhs.Degree + rhs.Degree;
                var coeffs = new double[deg + 1];
                for (int ld = 0; ld <= lhs.Degree; ld++) {
                    for (int rd = 0; rd <= rhs.Degree; rd++) {
                        coeffs[ld + rd] += lhs.Coefficients.ElementAt(ld) * rhs.Coefficients.ElementAt(rd);
                    }
                }
                IVariable newVar = lhs is IConstant ? rhs.Var : lhs.Var;
                return Polynomial.CreatePolynomial(newVar, coeffs);
            } else {
                return new BinaryExpression((AbstractExpr)lhs, new Times(), (AbstractExpr)rhs);
            }
        }

        public IAbstractExpr Visit(Divide op, IPolynomial lhs, IPolynomial rhs, Ignore p) { // ......
            // TODO: Alternative - try polynomial division. Create polynomial; or polynomial + remainder/rhs.
            var rConst = rhs as IConstant;
            if (rConst != null) {
                return Polynomial.CreatePolynomial(lhs.Var, lhs.Coefficients.Select(c => c / rConst.Value)).Accept(this, p);
            } else {
                return (AbstractExpr)lhs / (AbstractExpr)rhs;
            }
        }

        #endregion Implementation of ISolverModelBinaryOpVisitor<in IPolynomial,in IPolynomial,in Ignore,out AbstractExpr>
    }
}
