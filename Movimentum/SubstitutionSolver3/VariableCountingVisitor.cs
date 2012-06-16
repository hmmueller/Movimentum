﻿using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    internal class VariableCountingVisitor : ISolverModelExprVisitor<Ignore> {
        readonly Dictionary<Variable,int> _variableOccurrences = new Dictionary<Variable, int>();

        public int Occurrences(Variable v) {
            int result;
            _variableOccurrences.TryGetValue(v, out result);
            return result;
        }

        public IEnumerable<Variable> Variables() {
            return _variableOccurrences.Keys;
        }

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        public Ignore Visit(Constant constant, Ignore p) {
            return p;
        }

        public Ignore Visit(NamedVariable namedVariable, Ignore p) {
            return IncrementCount(namedVariable);
        }

        private Ignore IncrementCount(Variable variable) {
            if (!_variableOccurrences.ContainsKey(variable)) {
                _variableOccurrences.Add(variable, 0);
            }
            _variableOccurrences[variable]++;
            return Ig.nore;
        }

        public Ignore Visit(AnchorVariable anchorVariable, Ignore p) {
            return IncrementCount(anchorVariable);
        }

        public Ignore Visit(UnaryExpression unaryExpression, Ignore p) {
            return unaryExpression.Inner.Accept(this, Ig.nore);
        }

        public Ignore Visit(BinaryExpression binaryExpression, Ignore p) {
            binaryExpression.Lhs.Accept(this, Ig.nore);
            binaryExpression.Rhs.Accept(this, Ig.nore);
            return p;
        }

        public Ignore Visit(RangeExpr rangeExpr, Ignore p) {
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
