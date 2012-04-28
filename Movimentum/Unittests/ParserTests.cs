using System;
using System.Linq;
using Antlr.Runtime;
using Movimentum.Lexer;
using Movimentum.Model;
using Movimentum.Parser;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestsForParserxxxx {
        #region Script parsing

        [Test]
        public void TestParseSliderCrankSetup() {
            const string s = @".config(12,      // number of  frames per time unit
                                       1, 1);   // width/height of output
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+[0,30];
            Piston : 'piston.gif' P = [30,50] Q = P+[1,0];
            Conrod : 'conrod.gif' P = [0,50] 
                                  Q = P+[100,50]; // lies horizontally in gif";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void TestParseSliderCrankSetupWithError() {
            const string s = @".config(12,      // number of  frames per time unit
                                       1, 1);   // width/height of output
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+...";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        [Test]
        public void TestParseSliderCrankScript() {
            const string s = @".config(12,      // number of  frames per time unit
                                       1, 1);   // width/height of output
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+[0,30];
            Piston : 'piston.gif' P = [30,50] Q = P+[1,0];
            Conrod : 'conrod.gif' P = [0,50] 
                                  Q = P+[100,50]; // lies horizontally in gif

            // Placing all the parts
            @000.0    Engine.P = [20,80]; Engine.Q = Engine.P + [_,0];
            @+02.0    Crank.P = Engine.P + [200,70]; 
                      Crank.Q = Crank.P + [_,0];
            @+02.0    Conrod.P = Crank.Q; Conrod.Q = [x, 70]; x < Crank.P.x;
            @+02.0    Piston.P = Conrod.Q;

            // Let's move it!
            @+02.0    Crank.Q = Crank.P + [_,0].rotate(180 * t);

            @+08.0   b = -180 / 6; // angular acceleration
                     w = .integral(b) + 180;
                     Crank.Q = Crank.P + [_,0].rotate(w * t);

            @+06.0   w = 0;";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        [Test]
        public void TestParseBar() {
            const string s =
                @".config(12,          // number of  frames per time unit
                          100, 100);   // width/height of output
                B1 : .bar P = [0,0] Q = [0,20] R = [20,0];
                B2 : .bar P = [0,0] Q = [20,20] R = [40,0];";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        [Test, Ignore("Anchors link and sink do not yet work! - grammar problem.")]
        public void TestPrefixIdents() {
            const string s =
                @".config(12,          // number of  frames per time unit
                          100, 100);   // width/height of output
                B : .bar pink = [0,0]; link = [1,1]; sink = [2,2];

                @1 B.pink = [10,10]; B.link = [11,11]; B.sink = [12,12];";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        [Test]
        public void TestParseBar1() {
            const string s = @".config(10, 600, 400);

                    // reversing rod (reach rod) (3)
                    ER	:	.bar L = [0,0]	R = [30,0]; 
                    // lifting lever (5, 6)
                    LL	:	.bar L = [0,10] C = [0,0] R = [10,0]	;

                    @0
	                    ER.L = ER.R - [_,0];";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        #endregion Script parsing

        #region Model building

        [Test]
        public void TestParseAndBuildSliderCrankSetup() {
            const string s = @".config(12,      // number of  frames per time unit
                                       1, 1);   // width/height of output
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+[0,30];
            Piston : 'piston.gif' P = [30,50] Q = P+[1,0];
            Conrod : 'conrod.gif' P = [0,50] 
                                  Q = P+[100,50]; // lies horizontally in gif";

            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));
            Assert.IsNotNull(script);
            Assert.IsNotNull(script.Config);
            Assert.AreEqual(12, script.Config.FramesPerTimeunit);
            Assert.AreEqual(4, script.Things.Count());

        }

        [Test]
        public void TestParseAndBuildSliderCrank() {
            const string s = @".config(12,      // number of  frames per time unit
                                       1, 1);   // width/height of output
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+[0,30];
            Piston : 'piston.gif' P = [30,50] Q = P+[1,0];
            Conrod : 'conrod.gif' P = [0,50] 
                                  Q = P+[100,50]; // lies horizontally in gif

            // Placing all the parts
            @+10.0    Engine.P = [20,80]; Engine.Q = Engine.P + [_,0];
            @+02.0    Crank.P = Engine.P + [200,70]; 
                      Crank.Q = Crank.P + [_,0];
            @+02.0    Conrod.P = Crank.Q; Conrod.Q = [x, 70]; x < Crank.P.x;
            @+02.0    Piston.P = Conrod.Q;

            // Let's move it!
            @+02.0    Crank.Q = Crank.P + [_,0].rotate(180 * t);

            @+08.0   b = -180 / 6; // angular acceleration
                     w = .integral(b) + 180;
                     Crank.Q = Crank.P + [_,0].rotate(w * t);

            @+06.0   w = 0;";

            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));

            Assert.IsNotNull(script);
            Assert.IsNotNull(script.Config);
            Assert.AreEqual(12, script.Config.FramesPerTimeunit);
            Assert.AreEqual(4, script.Things.Count());

            Assert.AreEqual(7, script.Steps.Count());
            CollectionAssert.AreEqual(new[] { 10.0, 12.0, 14.0, 16.0, 18.0, 26.0, 32.0 }, script.Steps.Select(st => st.Time).ToArray());

            Assert.AreEqual(2       // 1 rigid body constraint pair
                            + 1     // 1 constraint ZERO = 0
                            + 2     // 2 2d constraints
                            + 2     // 2 constraints specified in step 0
                            , script.Steps.ElementAt(0).Constraints.Count());

            Assert.AreEqual(2       // 1 rigid body constraint pair
                            + 2     // 2 2d constraints
                            + 2     // 2 constraints specified in step 1
                            , script.Steps.ElementAt(1).Constraints.Count());

            Assert.AreEqual(2       // 1 rigid body constraint pair
                            + 2     // 2 2d constraints
                            + 3     // 3 constraints specified in step 2
                            , script.Steps.ElementAt(2).Constraints.Count());

            Assert.AreEqual(2       // 1 rigid body constraint pair
                            + 2     // 2 2d constraints
                            + 1     // 1 constraint specified in step 3
                            , script.Steps.ElementAt(3).Constraints.Count());

            Assert.AreEqual(1       // 1 constraint specified in step 4
                            , script.Steps.ElementAt(4).Constraints.Count());

            Assert.AreEqual(3       // 3 constraints specified in step 5
                            , script.Steps.ElementAt(5).Constraints.Count());

            Assert.AreEqual(1       // 1 constraint specified in step 6
                            , script.Steps.ElementAt(6).Constraints.Count());

        }


        private static MovimentumParser CreateParserForSyntacticTests(string scriptSnippet) {
            ICharStream chars = new ANTLRStringStream(scriptSnippet);
            MovimentumLexer lexer = new MovimentumLexer(chars);
            ITokenStream tokens = new CommonTokenStream(lexer);
            MovimentumParser parser = new TestMovimentumParser(tokens);
            return parser;
        }
        [Test]
        public void Test_number() {
            Assert.AreEqual(1.0, CreateParserForSyntacticTests("1").number(), 1e-10);
            Assert.AreEqual(1.0, CreateParserForSyntacticTests("1.").number(), 1e-10);
            Assert.AreEqual(10.0, CreateParserForSyntacticTests("1.E1").number(), 1e-10);
            Assert.AreEqual(0.01, CreateParserForSyntacticTests("1.E-2").number(), 1e-10);
            Assert.AreEqual(10.1, CreateParserForSyntacticTests("1.01E1").number(), 1e-10);
            Assert.AreEqual(0.010001, CreateParserForSyntacticTests("1.0001E-2").number(), 1e-10);
        }

        [Test]
        public void TestJustParse_scalarexpr() {
            CreateParserForSyntacticTests("a+b ;").scalarexpr();
            CreateParserForSyntacticTests("a+b*c ;").scalarexpr();
            CreateParserForSyntacticTests("(a-b)*c ;").scalarexpr();
            CreateParserForSyntacticTests("a+b*-c*d ;").scalarexpr();
            CreateParserForSyntacticTests("(a-b*c)/-(-d) ;").scalarexpr();
            CreateParserForSyntacticTests(".i(a+2*b) ;").scalarexpr();
            CreateParserForSyntacticTests("(3+.d(-a-b*2))-d ;").scalarexpr();
            CreateParserForSyntacticTests("-.a([0,1],[1,1]) ;").scalarexpr();
            CreateParserForSyntacticTests("_+_ ;").scalarexpr();
            CreateParserForSyntacticTests("3*.t ;").scalarexpr();
            CreateParserForSyntacticTests("(.iv-.t)/2 ;").scalarexpr();
            // Exactly the same string is also allowed as a vectorexpr - see below.
            CreateParserForSyntacticTests("((_-_)+_) ;").scalarexpr();
            CreateParserForSyntacticTests("(((_-_)+_).x) ;").scalarexpr();
        }

        [Test]
        public void TestCheck_scalarexpr() {
            {
                ScalarExpr x = CreateParserForSyntacticTests("a+b ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.PLUS, new ScalarVariable("b"))
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("a+b*c ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.PLUS,
                                                     new BinaryScalarExpr(new ScalarVariable("b"), BinaryScalarOperator.TIMES, new ScalarVariable("c")))
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("(a-b)*c ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.MINUS, new ScalarVariable("b")),
                                                     BinaryScalarOperator.TIMES,
                                                     new ScalarVariable("c"))
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("a+b*-c/d ;").scalarexpr();
                Assert.AreEqual(
                    new BinaryScalarExpr(new ScalarVariable("a"),
                        BinaryScalarOperator.PLUS,
                        new BinaryScalarExpr(
                            new BinaryScalarExpr(new ScalarVariable("b"),
                                BinaryScalarOperator.TIMES,
                                new UnaryScalarExpr(UnaryScalarOperator.MINUS,
                                    new ScalarVariable("c")
                                )
                            ),
                            BinaryScalarOperator.DIVIDE,
                            new ScalarVariable("d")
                        )
                    ), x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests(".i(a+2*b) ;").scalarexpr();
                Assert.AreEqual(new UnaryScalarExpr(UnaryScalarOperator.INTEGRAL, new BinaryScalarExpr(new ScalarVariable("a"), BinaryScalarOperator.PLUS,
                                                     new BinaryScalarExpr(new ConstScalar(2.0), BinaryScalarOperator.TIMES, new ScalarVariable("b")))), x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("-.a([0,1],[1,1]) ;").scalarexpr();
                Assert.AreEqual(new UnaryScalarExpr(UnaryScalarOperator.MINUS,
                        new BinaryVectorScalarExpr(new Vector(new ConstScalar(0), new ConstScalar(1), new ConstScalar(0)),
                        BinaryVectorScalarOperator.ANGLE,
                        new Vector(new ConstScalar(1), new ConstScalar(1), new ConstScalar(0))))
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("_+_ ;").scalarexpr();
                var b = (BinaryScalarExpr)x;
                var lhs = (ScalarVariable)b.Lhs;
                var rhs = (ScalarVariable)b.Rhs;
                Assert.AreNotEqual(lhs.Name, rhs.Name);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("3*.t ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(new ConstScalar(3), BinaryScalarOperator.TIMES, new T())
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("(.iv-.t)/2 ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(
                    new BinaryScalarExpr(new IV(), BinaryScalarOperator.MINUS, new T()), BinaryScalarOperator.DIVIDE, new ConstScalar(2))
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("((_-_)+_) ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(new BinaryScalarExpr(new ScalarVariable("_0"), BinaryScalarOperator.MINUS, new ScalarVariable("_1")),
                                                     BinaryScalarOperator.PLUS,
                                                     new ScalarVariable("_2"))
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("(((_-_)+_).x) ;").scalarexpr();
                Assert.AreEqual(
                    new UnaryVectorScalarExpr(
                    new BinaryVectorExpr(new BinaryVectorExpr(new VectorVariable("#0"), BinaryVectorOperator.MINUS, new VectorVariable("#1")),
                                                     BinaryVectorOperator.PLUS,
                                                     new VectorVariable("#2")),
                                                     UnaryVectorScalarOperator.X)
                    , x);
            }
        }

        [Test]
        public void TestJustParse_vectorexpr() {
            CreateParserForSyntacticTests("a.P+b.Q ;").vectorexpr();
            CreateParserForSyntacticTests("(-a.P-b.Q).r(1).r(.a([0,1],[1,1])) ;").vectorexpr();
            CreateParserForSyntacticTests(".i(a.P+b.Q*c) ;").vectorexpr();
            CreateParserForSyntacticTests("([3,4]+.d(-a.P-b.R*2))-d.S ;").vectorexpr();

            // I now allow vector variables.
            CreateParserForSyntacticTests("((a-b)+c) ;").vectorexpr();
            CreateParserForSyntacticTests("((_-_)+_) ;").vectorexpr();
            // A vector expression with trailing .x is a scalarexpression!
            Assert.Throws<Exception>(() => CreateParserForSyntacticTests("(((_-_)+_).x) ;").vectorexpr());
        }


        [Test]
        public void TestCheck_vectorexpr() {
            {
                VectorExpr x = CreateParserForSyntacticTests("a.P+b.Q ;").vectorexpr();
                Assert.AreEqual(
                    new BinaryVectorExpr(new Anchor("a", "P"), BinaryVectorOperator.PLUS, new Anchor("b", "Q"))
                    , x);
            }
            {
                VectorExpr x = CreateParserForSyntacticTests("(-a.P-b.Q).r(1).r(.a([0,1,2],[1,1,1])) ;").vectorexpr();
                Assert.AreEqual(
                    new BinaryScalarVectorExpr(
                        new BinaryScalarVectorExpr(
                            new BinaryVectorExpr(
                                new UnaryVectorExpr(UnaryVectorOperator.MINUS, new Anchor("a", "P")),
                                BinaryVectorOperator.MINUS,
                                new Anchor("b", "Q")
                            ),
                            BinaryScalarVectorOperator.ROTATE2D,
                            new ConstScalar(1)
                            ),
                        BinaryScalarVectorOperator.ROTATE2D,
                        new BinaryVectorScalarExpr(
                            new Vector(new ConstScalar(0), new ConstScalar(1), new ConstScalar(2)),
                            BinaryVectorScalarOperator.ANGLE,
                            new Vector(new ConstScalar(1), new ConstScalar(1), new ConstScalar(1)))
                        )
                    , x);
            }
            //{
            //VectorExpr x = CreateParserForSyntacticTests(".i(a.P+b.Q*c) ;").vectorexpr();
            //    Assert.AreEqual(___, x);
            //}
            //{
            //VectorExpr x = CreateParserForSyntacticTests("([3,4]+.d(-a.P-b.R*2))-d.S ;").vectorexpr();
            //    Assert.AreEqual(___, x);
            //}
            {
                VectorExpr x = CreateParserForSyntacticTests("((_-_)+_) ;").vectorexpr();
                Assert.AreEqual(new BinaryVectorExpr(new BinaryVectorExpr(new VectorVariable("#0"), BinaryVectorOperator.MINUS, new VectorVariable("#1")),
                                                     BinaryVectorOperator.PLUS,
                                                     new VectorVariable("#2"))
                    , x);
            }
        }

        [Test]
        public void TestWrongOrderOfAcnhorsInSetup() {
            CreateParserForSyntacticTests("Engine : 'engine.gif' P = [30,50] Q = P+[1,0];").thingdefinition();
            Assert.Throws<Exception>(() =>
                CreateParserForSyntacticTests("Engine : 'engine.gif' Q = P+[1,0] P = [30,50];").thingdefinition()
            );
        }

        #endregion Model building
    }
}
