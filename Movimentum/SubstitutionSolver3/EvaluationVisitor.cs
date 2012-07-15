using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    public class EvaluationVisitor : ISolverModelConstraintVisitor<Ignore, bool>
                                 , ISolverModelExprVisitor<Ignore, double>
                                 , ISolverModelUnaryOpVisitor<double, Ignore, double>
                                 , ISolverModelBinaryOpVisitor<double, Ignore, double> {
        private readonly IDictionary<IVariable, double> _variableValues;
        public EvaluationVisitor(IDictionary<IVariable, double> variableValues) {
            _variableValues = variableValues;
        }

        public EvaluationVisitor(IVariable variable, double value)
            : this(new Dictionary<IVariable, double> { { variable, value } }) {
        }

        // STEPL
        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out bool>

        public bool Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            double result = equalsZero.Expr.Accept(this, p);
            return result.Near(0);
        }

        public bool Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            double result = moreThanZero.Expr.Accept(this, p);
            return result > 0;
        }

        public bool Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            double result = atLeastZero.Expr.Accept(this, p);
            return result.Near(0) | result > 0;
        }

        #endregion
        #region ISolverModelExprVisitor

        public double Visit(IConstant constant, Ignore p) {
            return constant.Value;
        }

        public double Visit(INamedVariable namedVariable, Ignore p) {
            return GetValue(namedVariable);
        }

        public double Visit(IAnchorVariable anchorVariable, Ignore p) {
            return GetValue(anchorVariable);
        }

        private double GetValue(IVariable variable) {
            double result;
            if (!_variableValues.TryGetValue(variable, out result)) {
                throw new NotSupportedException("Variable " + variable + " has no value");
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

        public double VisitSTEPB(IGeneralPolynomialSTEPB polynomial, Ignore parameter) {
            return polynomial.EvaluateAtSTEPC(GetValue(polynomial.Var));
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
