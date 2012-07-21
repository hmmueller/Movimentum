using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Movimentum.SubstitutionSolver3 {
    class PolynomialFoldingVisitorNEW : ISolverModelConstraintVisitor<AbstractConstraint>
                                      , ISolverModelExprVisitor<IAbstractExpr> {
        private static readonly IConstant ZERO = Polynomial.CreateConstant(0);

        private class PsPlusE {
            public readonly double C;
            public readonly SortedDictionary<IVariable, IPolynomial> Ps = new SortedDictionary<IVariable, IPolynomial>();
            [NotNull]
            public readonly IAbstractExpr E;

            public PsPlusE(double c, IEnumerable<IPolynomial> constFreePolys, IAbstractExpr e) {
                #region Initialize C
                
                C = c;
                
                #endregion Initialize C

                #region Initialize Ps
                
                IEnumerable<IPolynomial> ps = constFreePolys ?? Enumerable.Empty<IPolynomial>();

                foreach (var p in ps) {
                    double c0 = p.Coefficient(0);
                    if (!c0.Near(0) && !double.IsNaN(c0)) {
                        throw new ArgumentException(p + " is not const-free");
                    }
                }
                Ps = new SortedDictionary<IVariable, IPolynomial>(ps.ToDictionary(p => p.Var, p => p));

                #endregion Initialize Ps

                #region Initialize E

                var eUn = e as UnaryExpression;
                if (eUn != null && eUn.Inner is IPolynomial && (eUn.Op is Square | eUn.Op is UnaryMinus)) {
                    // P [+] Q² should be P+Q² [+] null
                    throw new ArgumentException("e must not be Polynomial behind Square or UnaryMinus operator");
                }

                var eBin = e as BinaryExpression;
                if (eBin != null) {
                    IPolynomial eBinLhs = eBin.Lhs as IPolynomial;
                    IPolynomial eBinRhs = eBin.Rhs as IPolynomial;
                    if (eBin.Op is Plus && eBinLhs != null) {
                        // P [+] Q + E should be P+Q [+] E
                        // P [+] E + Q should be P+Q [+] E
                        throw new ArgumentException("e must not contain polynomial behind Plus");
                    }
                    if (eBin.Op is Plus && eBinRhs != null) {
                        // P [+] Q + E should be P+Q [+] E
                        // P [+] E + Q should be P+Q [+] E
                        throw new ArgumentException("e must not contain polynomial behind Plus");
                    }
                    if (eBin.Op is Times && eBinLhs != null && eBinRhs != null
                        && SameVarOrNull(eBinLhs, eBinRhs) != null) {
                        // P [+] Q * R should be P+Q*R [+] null
                        throw new ArgumentException("e must not contain two polynomials with same variable behind Times");
                    }
                }
                E = e ?? ZERO;

                #endregion Initialize E
            }

            public override string ToString() {
                return "[" + C + "(+)" +
                        string.Join("(+)", Ps.Select(kvp => kvp.Key + ":" + kvp.Value)) + "(+)" +
                        E + "]";
            }

            public IAbstractExpr ToExpr() {
                // ((P0+C)) + ( P1 + (P2 + ... + (Pn + E) ... ) )
                if (Ps.Any()) {
                    IPolynomial[] polys = Ps.Values.ToArray();
                    IAbstractExpr result = E;
                    for (int i = polys.Length - 1; i >= 1; i--) {
                        result = result.C + polys[i];
                    }
                    var topCoeffs = polys[0].Coefficients.ToArray();
                    topCoeffs[topCoeffs.Length - 1] = C;
                    IPolynomial topPoly = Polynomial.CreatePolynomial(polys[0].Var, topCoeffs);
                    return topPoly.C + result;
                } else {
                    return Polynomial.CreateConstant(C).C + E;
                }
            }

            private static readonly EvaluationVisitor eval = new EvaluationVisitor(new Dictionary<IVariable, double>());

            public PsPlusE EvaluateIfConstantOrNull(UnaryOperator op) {
                var eConst = E as IConstant;
                if (!Ps.Any() & eConst != null) {
                    IAbstractExpr expr = new UnaryExpression(Polynomial.CreateConstant(C + eConst.Value), op);
                    double result = expr.Accept(eval);
                    return new PsPlusE(result, null, null);
                } else {
                    return null;
                }
            }
        }

        private class PolynomialFoldingOpVisitor : ISolverModelUnaryOpVisitor<PsPlusE, Ignore, PsPlusE>
                                          , ISolverModelBinaryOpVisitor<PsPlusE, Ignore, PsPlusE>
                                          , ISolverModelExprVisitor<Ignore, PsPlusE> {

            #region Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

            public PsPlusE Visit(FormalSquareroot op, PsPlusE inner, Ignore p) {
                return new PsPlusE(0, null, new UnaryExpression(inner.ToExpr(), op));
            }

            public PsPlusE Visit(PositiveSquareroot op, PsPlusE inner, Ignore p) {
                return inner.EvaluateIfConstantOrNull(op)
                    ?? new PsPlusE(0, null, new UnaryExpression(inner.ToExpr(), op));
            }

            public PsPlusE Visit(Sin op, PsPlusE inner, Ignore p) {
                return inner.EvaluateIfConstantOrNull(op)
                    ?? new PsPlusE(0, null, new UnaryExpression(inner.ToExpr(), op));
            }

            public PsPlusE Visit(Cos op, PsPlusE inner, Ignore p) {
                return inner.EvaluateIfConstantOrNull(op)
                    ?? new PsPlusE(0, null, new UnaryExpression(inner.ToExpr(), op));
            }
            public PsPlusE Visit(UnaryMinus op, PsPlusE inner, Ignore p) {
                PsPlusE result = inner.EvaluateIfConstantOrNull(op);
                if (result == null) {
                    // There are variables inside inner.
                    double minusC = -inner.C;
                    IEnumerable<IPolynomial> minusPs =
                        inner.Ps.Values.Select(poly => Polynomial.CreatePolynomial(poly.Var, poly.Coefficients.Select(c => -c)));
                    IAbstractExpr minusE;
                    {
                        UnaryExpression innerEAsUnary = inner.E as UnaryExpression;
                        // Optimize --X to X.
                        minusE = innerEAsUnary != null && innerEAsUnary.Op is UnaryMinus
                                     ? innerEAsUnary.Inner
                                     : -inner.E.C;
                    }
                    result = new PsPlusE(minusC, minusPs, minusE);
                }
                return result;
            }

            public PsPlusE Visit(Square op, PsPlusE inner, Ignore p) {
                // Lazy version - this will not optimize away a sqrt of inner.E
                return Visit(new Times(), inner, inner, p);
            }

            #endregion Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

            private IPolynomial Times(IPolynomial poly, double d) {
                return Polynomial.CreatePolynomial(poly.Var, poly.Coefficients.Select(c => d * c));
            }

            private IPolynomial Plus(params IPolynomial[] polys) {
                int resultDegree = polys.Max(p => p.Degree);
                var resultCoefficients = new double[resultDegree + 1];
                IPolynomial p0 = polys[0];
                IVariable resultVar = p0.Var;
                foreach (var p in polys) {
                    resultVar = SameVarOrNull(p0, p);
                    for (int d = 0; d <= p.Degree; d++) {
                        resultCoefficients[resultDegree - d] += p.Coefficient(d);
                    }
                }
                if (resultVar == null) {
                    throw new ArgumentException("All polynomials must have same variable");
                }
                return Polynomial.CreatePolynomial(resultVar, resultCoefficients);
            }

            private IPolynomial Times(IPolynomial lhs, IPolynomial rhs) {
                if (SameVarOrNull(lhs, rhs) == null) {
                    throw new ArgumentException("Both polynomials must have same variable, not " + lhs.Var + " and " + rhs.Var);
                }
                var deg = lhs.Degree + rhs.Degree;
                var coeffs = new double[deg + 1];
                for (int ld = 0; ld <= lhs.Degree; ld++) {
                    for (int rd = 0; rd <= rhs.Degree; rd++) {
                        coeffs[ld + rd] += lhs.Coefficients.ElementAt(ld) * rhs.Coefficients.ElementAt(rd);
                    }
                }
                return Polynomial.CreatePolynomial(SameVarOrNull(lhs, rhs), coeffs);
            }


            #region Implementation of ISolverModelBinaryOpVisitor<in IPolynomial,in IPolynomial,in Ignore,out AbstractExpr>

            public PsPlusE Visit(Plus op, PsPlusE lhs, PsPlusE rhs, Ignore p) {
                double resultC = lhs.C + rhs.C;

                // Add polynomials according to their variable.
                IEnumerable<IVariable> commonVars = lhs.Ps.Keys.Intersect(rhs.Ps.Keys);
                IEnumerable<IVariable> onlyLhsVars = lhs.Ps.Keys.Except(rhs.Ps.Keys);
                IEnumerable<IVariable> onlyRhsVars = rhs.Ps.Keys.Except(lhs.Ps.Keys);

                var resultPs = new List<IPolynomial>();
                foreach (var v in commonVars) {
                    var sum = Plus(lhs.Ps[v], rhs.Ps[v]);
                    // If the polnomials cancelled each other, we do not add the resulting zero polynomial.
                    // Example: ((3x)) and ((-3x))
                    // Because all the polynomials are const-free, there cannot emerge a non-zero constant,
                    // so we do not have to add anything to resultC.
                    if (sum.Degree > 0) {
                        resultPs.Add(sum);
                    }
                }
                foreach (var v in onlyLhsVars) {
                    resultPs.Add(lhs.Ps[v]);
                }
                foreach (var v in onlyRhsVars) {
                    resultPs.Add(rhs.Ps[v]);
                }

                IAbstractExpr resultE = lhs.E.C + rhs.E;

                return new PsPlusE(resultC, resultPs, resultE);
            }

            public PsPlusE Visit(Times op, PsPlusE lhs, PsPlusE rhs, Ignore p) {
                // Result matrix:
                //
                //                 | {1}               {2}                  {3}                 {4}
                //                 | CR                PRcommon[V]          PRonly[V"]          ER
                // ----------------+-----------------------------------------------------------------
                // {A}          CL | c CL+CR           p (a)                p PER[V"]:          e CL*ER
                //                 |                                          ((C*PRonly[V"]))    
                //                 |
                // {B} PLCommon[V] | p (a)             p PER[V]:            e SUM[V]:SUM[V"]:     .       
                //                 |                     PLCommon[V]          PLCommon[V]         .
                //                 |                     * PRcommon[V]        * PRonly[V"]      e ER *
                //                 |                 + e PER[V]:PER[V2\V]
                //                 |                     PLCommon[V]*PRcommon[V2]
                // {C}  PLonly[V'] | p PER[V']:                                                   SUM[V]:
                //                 |  ((C*PRonly[V'])) e .........SUM[V']:SUM[V"]:........      PL[V]
                //                 |                             PLonly[V']                   .
                //                 |                             * PR[V"]                     .
                //                 |
                // {D}          EL | e CR*EL           e .........EL * SUM[V]:PR[V].......    e EL * ER
                //
                //
                //  (a): PER[V]:((CL*PRCommon[V] + CR*PLCommon[V]))

                double resultC = lhs.C * rhs.C;                     // {A1}

                IEnumerable<IVariable> commonVars = lhs.Ps.Keys.Intersect(rhs.Ps.Keys);
                IEnumerable<IVariable> onlyLhsVars = lhs.Ps.Keys.Except(rhs.Ps.Keys);
                IEnumerable<IVariable> onlyRhsVars = rhs.Ps.Keys.Except(lhs.Ps.Keys);

                IPolynomial[] resultPs = commonVars.Select(v =>
                                Plus(Times(lhs.Ps[v], rhs.C),       // {B1}
                                     Times(rhs.Ps[v], lhs.C),       // {A2}
                                     Times(lhs.Ps[v], rhs.Ps[v])    // {B2.1}
                                    )
                                ).Concat(onlyRhsVars.Select(v =>
                                    Times(rhs.Ps[v], lhs.C)         // {A3}
                                )).Concat(onlyLhsVars.Select(v =>
                                    Times(lhs.Ps[v], rhs.C)         // {C1}
                                )).ToArray();

                AbstractExpr resultE =
                    Polynomial.CreateConstant(lhs.C).C * rhs.E      // {A4}
                    + Polynomial.CreateConstant(rhs.C).C * lhs.E    // {D1}
                    + lhs.E.C * rhs.E;                              // {D4}

                foreach (var vL in commonVars) {
                    foreach (var vR in commonVars) {
                        if (!vL.Equals(vR)) {
                            resultE += lhs.Ps[vL].C * rhs.Ps[vR];   // {B2.2}
                        }
                    }
                    foreach (var vR in onlyRhsVars) {
                        resultE += lhs.Ps[vL].C + rhs.Ps[vR];       // {B3}
                    }
                }
                foreach (var vL in onlyLhsVars) {
                    foreach (var pR in rhs.Ps.Values) {
                        resultE += lhs.Ps[vL].C * pR;               // {C2} {C3}
                    }
                }
                foreach (var pL in lhs.Ps.Values) {
                    resultE += pL.C * rhs.E;                        // {B4} {C4}
                }
                foreach (var pR in rhs.Ps.Values) {
                    resultE += pR.C * lhs.E;                        // {D2} {D3}
                }
                return new PsPlusE(resultC, resultPs.Where(rP => !rP.Equals(ZERO)), resultE);
            }

            public PsPlusE Visit(Divide op, PsPlusE lhs, PsPlusE rhs, Ignore p) {
                if (!rhs.Ps.Any() & rhs.E.Equals(ZERO)) {
                    return new PsPlusE(lhs.C / rhs.C,
                        lhs.Ps.Values.Select(pL => Times(pL, 1 / rhs.C)),
                        lhs.E.C / Polynomial.CreateConstant(rhs.C)
                        );
                } else {
                    return new PsPlusE(0, null, lhs.ToExpr().C / rhs.ToExpr());
                }
            }

            #endregion Implementation of ISolverModelBinaryOpVisitor<in IPolynomial,in IPolynomial,in Ignore,out AbstractExpr>

            #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

            public PsPlusE Visit(IConstant constant, Ignore p) {
                return new PsPlusE(constant.Value, null, null);
            }

            public PsPlusE Visit(INamedVariable namedVariable, Ignore p) {
                return new PsPlusE(0, new[] { namedVariable }, null);
            }

            public PsPlusE Visit(IAnchorVariable anchorVariable, Ignore p) {
                return new PsPlusE(0, new[] { anchorVariable }, null);
            }

            public PsPlusE Visit(UnaryExpression unaryExpression, Ignore p) {
                PsPlusE newInner = unaryExpression.Inner.Accept(this);

                return unaryExpression.Op.Accept(this, newInner, p);
            }

            public PsPlusE Visit(BinaryExpression binaryExpression, Ignore p) {
                PsPlusE newLhs = binaryExpression.Lhs.Accept(this);
                PsPlusE newRhs = binaryExpression.Rhs.Accept(this);

                return binaryExpression.Op.Accept(this, newLhs, newRhs, p);
            }

            public PsPlusE Visit(IGeneralPolynomial polynomial, Ignore p) {
                double[] constFreeCoeffs = polynomial.Coefficients.ToArray();
                constFreeCoeffs[polynomial.Degree] = 0;
                return new PsPlusE(
                    polynomial.Coefficient(0),
                    new[] { Polynomial.CreatePolynomial(polynomial.Var, constFreeCoeffs) },
                    null);
            }

            public PsPlusE Visit(RangeExpr rangeExpr, Ignore p) {
                throw new NotImplementedException();
            }
        }

        private static IVariable SameVarOrNull(IPolynomial lhs, IPolynomial rhs) {
            return lhs.Var.Equals(rhs.Var) ? lhs.Var
                : lhs is IConstant ? rhs.Var
                : rhs is IConstant ? lhs.Var
                : null;
        }

        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out ScalarConstraint>

        private static readonly PolynomialFoldingOpVisitor _opVisitor = new PolynomialFoldingOpVisitor();

        public AbstractConstraint Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            return new EqualsZeroConstraint(equalsZero.Expr.Accept(_opVisitor).ToExpr());
        }

        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            return new MoreThanZeroConstraint(moreThanZero.Expr.Accept(_opVisitor).ToExpr());
        }

        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            return new AtLeastZeroConstraint(atLeastZero.Expr.Accept(_opVisitor).ToExpr());
        }

        #endregion


            #endregion

        #region Implementation of ISolverModelExprVisitor<in Ignore,out IAbstractExpr>

        public IAbstractExpr Visit(IConstant constant, Ignore p) {
            return constant;
        }

        public IAbstractExpr Visit(INamedVariable namedVariable, Ignore p) {
            return namedVariable;
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVariable, Ignore p) {
            return anchorVariable;
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpression, Ignore p) {
            return unaryExpression.Accept(_opVisitor).ToExpr();
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpression, Ignore p) {
            return binaryExpression.Accept(_opVisitor).ToExpr();
        }

        public IAbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            return rangeExpr.Accept(_opVisitor).ToExpr();
        }

        public IAbstractExpr Visit(IGeneralPolynomial polynomial, Ignore parameter) {
            return polynomial;
        }

        #endregion
    }
}
