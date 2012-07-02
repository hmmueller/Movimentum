using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    public class EvaluationVisitor : ISolverModelExprVisitor<Ignore, double>
                                 , ISolverModelUnaryOpVisitor<double, Ignore, double>
                                 , ISolverModelBinaryOpVisitor<double, Ignore, double> {
        private readonly IDictionary<Variable, double> _variableValues;
        public EvaluationVisitor(IDictionary<Variable, double> variableValues) {
            _variableValues = variableValues;
        }

        public EvaluationVisitor(Variable variable, double value) : this(new Dictionary<Variable, double> {{variable, value}}){
        }

        #region ISolverModelExprVisitor

        public double Visit(Constant constant, Ignore p) {
            return constant.Value;
        }

        public double Visit(NamedVariable namedVariable, Ignore p) {
            return GetValue(namedVariable);
        }

        public double Visit(AnchorVariable anchorVariable, Ignore p) {
            return GetValue(anchorVariable);
        }

        private double GetValue(Variable namedVariable) {
            double result;
            if (!_variableValues.TryGetValue(namedVariable, out result)) {
                throw new NotSupportedException("Variable " + namedVariable + " has no value");
            }
            return result;
        }

        public double Visit(UnaryExpression unaryExpression, Ignore p) {
            double inner = unaryExpression.Inner.Accept(this, p);
            return unaryExpression.Op.Accept(this, inner, p);
        }

        public double Visit(BinaryExpression binaryExpression, Ignore p) {
            double lhs = binaryExpression.Lhs.Accept(this, p);
            double rhs = binaryExpression.Rhs.Accept(this, p);
            return binaryExpression.Op.Accept(this, lhs, rhs, p);
        }

        public double Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException("To be implemented when n-ary RangeExpr is reduced to quartary RangeExpr.");
            //double value = rangeExpr.Expr.Accept(this, p);
            //double result = rangeExpr.Value0.Accept(this, p);
            //foreach (var pair in rangeExpr.Pairs) {
            //    double compareValue = pair.MoreThan.Accept(this, p);
            //    if (value <= compareValue) {
            //        break;
            //    }
            //    result = pair.Value.Accept(this, p);
            //}
            //return result;
        }

        #endregion ISolverModelExprVisitor

        #region ISolverModelUnaryOpVisitor

        public double Visit(UnaryMinus op, double inner, Ignore p) {
            return -inner;
        }

        public double Visit(FormalSquareroot op, double inner, Ignore p) {
            if (inner.Near(0)) {
                return 0;
            } else {
                throw new NotSupportedException("Cannot evaluate formal square root of " + inner);
            }
        }

        public double Visit(PositiveSquareroot op, double inner, Ignore p) {
            return Math.Sqrt(inner);
        }

        public double Visit(Square op, double inner, Ignore p) {
            return inner * inner;
        }

        //public double Visit(Integral op, double e, Ignore p) {
        //    throw new NotImplementedException();
        //}

        //public double Visit(Differential op, double e, Ignore p) {
        //    throw new NotImplementedException();
        //}

        public double Visit(Sin op, double inner, Ignore p) {
            return Math.Sin(inner / 180 * Math.PI);
        }

        public double Visit(Cos op, double inner, Ignore p) {
            return Math.Cos(inner / 180 * Math.PI);
        }

        #endregion ISolverModelUnaryOpVisitor

        #region ISolverModelBinaryOpVisitor

        public double Visit(Plus op, double lhs, double rhs, Ignore p) {
            return lhs + rhs;
        }

        public double Visit(Times op, double lhs, double rhs, Ignore p) {
            return lhs * rhs;
        }

        public double Visit(Divide op, double lhs, double rhs, Ignore p) {
            return lhs / rhs;
        }

        #endregion ISolverModelBinaryOpVisitor
    }
}
