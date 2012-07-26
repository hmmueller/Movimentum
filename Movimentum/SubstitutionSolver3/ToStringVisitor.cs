using System.Globalization;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    class ToStringVisitor : ISolverModelConstraintVisitor<Ignore, string>
                , ISolverModelExprVisitor<int, string>
                , ISolverModelBinaryOpVisitor<IAbstractExpr, int, string>
                , ISolverModelUnaryOpVisitor<IAbstractExpr, int, string> {

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

        public string Visit(IConstant constant, int parentPrecedence) {
            return constant.Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Visit(INamedVariable namedVar, int parentPrecedence) {
            return namedVar.Name;
        }

        public string Visit(IAnchorVariable anchorVar, int parentPrecedence) {
            return anchorVar.Name;
        }

        public string Visit(UnaryExpression unaryExpr, int parentPrecedence) {
            return unaryExpr.Op.Accept(this, unaryExpr.Inner, parentPrecedence);
        }

        public string Visit(BinaryExpression binaryExpr, int parentPrecedence) {
            return binaryExpr.Op.Accept(this, binaryExpr.Lhs, binaryExpr.Rhs, parentPrecedence);
        }

        public string Visit(RangeExpr rangeExpr, int parentPrecedence) {
            string result = rangeExpr.Expr.Accept(this, 0);
            result += " : " + rangeExpr.Value0.Accept(this, 0);
            foreach (var pair in rangeExpr.Pairs) {
                result += " > " + VisitRangeExprPair(pair);
            }
            return result;
        }

        public string Visit(IGeneralPolynomial polynomial, int p) {
            var result = new StringBuilder("((");
            string varName = polynomial.Var.Accept(this, p);
            string firstPlus = "";
            for (int d = polynomial.Degree; d > 1; d--) {
                double coefficient = polynomial.Coefficient(d);
                if (!coefficient.Near(0)) {
                    ToStringCoefficient(firstPlus, result, coefficient);
                    result.Append(varName);
                    result.Append('^');
                    result.Append(d);
                    firstPlus = "+";
                }
            }
            if (polynomial.Degree > 0 && !polynomial.Coefficient(1).Near(0)) {
                ToStringCoefficient(firstPlus, result, polynomial.Coefficient(1));
                result.Append(varName);
                firstPlus = "+";
            }
            if (!polynomial.Coefficient(0).Near(0)) {
                var coefficient = polynomial.Coefficient(0);
                if (!(coefficient <= 0)) {
                    // Also NaN gets a +.
                    result.Append(firstPlus);
                }
                result.Append(coefficient.ToString(CultureInfo.InvariantCulture));
            }
            result.Append("))");
            return result.ToString();
        }

        private static void ToStringCoefficient(string firstPlus, StringBuilder result, double coefficient) {
            if (coefficient > 0) {
                result.Append(firstPlus);
                if (!coefficient.Near(1)) {
                    result.Append(coefficient.ToString(CultureInfo.InvariantCulture));
                    result.Append('*');
                }
            } else {
                if (coefficient.Near(-1)) {
                    result.Append('-');
                } else {
                    result.Append(coefficient.ToString(CultureInfo.InvariantCulture));
                    result.Append('*');
                }
            }
        }

        private string VisitRangeExprPair(RangeExpr.Pair pair) {
            return pair.MoreThan.Accept(this, 0) + " : " + pair.Value.Accept(this, 0);
        }

        #endregion Implementation of ISolverModelExprVisitor<int, string>

        #region Implementation of ISolverModelBinaryOpVisitor<in Ignore,in Ignore,out Tuple<string,int>>

        private const int PLUS_PRECEDENCE = 1;
        private const int TIMES_PRECEDENCE = 2;
        private const int UNARY_MINUS_PRECEDENCE = 3;

        private string Visit(IAbstractExpr lhs, IAbstractExpr rhs,
                                int parentPrecedence, string opString, int localPrecedence) {
            string r = lhs.Accept(this, localPrecedence)
                        + opString
                        + rhs.Accept(this, localPrecedence);
            return Parenthesize(parentPrecedence, localPrecedence, r);
        }

        public string Visit(Plus op, IAbstractExpr lhs,
                        IAbstractExpr rhs, int parentPrecedence) {
            return Visit(lhs, rhs, parentPrecedence, "+", PLUS_PRECEDENCE);
        }

        public string Visit(Times op, IAbstractExpr lhs,
                            IAbstractExpr rhs, int parentPrecedence) {
            return Visit(lhs, rhs, parentPrecedence, "*", TIMES_PRECEDENCE);
        }

        public string Visit(Divide op, IAbstractExpr lhs,
                            IAbstractExpr rhs, int parentPrecedence) {
            return Visit(lhs, rhs, parentPrecedence, "/", TIMES_PRECEDENCE);
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in Ignore,in Ignore,out Tuple<string,int>>

        public string Visit(UnaryMinus op, IAbstractExpr inner, int parentPrecedence) {
            string result = "-" + inner.Accept(this, UNARY_MINUS_PRECEDENCE);
            return parentPrecedence > UNARY_MINUS_PRECEDENCE ? "(" + result + ")" : result;
        }

        public string Visit(FormalSquareroot op, IAbstractExpr inner, int parentPrecedence) {
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, "root " + inner.Accept(this, FUNCTION_PRECEDENCE)).Replace("root (", "root(");
        }

        public string Visit(PositiveSquareroot op, IAbstractExpr inner, int parentPrecedence) {
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, "sqrt " + inner.Accept(this, FUNCTION_PRECEDENCE)).Replace("sqrt (", "sqrt(");
        }

        private const int FUNCTION_PRECEDENCE = 4;
        private const int SQUARE_PRECEDENCE = 5;

        private static string Parenthesize(int parentPrecedence, int localPrecedence, string r) {
            return parentPrecedence > localPrecedence ? "(" + r + ")" : r;
        }

        public string Visit(Square op, IAbstractExpr inner, int parentPrecedence) {
            string result = inner.Accept(this, SQUARE_PRECEDENCE) + "²";
            return Parenthesize(parentPrecedence, SQUARE_PRECEDENCE, result);
        }

        public string Visit(Sin op, IAbstractExpr inner, int parentPrecedence) {
            string result = "sin " + inner.Accept(this, FUNCTION_PRECEDENCE);
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, result);
        }

        public string Visit(Cos op, IAbstractExpr inner, int parentPrecedence) {
            return Parenthesize(parentPrecedence, FUNCTION_PRECEDENCE, "cos " + inner.Accept(this, FUNCTION_PRECEDENCE)).Replace("cos (", "cos(");
        }

        #endregion

    }
}
