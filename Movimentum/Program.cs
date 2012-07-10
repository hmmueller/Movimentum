using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Antlr.Runtime;
using Movimentum.Lexer;
using Movimentum.Model;
using Movimentum.Parser;
using Movimentum.SubstitutionSolver3;

namespace Movimentum {
    class Program {
        static void Main(string[] args) {
            string scriptText;
            using (StreamReader sr = new StreamReader(args[0])) {
                scriptText = sr.ReadToEnd();
            }
            Script script = Parse(scriptText);
            Interpret(script, args[1]);
        }

        internal static Script Parse(string text, Func<ITokenStream, MovimentumParser> createParser = null) {
            ICharStream chars = new ANTLRStringStream(text);
            MovimentumLexer lexer = new MovimentumLexer(chars);
            ITokenStream tokens = new CommonTokenStream(lexer);
            MovimentumParser parser = createParser == null ? new MovimentumParser(tokens) : createParser(tokens);
            Script script = parser.script();
            //if (parser.Errors.Count > 0) {
            //    throw new InvalidOperationException("Parse-Fehler: " + string.Join("\r\n", parser.Errors.ToArray()));
            //}

            script.AddRigidBodyAnd2DConstraints();

            return script;
        }

        internal static void Interpret(Script script, string prefix) {
            IEnumerable<Frame> frames = script.CreateFrames();

            double range;
            {
                double maxDist = (script.Config.Width + script.Config.Height) * 10;
                range = maxDist * maxDist;
            }

            IDictionary<Variable, VariableValueRestriction> previousState = new Dictionary<Variable, VariableValueRestriction>();

            foreach (var f in frames) {
                var bitmap = new Bitmap(script.Config.Width, script.Config.Height);
                Graphics drawingPane = Graphics.FromImage(bitmap);

                // Compute locations for each anchor of each thing.
                IDictionary<string, IDictionary<string, ConstVector>> anchorLocations = f.SolveConstraints(range, ref previousState);
                foreach (var th in script.Things) {
                    th.Draw(drawingPane, anchorLocations[th.Name]);
                }

                bitmap.Save(string.Format("{0}{1:000000}.jpg", prefix, f.FrameNo), ImageFormat.Jpeg);
            }
        }
    }
}
