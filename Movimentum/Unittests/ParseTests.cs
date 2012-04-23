using System;
using System.Drawing;
using System.Linq;
using Antlr.Runtime;
using Movimentum.Lexer;
using Movimentum.Model;
using Movimentum.Parser;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class ParseTests {
        #region Script parsing

        [Test]
        public void TestParseSliderCrankSetup() {
            const string s = @".config(12); // number of  frames per time unit
        
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
            const string s = @".config(12); // number of  frames per time unit
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+...";

            Program.Parse(s, tokens => new TestMovimentumParser(tokens));
        }

        [Test]
        public void TestParseSliderCrankScript() {
            const string s = @".config(12); // number of  frames per time unit
        
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

        #endregion Script parsing

        #region Model building

        [Test]
        public void TestParseAndBuildSliderCrankSetup() {
            const string s = @".config(12); // number of  frames per time unit
        
            Engine : 'engine.gif' P = [30,50] Q = P+[1,0];
            Crank  : 'crank.gif'  P = [20,20] Q = P+[0,30];
            Piston : 'piston.gif' P = [30,50] Q = P+[1,0];
            Conrod : 'conrod.gif' P = [0,50] 
                                  Q = P+[100,50]; // lies horizontally in gif
        ";

            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));
            Assert.IsNotNull(script);
            Assert.IsNotNull(script.Config);
            Assert.AreEqual(12, script.Config.FramesPerTimeunit);
            Assert.AreEqual(4, script.Things.Count());

        }

        [Test]
        public void TestParseAndBuildSliderCrank() {
            const string s = @".config(12); // number of  frames per time unit
        
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

            Step firstStep = script.Steps.First(st => st.Time == 10);
            Assert.AreEqual(4*2 + 2, firstStep.Constraints.Count());
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
                                                     new BinaryScalarExpr(new Constant(2.0), BinaryScalarOperator.TIMES, new ScalarVariable("b")))), x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("-.a([0,1],[1,1]) ;").scalarexpr();
                Assert.AreEqual(new UnaryScalarExpr(UnaryScalarOperator.MINUS,
                        new BinaryScalarVectorExpr(new Vector(new Constant(0), new Constant(1), new Constant(0)), 
                        BinaryScalarVectorOperator.ANGLE,
                        new Vector(new Constant(1), new Constant(1), new Constant(0))))
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
                Assert.AreEqual(new BinaryScalarExpr(new Constant(3), BinaryScalarOperator.TIMES, new T())
                    , x);
            }
            {
                ScalarExpr x = CreateParserForSyntacticTests("(.iv-.t)/2 ;").scalarexpr();
                Assert.AreEqual(new BinaryScalarExpr(
                    new BinaryScalarExpr(new IV(), BinaryScalarOperator.MINUS, new T()), BinaryScalarOperator.DIVIDE, new Constant(2))
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
                    new UnaryScalarVectorExpr(
                    new BinaryVectorExpr(new BinaryVectorExpr(new VectorVariable("#0"), BinaryVectorOperator.MINUS, new VectorVariable("#1")),
                                                     BinaryVectorOperator.PLUS,
                                                     new VectorVariable("#2")),
                                                     UnaryScalarVectorOperator.X)
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
                    new VectorScalarExpr(
                        new VectorScalarExpr(
                            new BinaryVectorExpr(
                                new UnaryVectorExpr(UnaryVectorOperator.MINUS, new Anchor("a", "P")),
                                BinaryVectorOperator.MINUS,
                                new Anchor("b", "Q")
                            ),
                            VectorScalarOperator.ROTATE,
                            new Constant(1)
                            ),
                        VectorScalarOperator.ROTATE,
                        new BinaryScalarVectorExpr(
                            new Vector(new Constant(0), new Constant(1), new Constant(2)), 
                            BinaryScalarVectorOperator.ANGLE,
                            new Vector(new Constant(1), new Constant(1), new Constant(1)))
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
        public void TestJustParse_setup() {
            CreateParserForSyntacticTests("Engine : 'engine.gif' P = [30,50] Q = P+[1,0];").thingdefinition();
            Assert.Throws<Exception>(() =>
                CreateParserForSyntacticTests("Engine : 'engine.gif' Q = P+[1,0] P = [30,50];").thingdefinition()
            );
        }

        #endregion Model building
    }

    internal class TestMovimentumParser : MovimentumParser {
        public TestMovimentumParser(ITokenStream tokens)
            : base(tokens) {
            // empty
        }

        protected override Image ImageFromFile(string filename) {
            return default(Image);
        }
    }
}
