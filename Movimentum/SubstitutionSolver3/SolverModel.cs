using System;
using System.Collections.Generic;
using System.Linq;
using Movimentum.Model;

namespace Movimentum.SubstitutionSolver3 {
    #region Input constraints

    public abstract class AbstractConstraint {
        private static int _debugCt = 1000;
        private readonly int _debugId = _debugCt++;
        private int? _debugCreatingNodeId;

        public string DebugId {
            get { return _debugId + "{" + _debugCreatingNodeId + "}"; }
        }

        public void SetDebugCreatingNodeIdIfMissing(int currentNodeId) {
            if (!_debugCreatingNodeId.HasValue) {
                _debugCreatingNodeId = currentNodeId;
            }
        }

        public override string ToString() {
            //return "{" + GetType().Name + "/" + _debugCreatingNodeId + "}" + Accept(new ToStringVisitor(), Ig.nore);
            return "{" + GetType().Name + "}" + Accept(new ToStringVisitor(), Ig.nore);
        }

        public abstract TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p);
    }

    public abstract class ScalarConstraint : AbstractConstraint {
        private readonly IAbstractExpr _expr;

        protected ScalarConstraint(IAbstractExpr expr) {
            _expr = expr;
        }

        public IAbstractExpr Expr { get { return _expr; } }


        protected bool ScalarConstraintEquals(ScalarConstraint other) {
            return other != null && other._expr.Equals(_expr);
        }

        protected int ScalarConstraintGetHashCode() {
            return _expr.GetHashCode();
        }
    }

    public class EqualsZeroConstraint : ScalarConstraint {
        public EqualsZeroConstraint(IAbstractExpr expr)
            : base(expr) {
        }

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
        public MoreThanZeroConstraint(IAbstractExpr expr)
            : base(expr) {
        }

        public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }

        public override bool Equals(object obj) {
            return ScalarConstraintEquals(obj as MoreThanZeroConstraint);
        }

        public override int GetHashCode() {
            return ScalarConstraintGetHashCode();
        }
    }

    public class AtLeastZeroConstraint : ScalarConstraint {
        public AtLeastZeroConstraint(IAbstractExpr expr)
            : base(expr) {
        }

        public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
        public override bool Equals(object obj) {
            return ScalarConstraintEquals(obj as AtLeastZeroConstraint);
        }

        public override int GetHashCode() {
            return ScalarConstraintGetHashCode();
        }
    }

    #endregion Input constraints

    #region Expressions

    public interface IAbstractExpr {
        TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p);
        AbstractExpr E { get; }
    }
    public interface IPolynomial : IAbstractExpr {
        IVariable Var { get; }
        int Degree { get; }
        double Coefficient(int power);
        IEnumerable<double> Coefficients { get; }
    }
    public interface IConstant : IPolynomial {
        double Value { get; }
    }
    public interface IVariable : IPolynomial {
        string Name { get; }
    }
    public interface INamedVariable : IVariable {
    }
    public interface IAnchorVariable : IVariable {
        Anchor.Coordinate Coordinate { get; }
        Anchor Anchor { get; }
    }
    public interface IGeneralPolynomialSTEPB : IPolynomial {
        double EvaluateAtSTEPC(double getValue);
    }

    public abstract class AbstractExpr : IAbstractExpr {
        public abstract TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p);

        public override string ToString() {
            return "{" + GetType().Name + "}" + Accept(new ToStringVisitor(), 0);
        }
        public AbstractExpr E { get { return this; } }

        public static BinaryExpression operator +(AbstractExpr lhs, IAbstractExpr rhs) {
            return new BinaryExpression(lhs, new Plus(), rhs);
        }
        public static BinaryExpression operator *(AbstractExpr lhs, IAbstractExpr rhs) {
            return new BinaryExpression(lhs, new Times(), rhs);
        }
        public static BinaryExpression operator /(AbstractExpr lhs, IAbstractExpr rhs) {
            return new BinaryExpression(lhs, new Divide(), rhs);
        }
        public static UnaryExpression operator -(AbstractExpr inner) {
            return new UnaryExpression(inner, new UnaryMinus());
        }
    }

    public abstract class AbstractOperator { }

    public abstract class Polynomial : AbstractExpr, IPolynomial {

        private class Constant : Polynomial, IConstant {
            private readonly double _value;

            internal Constant(double value) {
                _value = value;
            }

            public double Value {
                get { return _value; }
            }

            public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
                return visitor.Visit(this, p);
            }

            public override bool Equals(object obj) {
                return Equals(obj as Constant);
            }

            private bool Equals(Constant obj) {
                // ReSharper disable CompareOfFloatsByEqualityOperator
                // Necessary for compatibility with GetHashCode()
                return obj != null && obj.Value == Value;
                // ReSharper restore CompareOfFloatsByEqualityOperator
            }

            public override int GetHashCode() {
                return _value.GetHashCode();
            }

            #region Overrides of Polynomial

            private static readonly IVariable CONSTANT_DUMMY_VAR = new NamedVariable("Dummy variable for constant polynomial");

            public override IVariable Var {
                get { return CONSTANT_DUMMY_VAR; }
            }

            public override int Degree {
                get { return 0; } // Also if constant is 0 ... in math, it is -inf then.
            }

            public override double Coefficient(int power) {
                return power == 0 ? _value : 0;
            }

            public override IEnumerable<double> Coefficients {
                get { return new[] { _value }; }
            }

            #endregion
        }

        private abstract class Variable : Polynomial, IVariable, IComparable {
            private readonly string _name;

            protected Variable(string name) {
                _name = name;
            }

            public string Name {
                get { return _name; }
            }

            public override bool Equals(object obj) {
                return Equals(obj as Variable);
            }

            private bool Equals(Variable obj) {
                // NamedVariable and AnchorVariable are equal if name is equal.
                // This eases debugging and testing. I hope it creates no problems.
                return obj != null && obj.Name == _name;
            }

            public override int GetHashCode() {
                return _name.GetHashCode();
            }

            public int CompareTo(object obj) {
                return _name.CompareTo(((Variable) obj).Name);
            }

            #region Overrides of Polynomial

            public override IVariable Var {
                get { return this; }
            }

            public override int Degree {
                get { return 1; }
            }

            public override IEnumerable<double> Coefficients {
                get { return new[] { 1.0, 0.0 }; }
            }

            public override double Coefficient(int power) {
                return power == 1 ? 1 : 0;
            }
            #endregion
        }

        private class NamedVariable : Variable, INamedVariable {
            public NamedVariable(string name) : base(name) { }

            public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
                return visitor.Visit(this, p);
            }

        }

        private class AnchorVariable : Variable, IAnchorVariable {
            private readonly Anchor _anchor;
            private readonly Anchor.Coordinate _coordinate;

            internal AnchorVariable(Anchor anchor, Anchor.Coordinate coordinate)
                : base(VariableName(anchor, coordinate)) {
                _anchor = anchor;
                _coordinate = coordinate;
            }

            private static string VariableName(Anchor anchor, Anchor.Coordinate coordinate) {
                return anchor.Thing + "." + anchor.Name + "." + coordinate;
            }

            public Anchor.Coordinate Coordinate {
                get { return _coordinate; }
            }

            public Anchor Anchor {
                get { return _anchor; }
            }

            public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
                return visitor.Visit(this, p);
            }
        }

        private class GeneralPolynomialSTEPB : Polynomial, IGeneralPolynomialSTEPB {
            private readonly IVariable _variable;
            private readonly double[] _coefficientsInInternalOrder;
            private readonly double[] _coefficientsInMathOrder;
            internal GeneralPolynomialSTEPB(IVariable variable, IEnumerable<double> coefficientsInInternalOrder) { // ...
                _coefficientsInInternalOrder = coefficientsInInternalOrder.ToArray();
                _coefficientsInMathOrder = coefficientsInInternalOrder.Reverse().ToArray();
                if (_coefficientsInInternalOrder[_coefficientsInInternalOrder.Length - 1].Near(0)) {
                    throw new ArgumentException("Top coefficient must not be zero");
                }
                _variable = variable;
            }

            public override IVariable Var {
                get { return _variable; }
            }

            // Not zero poly as -inf, but as 0 ...
            public override int Degree { get { return _coefficientsInInternalOrder.Length - 1; } } // ...

            public override double Coefficient(int power) { return _coefficientsInInternalOrder[power]; }

            public override bool Equals(object obj) {
                return Equals(obj as GeneralPolynomialSTEPB);
            }

            private bool Equals(GeneralPolynomialSTEPB other) {
                return other != null
                    && _variable.Equals(other._variable)
                    && _coefficientsInInternalOrder.SequenceEqual(other._coefficientsInInternalOrder);
            }

            public override int GetHashCode() {
                return _coefficientsInInternalOrder.Sum(c => c.GetHashCode() / 100).GetHashCode();
            }

            public double EvaluateAtSTEPC(double value) {
                // Evaluate by Horner's rule.
                double result = Coefficient(Degree);
                for (int i = Degree - 1; i >= 0; i--) {
                    result = value * result + Coefficient(i);
                }
                return result;
            }

            public override IEnumerable<double> Coefficients {
                get { return _coefficientsInMathOrder; }
            }

            public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
                return visitor.VisitSTEPB(this, p);
            }
        }


        public static IVariable CreateAnchorVariable(Anchor anchor, Anchor.Coordinate coordinate) {
            return new AnchorVariable(anchor, coordinate);
        }

        public static IConstant CreateConstant(double value) {
            return new Constant(value);
        }

        public static INamedVariable CreateNamedVariable(string name) {
            return new NamedVariable(name);
        }

        public abstract IVariable Var { get; }
        public abstract int Degree { get; }
        public abstract double Coefficient(int power);
        public abstract IEnumerable<double> Coefficients { get; }

        public static IPolynomial CreatePolynomial(IVariable variable, params double[] coefficients) {
            return CreatePolynomial(variable, (IEnumerable<double>)coefficients);
        }

        public static IPolynomial CreatePolynomial(IVariable variable, IEnumerable<double> coefficients) {
            int zeroPrefixLength = 0;
            foreach (var c in coefficients) {
                if (!c.Near(0)) {
                    break;
                }
                zeroPrefixLength++;
            }
            double[] normalizedCoefficients = coefficients.Skip(zeroPrefixLength).Reverse().ToArray();
            if (normalizedCoefficients.Length == 0) {
                normalizedCoefficients = new[] { 0.0 };
            }
            var deg = normalizedCoefficients.Length - 1;
            if (deg == 0) {
                return new Constant(normalizedCoefficients[0]);
            } else if (deg == 1 && normalizedCoefficients[0].Near(0) && normalizedCoefficients[1].Near(1)) {
                return variable;
            } else {
                return new GeneralPolynomialSTEPB(variable, normalizedCoefficients);
            }
        }
    }

    public class UnaryExpression : AbstractExpr {
        private readonly IAbstractExpr _inner;
        private readonly UnaryOperator _op;
        public UnaryExpression(IAbstractExpr inner, UnaryOperator op) {
            _inner = inner;
            _op = op;
        }

        public IAbstractExpr Inner { get { return _inner; } }
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
        private readonly IAbstractExpr _lhs;
        private readonly BinaryOperator _op;
        private readonly IAbstractExpr _rhs;
        public BinaryExpression(IAbstractExpr lhs, BinaryOperator op, IAbstractExpr rhs) {
            _lhs = lhs;
            _op = op;
            _rhs = rhs;
        }

        public IAbstractExpr Lhs { get { return _lhs; } }
        public BinaryOperator Op { get { return _op; } }
        public IAbstractExpr Rhs { get { return _rhs; } }

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
            public readonly IAbstractExpr MoreThan;
            public readonly IAbstractExpr Value;

            public Pair(IAbstractExpr moreThan, IAbstractExpr value) {
                MoreThan = moreThan;
                Value = value;
            }
        }

        private readonly IAbstractExpr _expr;
        private readonly IAbstractExpr _value0;
        private readonly IEnumerable<Pair> _pairs;

        public RangeExpr(IAbstractExpr expr, IAbstractExpr value0, IEnumerable<Pair> pairs) {
            _expr = expr;
            _value0 = value0;
            _pairs = pairs;
        }

        public IAbstractExpr Expr { get { return _expr; } }
        public IAbstractExpr Value0 { get { return _value0; } }
        public IEnumerable<Pair> Pairs { get { return _pairs; } }

        public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }
    }

    #endregion Expressions
}
