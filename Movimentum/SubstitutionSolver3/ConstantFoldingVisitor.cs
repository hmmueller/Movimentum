using System;

namespace Movimentum.SubstitutionSolver3 {
    internal class ConstantFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<AbstractExpr> {
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

        public AbstractConstraint Visit(BacksubstitutionConstraint backsubstitutionConstraint, Ignore p) {
            AbstractExpr result = backsubstitutionConstraint.Expr.Accept(this, Ig.nore);
            return result != backsubstitutionConstraint.Expr ? new BacksubstitutionConstraint(backsubstitutionConstraint.Variable, result) : backsubstitutionConstraint;
        }

        //public AbstractConstraint Visit(VariableEqualsConstantConstraint variableEqualsConstant, Ignore p) {
        //    return variableEqualsConstant;
        //}

        //public AbstractConstraint Visit(VariableInRangeConstraint variableInRange, Ignore p) {
        //    return variableInRange;
        //}

        //public AbstractConstraint Visit(VariableEqualsExpressionConstraint variableEqualsExpression, Ignore p) {
        //    AbstractExpr result = variableEqualsExpression.Expr.Accept(this, Ig.nore);
        //    return result != variableEqualsExpression.Expr ? new VariableEqualsExpressionConstraint(variableEqualsExpression.Variable, result) : variableEqualsExpression;
        //}

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
                return new Constant(unaryExpression.Op.Evaluate(((Constant)newInner).Value));
            } else if (newInner != oldInner) {
                return new UnaryExpression(newInner, unaryExpression.Op);
            } else {
                return unaryExpression;
            }
        }

        private readonly static Constant ZERO = new Constant(0);

        public AbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
            AbstractExpr oldLhs = binaryExpression.Lhs;
            AbstractExpr oldRhs = binaryExpression.Rhs;
            AbstractExpr newLhs = oldLhs.Accept(this, Ig.nore);
            AbstractExpr newRhs = oldRhs.Accept(this, Ig.nore);
            if (binaryExpression.Op is Times && (newLhs.Equals(ZERO) | newRhs.Equals(ZERO))) {
                return ZERO;
            } else if (binaryExpression.Op is Plus && newLhs.Equals(ZERO)) {
                return newRhs;
            } else if (binaryExpression.Op is Plus && newRhs.Equals(ZERO)) {
                return newLhs;
            } else if (newLhs is Constant & newRhs is Constant) {
                return new Constant(binaryExpression.Op.Evaluate(((Constant)newLhs).Value, ((Constant)newRhs).Value));
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
    }
}
