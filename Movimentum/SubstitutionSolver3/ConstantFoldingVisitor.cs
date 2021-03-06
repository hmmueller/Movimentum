﻿using System;

namespace Movimentum.SubstitutionSolver3 {
    class ConstantFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<IAbstractExpr>
                                          , ISolverModelUnaryOpVisitor<IConstant, Ignore, double>
                                          , ISolverModelBinaryOpVisitor<IConstant, Ignore, double> {
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

        public IAbstractExpr Visit(IConstant constant, Ignore p) {
            return constant;
        }

        public IAbstractExpr Visit(INamedVariable namedVar, Ignore p) {
            return namedVar;
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVar, Ignore p) {
            return anchorVar;
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpr, Ignore p) {
            IAbstractExpr oldInner = unaryExpr.Inner;
            IAbstractExpr newInner = oldInner.Accept(this);
            if (newInner is IConstant && !(unaryExpr.Op is FormalSquareroot)) {
                return Polynomial.CreateConstant(unaryExpr.Op.Accept(this, (IConstant)newInner, Ig.nore));
            } else if (newInner != oldInner) {
                return new UnaryExpression(newInner, unaryExpr.Op);
            } else {
                return unaryExpr;
            }
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpr, Ignore p) {
            IAbstractExpr oldLhs = binaryExpr.Lhs;
            IAbstractExpr oldRhs = binaryExpr.Rhs;
            IAbstractExpr newLhs = oldLhs.Accept(this);
            IAbstractExpr newRhs = oldRhs.Accept(this);
            if (newLhs is IConstant & newRhs is IConstant) {
                return Polynomial.CreateConstant(binaryExpr.Op.Accept(this, (IConstant)newLhs, (IConstant)newRhs, Ig.nore));
            } else if (newLhs != oldLhs | newRhs != oldRhs) {
                return new BinaryExpression(newLhs, binaryExpr.Op, newRhs);
            } else {
                return binaryExpr;
            }
        }

        public IAbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
            //AbstractExpr newExpr = rangeExpr.Expr.Accept(this);
            //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this);
            //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
            //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
        }

        public IAbstractExpr Visit(IGeneralPolynomial polynomial, Ignore p) {
            // We decree that polynomials are always "constant folded:" There is never a general polynomial
            // with degree 0 around - this must be made sure by the Polynomial.Create... methods.
            return polynomial;
        }

        //private Tuple<RangeExpr.Pair, RangeExpr.Pair> VisitPair(RangeExpr.Pair pair) {
        //    AbstractExpr newMoreThan = pair.MoreThan.Accept(this);
        //    AbstractExpr newValue = pair.Value.Accept(this);
        //    return pair.MoreThan != newMoreThan || pair.Value != newValue
        //        ? Tuple.Create(pair, new RangeExpr.Pair(newMoreThan, newValue))
        //        : Tuple.Create(pair, pair);
        //}

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in IConstant,in Ignore,out double>

        public double Visit(UnaryMinus op, IConstant inner, Ignore p) {
            return -inner.Value;
        }

        public double Visit(Square op, IConstant inner, Ignore p) {
            return inner.Value * inner.Value;
        }

        public double Visit(FormalSquareroot op, IConstant inner, Ignore p) {
            throw new NotImplementedException();
        }

        public double Visit(PositiveSquareroot op, IConstant inner, Ignore p) {
            return inner.Value.Near(0) ? 0 : Math.Sqrt(inner.Value);
        }

        public double Visit(Sin op, IConstant inner, Ignore p) {
            return Math.Sin(inner.Value * Math.PI / 180);
        }

        public double Visit(Cos op, IConstant inner, Ignore p) {
            return Math.Cos(inner.Value * Math.PI / 180);
        }

        #endregion

        #region Implementation of ISolverModelBinaryOpVisitor<in IConstant,in IConstant,out double>

        public double Visit(Plus op, IConstant lhs, IConstant rhs, Ignore p) {
            return lhs.Value + rhs.Value;
        }

        public double Visit(Times op, IConstant lhs, IConstant rhs, Ignore p) {
            return lhs.Value * rhs.Value;
        }

        public double Visit(Divide op, IConstant lhs, IConstant rhs, Ignore p) {
            return lhs.Value / rhs.Value;
        }

        #endregion
    }
}
