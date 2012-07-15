using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestPolynomials {
        private IConstant C(double d) {
            return Polynomial.CreateConstant(d);
        }
        private IVariable V(string name) {
            return Polynomial.CreateNamedVariable(name);
        }
        private IPolynomial P(string name, params double[] coefficients) {
            return Polynomial.CreatePolynomial(V(name), coefficients);
        }

        [Test]
        public void TestDeg() {
            Assert.AreEqual(0, P("x", 0, 0, 0).Degree);
            Assert.AreEqual(0, P("x", 0, 0, 5).Degree);
            Assert.AreEqual(1, P("x", 0, 5, 0).Degree);
            Assert.AreEqual(2, P("x", 5, 0, 0).Degree);
            Assert.AreEqual(2, P("x", 5, 0, 1).Degree);
            Assert.AreEqual(1, P("x", 0, 1, 0).Degree);
        }

        private object Snip(string s) {
            return s.Substring(typeof(IGeneralPolynomialSTEPB).Name.Length + 1);
        }

        [Test]
        public void TestToString() {
            Assert.AreEqual("(x^2+x+1)", Snip(P("x", 1, 1, 1).ToString()));
            Assert.AreEqual("(x^2+x-1)", Snip(P("x", 1, 1, -1).ToString()));
            Assert.AreEqual("(x^2-x+1)", Snip(P("x", 1, -1, 1).ToString()));
            Assert.AreEqual("(-x^2+x+1)", Snip(P("x", -1, 1, 1).ToString()));
            Assert.AreEqual("(2.1*x^2+2.2*x+2.3)", Snip(P("x", 2.1, 2.2, 2.3).ToString()));
            Assert.AreEqual("(2*x^2+2*x-2)", Snip(P("x", 2, 2, -2).ToString()));
            Assert.AreEqual("(2*x^2-2*x+2)", Snip(P("x", 2, -2, 2).ToString()));
            Assert.AreEqual("(-2*x^2+2*x+2)", Snip(P("x", -2, 2, 2).ToString()));
        }

        [Test]
        public void TestSimplifyToConstant() {
            Assert.AreEqual(C(0), P("x", 0, 0, 0));
            Assert.AreEqual(C(5), P("x", 0, 0, 5));
        }

        [Test]
        public void TestSimplifyToVariable() {
            Assert.AreEqual(V("x"), P("x", 0, 1, 0));
        }

        #region PolynomialFoldingVisitor tests

        private IAbstractExpr Fold(IAbstractExpr expr) {
            return expr.Accept(new PolynomialFoldingVisitor(), 0);
        }

        // Test -0        -> 0					
        [Test]
        public void Test001() {
            Assert.AreEqual(C(0), Fold(-C(0).E));
        }
        // Test -1        -> C
        [Test]
        public void Test002() {
            Assert.AreEqual(C(-1), Fold(-C(1).E));
        }
        // Test -C        -> C'
        [Test]
        public void Test003() {
            Assert.AreEqual(C(-3), Fold(-C(3).E));
        }
        // Test -P[V]     -> P'[V]
        [Test]
        public void Test004() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", -1, -2, -3);
            Assert.AreEqual(p_, Fold(-p.E));
        }
        // Test P[V]²    -> P'[V]
        [Test]
        public void Test005() {
            IAbstractExpr p = P("x", 1, 2, 3);
            // 1  2  3
            //    2  4  6
            //       3  6  9
            // -------------
            // 1  4 10 12  9
            IAbstractExpr p_ = P("x", 1, 4, 10, 12, 9);
            Assert.AreEqual(p_, Fold(new UnaryExpression(p, new Square())));
        }
        // Test -V        -> P[V]
        [Test]
        public void Test006() {
            IVariable v = V("x");
            IAbstractExpr p = P("x", -1, 0);
            Assert.AreEqual(p, Fold(-v.E));
        }
        // Test 0*0    -> 0
        [Test]
        public void Test007() {
            Assert.AreEqual(C(0), Fold(C(0).E * C(0)));
        }
        // Test 0*1    -> 0
        [Test]
        public void Test008() {
            Assert.AreEqual(C(0), Fold(C(0).E * C(1)));
        }
        // Test 0*C    -> 0
        [Test]
        public void Test009() {
            Assert.AreEqual(C(0), Fold(C(0).E * C(2)));
        }
        // Test 0*P[V] -> 0
        [Test]
        public void Test010() {
            IAbstractExpr fold = Fold(C(0).E * P("x", 1, 2, 3));
            Assert.AreEqual(C(0), fold);
        }

        // Test 0*V    -> 0
        [Test]
        public void Test011() {
            IAbstractExpr fold = Fold(C(0).E * V("x"));
            Assert.AreEqual(C(0), fold);
        }

        // Test 0+0    -> 0
        [Test]
        public void Test012() {
            Assert.AreEqual(C(0), Fold(C(0).E + C(0)));
        }
        // Test 0+1    -> 1
        [Test]
        public void Test013() {
            Assert.AreEqual(C(1), Fold(C(0).E + C(1)));
        }
        // Test 0+C    -> C
        [Test]
        public void Test014() {
            Assert.AreEqual(C(2), Fold(C(0).E + C(2)));
        }
        // Test 0+P[V] -> P[V]
        [Test]
        public void Test015() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr fold = Fold(C(0).E + p);
            Assert.AreEqual(p, fold);
        }
        // Test 0+V    -> V
        [Test]
        public void Test016() {
            IAbstractExpr v = V("x");
            IAbstractExpr fold = Fold(C(0).E + v);
            Assert.AreEqual(v, fold);
        }
        // Test 0/0    -> NaN
        [Test]
        public void Test017() {
            IConstant fold = Fold(C(0).E / C(0)) as IConstant;
            Assert.IsNotNull(fold);
            Assert.IsTrue(double.IsNaN(fold.Value));
        }

        // Test 0/1    -> 0
        [Test]
        public void Test018() {
            Assert.AreEqual(C(0), Fold(C(0).E / C(1)));
        }
        // Test 0/C    -> 0
        [Test]
        public void Test019() {
            Assert.AreEqual(C(0), Fold(C(0).E / C(2)));
        }
        // Test 0/P[V] -> 0
        [Test]
        public void Test020() {
            IAbstractExpr fold = Fold(C(0).E / P("x", 1, 2, 3));
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test 0/V    -> 0
        [Test]
        public void Test021() {
            IAbstractExpr fold = Fold(C(0).E / V("x"));
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test 0²        -> 0
        [Test]
        public void Test022() {
            Assert.AreEqual(C(0), Fold(new UnaryExpression(C(0), new Square())));
        }
        // Test 1*0    -> 0
        [Test]
        public void Test023() {
            Assert.AreEqual(C(0), Fold(C(1).E * C(0)));
        }
        // Test 1*1    -> 1
        [Test]
        public void Test024() {
            Assert.AreEqual(C(1), Fold(C(1).E * C(1)));
        }
        // Test 1*C    -> C
        [Test]
        public void Test025() {
            Assert.AreEqual(C(2), Fold(C(1).E * C(2)));
        }
        // Test 1*P[V] -> P[V]
        [Test]
        public void Test026() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr fold = Fold(C(1).E * p);
            Assert.AreEqual(p, fold);
        }
        // Test 1*V    -> V
        [Test]
        public void Test027() {
            IAbstractExpr v = V("x");
            IAbstractExpr fold = Fold(C(1).E * v);
            Assert.AreEqual(v, fold);
        }
        // Test 1+0    -> 1
        [Test]
        public void Test028() {
            Assert.AreEqual(C(1), Fold(C(1).E + C(0)));
        }
        // Test 1+1    -> C
        [Test]
        public void Test029() {
            Assert.AreEqual(C(2), Fold(C(1).E + C(1)));
        }
        // Test 1+C    -> C'
        [Test]
        public void Test030() {
            Assert.AreEqual(C(5), Fold(C(1).E + C(4)));
        }
        // Test 1+P[V] -> P'[V]
        [Test]
        public void Test031() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 1, 2, 4);
            Assert.AreEqual(p_, Fold(C(1).E + p));
        }
        // Test 1+V    -> P[V]
        [Test]
        public void Test032() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 1, 1);
            Assert.AreEqual(p, Fold(C(1).E + v));
        }
        // Test 1/0    -> PositiveInfinity
        [Test]
        public void Test033() {
            Assert.AreEqual(C(double.PositiveInfinity), Fold(C(1).E / C(0)));
        }
        // Test 1/1    -> 1
        [Test]
        public void Test034() {
            Assert.AreEqual(C(1), Fold(C(1).E / C(1)));
        }
        // Test 1/C    -> C'
        [Test]
        public void Test035() {
            Assert.AreEqual(C(-0.5), Fold(C(1).E / C(-2)));
        }
        // Test 1/P[V] -> E
        [Test]
        public void Test036() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(C(1).E / p));
        }
        // Test 1/V    -> E
        [Test]
        public void Test037() {
            IAbstractExpr v = V("x");
            Assert.IsNotInstanceOf<IPolynomial>(Fold(C(1).E / v));
        }
        // Test 1²        -> 1
        [Test]
        public void Test038() {
            Assert.AreEqual(C(1), Fold(new UnaryExpression(C(1), new Square())));
        }
        // Test C*0    -> 0
        [Test]
        public void Test039() {
            Assert.AreEqual(C(0), Fold(C(2).E * C(0)));
        }
        // Test C*1    -> C
        [Test]
        public void Test040() {
            Assert.AreEqual(C(-7), Fold(C(-7).E * C(1)));
        }
        // Test C*C'   -> C"
        [Test]
        public void Test041() {
            Assert.AreEqual(C(42), Fold(C(-7).E * C(-6)));
        }
        // Test C*P[V] -> P'[V]
        [Test]
        public void Test042() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 2, 4, 6);
            IAbstractExpr fold = Fold(C(2).E * p);
            Assert.AreEqual(p_, fold);
        }
        // Test C*V    -> P[V]
        [Test]
        public void Test043() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 5, 0);
            IAbstractExpr fold = Fold(C(5).E * v);
            Assert.AreEqual(p, fold);
        }
        // Test C+0    -> C
        [Test]
        public void Test044() {
            Assert.AreEqual(C(-7), Fold(C(-7).E + C(0)));
        }
        // Test C+1    -> C'
        [Test]
        public void Test045() {
            Assert.AreEqual(C(-6), Fold(C(-7).E + C(1)));
        }
        // Test C+C'   -> C"
        [Test]
        public void Test046() {
            Assert.AreEqual(C(0), Fold(C(-7).E + C(7)));
            Assert.AreEqual(C(23), Fold(C(-7).E + C(6).E * C(5)));
            Assert.AreEqual(C(-37), Fold(C(-7).E * C(6) + C(5)));
        }
        // Test C+P[V] -> P'[V]
        [Test]
        public void Test047() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 1, 2, -4);
            Assert.AreEqual(p_, Fold(C(-7).E + p));
        }
        // Test C+V    -> P[V]
        [Test]
        public void Test048() {
            IAbstractExpr p = P("x", 1, -3);
            Assert.AreEqual(p, Fold(C(-3).E + V("x")));
        }
        // Test C/0    -> PositiveInfinity
        [Test]
        public void Test049() {
            Assert.AreEqual(C(double.PositiveInfinity), Fold(C(1).E / C(0)));
        }
        // Test C/1    -> C
        [Test]
        public void Test050() {
            Assert.AreEqual(C(9), Fold(C(9).E / C(1)));
        }
        // Test C/C'   -> C"
        [Test]
        public void Test051() {
            Assert.AreEqual(C(3), Fold(C(12).E / C(4)));
        }
        // Test C/P[V] -> E
        [Test]
        public void Test052() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(C(-3).E / p));
        }
        // Test C/V    -> E
        [Test]
        public void Test053() {
            IAbstractExpr fold = Fold(C(-3).E / V("x"));
            Assert.IsNotInstanceOf<IPolynomial>(fold);
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test C²        -> C'
        [Test]
        public void Test054() {
            Assert.AreEqual(C(9), Fold(new UnaryExpression(C(-3), new Square())));
        }
        // Test P[V]*0     -> 0
        [Test]
        public void Test055() {
            IAbstractExpr p = P("x", 1, -3);
            IAbstractExpr fold = Fold(p.E * C(0));
            Assert.AreEqual(C(0), fold);
        }
        // Test P[V]*1     -> P[V]
        [Test]
        public void Test056() {
            IAbstractExpr p = P("x", 1, -3);
            IAbstractExpr fold = Fold(p.E * C(1));
            Assert.AreEqual(p, fold);
        }
        // Test P[V]*C     -> P'[V]
        [Test]
        public void Test057() {
            IAbstractExpr p = P("x", 1, 6, -3);
            IAbstractExpr p_ = P("x", -2, -12, 6);
            IAbstractExpr fold = Fold(p.E * C(-2));
            Assert.AreEqual(p_, fold);
        }
        // Test P[V]*P[V'] -> E
        [Test]
        public void Test058() {
            IAbstractExpr p = P("x", 1, 2);
            IAbstractExpr p_ = P("y", 2, 1);
            IAbstractExpr fold = Fold(p.E * p_);
            Assert.IsNotInstanceOf<IPolynomial>(fold);
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test P[V]*P'[V]  -> P"[V]
        [Test]
        public void Test059() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 2, 3, 4);
            // 2  3  4
            //    4  6  8
            //       6  9 12
            // -------------
            // 2  7 16 17 12
            IAbstractExpr p__ = P("x", 2, 7, 16, 17, 12);
            Assert.AreEqual(p__, Fold(p.E * p_));
            Assert.AreEqual(p__, Fold(p_.E * p));
        }
        // Test P[V]*V     -> P'[V]
        [Test]
        public void Test060() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 1, 2, 3, 0);
            Assert.AreEqual(p_, Fold(p.E * V("x")));
        }
        // Test P[V]*V'    -> E
        [Test]
        public void Test061() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(p.E * V("y")));
        }
        // Test P[V]+0     -> P[V]
        [Test]
        public void Test062() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.AreEqual(p, Fold(p.E + C(0)));
        }
        // Test P[V]+1     -> P'[V]
        [Test]
        public void Test063() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 1, 2, 4);
            Assert.AreEqual(p_, Fold(p.E + C(1)));
        }
        // Test P[V]+C     -> P'[V]
        [Test]
        public void Test064() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 1, 2, 103);
            Assert.AreEqual(p_, Fold(p.E + C(100)));
        }
        // Test P[V]+P'[V] -> P"[V]
        [Test]
        public void Test065() {
            IAbstractExpr p = P("x", 0, 1, 2, 3);
            IAbstractExpr p_ = P("x", 1, 2, 3, 0);
            IAbstractExpr p__ = P("x", 1, 3, 5, 3);
            Assert.AreEqual(p__, Fold(p.E + p_));
        }
        // Test P[V]+P[V'] -> E
        [Test]
        public void Test066() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("y", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(p.E + p_));
        }
        // Test P[V]+V     -> P'[V]
        [Test]
        public void Test067() {
            IAbstractExpr p = P("x", 1, 2, 4);
            IAbstractExpr p_ = P("x", 1, 3, 4);
            Assert.AreEqual(p_, Fold(p.E + V("x")));
        }
        // Test P[V]+V'    -> E
        [Test]
        public void Test068() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(p.E + V("y")));
        }
        // Test P[V]/0     -> P'[V]
        [Test]
        public void Test069() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
            IAbstractExpr fold = Fold(p.E / C(0));
            Assert.AreEqual(p_, fold);
        }
        // Test P[V]/1     -> P[V]
        [Test]
        public void Test070() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.AreEqual(p, Fold(p.E / C(1)));
        }
        // Test P[V]/C     -> P'[V]
        [Test]
        public void Test071() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 0.5, 1, 1.5);
            Assert.AreEqual(p_, Fold(p.E / C(2)));
        }
        // Test P[V]/P'[V] -> E
        [Test]
        public void Test072() {
            IAbstractExpr p = P("x", 1, 2, 3);
            IAbstractExpr p_ = P("x", 2, 3, 4);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(p.E / p_));
        }
        // Test P[V]/V     -> V
        [Test]
        public void Test073a() {
            IAbstractExpr p = P("x", 1, 0, 0);
            IAbstractExpr fold = Fold(p.E / V("x"));
            Assert.IsNotInstanceOf<IVariable>(fold);
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test P[V]/V     -> E
        [Test]
        public void Test073b() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(Fold(p.E / V("x"))));
        }
        // Test P[V]/V     -> P[V] 
        [Test]
        public void Test074() {
            IAbstractExpr p = P("x", 1, 2, 0);
            IAbstractExpr fold = Fold(p.E / V("x"));
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test P[V]/V'    -> E
        [Test]
        public void Test075() {
            IAbstractExpr p = P("x", 1, 2, 3);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(p.E / V("y")));
        }
        // Test P[V]² see Test005

        // Test V*0     -> 0
        [Test]
        public void Test076() {
            IAbstractExpr v = V("x");
            IAbstractExpr fold = Fold(v.E * C(0));
            Assert.AreEqual(C(0), fold);
        }
        // Test V*1     -> V
        [Test]
        public void Test077() {
            IAbstractExpr v = V("x");
            IAbstractExpr fold = Fold(v.E * C(1));
            Assert.AreEqual(v, fold);
        }
        // Test V*C     -> P
        [Test]
        public void Test078() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 3, 0);
            IAbstractExpr fold = Fold(v.E * C(3));
            Assert.AreEqual(p, fold);
        }
        // Test V*P[V'] -> E
        [Test]
        public void Test079() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("y", 3, 0);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(v.E * p));
        }
        // Test V*P[V]  -> P'[V]
        [Test]
        public void Test080() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 3, 0);
            IAbstractExpr p_ = P("x", 3, 0, 0);
            Assert.AreEqual(p_, Fold(v.E * p));
        }
        // Test V*V     -> P[V]
        [Test]
        public void Test081() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 1, 0, 0);
            Assert.AreEqual(p, Fold(v.E * v));
        }
        // Test V*V'    -> E
        [Test]
        public void Test082() {
            IAbstractExpr v = V("x");
            IAbstractExpr v_ = V("y");
            Assert.IsNotInstanceOf<IPolynomial>(Fold(v.E * v_));
        }
        // Test V+0     -> V
        [Test]
        public void Test083() {
            Assert.AreEqual(V("x"), Fold(V("x").E + C(0)));
        }
        // Test V+1     -> P[V]
        [Test]
        public void Test084() {
            IAbstractExpr p = P("x", 1, 1);
            IAbstractExpr v = V("x");
            Assert.AreEqual(p, Fold(v.E + C(1)));
        }
        // Test V+C     -> P[V]
        [Test]
        public void Test085() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 1, -13);
            Assert.AreEqual(p, Fold(v.E + C(-13)));
        }
        // Test V+P[V'] -> E
        [Test]
        public void Test086() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("y", 3, 0);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(v.E + p));
        }
        // Test V+P[V]  -> P[V]
        [Test]
        public void Test087() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 2, 3, 0);
            IAbstractExpr p_ = P("x", 2, 4, 0);
            Assert.AreEqual(p_, Fold(v.E + p));
        }
        // Test V+V     -> P[V]
        [Test]
        public void Test088() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 2, 0);
            Assert.AreEqual(p, Fold(v.E + v));
        }
        // Test V+V'    -> E
        [Test]
        public void Test089() {
            IAbstractExpr v = V("x");
            IAbstractExpr v_ = V("y");
            Assert.IsNotInstanceOf<IPolynomial>(Fold(v.E + v_));
        }
        // Test V/0     -> P
        [Test]
        public void Test090() {
            IAbstractExpr v = V("x");
            IAbstractExpr fold = Fold(v.E / C(0));
            var p = P("x", double.PositiveInfinity, double.NaN);
            Assert.AreEqual(p, fold);
        }
        // Test V/1     -> V
        [Test]
        public void Test091() {
            IAbstractExpr v = V("x");
            Assert.AreEqual(v, Fold(v.E / C(1)));
        }
        // Test V/C     -> P[V]
        [Test]
        public void Test092() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 0.25, 0);
            Assert.AreEqual(p, Fold(v.E / C(4)));
        }
        // Test V/P[V]  -> E
        [Test]
        public void Test093() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 2, 1);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(v.E / p));
        }
        // Test V/P[V]  -> C
        [Test]
        public void Test093b() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 2, 0);
            IAbstractExpr fold = Fold(v.E / p);
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test V/V     -> 1
        [Test]
        public void Test094() {
            IAbstractExpr v = V("x");
            IAbstractExpr fold = Fold(v.E / v);
            Assert.IsInstanceOf<BinaryExpression>(fold);
        }
        // Test V/V'    -> E
        [Test]
        public void Test095() {
            IAbstractExpr v = V("x");
            IAbstractExpr v_ = V("y");
            Assert.IsNotInstanceOf<IPolynomial>(Fold(v.E / v_));
        }
        // Test V²        -> P[V]
        [Test]
        public void Test096() {
            IAbstractExpr v = V("x");
            IAbstractExpr p = P("x", 1, 0, 0);
            Assert.AreEqual(p, Fold(new UnaryExpression(v, new Square())));
        }
        // Test sin 0     -> 0
        [Test]
        public void Test097() {
            Assert.AreEqual(C(0), Fold(new UnaryExpression(C(0), new Sin())));
        }
        // Test sin 1     -> C
        [Test]
        public void Test098() {
            Assert.IsInstanceOf<IConstant>(Fold(new UnaryExpression(C(1), new Sin())));
        }
        // Test sin C     -> C'
        [Test]
        public void Test099() {
            Assert.IsInstanceOf<IConstant>(Fold(new UnaryExpression(C(2), new Sin())));
        }
        // Test sin P[V]  -> E
        [Test]
        public void Test100() {
            IAbstractExpr p = P("x", 1, 0, 0);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(new UnaryExpression(p, new Sin())));
        }
        // Test sin V     -> E
        [Test]
        public void Test101() {
            IAbstractExpr v = V("x");
            Assert.IsNotInstanceOf<IPolynomial>(Fold(new UnaryExpression(v, new Sin())));
        }
        // Test sqrt 0    -> 0
        [Test]
        public void Test102() {
            Assert.AreEqual(C(0), Fold(new UnaryExpression(C(0), new PositiveSquareroot())));
        }
        // Test sqrt 1    -> 1
        [Test]
        public void Test103() {
            Assert.AreEqual(C(1), Fold(new UnaryExpression(C(1), new PositiveSquareroot())));
        }
        // Test sqrt C    -> C
        [Test]
        public void Test104() {
            Assert.AreEqual(C(9), Fold(new UnaryExpression(C(81), new PositiveSquareroot())));
        }
        // Test sqrt P[V] -> E
        [Test]
        public void Test105() {
            IAbstractExpr p = P("x", 1, 0, 0);
            Assert.IsNotInstanceOf<IPolynomial>(Fold(new UnaryExpression(p, new PositiveSquareroot())));
        }
        // Test sqrt V    -> E
        [Test]
        public void Test106() {
            IAbstractExpr p = V("x");
            Assert.IsNotInstanceOf<IPolynomial>(Fold(new UnaryExpression(p, new PositiveSquareroot())));
        }

        #endregion PolynomialFoldingVisitor tests

        [Test]
        public void TestEvaluatePolynomial() {
            const double expected = 1 * 2 * 2 * 2 + 2 * 2 * 2 + 3 * 2 + 4;
            Assert.AreEqual(expected, P("x", 1, 2, 3, 4).Accept(new EvaluationVisitor(V("x"), 2), Ig.nore), 1e-14);
        }

        #region Rewriting of polynomials

        [Test]
        public void TestRewritePolynomialWithPolynomial() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            IPolynomial expected = P("y", 1, 0, 2, 0, 3, 0, 4);
            IAbstractExpr result = Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), V("y").E * V("y")), Ig.nore));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestRewritePolynomialWithSquareY() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            IPolynomial expected = P("y", 1, 0, 2, 0, 3, 0, 4);
            IAbstractExpr result = Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), new UnaryExpression(V("y"), new Square())), Ig.nore));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestRewritePolynomialWithItself() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), p), Ig.nore));
            // It works :-)
        }

        [Test]
        public void TestRewritePolynomialWIthPolynomial1() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            // (x+1)^3 + 2(x+1)^2 + 3(x+1) + 4 =
            // x^3+3x^2+3x+1 + 2x^2+4x+2 + 3x+3 + 4 =
            // x^3 + (3+2)x^2 + (3+4+3)x + 1+2+3+4 =
            // x^3 + 5x^2 + 10x + 10

            IPolynomial expected = P("x", 1, 5, 10, 10);
            IAbstractExpr result = Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), V("x").E + C(1)), Ig.nore));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestRewritePolynomialWithPolynomial2() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            // (2x+1)^3 + 2(2x+1)^2 + 3(2x+1) + 4 =
            // 8x^3+12x^2+5x+1 + 8x^2+8x+2 + 6x+3 + 4 =
            // 8x^3 + (12+8)x^2 + (6+8+6)x + 1+2+3+4 =
            // 8x^3 + 20x^2 + 20x + 10

            IPolynomial expected = P("x", 8, 20, 20, 10);
            IAbstractExpr result = Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), (V("x").E * C(2)).E + C(1)), Ig.nore));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestRewritePolynomialWithMinusX() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            IPolynomial expected = P("x", -1, 2, -3, 4);
            IAbstractExpr result = Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), -V("x").E), Ig.nore));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestRewritePolynomialWithSinX() {
            IPolynomial p = P("x", 1, 2, 3, 4);
            AbstractExpr sinx = new UnaryExpression(V("x"), new Sin()).E;
            BinaryExpression expected = sinx * (sinx * (C(1).E * sinx + C(2)) + C(3)) + C(4);

            IAbstractExpr result = Fold(p.Accept(new RewritingVisitorSTEPC(V("x"), sinx), Ig.nore));

            Assert.AreEqual(expected, result);
        }

        #endregion Rewriting of polynomials

        #region Normalizing of expressions with polynomials

        [Test]
        public void TestNormalizeBothSides() {
            var p = (P("x", 1, 2, 3).E + new UnaryExpression(V("x"), new PositiveSquareroot()).E
                     + (P("x", 2, 3, 4).E + new UnaryExpression(V("x"), new PositiveSquareroot())));
            var p_ = P("x", 3, 5, 7).E + (new UnaryExpression(V("x"), new PositiveSquareroot()).E + new UnaryExpression(V("x"), new PositiveSquareroot()));
            var fold = Fold(p);
            Assert.AreEqual(p_, fold);
        }


        [Test]
        public void TestNormalizeRightSides() {
            var p = C(200).E + -(P("x", 2, 3, 4).E + new UnaryExpression(V("x"), new PositiveSquareroot()));

            var p_ = P("x", -2, -3, 196).E + -(new UnaryExpression(V("x"), new PositiveSquareroot()).E);
            var fold = Fold(p);
            Assert.AreEqual(p_, fold);
        }

        #endregion Normalizing of expressions with polynomials

    }
}
