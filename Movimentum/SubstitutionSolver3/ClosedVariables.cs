namespace Movimentum.SubstitutionSolver3 {
    public abstract class AbstractClosedVariable {
        private readonly Variable _variable;

        protected AbstractClosedVariable(Variable variable) {
            _variable = variable;
        }

        public Variable Variable { get { return _variable; } }
    }

    public class VariableWithValue : AbstractClosedVariable {
        private readonly double _value;

        public VariableWithValue(Variable variable, double value)
            : base(variable) {
            _value = value;
        }

        public double Value { get { return _value; } }

        public override string ToString() {
            return Variable.Name + ":=" + Value;
        }
    }

    public class VariableWithBacksubstitution : AbstractClosedVariable {
        public readonly AbstractExpr Expr;

        public VariableWithBacksubstitution(Variable variable, AbstractExpr expr)
            : base(variable) {
            Expr = expr;
        }

        public override string ToString() {
            return Variable.Name + ":=" + Expr;
        }
    }


}