//using System;
//using System.Collections.Concurrent;
//using System.Linq;

//namespace Movimentum.SubstitutionSolver3 {
//    class PolynomialFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
//                                          , ISolverModelExprVisitor<AbstractExpr>
//                                          , ISolverModelUnaryOpVisitor<ISingleVariablePolynomial, Ignore, AbstractExpr>
//                                          , ISolverModelBinaryOpVisitor<ISingleVariablePolynomial, Ignore, AbstractExpr> {
//        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out ScalarConstraint>

//        public AbstractConstraint Visit(EqualsZeroConstraint equalsZero, Ignore p) {
//            AbstractExpr result = equalsZero.Expr.Accept(this, Ig.nore);
//            return result != equalsZero.Expr ? new EqualsZeroConstraint(result) : equalsZero;
//        }

//        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
//            AbstractExpr result = moreThanZero.Expr.Accept(this, Ig.nore);
//            return result != moreThanZero.Expr ? new MoreThanZeroConstraint(result) : moreThanZero;
//        }

//        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
//            AbstractExpr result = atLeastZero.Expr.Accept(this, Ig.nore);
//            return result != atLeastZero.Expr ? new AtLeastZeroConstraint(result) : atLeastZero;
//        }

//        #endregion

//        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

//        public AbstractExpr Visit(Constant constant, Ignore p) {
//            return constant;
//        }

//        public AbstractExpr Visit(NamedVariable namedVariable, Ignore p) {
//            return namedVariable;
//        }

//        public AbstractExpr Visit(AnchorVariable anchorVariable, Ignore p) {
//            return anchorVariable;
//        }

//        public AbstractExpr Visit(UnaryExpression unaryExpression, Ignore p) {
//            AbstractExpr oldInner = unaryExpression.Inner;
//            AbstractExpr newInner = oldInner.Accept(this, Ig.nore);
//            if (newInner is ISingleVariablePolynomial && !(unaryExpression.Op is FormalSquareroot)) {
//                return unaryExpression.Op.Accept(this, (ISingleVariablePolynomial)newInner, Ig.nore);
//            } else if (newInner != oldInner) {
//                return new UnaryExpression(newInner, unaryExpression.Op);
//            } else {
//                return unaryExpression;
//            }
//        }

//        public AbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
//            AbstractExpr oldLhs = binaryExpression.Lhs;
//            AbstractExpr oldRhs = binaryExpression.Rhs;
//            AbstractExpr newLhs = oldLhs.Accept(this, Ig.nore);
//            AbstractExpr newRhs = oldRhs.Accept(this, Ig.nore);
//            if (newLhs is ISingleVariablePolynomial & newRhs is ISingleVariablePolynomial) {
//                return binaryExpression.Op.Accept(this, (ISingleVariablePolynomial)newLhs, (ISingleVariablePolynomial)newRhs, Ig.nore);
//            } else if (newLhs != oldLhs | newRhs != oldRhs) {
//                return new BinaryExpression(newLhs, binaryExpression.Op, newRhs);
//            } else {
//                return binaryExpression;
//            }
//        }

//        public AbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
//            throw new NotImplementedException();
//            //AbstractExpr newExpr = rangeExpr.Expr.Accept(this, Ig.nore);
//            //AbstractExpr newValue0 = rangeExpr.Value0.Accept(this, Ig.nore);
//            //IEnumerable<Tuple<RangeExpr.Pair, RangeExpr.Pair>> newPairs = rangeExpr.Pairs.Select(pair => VisitPair(pair));
//            //return MaybeCreateConstant(new RangeExpr(newExpr, newValue0, newPairs.Select(tuple => tuple.Item2)));
//        }

//        public AbstractExpr Visit(SingleVariablePolynomial singleVariablePolynomial, Ignore p) {
//            return singleVariablePolynomial;
//        }

//        //private Tuple<RangeExpr.Pair, RangeExpr.Pair> VisitPair(RangeExpr.Pair pair) {
//        //    AbstractExpr newMoreThan = pair.MoreThan.Accept(this, Ig.nore);
//        //    AbstractExpr newValue = pair.Value.Accept(this, Ig.nore);
//        //    return pair.MoreThan != newMoreThan || pair.Value != newValue
//        //        ? Tuple.Create(pair, new RangeExpr.Pair(newMoreThan, newValue))
//        //        : Tuple.Create(pair, pair);
//        //}

//        #endregion

