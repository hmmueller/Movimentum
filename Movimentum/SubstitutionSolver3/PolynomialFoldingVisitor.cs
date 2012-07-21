using System;
using System.Diagnostics;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    class PolynomialFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<int, IAbstractExpr>
                                          , ISolverModelUnaryOpVisitor<IPolynomial, int, IAbstractExpr>
                                          , ISolverModelBinaryOpVisitor<IPolynomial, int, IAbstractExpr> {
        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out ScalarConstraint>

        public AbstractConstraint Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            IAbstractExpr result = equalsZero.Expr.Accept(this, 0);
            return result != equalsZero.Expr ? new EqualsZeroConstraint(result) : equalsZero;
        }

        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            IAbstractExpr result = moreThanZero.Expr.Accept(this, 0);
            return result != moreThanZero.Expr ? new MoreThanZeroConstraint(result) : moreThanZero;
        }

        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            IAbstractExpr result = atLeastZero.Expr.Accept(this, 0);
            return result != atLeastZero.Expr ? new AtLeastZeroConstraint(result) : atLeastZero;
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(IConstant constant, int depth) {
            return constant;
        }

        public IAbstractExpr Visit(INamedVariable namedVariable, int depth) {
            return namedVariable;
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVariable, int depth) {
            return anchorVariable;
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpression, int depth) {
            DebugCheckDepth(unaryExpression, depth);
            IAbstractExpr oldInner = unaryExpression.Inner;
            IAbstractExpr newInner = oldInner.Accept(this, depth + 1);
            if (newInner is IPolynomial && !(unaryExpression.Op is FormalSquareroot)) {
                return unaryExpression.Op.Accept(this, (IPolynomial)newInner, depth + 1);
            } else if (newInner != oldInner) {
                return NormalizeStepD(new UnaryExpression(newInner, unaryExpression.Op), depth);
            } else {
                return NormalizeStepD(unaryExpression, depth);
            }
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpression, int depth) {
            DebugCheckDepth(binaryExpression, depth);
            IAbstractExpr oldLhs = binaryExpression.Lhs;
            IAbstractExpr oldRhs = binaryExpression.Rhs;
            IAbstractExpr newLhs = oldLhs.Accept(this, depth + 1);
            IAbstractExpr newRhs = oldRhs.Accept(this, depth + 1);
            if (newLhs is IPolynomial & newRhs is IPolynomial) {
                return binaryExpression.Op.Accept(this, (IPolynomial)newLhs, (IPolynomial)newRhs, depth + 1);
            } else if (newLhs != oldLhs | newRhs != oldRhs) {
                return NormalizeStepD(new BinaryExpression(newLhs, binaryExpression.Op, newRhs), depth);
            } else {
                return NormalizeStepD(binaryExpression, depth);
            }
        }

        private static void DebugCheckDepth(IAbstractExpr expr, int depth) {
            if (depth > 200) {
                Debug.WriteLine("Polynomial folding reached depth 2000 with " + expr);
            }
        }

        private static readonly TypeMatchTemplate<Polynomial> _poly1 = new TypeMatchTemplate<Polynomial>();
        private static readonly TypeMatchTemplate<Polynomial> _poly2 = new TypeMatchTemplate<Polynomial>();
        private static readonly TypeMatchTemplate<IAbstractExpr> _e1 = new TypeMatchTemplate<IAbstractExpr>();
        private static readonly TypeMatchTemplate<IAbstractExpr> _e2 = new TypeMatchTemplate<IAbstractExpr>();
        private static readonly TypeMatchTemplate<IConstant> _c = new TypeMatchTemplate<IConstant>();
        private static readonly TypeMatchTemplate<UnaryExpression> _ue = new TypeMatchTemplate<UnaryExpression>();
        private static readonly BinaryExpressionTemplate _tBothPlus = (_poly1 + _e1) + (_poly2 + _e2);
        private static readonly BinaryExpressionTemplate _tLeftPlus = (_poly1 + _e1) + _poly2;
        private static readonly BinaryExpressionTemplate _tLeftPlus2 = (_poly1 + _e1) + _e2;
        private static readonly BinaryExpressionTemplate _tRightPlus = _poly1 + (_poly2 + _e2);
        private static readonly BinaryExpressionTemplate _tRightPlus2 = _e1 + (_poly2 + _e2);
        private static readonly UnaryExpressionTemplate _tBelowMinus = -(_poly1 + _e1);
        // STEPF
        private static readonly UnaryExpressionTemplate _tMinusMinus = -(-_e1);
        // STEPG
        private static readonly BinaryExpressionTemplate _tDevourConstant1 = (_poly1 + _e1) * _c;
        private static readonly BinaryExpressionTemplate _tDevourConstant2 = (_poly1 + _e1) / _c;
        // STEPH
        private static readonly BinaryExpressionTemplate _tRightConstant1 = _e1 * _c;
        private static readonly BinaryExpressionTemplate _tRightConstant2 = _e1 / _c;
        // STEPI
        //private static readonly BinaryExpressionTemplate _tSqrt1 = _poly1 * _ue;
        //private static readonly BinaryExpressionTemplate _tSqrt2 = _ue * _poly1;
        //private static readonly BinaryExpressionTemplate _tSqrt3 = _ue / _poly1;
        private static readonly BinaryExpressionTemplate _tSqrt1 = _c * _ue;
        private static readonly BinaryExpressionTemplate _tSqrt2 = _ue * _c;
        private static readonly BinaryExpressionTemplate _tSqrt3 = _ue / _c;
        // STEPJ
        private static readonly BinaryExpressionTemplate _tBothTimes = (_poly1 * _e1) * (_poly2 * _e2);
        private static readonly BinaryExpressionTemplate _tRightTimes = _poly1 * (_poly2 * _e2);
        private static readonly BinaryExpressionTemplate _tLeftTimes = (_poly1 * _e1) * _poly2;
        // STEPK
        private static readonly BinaryExpressionTemplate _tPolynomialTimesMinus1 = _poly1 * _ue;
        private static readonly BinaryExpressionTemplate _tPolynomialTimesMinus2 = _ue * _poly1;

        private IAbstractExpr NormalizeStepD(UnaryExpression unaryExpression, int depth) {
            {
                ExpressionMatcher matcher = _tMinusMinus.CreateMatcher();
                if (matcher.TryMatch(unaryExpression)) {
                    // --Z --> Z
                    IAbstractExpr result = matcher.Match(_e1);
                    return Simplify(result, depth + 1);
                }
            }
            {
                // STEPF
                ExpressionMatcher matcher = _tBelowMinus.CreateMatcher();
                if (matcher.TryMatch(unaryExpression)) {
                    // -(P + Z) --> -P + (-Z)
                    IAbstractExpr result = (-matcher.Match(_poly1)).C + -matcher.Match(_e1).C;
                    return Simplify(result, depth + 1);
                }
            }
            return unaryExpression;
        }

        private IAbstractExpr NormalizeStepD(BinaryExpression binaryExpression, int depth) {
            {
                ExpressionMatcher matcher = _tBothPlus.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P[V] + Z) + (P'[V] + Z') --> P" + (Z + Z')
                    Polynomial poly1 = matcher.Match(_poly1);
                    Polynomial poly2 = matcher.Match(_poly2);
                    if (poly1.Var.Equals(poly2.Var) | poly1 is IConstant | poly2 is IConstant) {
                        IAbstractExpr result = (poly1 + poly2).C
                                                  + (matcher.Match(_e1).C + matcher.Match(_e2));
                        return Simplify(result, depth);
                    }
                }
            }
            {
                ExpressionMatcher matcher = _tLeftPlus.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P[V] + Z) + P'[V] --> P"[V] + Z, where P" = P+P' simplified
                    Polynomial poly1 = matcher.Match(_poly1);
                    Polynomial poly2 = matcher.Match(_poly2);
                    if (poly1.Var.Equals(poly2.Var) | poly1 is IConstant | poly2 is IConstant) {
                        IAbstractExpr result = (poly1 + poly2).C
                                                  + matcher.Match(_e1);
                        return Simplify(result, depth);
                    }
                }
            }
            {
                ExpressionMatcher matcher = _tLeftPlus2.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P + Z) + Z' --> P + (Z + Z')
                    return matcher.Match(_poly1).C + (matcher.Match(_e1).C + matcher.Match(_e2));
                }
            }
            {
                ExpressionMatcher matcher = _tRightPlus.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // P[V] + (P'[V] + Z') --> P"[V] + Z', where P" = P+P'
                    Polynomial poly1 = matcher.Match(_poly1);
                    Polynomial poly2 = matcher.Match(_poly2);
                    if (poly1.Var.Equals(poly2.Var) | poly1 is IConstant | poly2 is IConstant) {
                        IAbstractExpr result = (poly1 + poly2).C
                                                  + matcher.Match(_e2);
                        return Simplify(result, depth);
                    }
                }
            }
            {
                ExpressionMatcher matcher = _tRightPlus2.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // Z + (P + Z') --> P + (Z + Z')
                    return matcher.Match(_poly2).C + (matcher.Match(_e1).C + matcher.Match(_e2));
                }
            }
            // STEPG
            {
                ExpressionMatcher matcher = _tDevourConstant1.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P + Z) * C --> P' + Z*C, where P'=P*C simplified
                    // TODO: Why not simplify all, i.e., also Z*C??
                    IConstant constant = matcher.Match(_c);
                    IAbstractExpr result = (matcher.Match(_poly1).C * constant).C
                                            + (matcher.Match(_e1).C * constant);
                    return Simplify(result, depth);
                }
            }
            {
                ExpressionMatcher matcher = _tDevourConstant2.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P + Z) / C --> [P' + Z/C] where c'i = ci/C
                    IConstant constant = matcher.Match(_c);
                    IAbstractExpr result = (matcher.Match(_poly1).C / constant).C
                                                  + (matcher.Match(_e1).C / constant);
                    return Simplify(result, depth);
                }
            }
            // STEPH
            {
                ExpressionMatcher matcher = _tRightConstant1.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // E * C --> C * E
                    IAbstractExpr result = matcher.Match(_c).C * matcher.Match(_e1);
                    return Simplify(result, depth);
                }
            }
            {
                ExpressionMatcher matcher = _tRightConstant2.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // E / C --> 1/C * E
                    IAbstractExpr result =
                        Polynomial.CreateConstant(1 / matcher.Match(_c).Value).C
                        * matcher.Match(_e1);
                    return Simplify(result, depth);
                }
            }
            // STEPI
            //{
            //    ExpressionMatcher matcher = _tSqrt1.CreateMatcher();
            //    if (matcher.TryMatch(binaryExpression) && matcher.Match(_ue).Op is PositiveSquareroot) {
            //        // P * sqrt(E) --> sqrt(P * P * E)
            //        UnaryExpression result =
            //            new UnaryExpression(
            //                matcher.Match(_poly1).E * matcher.Match(_poly1) * matcher.Match(_ue).Inner,
            //                new PositiveSquareroot());
            //        return Simplify(result, depth);
            //    }
            //}
            //{
            //    ExpressionMatcher matcher = _tSqrt2.CreateMatcher();
            //    if (matcher.TryMatch(binaryExpression) && matcher.Match(_ue).Op is PositiveSquareroot) {
            //        // sqrt(E) * P --> sqrt(P * P * E)
            //        UnaryExpression result =
            //            new UnaryExpression(
            //                matcher.Match(_poly1).E * matcher.Match(_poly1) * matcher.Match(_ue).Inner,
            //                new PositiveSquareroot());
            //        return Simplify(result, depth);
            //    }
            //}
            //{
            //    ExpressionMatcher matcher = _tSqrt3.CreateMatcher();
            //    if (matcher.TryMatch(binaryExpression) && matcher.Match(_ue).Op is PositiveSquareroot) {
            //        // sqrt(E) / P --> sqrt(E / (P * P))
            //        UnaryExpression result =
            //            new UnaryExpression(
            //                matcher.Match(_ue).Inner.E / (matcher.Match(_poly1).E * matcher.Match(_poly1)),
            //                new PositiveSquareroot());
            //        return Simplify(result, depth);
            //    }
            //}

            {
                ExpressionMatcher matcher = _tSqrt1.CreateMatcher();
                if (matcher.TryMatch(binaryExpression) && matcher.Match(_ue).Op is PositiveSquareroot) {
                    // C * sqrt(E) --> sqrt(C * C * E)      if C > 0
                    // C * sqrt(E) --> 0                    if C = 0
                    // C * sqrt(E) --> -sqrt(C * C * E)     if C < 0
                    double value = matcher.Match(_c).Value;
                    Double square = value * value;
                    if (value.Near(0)) {
                        return Polynomial.CreateConstant(0);
                    } else {
                        IAbstractExpr result =
                            new UnaryExpression(Polynomial.CreateConstant(square).C
                                                * matcher.Match(_ue).Inner,
                                                new PositiveSquareroot());
                        if (value < 0) {
                            result = -result.C;
                        }
                        return Simplify(result, depth);
                    }
                }
            }
            {
                ExpressionMatcher matcher = _tSqrt2.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // unop(E) * C --> C * unop(E)
                    IAbstractExpr result =
                        matcher.Match(_c).C * matcher.Match(_ue);
                    return Simplify(result, depth);
                }
            }
            {
                ExpressionMatcher matcher = _tSqrt3.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // unop(E) / C --> 1/C * unop(E)
                    double value = matcher.Match(_c).Value;
                    IAbstractExpr result =
                        Polynomial.CreateConstant(1 / value).C * matcher.Match(_ue);
                    return Simplify(result, depth);
                }
            }
            //STEPJ
            {
                ExpressionMatcher matcher = _tBothTimes.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P[V] * Z) * (P'[V] * Z') --> P" * (Z * Z')
                    Polynomial poly1 = matcher.Match(_poly1);
                    Polynomial poly2 = matcher.Match(_poly2);
                    if (poly1.Var.Equals(poly2.Var) | poly1 is IConstant | poly2 is IConstant) {
                        IAbstractExpr result = (poly1 * poly2).C
                                                  * (matcher.Match(_e1).C * matcher.Match(_e2));
                        return Simplify(result, depth);
                    }
                }
            }
            {
                ExpressionMatcher matcher = _tLeftTimes.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // (P[V] * Z) * P'[V] --> P"[V] * Z, where P" = P*P' simplified
                    Polynomial poly1 = matcher.Match(_poly1);
                    Polynomial poly2 = matcher.Match(_poly2);
                    if (poly1.Var.Equals(poly2.Var) | poly1 is IConstant | poly2 is IConstant) {
                        IAbstractExpr result = (poly1 * poly2).C
                                                  * matcher.Match(_e1);
                        return Simplify(result, depth);
                    }
                }
            }
            {
                ExpressionMatcher matcher = _tRightTimes.CreateMatcher();
                if (matcher.TryMatch(binaryExpression)) {
                    // P[V] * (P'[V] * Z') --> P"[V] * Z', where P" = P*P'
                    Polynomial poly1 = matcher.Match(_poly1);
                    Polynomial poly2 = matcher.Match(_poly2);
                    if (poly1.Var.Equals(poly2.Var) | poly1 is IConstant | poly2 is IConstant) {
                        IAbstractExpr result = (poly1 * poly2).C
                                                  * matcher.Match(_e2);
                        return Simplify(result, depth);
                    }
                }
            }
            // STEPK
            {
                ExpressionMatcher matcher = _tPolynomialTimesMinus1.CreateMatcher();
                if (matcher.TryMatch(binaryExpression) && matcher.Match(_ue).Op is UnaryMinus) {
                    // P * -E --> -P * E
                    IAbstractExpr result =
                        (-matcher.Match(_poly1)).C * matcher.Match(_ue).Inner;
                    return Simplify(result, depth);
                }
            }
            {
                ExpressionMatcher matcher = _tPolynomialTimesMinus2.CreateMatcher();
                if (matcher.TryMatch(binaryExpression) && matcher.Match(_ue).Op is UnaryMinus) {
                    // P * -E --> -P * E
                    IAbstractExpr result =
                        (-matcher.Match(_poly1)).C * matcher.Match(_ue).Inner;
                    return Simplify(result, depth);
                }
            }
            return binaryExpression;
        }

        private IAbstractExpr Simplify(IAbstractExpr expr, int depth) {
            return expr.Accept(this, depth + 1);
        }

        public IAbstractExpr Visit(IGeneralPolynomial polynomial, int depth) {
            return polynomial;
        }

        public IAbstractExpr Visit(RangeExpr rangeExpr, int depth) {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(UnaryMinus op, IPolynomial inner, int depth) {
            return Polynomial.CreatePolynomial(inner.Var, inner.Coefficients.Select(c => -c));
        }

        public IAbstractExpr Visit(Square op, IPolynomial inner, int depth) {
            return (inner.C * inner).Accept(this, depth + 1);
        }

        public IAbstractExpr Visit(FormalSquareroot op, IPolynomial inner, int depth) {
            return new UnaryExpression(inner, new FormalSquareroot());
        }

        private static IAbstractExpr Fold(UnaryOperator op, IPolynomial inner, Func<double, double> value, IConstant innerAsConstant) {
            return innerAsConstant == null
                       ? (IAbstractExpr)new UnaryExpression(inner, op)
                       : Polynomial.CreateConstant(value(innerAsConstant.Value));
        }

        public IAbstractExpr Visit(PositiveSquareroot op, IPolynomial inner, int depth) {
            return Fold(op, inner, x => Math.Sqrt(x), inner as IConstant);
        }

        public IAbstractExpr Visit(Sin op, IPolynomial inner, int depth) {
            return Fold(op, inner, x => Math.Sin(x / 180 * Math.PI), inner as IConstant);
        }

        public IAbstractExpr Visit(Cos op, IPolynomial inner, int depth) {
            return Fold(op, inner, x => Math.Cos(x / 180 * Math.PI), inner as IConstant);
        }

        #endregion Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

        #region Implementation of ISolverModelBinaryOpVisitor<in IPolynomial,in IPolynomial,in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(Plus op, IPolynomial lhs, IPolynomial rhs, int depth) {
            if (lhs.Var.Equals(rhs.Var)
                | lhs is IConstant     // constants have a funny variable in them that does not compare well.
                | rhs is IConstant) {
                var deg = Math.Max(lhs.Degree, rhs.Degree);
                double[] lhsCoeff = new double[deg - lhs.Degree].Concat(lhs.Coefficients).ToArray();
                double[] rhsCoeff = new double[deg - rhs.Degree].Concat(rhs.Coefficients).ToArray();
                var coeffs = new double[deg + 1];
                for (int i = 0; i <= deg; i++) {
                    coeffs[i] = lhsCoeff[i] + rhsCoeff[i];
                }
                IVariable newVar = lhs is IConstant ? rhs.Var : lhs.Var;
                return Polynomial.CreatePolynomial(newVar, coeffs);
            } else {
                return new BinaryExpression((AbstractExpr)lhs, new Plus(), (AbstractExpr)rhs);
            }
        }

        public IAbstractExpr Visit(Times op, IPolynomial lhs, IPolynomial rhs, int depth) {
            if (lhs.Var.Equals(rhs.Var) | lhs is IConstant | rhs is IConstant) {
                var deg = lhs.Degree + rhs.Degree;
                var coeffs = new double[deg + 1];
                for (int ld = 0; ld <= lhs.Degree; ld++) {
                    for (int rd = 0; rd <= rhs.Degree; rd++) {
                        coeffs[ld + rd] += lhs.Coefficients.ElementAt(ld) * rhs.Coefficients.ElementAt(rd);
                    }
                }
                IVariable newVar = lhs is IConstant ? rhs.Var : lhs.Var;
                return Polynomial.CreatePolynomial(newVar, coeffs);
            } else {
                return new BinaryExpression((AbstractExpr)lhs, new Times(), (AbstractExpr)rhs);
            }
        }

        public IAbstractExpr Visit(Divide op, IPolynomial lhs, IPolynomial rhs, int depth) { // ......
            // TODO: Alternative - try polynomial division. Create polynomial; or polynomial + remainder/rhs.
            var rConst = rhs as IConstant;
            if (rConst != null) {
                return Polynomial.CreatePolynomial(lhs.Var, lhs.Coefficients.Select(c => c / rConst.Value)).Accept(this, depth + 1);
            } else {
                return (AbstractExpr)lhs / (AbstractExpr)rhs;
            }
        }

        #endregion Implementation of ISolverModelBinaryOpVisitor<in IPolynomial,in IPolynomial,in Ignore,out AbstractExpr>
    }
}
