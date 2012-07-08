using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public enum VariableDegree { Zero = 0, One = 1, Two = 2, Other = 3 }

    internal class VariableDegreeVisitor : ISolverModelExprVisitor<Ignore, Dictionary<Variable, VariableDegree>>
                , ISolverModelBinaryOpVisitor<Dictionary<Variable, VariableDegree>, Dictionary<Variable, VariableDegree>>
                , ISolverModelUnaryOpVisitor<Dictionary<Variable, VariableDegree>, Dictionary<Variable, VariableDegree>> {

        private static readonly VariableDegree[,] MaxDegree = new[,] {
                                                        {VariableDegree.Zero, VariableDegree.One, VariableDegree.Two,VariableDegree.Other}, 
                                                        {VariableDegree.One, VariableDegree.One, VariableDegree.Two,VariableDegree.Other}, 
                                                        {VariableDegree.Two, VariableDegree.Two, VariableDegree.Two,VariableDegree.Other}, 
                                                        {VariableDegree.Other, VariableDegree.Other, VariableDegree.Other, VariableDegree.Other}, 
                                                    };

        private static readonly VariableDegree[,] PlusDegree = new[,] {
                                                        {VariableDegree.Zero, VariableDegree.One, VariableDegree.Two,VariableDegree.Other}, 
                                                        {VariableDegree.One, VariableDegree.Two, VariableDegree.Other,VariableDegree.Other}, 
                                                        {VariableDegree.Two, VariableDegree.Other, VariableDegree.Other,VariableDegree.Other}, 
                                                        {VariableDegree.Other, VariableDegree.Other, VariableDegree.Other, VariableDegree.Other}, 
                                                    };

        private static readonly VariableDegree[] TwiceDegree = new[] { VariableDegree.Zero, VariableDegree.Two, VariableDegree.Other, VariableDegree.Other };

        readonly Dictionary<Variable, VariableDegree> _variableDegree = new Dictionary<Variable, VariableDegree>();

        public VariableDegree Degree(Variable v) {
            VariableDegree result;
            _variableDegree.TryGetValue(v, out result);
            return result;
        }

        public IEnumerable<Variable> Variables() {
            return _variableDegree.Where(kvp => kvp.Value != VariableDegree.Zero).Select(kvp => kvp.Key);
        }

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        private static readonly Dictionary<Variable, VariableDegree> EMPTY = new Dictionary<Variable, VariableDegree>();

        public Dictionary<Variable, VariableDegree> Visit(Constant constant, Ignore p) {
            return EMPTY;
        }

        public Dictionary<Variable, VariableDegree> Visit(NamedVariable namedVariable, Ignore p) {
            return new Dictionary<Variable, VariableDegree> { { namedVariable, VariableDegree.One } };
        }

        public Dictionary<Variable, VariableDegree> Visit(AnchorVariable anchorVariable, Ignore p) {
            return new Dictionary<Variable, VariableDegree> { { anchorVariable, VariableDegree.One } };
        }

        public Dictionary<Variable, VariableDegree> Visit(UnaryExpression unaryExpression, Ignore p) {
            Dictionary<Variable, VariableDegree> innerResult = unaryExpression.Inner.Accept(this, Ig.nore);
            return unaryExpression.Op.Accept(this, innerResult, p);
        }

        public Dictionary<Variable, VariableDegree> Visit(BinaryExpression binaryExpression, Ignore p) {
            Dictionary<Variable, VariableDegree> lhsResult = binaryExpression.Lhs.Accept(this, Ig.nore);
            Dictionary<Variable, VariableDegree> rhsResult = binaryExpression.Rhs.Accept(this, Ig.nore);
            return binaryExpression.Op.Accept(this, lhsResult, rhsResult, p);
        }

        public Dictionary<Variable, VariableDegree> Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
            //AbstractExpr newExpr = rangeExpr.Expr.Accept(this, Ig.nore);
            //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this, Ig.nore);
            //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
            //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
        }

        ////public Dictionary<Variable, VariableDegree> Visit(SingleVariablePolynomial singleVariablePolynomial, Ignore p) {
        ////    VariableDegree degree;
        ////    switch (singleVariablePolynomial.Degree) {
        ////        case 0:
        ////            degree = VariableDegree.Zero;
        ////            break;
        ////        case 1:
        ////            degree = VariableDegree.One;
        ////            break;
        ////        case 2:
        ////            degree = VariableDegree.Two;
        ////            break;
        ////        default:
        ////            degree = VariableDegree.Other;
        ////            break;
        ////    }
        ////    return new Dictionary<Variable, VariableDegree> { { singleVariablePolynomial.Var, degree } };
        ////}

        //private Tuple<RangeExpr.Pair, RangeExpr.Pair> VisitPair(RangeExpr.Pair pair) {
        //    AbstractExpr newMoreThan = pair.MoreThan.Accept(this, Ig.nore);
        //    AbstractExpr newValue = pair.Value.Accept(this, Ig.nore);
        //    return pair.MoreThan != newMoreThan || pair.Value != newValue
        //        ? Tuple.Create(pair, new RangeExpr.Pair(newMoreThan, newValue))
        //        : Tuple.Create(pair, pair);
        //}

        #endregion

        #region Implementation of ISolverModelBinaryOpVisitor<in Ignore,in Dictionary<Variable,VariableDegree>,out Dictionary<Variable,VariableDegree>>

        private Dictionary<Variable, VariableDegree> Combine(
                Dictionary<Variable, VariableDegree> lhsResult,
                Dictionary<Variable, VariableDegree> rhsResult,
                Func<VariableDegree, VariableDegree> onlyInLhsResult,
                Func<VariableDegree, VariableDegree> onlyInRhsResult,
                Func<VariableDegree, VariableDegree, VariableDegree> inBoth) {
            var result = new Dictionary<Variable, VariableDegree>();
            foreach (var kvp in lhsResult) {
                VariableDegree r;
                result.Add(kvp.Key, rhsResult.TryGetValue(kvp.Key, out r) ? inBoth(kvp.Value, r) : onlyInLhsResult(kvp.Value));
            }
            foreach (var kvp in rhsResult) {
                if (!lhsResult.ContainsKey(kvp.Key)) {
                    result.Add(kvp.Key, onlyInRhsResult(kvp.Value));
                }
            }
            return result;
        }

        public Dictionary<Variable, VariableDegree> Visit(Plus op, Dictionary<Variable, VariableDegree> lhs, Dictionary<Variable, VariableDegree> rhs, Ignore p) {
            return Combine(lhs, rhs, d1 => d1, d2 => d2, (d1, d2) => MaxDegree[(int)d1, (int)d2]);
        }

        public Dictionary<Variable, VariableDegree> Visit(Times op, Dictionary<Variable, VariableDegree> lhs, Dictionary<Variable, VariableDegree> rhs, Ignore p) {
            return Combine(lhs, rhs, d1 => d1, d2 => d2, (d1, d2) => PlusDegree[(int)d1, (int)d2]);
        }

        public Dictionary<Variable, VariableDegree> Visit(Divide op, Dictionary<Variable, VariableDegree> lhs, Dictionary<Variable, VariableDegree> rhs, Ignore p) {
            return Combine(lhs, rhs, d1 => d1, d2 => VariableDegree.Other, (d1, d2) => VariableDegree.Other);
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in Ignore,in Dictionary<Variable,VariableDegree>,out Dictionary<Variable,VariableDegree>>

        public Dictionary<Variable, VariableDegree> Visit(UnaryMinus op, Dictionary<Variable, VariableDegree> inner, Ignore p) {
            return inner;
        }

        public Dictionary<Variable, VariableDegree> Visit(Square op, Dictionary<Variable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => TwiceDegree[(int)kvp.Value]);
        }

        public Dictionary<Variable, VariableDegree> Visit(FormalSquareroot op, Dictionary<Variable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        public Dictionary<Variable, VariableDegree> Visit(PositiveSquareroot op, Dictionary<Variable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        public Dictionary<Variable, VariableDegree> Visit(Sin op, Dictionary<Variable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        public Dictionary<Variable, VariableDegree> Visit(Cos op, Dictionary<Variable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        #endregion
    }
}
