using System;
using System.Collections.Generic;

namespace Movimentum.SubstitutionSolver3 {
    class PolynomialFoldingVisitor : ISolverModelConstraintVisitor<AbstractConstraint>
                                          , ISolverModelExprVisitor<Ignore, IAbstractExpr>
                                          , ISolverModelUnaryOpVisitor<IAbstractExpr, Ignore, IAbstractExpr>
                                          , ISolverModelBinaryOpVisitor<IAbstractExpr, Ignore, IAbstractExpr> {
        #region Implementation of ISolverModelConstraintVisitor<in Ignore,out ScalarConstraint>

        public AbstractConstraint Visit(EqualsZeroConstraint equalsZero, Ignore p) {
            return new EqualsZeroConstraint(equalsZero.Expr.Accept(this, p));
        }

        public AbstractConstraint Visit(MoreThanZeroConstraint moreThanZero, Ignore p) {
            return new MoreThanZeroConstraint(moreThanZero.Expr.Accept(this, p));
        }

        public AbstractConstraint Visit(AtLeastZeroConstraint atLeastZero, Ignore p) {
            return new AtLeastZeroConstraint(atLeastZero.Expr.Accept(this, p));
        }

        #endregion

        #region Implementation of ISolverModelExprVisitor<in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(IConstant constant, Ignore p) {
            return constant;
        }

        public IAbstractExpr Visit(INamedVariable namedVar, Ignore p) {
            return namedVar;
        }

        public IAbstractExpr Visit(IAnchorVariable anchorVar, Ignore p) {
            return anchorVar;
        }

        public IAbstractExpr Visit(UnaryExpression unaryExpr, Ignore p) {
            IAbstractExpr newInner = unaryExpr.Inner.Accept(this, p);
            return unaryExpr.Op.Accept(this, newInner, p);
        }

        public IAbstractExpr Visit(BinaryExpression binaryExpr, Ignore p) {
            IAbstractExpr newLhs = binaryExpr.Lhs.Accept(this, p);
            IAbstractExpr newRhs = binaryExpr.Rhs.Accept(this, p);

            return binaryExpr.Op.Accept(this, newLhs, newRhs, p);
        }

        public IAbstractExpr Visit(IGeneralPolynomial polynomial, Ignore p) {
            return polynomial;
        }

