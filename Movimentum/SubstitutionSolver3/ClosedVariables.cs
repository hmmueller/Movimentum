namespace Movimentum.SubstitutionSolver3 {
    public abstract class AbstractClosedVariable {
        private readonly IVariable _variable;

        protected AbstractClosedVariable(IVariable variable) {
            _variable = variable;
        }

        public IVariable Variable { get { return _variable; } }
    }

    public class VariableWithValue : AbstractClosedVariable {
        private readonly double _value;

        public VariableWithValue(IVariable variable, double value)
            : base(variable) {
            _value = value;
        }

        public double Value { get { return _value; } }

        public override string ToString() {
            return Variable.Name + ":=" + Value;
        }
    }

    public class VariableWithBacksubstitution : AbstractClosedVariable {
        public readonly IAbstractExpr Expr;

        public VariableWithBacksubstitution(IVariable variable, IAbstractExpr expr)
            : base(variable) {
            Expr = expr;
        }

        public override string ToString() {
            return Variable.Name + ":=" + Expr;
        }
    }


}