using System;

namespace Movimentum.SubstitutionSolver3 {
    class ConstantFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<AbstractExpr>
                                          , ISolverModelUnaryOpVisitor<Constant, Ignore, double>
                                          , ISolverModelBinaryOpVisitor<Constant, Ignore, double> {
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

        public AbstractExpr Visit(Constant constant, Ignore p) {
            return constant;
        }

        public AbstractExpr Visit(NamedVariable namedVariable, Ignore p) {
            return namedVariable;
        }

        public AbstractExpr Visit(AnchorVariable anchorVariable, Ignore p) {
            return anchorVariable;
        }

        public AbstractExpr Visit(UnaryExpression unaryExpression, Ignore p) {
            AbstractExpr oldInner = unaryExpression.Inner;
            AbstractExpr newInner = oldInner.Accept(this, Ig.nore);
            if (newInner is Constant && !(unaryExpression.Op is FormalSquareroot)) {
                return new Constant(unaryExpression.Op.Accept(this, (Constant)newInner, Ig.nore));
            } else if (newInner != oldInner) {
                return new UnaryExpression(newInner, unaryExpression.Op);
            } else {
                return unaryExpression;
            }
        }

        public AbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
            AbstractExpr oldLhs = binaryExpression.Lhs;
            AbstractExpr oldRhs = binaryExpression.Rhs;
            AbstractExpr newLhs = oldLhs.Accept(this, Ig.nore);
            AbstractExpr newRhs = oldRhs.Accept(this, Ig.nore);
            if (newLhs is Constant & newRhs is Constant) {
                return new Constant(binaryExpression.Op.Accept(this, (Constant)newLhs, (Constant)newRhs, Ig.nore));
            } else if (newLhs != oldLhs | newRhs != oldRhs) {
                return new BinaryExpression(newLhs, binaryExpression.Op, newRhs);
            } else {
                return binaryExpression;
            }
        }

        public AbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
            //AbstractExpr newExpr = rangeExpr.Expr.Accept(this, Ig.nore);
            //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this, Ig.nore);
            //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
            //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
        }

        //private Tuple<RangeExpr.Pair, RangeExpr.Pair> VisitPair(RangeExpr.Pair pair) {
        //    AbstractExpr newMoreThan = pair.MoreThan.Accept(this, Ig.nore);
        //    AbstractExpr newValue = pair.Value.Accept(this, Ig.nore);
        //    return pair.MoreThan != newMoreThan || pair.Value != newValue
        //        ? Tuple.Create(pair, new RangeExpr.Pair(newMoreThan, newValue))
        //        : Tuple.Create(pair, pair);
        //}

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in Constant,in Ignore,out double>

        public double Visit(UnaryMinus op, Constant inner, Ignore p) {
            return -inner.Value;
        }

        public double Visit(Square op, Constant inner, Ignore p) {
            return inner.Value * inner.Value;
        }

        public double Visit(FormalSquareroot op, Constant inner, Ignore p) {
            throw new NotImplementedException();
        }

        public double Visit(PositiveSquareroot op, Constant inner, Ignore p) {
            return Math.Sqrt(inner.Value);
        }

        public double Visit(Sin op, Constant inner, Ignore p) {
            return Math.Sin(inner.Value * Math.PI / 180);
        }

        public double Visit(Cos op, Constant inner, Ignore p) {
            return Math.Cos(inner.Value * Math.PI / 180);
        }

        #endregion

        #region Implementation of ISolverModelBinaryOpVisitor<in Constant,in Constant,out double>

        public double Visit(Plus op, Constant lhs, Constant rhs, Ignore p) {
            return lhs.Value + rhs.Value;
        }

        public double Visit(Times op, Constant lhs, Constant rhs, Ignore p) {
            return lhs.Value * rhs.Value;
        }

        public double Visit(Divide op, Constant lhs, Constant rhs, Ignore p) {
            return lhs.Value / rhs.Value;
        }

        #endregion
    }
}
