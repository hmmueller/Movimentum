using System.Collections.Generic;
using System.Linq;
using Movimentum.Model;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    internal class TestsWithFrames {
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
                                                   ? new ScalarEqualityConstraint(key, new Constant(constant))
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
            Program.Interpret(script);

        }
    }
}
