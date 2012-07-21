using System.Globalization;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    class SimpleToStringVisitor : ISolverModelConstraintVisitor<Ignore, string>
                                , ISolverModelExprVisitor<Ignore, string>
                                , ISolverModelBinaryOpVisitor<Ignore, string>
                                , ISolverModelUnaryOpVisitor<Ignore, string> {

        #region Implementation of ISolverModelConstraintVisitor<in int,out string>

        public string Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            return "0 = " + equalsZero.Expr.Accept(this);
        }

        public string Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            return "0 < " + moreThanZero.Expr.Accept(this);
        }

        public string Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            return "0 <= " + atLeastZero.Expr.Accept(this);
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<int, string>

        public string Visit(IConstant constant, Ignore p) {
            return constant.Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Visit(INamedVariable namedVariable, Ignore p) {
            return namedVariable.Name;
        }

        public string Visit(IAnchorVariable anchorVariable, Ignore p) {
            return anchorVariable.Name;
        }

        public string Visit(UnaryExpression unaryExpression, Ignore p) {
            string op = unaryExpression.Op.Accept(this, Ig.nore, Ig.nore);
            return op + "(" + unaryExpression.Inner.Accept(this) + ")";
        }

        public string Visit(BinaryExpression binaryExpression, Ignore p) {
            string op = binaryExpression.Op.Accept(this, Ig.nore, Ig.nore, Ig.nore);
            string result = binaryExpression.Lhs.Accept(this)
                          + op
                          + binaryExpression.Rhs.Accept(this);
            return "(" + result + ")";
        }

        public string Visit(RangeExpr rangeExpr, Ignore p) {
            string result = rangeExpr.Expr.Accept(this);
            result += " : " + rangeExpr.Value0.Accept(this);
            foreach (var pair in rangeExpr.Pairs) {
                result += " > " + VisitRangeExprPair(pair);
            }
            return result;
        }

        public string Visit(IGeneralPolynomial polynomial, Ignore p) {
            var result = new StringBuilder("[");
            string varName = polynomial.Var.Accept(this);
            for (int d = polynomial.Degree; d > 1; d--) {
                double coefficient = polynomial.Coefficient(d);
                if (!coefficient.Near(0)) {
                    result.Append(coefficient);
                    result.Append(varName);
                    result.Append('^');
                    result.Append(d);
                }
            }
            if (polynomial.Degree > 0 && !polynomial.Coefficient(1).Near(0)) {
                result.Append(polynomial.Coefficient(1));
                result.Append(varName);
            }
            if (!polynomial.Coefficient(0).Near(0)) {
                result.Append(polynomial.Coefficient(0));
            }
            result.Append("]");
            return result.ToString();
        }

        private string VisitRangeExprPair(RangeExpr.Pair pair) {
            return pair.MoreThan.Accept(this) + " : " + pair.Value.Accept(this);
        }

        #endregion Implementation of ISolverModelExprVisitor<int, string>

        #region Implementation of ISolverModelBinaryOpVisitor<in Ignore,in Ignore,out Tuple<string,int>>

        public string Visit(Plus op, Ignore lhs, Ignore rhs, Ignore p) {
            return "+";
        }
        // etc.

        public string Visit(Times op, Ignore lhs, Ignore rhs, Ignore p) {
            return "*";
        }

        public string Visit(Divide op, Ignore lhs, Ignore rhs, Ignore p) {
            return "/";
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in Ignore,in Ignore,out Tuple<string,int>>

        public string Visit(UnaryMinus op, Ignore inner, Ignore p) {
            return "-";
        }

        public string Visit(FormalSquareroot op, Ignore inner, Ignore p) {
            return "root";
        }

        public string Visit(PositiveSquareroot op, Ignore inner, Ignore p) {
            return "sqrt";
        }

        public string Visit(Square op, Ignore inner, Ignore p) {
            return "square";
        }

        public string Visit(Sin op, Ignore inner, Ignore p) {
            return "sin";
        }

        public string Visit(Cos op, Ignore inner, Ignore p) {
            return "cos";
        }

        #endregion

    }
}
