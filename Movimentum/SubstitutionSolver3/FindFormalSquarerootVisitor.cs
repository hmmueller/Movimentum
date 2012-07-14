using System;

namespace Movimentum.SubstitutionSolver3 {
    internal class FindFormalSquarerootVisitor : ISolverModelExprVisitor<FindFormalSquarerootVisitor> {
        private UnaryExpression _someFormalSquareRoot;

        public UnaryExpression SomeFormalSquareroot {
            get { return _someFormalSquareRoot; }
        }

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        public FindFormalSquarerootVisitor Visit(IConstant constant, Ignore p) {
            return this;
        }

        public FindFormalSquarerootVisitor Visit(INamedVariable namedVariable, Ignore p) {
            return this;
        }

        public FindFormalSquarerootVisitor Visit(IAnchorVariable anchorVariable, Ignore p) {
            return this;
        }

        public FindFormalSquarerootVisitor Visit(UnaryExpression unaryExpression, Ignore p) {
            if (unaryExpression.Op is FormalSquareroot) {
                _someFormalSquareRoot = unaryExpression;
            } else {
                unaryExpression.Inner.Accept(this, Ig.nore);
            }
            return this;
        }

        public FindFormalSquarerootVisitor Visit(BinaryExpression binaryExpression, Ignore p) {
            binaryExpression.Lhs.Accept(this, Ig.nore);
            binaryExpression.Rhs.Accept(this, Ig.nore);
            return this;
        }

        public FindFormalSquarerootVisitor Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
            //AbstractExpr newExpr = rangeExpr.Expr.Accept(this, Ig.nore);
            //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this, Ig.nore);
            //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
            //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
        }

        public FindFormalSquarerootVisitor VisitSTEPB(IGeneralPolynomialSTEPB polynomial, Ignore parameter) {
            return this;
        }

        ////public FindFormalSquarerootVisitor Visit(SingleVariablePolynomial singleVariablePolynomial, Ignore p) {
        ////    return this;
        ////}

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
