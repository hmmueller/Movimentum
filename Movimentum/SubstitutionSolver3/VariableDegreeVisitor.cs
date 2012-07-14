using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public enum VariableDegree { Zero = 0, One = 1, Two = 2, Other = 3 }

    internal class VariableDegreeVisitor : ISolverModelExprVisitor<Ignore, Dictionary<IVariable, VariableDegree>>
                , ISolverModelBinaryOpVisitor<Dictionary<IVariable, VariableDegree>, Dictionary<IVariable, VariableDegree>>
                , ISolverModelUnaryOpVisitor<Dictionary<IVariable, VariableDegree>, Dictionary<IVariable, VariableDegree>> {

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

        readonly Dictionary<IVariable, VariableDegree> _variableDegree = new Dictionary<IVariable, VariableDegree>();

        public VariableDegree Degree(IVariable v) {
            VariableDegree result;
            _variableDegree.TryGetValue(v, out result);
            return result;
        }

        public IEnumerable<IVariable> Variables() {
            return _variableDegree.Where(kvp => kvp.Value != VariableDegree.Zero).Select(kvp => kvp.Key);
        }

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        private static readonly Dictionary<IVariable, VariableDegree> EMPTY = new Dictionary<IVariable, VariableDegree>();

        public Dictionary<IVariable, VariableDegree> Visit(IConstant constant, Ignore p) {
            return EMPTY;
        }

        public Dictionary<IVariable, VariableDegree> Visit(INamedVariable namedVariable, Ignore p) {
            return new Dictionary<IVariable, VariableDegree> { { namedVariable, VariableDegree.One } };
        }

        public Dictionary<IVariable, VariableDegree> Visit(IAnchorVariable anchorVariable, Ignore p) {
            return new Dictionary<IVariable, VariableDegree> { { anchorVariable, VariableDegree.One } };
        }

        public Dictionary<IVariable, VariableDegree> Visit(UnaryExpression unaryExpression, Ignore p) {
            Dictionary<IVariable, VariableDegree> innerResult = unaryExpression.Inner.Accept(this, Ig.nore);
            return unaryExpression.Op.Accept(this, innerResult, p);
        }

        public Dictionary<IVariable, VariableDegree> Visit(BinaryExpression binaryExpression, Ignore p) {
            Dictionary<IVariable, VariableDegree> lhsResult = binaryExpression.Lhs.Accept(this, Ig.nore);
            Dictionary<IVariable, VariableDegree> rhsResult = binaryExpression.Rhs.Accept(this, Ig.nore);
            return binaryExpression.Op.Accept(this, lhsResult, rhsResult, p);
        }

        public Dictionary<IVariable, VariableDegree> Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
            //AbstractExpr newExpr = rangeExpr.Expr.Accept(this, Ig.nore);
            //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this, Ig.nore);
            //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
            //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
        }

        public Dictionary<IVariable, VariableDegree> VisitSTEPB(IGeneralPolynomialSTEPB polynomial, Ignore parameter) {
            VariableDegree degree;
            switch (polynomial.Degree) {
                case 0:
                    degree = VariableDegree.Zero;
                    break;
                case 1:
                    degree = VariableDegree.One;
                    break;
                case 2:
                    degree = VariableDegree.Two;
                    break;
                default:
                    degree = VariableDegree.Other;
                    break;
            }
            return new Dictionary<IVariable, VariableDegree> { { polynomial.Var, degree } };
        }

        //private Tuple<RangeExpr.Pair, RangeExpr.Pair> VisitPair(RangeExpr.Pair pair) {
        //    AbstractExpr newMoreThan = pair.MoreThan.Accept(this, Ig.nore);
        //    AbstractExpr newValue = pair.Value.Accept(this, Ig.nore);
        //    return pair.MoreThan != newMoreThan || pair.Value != newValue
        //        ? Tuple.Create(pair, new RangeExpr.Pair(newMoreThan, newValue))
        //        : Tuple.Create(pair, pair);
        //}

        #endregion

        #region Implementation of ISolverModelBinaryOpVisitor<in Ignore,in Dictionary<IVariable,VariableDegree>,out Dictionary<IVariable,VariableDegree>>

        private Dictionary<IVariable, VariableDegree> Combine(
                Dictionary<IVariable, VariableDegree> lhsResult,
                Dictionary<IVariable, VariableDegree> rhsResult,
                Func<VariableDegree, VariableDegree> onlyInLhsResult,
                Func<VariableDegree, VariableDegree> onlyInRhsResult,
                Func<VariableDegree, VariableDegree, VariableDegree> inBoth) {
            var result = new Dictionary<IVariable, VariableDegree>();
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

        public Dictionary<IVariable, VariableDegree> Visit(Plus op, Dictionary<IVariable, VariableDegree> lhs, Dictionary<IVariable, VariableDegree> rhs, Ignore p) {
            return Combine(lhs, rhs, d1 => d1, d2 => d2, (d1, d2) => MaxDegree[(int)d1, (int)d2]);
        }

        public Dictionary<IVariable, VariableDegree> Visit(Times op, Dictionary<IVariable, VariableDegree> lhs, Dictionary<IVariable, VariableDegree> rhs, Ignore p) {
            return Combine(lhs, rhs, d1 => d1, d2 => d2, (d1, d2) => PlusDegree[(int)d1, (int)d2]);
        }

        public Dictionary<IVariable, VariableDegree> Visit(Divide op, Dictionary<IVariable, VariableDegree> lhs, Dictionary<IVariable, VariableDegree> rhs, Ignore p) {
            return Combine(lhs, rhs, d1 => d1, d2 => VariableDegree.Other, (d1, d2) => VariableDegree.Other);
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in Ignore,in Dictionary<IVariable,VariableDegree>,out Dictionary<IVariable,VariableDegree>>

        public Dictionary<IVariable, VariableDegree> Visit(UnaryMinus op, Dictionary<IVariable, VariableDegree> inner, Ignore p) {
            return inner;
        }

        public Dictionary<IVariable, VariableDegree> Visit(Square op, Dictionary<IVariable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => TwiceDegree[(int)kvp.Value]);
        }

        public Dictionary<IVariable, VariableDegree> Visit(FormalSquareroot op, Dictionary<IVariable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        public Dictionary<IVariable, VariableDegree> Visit(PositiveSquareroot op, Dictionary<IVariable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        public Dictionary<IVariable, VariableDegree> Visit(Sin op, Dictionary<IVariable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        public Dictionary<IVariable, VariableDegree> Visit(Cos op, Dictionary<IVariable, VariableDegree> inner, Ignore p) {
            return inner.ToDictionary(kvp => kvp.Key, kvp => VariableDegree.Other);
        }

        #endregion
    }
}
