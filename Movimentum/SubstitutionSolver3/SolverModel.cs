using System.Collections.Generic;
using Movimentum.Model;

namespace Movimentum.SubstitutionSolver3 {
    #region Input constraints

    public abstract class AbstractConstraint {
        private static int _debugCt = 1000;
        private readonly int _debugId = _debugCt++;

        public int DebugId {
            get { return _debugId; }
        }

        public override string ToString() {
            return "{" + GetType().Name + "}" + Accept(new ToStringVisitor(), Ig.nore);
        }

        public abstract TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p);
    }

    public abstract class ScalarConstraint : AbstractConstraint {
        private readonly AbstractExpr _expr;

        protected ScalarConstraint(AbstractExpr expr) {
            _expr = expr;
        }

        public AbstractExpr Expr { get { return _expr; } }


        protected bool ScalarConstraintEquals(ScalarConstraint other) {
            return other != null && other._expr.Equals(_expr);
        }

        protected int ScalarConstraintGetHashCode() {
            return _expr.GetHashCode();
        }
    }

    public class EqualsZeroConstraint : ScalarConstraint {
        public EqualsZeroConstraint(AbstractExpr expr) : base(expr) { }

        public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }

        public override bool Equals(object obj) {
            return ScalarConstraintEquals(obj as EqualsZeroConstraint);
        }

        public override int GetHashCode() {
            return ScalarConstraintGetHashCode();
        }
    }

    public class MoreThanZeroConstraint : ScalarConstraint {
        public MoreThanZeroConstraint(AbstractExpr expr)
            : base(expr) {
        }

        public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
    }

    public class AtLeastZeroConstraint : ScalarConstraint {
        public AtLeastZeroConstraint(AbstractExpr expr)
            : base(expr) {
        }

        public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
    }

    #endregion Input constraints

    #region Expressions

    public abstract class AbstractExpr {
        public abstract TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p);

        public override string ToString() {
            return "{" + GetType().Name + "}" + Accept(new ToStringVisitor(), 0);
        }

        public static AbstractExpr operator +(AbstractExpr lhs, AbstractExpr rhs) {
            return new BinaryExpression(lhs, new Plus(), rhs);
        }
        public static AbstractExpr operator *(AbstractExpr lhs, AbstractExpr rhs) {
            return new BinaryExpression(lhs, new Times(), rhs);
        }
        public static AbstractExpr operator /(AbstractExpr lhs, AbstractExpr rhs) {
            return new BinaryExpression(lhs, new Divide(), rhs);
        }
        public static AbstractExpr operator -(AbstractExpr inner) {
            return new UnaryExpression(inner, new UnaryMinus());
        }
    }
    public abstract class AbstractOperator { }

    public class Constant : AbstractExpr {
        private readonly double _value;
        public Constant(double value) {
            _value = value;
        }

        public double Value { get { return _value; } }

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }

        public override bool Equals(object obj) {
            return Equals(obj as Constant);
        }
        public bool Equals(Constant obj) {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            // Necessary for compatibility with GetHashCode()
            return obj != null && obj.Value == Value;
            // ReSharper restore CompareOfFloatsByEqualityOperator
        }
        public override int GetHashCode() {
            return _value.GetHashCode();
        }
    }

    public abstract class Variable : AbstractExpr {
        private readonly string _name;

        protected Variable(string name) {
            _name = name;
        }

        public string Name { get { return _name; } }

        public override bool Equals(object obj) {
            return Equals(obj as Variable);
        }
        public bool Equals(Variable obj) {
            return obj != null && obj._name == _name;
        }
        public override int GetHashCode() {
            return _name.GetHashCode();
        }
    }

    public class NamedVariable : Variable {
        public NamedVariable(string name) : base(name) { }

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
    }

    public class AnchorVariable : Variable {
        private readonly Anchor _anchor;
        private readonly Anchor.Coordinate _coordinate;

        public AnchorVariable(Anchor anchor, Anchor.Coordinate coordinate)
            : base(VariableName(anchor, coordinate)) {
            _anchor = anchor;
            _coordinate = coordinate;
        }

        public Anchor.Coordinate Coordinate { get { return _coordinate; } }
        public Anchor Anchor { get { return _anchor; } }

        public static string VariableName(Anchor anchor, Anchor.Coordinate coordinate) {
            return anchor.Thing + "." + anchor.Name + "." + coordinate;
        }

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
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

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }

        public override bool Equals(object obj) {
            return Equals(obj as UnaryExpression);
        }
        public bool Equals(UnaryExpression obj) {
            return obj != null && obj._op.Equals(_op) && obj._inner.Equals(_inner);
        }
        public override int GetHashCode() {
            return _inner.GetHashCode();
        }
    }

    public abstract class UnaryOperator : AbstractOperator {
        public abstract TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor,
            TExpression innerResult, TParameter p);

        public static bool operator ==(UnaryOperator op1, UnaryOperator op2) {
            return Equals(op1, op2);
        }
        public static bool operator !=(UnaryOperator op1, UnaryOperator op2) {
            return !(op1 == op2);
        }

        public override bool Equals(object obj) {
            return obj != null && obj.GetType() == GetType();
        }

        public override int GetHashCode() {
            return GetType().GetHashCode();
        }
    }

    public class UnaryMinus : UnaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
            return visitor.Visit(this, innerResult, p);
        }
    }
    public class Square : UnaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
            return visitor.Visit(this, innerResult, p);
        }
    }
    public class FormalSquareroot : UnaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
            return visitor.Visit(this, innerResult, p);
        }
    }

    public class PositiveSquareroot : UnaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
            return visitor.Visit(this, innerResult, p);
        }
    }
    //public class Integral : UnaryOperator {
    //    public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
    //        return visitor.Visit(this, innerResult, p);
    //    }
    //}
    //public class Differential : UnaryOperator {
    //    public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
    //        return visitor.Visit(this, innerResult, p);
    //    }
    //}
    public class Sin : UnaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
            return visitor.Visit(this, innerResult, p);
        }
    }
    public class Cos : UnaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
            return visitor.Visit(this, innerResult, p);
        }
    }

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

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
        public override bool Equals(object obj) {
            return Equals(obj as BinaryExpression);
        }
        public bool Equals(BinaryExpression obj) {
            return obj != null && obj._op.Equals(_op) && obj._lhs.Equals(_lhs) && obj._rhs.Equals(_rhs);
        }
        public override int GetHashCode() {
            return _lhs.GetHashCode() + _rhs.GetHashCode();
        }
    }

    public abstract class BinaryOperator : AbstractOperator {
        public abstract TResult Accept<TExpression, TParameter, TResult>(ISolverModelBinaryOpVisitor<TExpression, TParameter, TResult> visitor,
            TExpression lhsResult, TExpression rhsResult, TParameter p);
        public static bool operator ==(BinaryOperator op1, BinaryOperator op2) {
            return Equals(op1, op2);
        }
        public static bool operator !=(BinaryOperator op1, BinaryOperator op2) {
            return !(op1 == op2);
        }

        public override bool Equals(object obj) {
            return obj != null && obj.GetType() == GetType();
        }

        public override int GetHashCode() {
            return GetType().GetHashCode();
        }
    }

    public class Plus : BinaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelBinaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression lhsResult, TExpression rhsResult, TParameter p) {
            return visitor.Visit(this, lhsResult, rhsResult, p);
        }
    }

    public class Times : BinaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelBinaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression lhsResult, TExpression rhsResult, TParameter p) {
            return visitor.Visit(this, lhsResult, rhsResult, p);
        }
    }

    public class Divide : BinaryOperator {
        public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelBinaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression lhsResult, TExpression rhsResult, TParameter p) {
            return visitor.Visit(this, lhsResult, rhsResult, p);
        }
    }

    public class RangeExpr : AbstractExpr {
        public class Pair {
            public readonly AbstractExpr MoreThan;
            public readonly AbstractExpr Value;

            public Pair(AbstractExpr moreThan, AbstractExpr value) {
                MoreThan = moreThan;
                Value = value;
            }
        }

        private readonly AbstractExpr _expr;
        private readonly AbstractExpr _value0;
        private readonly IEnumerable<Pair> _pairs;

        public RangeExpr(AbstractExpr expr, AbstractExpr value0, IEnumerable<Pair> pairs) {
            _expr = expr;
            _value0 = value0;
            _pairs = pairs;
        }

        public AbstractExpr Expr { get { return _expr; } }
        public AbstractExpr Value0 { get { return _value0; } }
        public IEnumerable<Pair> Pairs { get { return _pairs; } }

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
    }

    #endregion Expressions
}
