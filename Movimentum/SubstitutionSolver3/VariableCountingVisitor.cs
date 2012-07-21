using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    //internal class VariableCountingVisitor : ISolverModelExprVisitor<Ignore> {
    //    readonly Dictionary<IVariable, int> _variableOccurrences = new Dictionary<IVariable, int>();

    //    public int Occurrences(IVariable v) {
    //        int result;
    //        _variableOccurrences.TryGetValue(v, out result);
    //        return result;
    //    }

    //    public IEnumerable<IVariable> Variables() {
    //        return _variableOccurrences.Keys;
    //    }

    //    #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

    //    public Ignore Visit(IConstant constant, Ignore p) {
    //        return p;
    //    }

    //    public Ignore Visit(INamedVariable namedVariable, Ignore p) {
    //        return IncrementCount(namedVariable);
    //    }

    //    private Ignore IncrementCount(IVariable variable) {
    //        if (!_variableOccurrences.ContainsKey(variable)) {
    //            _variableOccurrences.Add(variable, 0);
    //        }
    //        _variableOccurrences[variable]++;
    //        return Ig.nore;
    //    }

    //    public Ignore Visit(IAnchorVariable anchorVariable, Ignore p) {
    //        return IncrementCount(anchorVariable);
    //    }

    //    public Ignore Visit(UnaryExpression unaryExpression, Ignore p) {
    //        return unaryExpression.Inner.Accept(this);
    //    }

    //    public Ignore Visit(BinaryExpression binaryExpression, Ignore p) {
    //        binaryExpression.Lhs.Accept(this);
    //        binaryExpression.Rhs.Accept(this);
    //        return p;
    //    }

    //    public Ignore Visit(RangeExpr rangeExpr, Ignore p) {
    //        throw new NotImplementedException();
    //        //AbstractExpr newExpr = rangeExpr.Expr.Accept(this);
    //        //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this);
    //        //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
    //        //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
    //    }

    //    public Ignore Visit(IGeneralPolynomial polynomial, Ignore p) {
    //        return polynomial.Var.Accept(this, p);
    //    }

    //    ////public Ignore Visit(SingleVariablePolynomial singleVariablePolynomial, Ignore p) {
    //    ////    throw new NotImplementedException();
    //    ////}

    //    //private Tuple<RangeExpr.Pair, RangeExpr.Pair> VisitPair(RangeExpr.Pair pair) {
    //    //    AbstractExpr newMoreThan = pair.MoreThan.Accept(this);
    //    //    AbstractExpr newValue = pair.Value.Accept(this);
    //    //    return pair.MoreThan != newMoreThan || pair.Value != newValue
    //    //        ? Tuple.Create(pair, new RangeExpr.Pair(newMoreThan, newValue))
    //    //        : Tuple.Create(pair, pair);
    //    //}

    //    #endregion
    //}
}
