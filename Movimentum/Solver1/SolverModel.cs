using Movimentum.Model;

namespace Movimentum.Solver1 {
    public abstract class AbstractExpr { }

    public abstract class AbstractOperator {}

    public class Constant : AbstractExpr {
        private readonly double _value;
        public Constant(double value) {
            _value = value;
        }

        public double Value { get { return _value; } }
    }

    public abstract class Variable : AbstractExpr {
        private readonly string _name;

        protected Variable(string name) {
            _name = name;
        }
    }

    public class NamedVariable : Variable {
        public NamedVariable(string name) : base(name) {}
    }

    public class AnchorVariable: Variable {
        private Anchor _anchor;
        private Anchor.Coordinate _coordinate;

        public AnchorVariable(Anchor anchor, Anchor.Coordinate coordinate) : base(anchor.Thing + "." + anchor.Name + "." + coordinate) {
            _anchor = anchor;
            _coordinate = coordinate;
        }
    }

    public class UnaryExpression : AbstractExpr {
        private readonly AbstractExpr _inner;
        private readonly UnaryOperator _op;
        public UnaryExpression(AbstractExpr inner, UnaryOperator op) {
            _inner = inner;
            _op = op;
        }

        public AbstractExpr Inner { get { return _inner; } }
        public UnaryOperator Op { get { return _op; } }
    }

    public abstract class UnaryOperator : AbstractOperator {}

    public class UnaryMinus : UnaryOperator { }
    public class Square : UnaryOperator { }
    public class Squareroot : UnaryOperator { }
    public class Cube : UnaryOperator { }
    public class Integral : UnaryOperator { }
    public class Differential : UnaryOperator { }
    public class Sin : UnaryOperator { }
    public class Cos : UnaryOperator { }

    public class BinaryExpression : AbstractExpr {
        private readonly AbstractExpr _lhs;
        private readonly BinaryOperator _op;
        private readonly AbstractExpr _rhs;
        public BinaryExpression(AbstractExpr lhs, BinaryOperator op, AbstractExpr rhs) {
            _lhs = lhs;
            _op = op;
            _rhs = rhs;
        }

        public AbstractExpr Lhs { get { return _lhs; } }
        public BinaryOperator Op { get { return _op; } }
        public AbstractExpr Rhs { get { return _rhs; } }
    }

    public abstract class BinaryOperator : AbstractOperator { }

    public class Plus : BinaryOperator { }
    public class Times : BinaryOperator { }
    public class Divide : BinaryOperator { }
    // some more to come

    public abstract class ScalarConstraint {}

    public class Equation : ScalarConstraint {
        private readonly AbstractExpr _lhs;
        private readonly AbstractExpr _rhs;
        public Equation(AbstractExpr lhs, AbstractExpr rhs) {
            _lhs = lhs;
            _rhs = rhs;
        }

        public AbstractExpr Lhs { get { return _lhs; } }
        public AbstractExpr Rhs { get { return _rhs; } }
    }

    public class LessInequality : ScalarConstraint {
        private readonly AbstractExpr _lhs;
        private readonly AbstractExpr _rhs;
        public LessInequality(AbstractExpr lhs, AbstractExpr rhs) {
            _lhs = lhs;
            _rhs = rhs;
        }

        public AbstractExpr Lhs { get { return _lhs; } }
        public AbstractExpr Rhs { get { return _rhs; } }
    }

    public class LessOrEqualInequality : ScalarConstraint {
        private readonly AbstractExpr _lhs;
        private readonly AbstractExpr _rhs;
        public LessOrEqualInequality(AbstractExpr lhs, AbstractExpr rhs) {
            _lhs = lhs;
            _rhs = rhs;
        }

        public AbstractExpr Lhs { get { return _lhs; } }
        public AbstractExpr Rhs { get { return _rhs; } }
    }
}
