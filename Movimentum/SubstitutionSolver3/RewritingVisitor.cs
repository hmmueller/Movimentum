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
            return Rewrite(constant);
        }

        private AbstractExpr Rewrite(AbstractExpr expr) {
            AbstractExpr result;
            return _rewrites.TryGetValue(expr, out result) ? result : expr;
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
                if (newInner is Constant && !(unaryExpression.Op is FormalSquareroot)) {
                    return new Constant(unaryExpression.Op.Evaluate(((Constant) newInner).Value));
                } else if (newInner is Constant && ((Constant)newInner).Value.Near(0)) {
                    // Here, Op is FormalSquareroot - all others are handled above.
                    return new Constant(0);
                } else if (newInner != oldInner) {
                    // With a FormalSquareroot of a non-zero constant or a non-constant, we arrive here.
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
                if (newLhs is Constant & newRhs is Constant) {
                    return new Constant(binaryExpression.Op.Evaluate(((Constant) newLhs).Value, ((Constant) newRhs).Value));
                } else if (newLhs != oldLhs | newRhs != oldRhs) {
                    return new BinaryExpression(newLhs, binaryExpression.Op, newRhs);
                } else {
                    return binaryExpression;
                }
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
