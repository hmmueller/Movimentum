namespace Movimentum.Model {
    public abstract class AbstractOperator {
        private readonly int _precedence;
        private readonly string _asString;

        private class DEBUG_OP_CLASS : AbstractOperator {
            public DEBUG_OP_CLASS() : base(0, "DEBUG_OP") { }
        }

        internal static AbstractOperator IGNORE_OP = new DEBUG_OP_CLASS();

        protected AbstractOperator(int precedence, string asString) {
            _precedence = precedence;
            _asString = asString;
        }

        public int Precedence { get { return _precedence; }}
        public override string ToString() { return _asString; }

        public string Wrap(Expr lhs, AbstractOperator op, Expr rhs) {
            string s = lhs.ToString(op) + op + rhs.ToString(op);
            return op.Precedence < Precedence ? "(" + s + ")" : s;
        }

        public string Wrap(AbstractOperator op, Expr e) {
            string s = e.ToString(op) + op;
            return op.Precedence < Precedence ? "(" + s + ")" : s;
        }
    }
}