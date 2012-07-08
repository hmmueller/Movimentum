using System.Globalization;
using System.Text;

namespace Movimentum.SubstitutionSolver3 {
    class SimpleToStringVisitor : ISolverModelConstraintVisitor<Ignore, string>
                                , ISolverModelExprVisitor<Ignore, string>
                                , ISolverModelBinaryOpVisitor<Ignore, string>
                                , ISolverModelUnaryOpVisitor<Ignore, string> {

        #region Implementation of ISolverModelConstraintVisitor<in int,out string>

        public string Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            return "0 = " + equalsZero.Expr.Accept(this, p);
        }

        public string Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            return "0 < " + moreThanZero.Expr.Accept(this, p);
        }

        public string Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            return "0 <= " + atLeastZero.Expr.Accept(this, p);
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<int, string>

        public string Visit(Constant constant, Ignore p) {
            return constant.Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Visit(NamedVariable namedVariable, Ignore p) {
            return namedVariable.Name;
        }

        public string Visit(AnchorVariable anchorVariable, Ignore p) {
            return anchorVariable.Name;
        }

        public string Visit(UnaryExpression unaryExpression, Ignore p) {
            string op = unaryExpression.Op.Accept(this, Ig.nore, Ig.nore);
            return op + "(" + unaryExpression.Inner.Accept(this, p) + ")";
        }

        public string Visit(BinaryExpression binaryExpression, Ignore p) {
            string op = binaryExpression.Op.Accept(this, Ig.nore, Ig.nore, Ig.nore);
            string result = binaryExpression.Lhs.Accept(this, p)
                          + op
                          + binaryExpression.Rhs.Accept(this, p);
            return "(" + result + ")";
        }

        public string Visit(RangeExpr rangeExpr, Ignore p) {
            string result = rangeExpr.Expr.Accept(this, p);
            result += " : " + rangeExpr.Value0.Accept(this, p);
            foreach (var pair in rangeExpr.Pairs) {
                result += " > " + VisitRangeExprPair(pair);
            }
            return result;
        }

        ////public string Visit(SingleVariablePolynomial singleVariablePolynomial, Ignore p) {
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
            return pair.MoreThan.Accept(this, Ig.nore) + " : " + pair.Value.Accept(this, Ig.nore);
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
