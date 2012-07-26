using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Movimentum.Model;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    internal class TestModelsWithFrames {
        [Test]
        public void TestThatFramesContainCorrectTimesAndSomeConstraints() {
            const string s = @"
            .config(1, 1, 1);

            @10.0     a = 1;
            // Frame at 10: T = 10, t = 0, iv = 1; a
            @11.0     b = 1;
            // Frame at 11: T = 11, t = 0, iv = 1.3;  a, b
            // Frame at 12: T = 12, t = 1, iv = 1.3;  a, b
            @12.3     c = 1;
            @12.5     d = 1;
            @12.7     e = 1;
            // Frame at 13: T = 13, t = 0.3, iv = 0.8;  a, b, c, d, e
            @13.5     f = 1;
            // Frame at 14: T = 14, t = 0.5, iv = 2;  a, b, c, d, e, f
            // Frame at 15: T = 15, t = 1.5, iv = 2;  a, b, c, d, e, f
            @15.5     g = 1;
            @16.0     h = 1;
            // Frame at 16: T = 16, t = 0, iv = 0;  a, b, c, d, e, f, g, h
            ";
            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));

            Frame[] frames = script.CreateFrames().ToArray();

            Assert.AreEqual(7, frames.Length);
            AssertFrame(frames[0], 10, 0, 1, "a 1");
            AssertFrame(frames[1], 11, 0, 1.3, "a 1", "b 1");
            AssertFrame(frames[2], 12, 1, 1.3, "a 1", "b 1");
            AssertFrame(frames[3], 13, 0.3, 0.8, "a 1", "b 1", "c 1", "d 1", "e 1");
            AssertFrame(frames[4], 14, 0.5, 2, "a 1", "b 1", "c 1", "d 1", "e 1", "f 1");
            AssertFrame(frames[5], 15, 1.5, 2, "a 1", "b 1", "c 1", "d 1", "e 1", "f 1");
            AssertFrame(frames[6], 16, 0, 0, "a 1", "b 1", "c 1", "d 1", "e 1", "f 1", "g 1", "h 1");
        }

        private void AssertFrame(Frame f, double absoluteTime, double t, double iv, params string[] constraintChecks) {
            // We 
            Assert.AreEqual(absoluteTime, f.AbsoluteTime, 1e-9);
            Assert.AreEqual(t, f.T, 1e-9);
            Assert.AreEqual(iv, f.IV, 1e-9);
            {
                int checkSum = constraintChecks.Sum(c => c.Split(' ').Length - 1);
                Assert.AreEqual(checkSum, f.Constraints.Count());
            }
            {
                foreach (var check in constraintChecks) {
                    string[] checkParts = check.Split();
                    string key = checkParts[0];
                    foreach (var constraintPattern in checkParts.Skip(1)) {
                        AssertExists(f.Constraints, key, constraintPattern);
                    }
                }
            }
        }

        // ReSharper disable UnusedParameter.Local
        // constraints is intentionally used only in Assert.
        private void AssertExists(IEnumerable<Constraint> constraints, string key, string constraintPattern) {
            // ReSharper restore UnusedParameter.Local
            int constant;
            ScalarEqualityConstraint compare = int.TryParse(constraintPattern, out constant)
                                                   ? new ScalarEqualityConstraint(key, new ConstScalar(constant))
                                                   : new ScalarEqualityConstraint(key, new ScalarVariable(constraintPattern));
            Assert.IsTrue(constraints.Any(c => c.Equals(compare)), "No constraint found with key {0} and rhs {1}", key, compare);
        }

        [Test]
        public void TestThatConstraintsChangeCorrectly() {
            const string s = @"
            .config(1, 1, 1);

            @10.0     a = 1;
                      a = a1;
                      b = 9;
            // Frame at 10: T = 10, t = 0, iv = 1.3;  a (1, a1), b (9)
            // Frame at 11: T = 11, t = 1, iv = 1.3;  a (1, a1), b (9)
            @11.3     a = 2;
                      a = a2;
            @11.6     a = 3;
                      a = a3;
            // Frame at 12: T = 12, t = 0.4, iv = 1.9;  a (3, a3), b (9)
            // Frame at 13: T = 13, t = 1.4, iv = 1.9;  a (3, a3), b (9)
            @13.5     b = 8;
                      b = b8;
                      a = 4;
                      a = a4;
            // Frame at 14: T = 14, t = 0.5, iv = 2;  a (4, a4), b (8, b8)
            // Frame at 15: T = 15, t = 1.5, iv = 2;  a (4, a4), b (8)
            @15.5     b = 7;
            // Frame at 16: T = 16, t = 0.5, iv = 1.5;  a (4, a4), b (7)
            @17.0     c = 1;
            // Frame at 17: T = 17, t = 0, iv = 0;  a (4, a4), b (7), c (1)
            ";
            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));

            Frame[] frames = script.CreateFrames().ToArray();

            Assert.AreEqual(8, frames.Length);
            AssertFrame(frames[0], 10, 0, 1.3, "a 1 a1", "b 9");
            AssertFrame(frames[1], 11, 1, 1.3, "a 1 a1", "b 9");
            AssertFrame(frames[2], 12, 0.4, 1.9, "a 3 a3", "b 9");
            AssertFrame(frames[3], 13, 1.4, 1.9, "a 3 a3", "b 9");
            AssertFrame(frames[4], 14, 0.5, 2, "a 4 a4", "b 8 b8");
            AssertFrame(frames[5], 15, 1.5, 2, "a 4 a4", "b 8 b8");
            AssertFrame(frames[6], 16, 0.5, 1.5, "a 4 a4", "b 7");
            AssertFrame(frames[7], 17, 0, 0, "a 4 a4", "b 7", "c 1");
        }

        [Test]
        public void TestSingleStepCreatesNoFrame() {
            const string s = @"
                .config(1, 1, 1);
                @10.0     a = 1;";

            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));

            Frame[] frames = script.CreateFrames().ToArray();

            Assert.AreEqual(0, frames.Length);
        }

        [Test]
        public void TestZeroStepsCreateNoFrame() {
            const string s = ".config(1, 1, 1);";
            Script script = Program.Parse(s, tokens => new TestMovimentumParser(tokens));

            Frame[] frames = script.CreateFrames().ToArray();

            Assert.AreEqual(0, frames.Length);
        }

        [Test]
        public void TestRunSimpleScript() {
            const string s =
                @".config (10, 200, 200);

                B1 : .bar P = [0,0] Q = [10,10];

                @02		B1.P = [10 + .t, 10 + .t];
                		B1.Q = [20 + .t, 20 + .t];
                @10";
            Script script = Program.Parse(s);
            Program.Interpret(script, Path.Combine(Path.GetTempPath(), "F_"));
        }

        [Test]
        public void TestInfinity() {
            Assert.IsTrue(3 > double.NegativeInfinity);
            Assert.IsTrue(3 < double.PositiveInfinity);
            Assert.IsTrue(3 <= double.PositiveInfinity);
            Assert.IsFalse(3 > double.PositiveInfinity);
        }

        private string MkTestDir() {
            string dir = Path.Combine(Path.GetTempPath(), "MovimentumTest");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, "F_");
        }

        [Test]
        public void TestMovingHockeyStick() {
            const string s =
                @".config (20, 400, 300);
            B : .bar P = [0,0] Q = [5,5] R = [5,30];

            @0  B.P = [20 + 20*.t, 30 + 20*.t];
                B.Q = [25 + 20*.t, 35 + 20*.t];
                B.R = [25 + 20*.t, 60 + 20*.t];
            @5  B.P = [20 + 35*.t - 75, 30 + 20*5];
                B.Q = [25 + 35*.t - 75, 35 + 20*5];
                B.R = [25 + 35*.t - 75, 60 + 20*5];
            @10";
            Script script = Program.Parse(s);

            string prefix = MkTestDir();
            int frameCount = Program.Interpret(script, prefix);
            Assert.AreEqual(201, frameCount);
        }

        [Test]
        public void TestRotatingBar() {
            const string s =
                @".config(8, 600, 400);
                WH : .bar C = [0,0] P = [-60,80];
                // length of bar: 100

                @0
	                WH.C 	= [200,200];
	                WH.P 	= WH.C + [len,0].r(360*.t + 22.5); 
                    len > 0;
                @2";
            Script script = Program.Parse(s);
            //script.DebugAddExpectedResult(1, new Dictionary<string, double> { { "WH.P.Y", 238.268343236509 }, { "len", 100 } });

            string prefix = MkTestDir();
            int frameCount = Program.Interpret(script, prefix);
            Assert.AreEqual(17, frameCount);
        }

        [Test]
        public void TestRotatingBarCoordinates() {
            const string s =
                @".config(8, 600, 400);
                WH : .bar C = [0,0] P = [-60,80];
                // length of bar: 100

                @0
	                WH.C 	= [200,200];
	                WH.P 	= WH.C + [len,0].r(360*.t + 22.5); 
                    len       > 0;
                @2";
            Script script = Program.Parse(s);
            //script.DebugAddExpectedResult(1, new Dictionary<string, double> { { "WH.P.Y", 238.268343236509 }, { "len", 10 } });

            IEnumerable<Frame> frames = script.CreateFrames();

            IDictionary<IVariable, VariableWithValue> previousState = new Dictionary<IVariable, VariableWithValue>();

            foreach (var f in frames) {
                IDictionary<string, IDictionary<string, ConstVector>> anchorLocations = f.SolveConstraints(10000, ref previousState);
                //double x = previousState[Polynomial.CreateNamedVariable("x")].Value;
                //Assert.AreEqual(10, x, 1e-2);

                ConstVector whcLocation = anchorLocations["WH"]["C"];
                Assert.AreEqual(200, whcLocation.X, 1e-2);
                Assert.AreEqual(200, whcLocation.Y, 1e-2);
                Assert.AreEqual(0, whcLocation.Z, 1e-2);

                double alpha = f.FrameNo / 8.0 * 2 * Math.PI - Math.PI / 8;

                ConstVector whpLocation = anchorLocations["WH"]["P"];
                double expectedX = 200 + 100 * Math.Cos(alpha);
                double expectedY = 200 + 100 * Math.Sin(alpha);
                Debug.WriteLine("expX=" + expectedX + ", expY=" + expectedY);
                Assert.AreEqual(expectedX, whpLocation.X, 1e-2);
                Assert.AreEqual(expectedY, whpLocation.Y, 1e-2);
                Assert.AreEqual(0, whpLocation.Z, 1e-2);
            }
        }

        [Test]
        public void TestRotatingTriangleSimpler() {
            const string s =
                @".config(8, 600, 400);
                WH : .bar C = [0,0] P = [-60,80] Q = [80,60];

                @0
	                WH.C 	= [200,200];
	                WH.P 	= WH.C + [10*x,0].r(360*.t); 
                    x       > 0;
                @2";
            Script script = Program.Parse(s);

            script.DebugAddExpectedResult(1, new Dictionary<string, double> {
                { "WH.P.X", 300 }, 
                { "WH.P.Y", 200 }, 
                { "WH.Q.X", 200 }, 
                { "WH.Q.Y", 100 }, 
                { "x", 10 }
            });

            int frameCount = Program.Interpret(script, Path.Combine(Path.GetTempPath(), "F_"));
            Assert.AreEqual(17, frameCount);
        }

        [Test]
        public void TestRotatingTriangle() {
            const string s =
                @".config(8, 600, 400);
                WH : .bar C = [0,0] P = [-60,80] Q = [80,60];

                @0
	                WH.C 	= [200,200];
	                WH.P 	= WH.C + [10*x,0].r(360*.t + 22.5); 
                    x       > 0;
                @2";
            Script script = Program.Parse(s);

            script.DebugAddExpectedResult(1, new Dictionary<string, double> {
                //{ "WH.P.X", 292.387953251129 }, 
                //{ "WH.P.Y", 238.268343236509 }, 
                //{ "WH.Q.X", 238.268343236509 }, 
                { "WH.Q.Y", 107.612046748871 }, 
                { "x", 10 }
            });

            int frameCount = Program.Interpret(script, Path.Combine(Path.GetTempPath(), "F_"));
            Assert.AreEqual(17, frameCount);
        }

        [Test, Ignore("Not yet there")]
        public void TestWalschaertsHeusinger() {
            const string s = @".config(10, 600, 400);

                    // see http://en.wikipedia.org/wiki/Walschaerts_valve_gear, 
                    //     http://www.illisoft.net/trains/books/images/how_steam_2.jpg
                    //     http://www.dampf-hobby.de/html/steuerungen.html
                    //
                    // reversing rod (reach rod) (3)
                    ER	:	.bar L = [0,0]	R = [30,0]; 
                    // lifting lever (5, 6)
                    LL	:	.bar L = [0,10] C = [0,0] R = [10,0]	;
                    // expansion link (7) 
                    EL	:	.bar U = [0,20] C = [0,10] L = [0,0]	;
                    // radius rod (8)
                    RR	: .bar L = [0,0] E = [5,0] R = [30,0]	;
                    // combination lever (12)
                    CL	:	.bar U = [0,30] C = [0,25] L = [0,0]	;
                    // valve spindle (13)
                    VS	:	.bar L = [0,0] R = [20,0] R2 = [20,5] R3 = [30,5] R4 = [30,0] 	;
                    // union link (11) 
                    UL	:	.bar L = [0,0] R = [15,0]	;
                    // piston rod 
                    PR	:	.bar C = [-5,0] L = [0,0] R = [40,0] R2 = [40,10] R3 = [40,-10]	;
                    // connecting rod
                    CR	:	.bar L = [0,0] R = [50,0]	;
                    // wheel with crank and return crank (1)
                    WH	:	.bar C = [0,0] P = [0,-20] R = [0,5]	;
                    // return crank rod (2)
                    CR	:	.bar L = [0,0] R = [30,0]	;

                    @0
	                    ER.L = ER.R - [_,0];
	                    ER.R = LL.L;
	                    LL.C = [100,200];
	                    LL.R = RR.L;
	                    RR.E = EL.C + (EL.L - EL.U) * _;
	                    EL.C = [120,180];
	                    RR.R = CL.U;
	                    CL.C = VS.L;
	                    VS.R = VS.L + [_,0];
	                    CL.L = UL.R;
	                    UL.L = PR.C;
	                    PR.R = PR.L + [_,0];
	                    CR.R = PR.L;
	                    CR.L = WH.P;
	                    WH.R = CR.L;
	                    CR.R = EL.L;
                        WH.C = [70,100];
                        WH.P = [0,1].r(360*.t/4);
                    @20";
            Script script = Program.Parse(s);
            Program.Interpret(script, Path.Combine(Path.GetTempPath(), "F_"));
        }

        [Test, Ignore("Not yet there")]
        public void TestMaximum() {
            const string s =
                @".config(10, 600, 400);
                WH : .bar C = [0,0] P = [-10,20] Q = [10,20];
                B  : .bar L = [0,0] R = [200,0];

                @0
	                WH.C 	= [100,100];
	                WH.P 	= WH.C + [_,0].r(360*.t/4);
                    B.L	 	= B.R + [_,0];
                    B.L		= [50,y];
                    y		= ({ WH.P.y   : WH.Q.y
                                 > WH.Q.y : WH.P.y
                               });
                @20";
            Script script = Program.Parse(s);
            Program.Interpret(script, Path.Combine(Path.GetTempPath(), "F_"));
        }
    }
}
