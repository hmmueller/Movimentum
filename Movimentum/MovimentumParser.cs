using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Movimentum.Model;

namespace Movimentum.Parser {
    public partial class MovimentumParser {
        public override void EmitErrorMessage(string msg) {
            throw new /*Recognition*/Exception(msg);
        }

        private double _currentTime = 0;
        private int _anonymousVarCt = 0;

        private ConstVector ConstAdd(
                int lineNo,
                IEnumerable<ConstAnchor> defs,
                string lhsName,
                ConstVector rhs,
                bool plus) {
            ConstAnchor lhsAnchor = defs.FirstOrDefault(a => a.Name == lhsName);
            if (lhsAnchor == null) {
                throw new Exception(string.Format(
                    "Line {0}: Anchor {1} not yet defined",
                    lineNo, lhsName));
            }
            ConstVector lhs = lhsAnchor.Location;
            if (lhs.Is2D() && rhs.Is2D()) {
                return plus
                    ? new ConstVector(lhs.X + rhs.X, lhs.Y + rhs.Y)
                    : new ConstVector(lhs.X - rhs.X, lhs.Y - rhs.Y);
            } else {
                return plus
                    ? new ConstVector(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z)
                    : new ConstVector(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
            }
        }

        protected virtual Image ImageFromFile(string filename) {
            return Image.FromFile(filename);
        }
    }
}