        public IAbstractExpr Visit(RangeExpr rangeExpr, Ignore p) {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

        public IAbstractExpr Visit(FormalSquareroot op, IAbstractExpr inner, Ignore p) {
            return new UnaryExpression(inner, new FormalSquareroot());
        }

        public IAbstractExpr Visit(PositiveSquareroot op, IAbstractExpr inner, Ignore p) {
            return FoldIfConstant(op, inner, x => x.Near(0) ? 0 : Math.Sqrt(x));
        }

        public IAbstractExpr Visit(Sin op, IAbstractExpr inner, Ignore p) {
            return FoldIfConstant(op, inner, x => Math.Sin(x / 180 * Math.PI));
        }

        public IAbstractExpr Visit(Cos op, IAbstractExpr inner, Ignore p) {
            return FoldIfConstant(op, inner, x => Math.Cos(x / 180 * Math.PI));
        }

        private static IAbstractExpr FoldIfConstant(UnaryOperator op,
                IAbstractExpr inner, Func<double, double> eval) {
            IConstant innerAsConst = inner as IConstant;
            return innerAsConst == null
                        ? (IAbstractExpr)new UnaryExpression(inner, op)
                        : Polynomial.CreateConstant(eval(innerAsConst.Value));
        }

        public IAbstractExpr Visit(UnaryMinus op, IAbstractExpr inner, Ignore p) {
            return Rewrite(inner, "Cannot handle unary minus", _unaryMinusRewrites);
        }

        public IAbstractExpr Visit(Square op, IAbstractExpr inner, Ignore p) {
            return Rewrite(inner, "Cannot handle square", _squareRewrites);
        }

        #endregion Implementation of ISolverModelUnaryOpVisitor<in IPolynomial,in Ignore,out AbstractExpr>

        #region Implementation of ISolverModelBinaryOpVisitor<in IAbstractExpr,in Ignore,out IAbstractExpr>

        public IAbstractExpr Visit(Plus op, IAbstractExpr lhs,
                                   IAbstractExpr rhs, Ignore p) {
            return Rewrite(new BinaryExpression(lhs, op, rhs),
                           "Cannot handle " + op,
                           _plusRewrites);
        }

        public IAbstractExpr Visit(Times op, IAbstractExpr lhs, IAbstractExpr rhs, Ignore p) {
            return Rewrite(new BinaryExpression(lhs, op, rhs), "Cannot handle " + op, _timesRewrites);
        }

        public IAbstractExpr Visit(Divide op, IAbstractExpr lhs, IAbstractExpr rhs, Ignore p) {
            return Rewrite(new BinaryExpression(lhs, op, rhs), "Cannot handle " + op, _divideRewrites);
        }

        #endregion

        private static readonly List<ExpressionRewrite> _unaryMinusRewrites = new List<ExpressionRewrite>();
        private static readonly List<ExpressionRewrite> _squareRewrites = new List<ExpressionRewrite>();

        private static readonly List<ExpressionRewrite> _plusRewrites = new List<ExpressionRewrite>();
        private static readonly List<ExpressionRewrite> _timesRewrites = new List<ExpressionRewrite>();
        private static readonly List<ExpressionRewrite> _divideRewrites = new List<ExpressionRewrite>();

        static PolynomialFoldingVisitor() {
            var c = new TypeMatchTemplate<IConstant>();
            var e = new TypeMatchTemplate<IAbstractExpr>();
            var f = new TypeMatchTemplate<IAbstractExpr>();
            var p = new TypeMatchTemplate<IPolynomial>();
            var q = new TypeMatchTemplate<IPolynomial>();
            var sqrtE = new UnaryExpressionTemplate(new PositiveSquareroot(), e);
            var sqrtF = new UnaryExpressionTemplate(new PositiveSquareroot(), f);
            var sqrtP = new UnaryExpressionTemplate(new PositiveSquareroot(), p);
            var sqrtQ = new UnaryExpressionTemplate(new PositiveSquareroot(), q);

            #region Rewrites for UnaryMinus

            new StandardExpressionRewrite("P+-E", _unaryMinusRewrites,
                p + -e, m => -(m & p) + (m & e));
            new StandardExpressionRewrite("P+E", _unaryMinusRewrites,
                p + e, m => -(m & p) + -(m & e));
            // -P should not happen; must have been rewritten to a P with inverted coefficients!
            new StandardExpressionRewrite("P", _unaryMinusRewrites,
                p, m => -(m & p));
            new StandardExpressionRewrite("-E", _unaryMinusRewrites,
                -e, m => m & e);
            new StandardExpressionRewrite("E", _unaryMinusRewrites,
                e, m => -(m & e));

            #endregion Rewrites for UnaryMinus

            #region Rewrites for Square

            {
                // -P should not happen; must have been rewritten to a P with inverted coefficients!
                new StandardExpressionRewrite("P", _squareRewrites,
                    p,
                    m => (m & p) * (m & p));

                new StandardExpressionRewrite("sqrt(E)", _squareRewrites,
                    sqrtE,
                    m => m & e);
                new StandardExpressionRewrite("-sqrt(E)", _squareRewrites,
                    -sqrtE,
                    m => m & e);
                new StandardExpressionRewrite("-E", _squareRewrites,
                    -e,
                    m => new UnaryExpression(m & e, new Square()));

                new StandardExpressionRewrite("P[V]+-sqrt(Q[V])", _squareRewrites,
                    p + -sqrtQ, HaveSameVar(p, q),
                    m => ((m & p) * (m & p) + (m & q)) + -2 * (m & p) * (m & sqrtQ));
                new StandardExpressionRewrite("P[V]+sqrt(Q[V])", _squareRewrites,
                    p + sqrtQ, HaveSameVar(p, q),
                    m => ((m & p) * (m & p) + (m & q)) + 2 * (m & p) * (m & sqrtQ));
                new StandardExpressionRewrite("P+-sqrt(E)", _squareRewrites,
                    p + -sqrtE,
                    m => ((m & p) * (m & p)) + (-2 * (m & p) * (m & sqrtE) + (m & e)));
                new StandardExpressionRewrite("P+sqrt(E)", _squareRewrites,
                    p + sqrtE,
                    m => ((m & p) * (m & p)) + (2 * (m & p) * (m & sqrtE) + (m & e)));
                new StandardExpressionRewrite("P+-E", _squareRewrites,
                    p + -e,
                    m => ((m & p) * (m & p)) + (-2 * (m & p) * (m & e) + new UnaryExpression(m & e, new Square())));
                new StandardExpressionRewrite("P+E", _squareRewrites,
                    p + e,
                    m => ((m & p) * (m & p)) + (2 * (m & p) * (m & e) + new UnaryExpression(m & e, new Square())));

                new StandardExpressionRewrite("E", _squareRewrites,
                    e,
                    m => new UnaryExpression(m & e, new Square()));
            }

            #endregion Rewrites for Square

            #region Rewrites for Plus
            //        | Q[V]+F              Q[V]            F
            // -------+------------------------------------------------
            // P[V]+E | {P[V]+Q[V]}+(E+F)   {P[V]+Q[V]}+E   P[V]+(E+F)
            //        |
            // P[V]   | {P[V]+Q[V]}+F       {P[V]+Q[V]}     P[V]+E
            //        |
            // E      | Q[V]+(E+F)          Q[V]+E          E+F


            {
                new StandardExpressionRewrite("(P+E)+(Q+F)", _plusRewrites,
                    (p + e) + (q + f),
                    HaveSameVar(p, q),
                    m => ((m & p) + (m & q)) + ((m & e) + (m & f)));
                //                PO         AE         AE

                new StandardExpressionRewrite("(P+E)+Q", _plusRewrites,
                    (p + e) + q,
                    HaveSameVar(p, q),
                    m => ((m & p) + (m & q)) + (m & e));

                new StandardExpressionRewrite("(P+E)+F", _plusRewrites,
                    (p + e) + f,
                    m => (m & p) + ((m & e) + (m & f)));
            }
            {
                new StandardExpressionRewrite("P+(Q+F)", _plusRewrites,
                    p + (q + f),
                    HaveSameVar(p, q),
                    m => ((m & p) + (m & q)) + (m & f));
                new StandardExpressionRewrite("P+Q", _plusRewrites,
                    p + q,
                    HaveSameVar(p, q),
                    m => ((m & p) + (m & q)));
                new StandardExpressionRewrite("P+F", _plusRewrites,
                    p + f,
                    m => (m & p) + (m & f));
            }
            {
                new StandardExpressionRewrite("E+(Q+F)", _plusRewrites,
                    e + (q + f),
                    m => (m & q) + ((m & e) + (m & f)));
                new StandardExpressionRewrite("E+Q", _plusRewrites,
                    e + q,
                    m => (m & q) + (m & e));
                new StandardExpressionRewrite("E+F", _plusRewrites,
                    e + f,
                    m => (m & e) + (m & f));
            }
            #endregion Rewrites for Plus

            #region Rewrites for Times
            // First attempt: All of P+√R, P+-√R, P+√E, P+-√E, P+E, P+-E, P, C, -E, E multiplied with each other; 
            // and additionally, expression under square root is equal or not equal to expression under other square root.
            // This gives us 10*14 = 140 rewrite rules. Panic.

            // Second attempt: Stage-wise processing -> 4+4+25.

            // Step I: Distribute.
            //
            //          Q+F                 F
            //
            //  P+E     {PQ}+Q*E+P*F+E*F    P*F+E*F
            //
            //  E       Q*E+E*F             =
            //
            {
                new StandardExpressionRewrite("(P+E)*(Q+F)", _timesRewrites,
                    (p + e) * (q + f),
                    HaveSameVar(p, q),
                    m => RecursivelyFold((m & p) * (m & q)
                                        + (m & q) * (m & e)
                                        + (m & p) * (m & f)
                                        + (m & e) * (m & f)));
                new StandardExpressionRewrite("(P+E)*F", _timesRewrites,
                    (p + e) * f,
                    m => RecursivelyFold((m & p) * (m & f)
                                        + (m & e) * (m & f)));
                new StandardExpressionRewrite("E*(Q+F)", _timesRewrites,
                    e * (q + f),
                    m => RecursivelyFold((m & q) * (m & e)
                                        + (m & e) * (m & f)));
            }
            // Step II: Lift unary minus to top.
            //
            //          -E       E
            //
            //  -F      E*F      -(E*F)
            //
            //  F       -(E*F)   =
            //
            {
                new StandardExpressionRewrite("-E*-F", _timesRewrites,
                    -e * -f,
                    m => RecursivelyFold((m & e) * (m & f)));
                new StandardExpressionRewrite("-E*F", _timesRewrites,
                    -e * f,
                    m => -(RecursivelyFold((m & e) * (m & f)).C));
                new StandardExpressionRewrite("E*-F", _timesRewrites,
                    e * -f,
                    m => -(RecursivelyFold((m & e) * (m & f)).C));
            }

            // Step III: Handle square roots.
            //
            //          √Q          √F              Q           F
            //
            //  √P      P=Q:  P     √(P*F)          √{PQ²}      =
            //          else: √{PQ}
            //
            //  √E      √(Q*E)      E=F:  E         √({Q²}*E)   =
            //                      else: √(E*F)
            //
            //  P       √({P²Q}     √({P²}*F)       {P*Q}       =  
            //
            //  E       =             =               =         =
            //
            {
                // 1st line
                new StandardExpressionRewrite("√P*√P", _timesRewrites,
                    sqrtP * sqrtP,
                    m => RecursivelyFold(m & p));
                new StandardExpressionRewrite("√P*√Q", _timesRewrites,
                    sqrtP * sqrtQ,
                    HaveSameVar(p, q),
                    m => PosSqrtAndRecursivelyFold((m & p) * (m & q)));
                new StandardExpressionRewrite("√P*√F", _timesRewrites,
                    sqrtP * sqrtF,
                    m => PosSqrtAndRecursivelyFold((m & p) * (m & f)));
                new StandardExpressionRewrite("√P*Q", _timesRewrites,
                    sqrtP * q,
                    HaveSameVar(p, q),
                    m => PosSqrtAndRecursivelyFold((m & q) * (m & q) * (m & p)));

                // 2nd line
                new StandardExpressionRewrite("√E*√Q", _timesRewrites,
                    sqrtE * sqrtQ,
                    m => PosSqrtAndRecursivelyFold((m & q) * (m & e)));
                new StandardExpressionRewrite("√E*√E", _timesRewrites,
                    sqrtE * sqrtE,
                    m => m & e);
                new StandardExpressionRewrite("√E*√F", _timesRewrites,
                    sqrtE * sqrtF,
                    m => PosSqrtAndRecursivelyFold((m & e) * (m & f)));
                new StandardExpressionRewrite("√E*D", _timesRewrites,
                    sqrtE * q,
                    m => PosSqrtAndRecursivelyFold((m & q) * (m & q) * (m & e)));

                // 3rd line
                new StandardExpressionRewrite("P*√Q", _timesRewrites,
                    p * sqrtQ,
                    HaveSameVar(p, q),
                    m => PosSqrtAndRecursivelyFold((m & p) * (m & p) * (m & q)));
                new StandardExpressionRewrite("P*√F", _timesRewrites,
                    p * sqrtF,
                    m => PosSqrtAndRecursivelyFold((m & p) * (m & p) * (m & f)));
                new StandardExpressionRewrite("P*Q", _timesRewrites,
                    p * q,
                    HaveSameVar(p, q),
                    m => (m & p) * (m & q));

                // 4th line
                new StandardExpressionRewrite("E", _timesRewrites,
                    e,
                    m => m & e);
            }
            #endregion Rewrites for Times

            #region Rewrites for Divide
            //          C               F
            //
            //  E       E*{1/C}         =
            new StandardExpressionRewrite("E/C", _divideRewrites,
                e / c,
                m => RecursivelyFold(
                      (m & e) * Polynomial.CreateConstant(1 / (m & c).Value)
                    )
            );
            new StandardExpressionRewrite("E", _divideRewrites,
                e,
                m => m & e);

            #endregion Rewrites for Divide
        }

        private static IAbstractExpr PosSqrtAndRecursivelyFold(AbstractExpr expr) {
            return new UnaryExpression(RecursivelyFold(expr), new PositiveSquareroot());
        }

        //private static int _debugDepth;

        //private static IAbstractExpr RecursivelyFoldX(AbstractExpr expr) {
        //    try {
        //        if (++_debugDepth > 10) {
        //            Debug.WriteLine("Endless recursion?");
        //            throw new InvalidOperationException();
        //        }
        //        return expr.Accept(_recursiveFolder);
        //    } finally {
        //        --_debugDepth;
        //    }
        //}

        private static readonly PolynomialFoldingVisitor
            _recursiveFolder = new PolynomialFoldingVisitor();

        private static IAbstractExpr RecursivelyFold(AbstractExpr expr) {
            return expr.Accept(_recursiveFolder);
        }

        private static Func<ExpressionMatcher, bool> HaveSameVar(TypeMatchTemplate<IPolynomial> q, TypeMatchTemplate<IPolynomial> p) {
            return m => {
                Polynomial p1 = m & p;
                Polynomial q1 = m & q;
                return p1 is IConstant || q1 is IConstant || p1.Var.Equals(q1.Var);
            };
        }

        private static IAbstractExpr Rewrite(IAbstractExpr e, string cannotHandleMessage, IEnumerable<ExpressionRewrite> rewrites) {
            foreach (var r in rewrites) {
                var result = r.SuccessfulMatch(e);
                if (result != null) {
                    return result;
                }
            }
            throw new InvalidOperationException(cannotHandleMessage);
        }
    }
}
