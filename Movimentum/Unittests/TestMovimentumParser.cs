using System.Drawing;
using Antlr.Runtime;
using Movimentum.Parser;

namespace Movimentum.Unittests {
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