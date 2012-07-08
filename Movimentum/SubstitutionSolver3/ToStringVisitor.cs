using System;
using System.Globalization;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    class ToStringVisitor : ISolverModelConstraintVisitor<Ignore, string>
                , ISolverModelExprVisitor<int, string>
                , ISolverModelBinaryOpVisitor<AbstractExpr, int, string>
                , ISolverModelUnaryOpVisitor<AbstractExpr, int, string> {

        #region Implementation of ISolverModelConstraintVisitor<in int,out string>

        public string Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            return "0 = " + equalsZero.Expr.Accept(this, 0);
        }

        public string Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            return "0 < " + moreThanZero.Expr.Accept(this, 0);
        }

        public string Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            return "0 <= " + atLeastZero.Expr.Accept(this, 0);
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<int, string>
        public string XXX() {
            throw new NotImplementedException();
        }

        public string Visit(Constant constant, int parentPrecedence) {
            return constant.Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Visit(NamedVariable namedVariable, int parentPrecedence) {
            return namedVariable.Name;
        }

        public string Visit(AnchorVariable anchorVariable, int parentPrecedence) {
            return anchorVariable.Name;
        }

        public string Visit(UnaryExpression unaryExpression, int parentPrecedence) {
            return unaryExpression.Op.Accept(this, unaryExpression.Inner, parentPrecedence);
        }

        public string Visit(BinaryExpression binaryExpression, int parentPrecedence) {
            return binaryExpression.Op.Accept(this, binaryExpression.Lhs, binaryExpression.Rhs, parentPrecedence);
        }

        public string Visit(RangeExpr rangeExpr, int parentPrecedence) {
            string result = rangeExpr.Expr.Accept(this, 0);
            result += " : " + rangeExpr.Value0.Accept(this, 0);
            foreach (var pair in rangeExpr.Pairs) {
                result += " > " + VisitRangeExprPair(pair);
            }
            return result;
        }

        ////public string Visit(SingleVariablePolynomial singleVariablePolynomial, int p) {
        ////    var result = new StringBuilder("[");
        ////    string varName = singleVariablePolynomial.Var.Accept(this, p);
        ////    for (int d = singleVariablePolynomial.Degree; d > 1; d--) {
        ////        double coefficient = singleVariablePolynomial.Coefficient(d);
        ////        if (!coefficient.Near(0)) {
        ////            result.Append(coefficient);
        ////            result.Append(varName);
        ////            result.Append('^');
        ////            result.Append(d);
        ////        }
        ////    }
        ////    if (singleVariablePolynomial.Degree > 0 && !singleVariablePolynomial.Coefficient(1).Near(0)) {
        ////        result.Append(singleVariablePolynomial.Coefficient(1));
        ////        result.Append(varName);
        ////    }
        ////    if (!singleVariablePolynomial.Coefficient(0).Near(0)) {
        ////        result.Append(singleVariablePolynomial.Coefficient(0));
        ////    }
        ////    result.Append("]");
        ////    return result.ToString();
        ////}

        private string VisitRangeExprPair(RangeExpr.Pair pair) {
            return pair.MoreThan.Accept(this, 0) + " : " + pair.Value.Accept(this, 0);
        }

        #endregion Implementation of ISolverModelExprVisitor<int, string>

        #region Implementation of ISolverModelBinaryOpVisitor<in Ignore,in Ignore,out Tuple<string,int>>

        private const int PLUS_PRECEDENCE = 1;
        private const int TIMES_PRECEDENCE = 2;
        private const int UNARY_MINUS_PRECEDENCE = 3;

        private string Visit(AbstractExpr lhs, AbstractExpr rhs,
                                int parentPrecedence, string opString, int localPrecedence) {
            string r = lhs.Accept(this, localPrecedence)
                        + opString
                        + rhs.Accept(this, localPrecedence);
            return Parenthesize(parentPrecedence, localPrecedence, r);
        }

        public string Visit(Plus op, AbstractExpr lhs,
                        AbstractExpr rhs, int parentPrecedence) {
            return Visit(lhs, rhs, parentPrecedence, "+", PLUS_PRECEDENCE);
        }

        public string Visit(Times op, AbstractExpr lhs,
                            AbstractExpr rhs, int parentPrecedence) {
            return Visit(lhs, rhs, parentPrecedence, "*", TIMES_PRECEDENCE);
        }

        public string Visit(Divide op, AbstractExpr lhs,
                            AbstractExpr rhs, int parentPrecedence) {
            return Visit(lhs, rhs, parentPrecedence, "/", TIMES_PRECEDENCE);
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in Ignore,in Ignore,out Tuple<string,int>>

        public string Visit(UnaryMinus op, AbstractExpr inner, int parentPrecedence) {
            string result = "-" + inner.Accept(this, UNARY_MINUS_PRECEDENCE);
            return parentPrecedence > UNARY_MINUS_PRECEDENCE ? "(" + result + ")" : result;
        }

        public string Visit(FormalSquareroot op, AbstractExpr inner, int parentPrecedence) {
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, "root " + inner.Accept(this, FUNCTION_PRECEDENCE)).Replace("root (", "root("); ; ;
        }

        public string Visit(PositiveSquareroot op, AbstractExpr inner, int parentPrecedence) {
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, "sqrt " + inner.Accept(this, FUNCTION_PRECEDENCE)).Replace("sqrt (", "sqrt("); ;
        }

        private const int FUNCTION_PRECEDENCE = 4;
        private const int SQUARE_PRECEDENCE = 5;

        private static string Parenthesize(int parentPrecedence, int localPrecedence, string r) {
            return parentPrecedence > localPrecedence ? "(" + r + ")" : r;
        }

        public string Visit(Square op, AbstractExpr inner, int parentPrecedence) {
            string result = inner.Accept(this, SQUARE_PRECEDENCE) + "²";
            return Parenthesize(parentPrecedence, SQUARE_PRECEDENCE, result);
        }

        public string Visit(Sin op, AbstractExpr inner, int parentPrecedence) {
            string result = "sin " + inner.Accept(this, FUNCTION_PRECEDENCE);
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, result);
        }

        public string Visit(Cos op, AbstractExpr inner, int parentPrecedence) {
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, "cos " + inner.Accept(this, FUNCTION_PRECEDENCE)).Replace("cos (", "cos(");
        }

        #endregion

    }
}