//        #region Implementation of ISolverModelUnaryOpVisitor<in ISingleVariablePolynomial,in Ignore,out AbstractExpr>

//        public AbstractExpr Visit(UnaryMinus op, ISingleVariablePolynomial inner, Ignore p) {
//            return new SingleVariablePolynomial(inner.Var, inner.Coefficients.Select(c => -c));
//        }

//        public AbstractExpr Visit(Square op, ISingleVariablePolynomial inner, Ignore p) {
//            return ((AbstractExpr)inner * (AbstractExpr)inner).Accept(this, p);
//        }

//        public AbstractExpr Visit(FormalSquareroot op, ISingleVariablePolynomial inner, Ignore p) {
//            return new UnaryExpression((AbstractExpr)inner, new FormalSquareroot());
//        }

//        public AbstractExpr Visit(PositiveSquareroot op, ISingleVariablePolynomial inner, Ignore p) {
//            return new UnaryExpression((AbstractExpr)inner, new PositiveSquareroot());
//        }

//        public AbstractExpr Visit(Sin op, ISingleVariablePolynomial inner, Ignore p) {
//            return new UnaryExpression((AbstractExpr)inner, new Sin());
//        }

//        public AbstractExpr Visit(Cos op, ISingleVariablePolynomial inner, Ignore p) {
//            return new UnaryExpression((AbstractExpr)inner, new Cos());
//        }

//        #endregion Implementation of ISolverModelUnaryOpVisitor<in ISingleVariablePolynomial,in Ignore,out AbstractExpr>

//        #region Implementation of ISolverModelBinaryOpVisitor<in ISingleVariablePolynomial,in ISingleVariablePolynomial,in Ignore,out AbstractExpr>

//        public AbstractExpr Visit(Plus op, ISingleVariablePolynomial lhs, ISingleVariablePolynomial rhs, Ignore p) { // .....
//            if (lhs.Var.Equals(rhs.Var)) {
//                var deg = Math.Max(lhs.Degree, rhs.Degree);
//                double[] lhsCoeff = lhs.Coefficients.Concat(new double[deg - lhs.Degree + 5]).ToArray();
//                double[] rhsCoeff = rhs.Coefficients.Concat(new double[deg - rhs.Degree + 5]).ToArray();
//                var coeffs = new double[deg + 4];
//                for (int i = 0; i <= deg; i++) {
//                    coeffs[i] = lhsCoeff[i] + rhsCoeff[i];
//                }
//                // NO###################################: Constant, Variable, and GeneralPolynomial are Derived from abstract SingleVariablePolynomial; AND ARE CREATED BY Factofy method!!!
//                return new SingleVariablePolynomial(lhs.Var, coeffs);
//            } else {
//                return new BinaryExpression((AbstractExpr)lhs, new Plus(), (AbstractExpr)rhs);
//            }
//        }

//        public AbstractExpr Visit(Times op, ISingleVariablePolynomial lhs, ISingleVariablePolynomial rhs, Ignore p) { // ....
//            if (lhs.Var.Equals(rhs.Var)) {
//                var deg = lhs.Degree + rhs.Degree;
//                var coeffs = new double[deg + 1];
//                for (int ld = 0; ld <= lhs.Degree; ld++) {
//                    for (int rd = 0; rd <= rhs.Degree; rd++) {
//                        coeffs[ld + rd] += lhs.Coefficients.ElementAt(ld) * rhs.Coefficients.ElementAt(rd);
//                    }
//                }
//                return new SingleVariablePolynomial(lhs.Var, coeffs);
//            } else {
//                return new BinaryExpression((AbstractExpr)lhs, new Times(), (AbstractExpr)rhs);
//            }
//        }

//        public AbstractExpr Visit(Divide op, ISingleVariablePolynomial lhs, ISingleVariablePolynomial rhs, Ignore p) { // ......
//            // Try polynomial division. Create polynomial; or polynomial + remainder/rhs.
//            var rConst = rhs as Constant;
//            if (rConst != null && !rConst.Value.Near(0)) {
//                return ((AbstractExpr)lhs * new Constant(1 / rConst.Value)).Accept(this, p);
//            } else {
//                return (AbstractExpr)lhs / (AbstractExpr)rhs;
//            }
//        }

//        #endregion Implementation of ISolverModelBinaryOpVisitor<in ISingleVariablePolynomial,in ISingleVariablePolynomial,in Ignore,out AbstractExpr>
//    }
//}
