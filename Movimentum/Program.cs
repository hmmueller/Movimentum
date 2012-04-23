using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Movimentum.Lexer;
using Movimentum.Model;
using Movimentum.Parser;

namespace Movimentum {
    class Program {
        static void Main(string[] args) {
            string scriptText; 
            using (StreamReader sr = new StreamReader(args[0])) {
                scriptText = sr.ReadToEnd();
            }
            Script script = Parse(scriptText);
            Interpret(script);
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
            return script;
        }

        private static void Interpret(Script script) {
            

            throw new NotImplementedException();
        }

    }
}
