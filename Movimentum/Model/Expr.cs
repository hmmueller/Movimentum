namespace Movimentum.Model {
    /// <summary>
    /// Base class for expressions that supports ToString() methods.
    /// </summary>
    public abstract class Expr {
        public override string ToString() {
            return ToString(AbstractOperator.IGNORE_OP);
        }

        protected internal abstract string ToString(AbstractOperator parentOp);
    }
}