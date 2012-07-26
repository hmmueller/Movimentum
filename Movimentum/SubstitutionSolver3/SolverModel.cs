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

        public TResult Accept<TResult>(ISolverModelConstraintVisitor<Ignore, TResult> visitor) {
            return Accept(visitor, Ig.nore);
        }
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
        TResult Accept<TResult>(ISolverModelExprVisitor<Ignore, TResult> visitor);
        AbstractExpr C { get; }
    }
    public interface IPolynomial : IAbstractExpr {
        IVariable Var { get; }
        int Degree { get; }
        double Coefficient(int power);
        IEnumerable<double> Coefficients { get; }
        Polynomial P { get; }
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
    public interface IGeneralPolynomial : IPolynomial {
    }

    public abstract class AbstractExpr : IAbstractExpr {
        public abstract TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p);

        public TResult Accept<TResult>(ISolverModelExprVisitor<Ignore, TResult> visitor) {
            return Accept(visitor, Ig.nore);
        }

        public override string ToString() {
            return "{" + GetType().Name + "}" + Accept(new ToStringVisitor(), 0);
        }
        public AbstractExpr C { get { return this; } }

        public static AbstractExpr operator +(AbstractExpr lhs, IAbstractExpr rhs) {
            return lhs.Equals(Polynomial.ZERO) ? rhs.C
                : rhs.Equals(Polynomial.ZERO) ? lhs
                : new BinaryExpression(lhs, new Plus(), rhs);
        }
        public static AbstractExpr operator *(AbstractExpr lhs, IAbstractExpr rhs) {
            return lhs.Equals(Polynomial.ZERO) ? Polynomial.ZERO.C
                : rhs.Equals(Polynomial.ZERO) ? Polynomial.ZERO.C
                : new BinaryExpression(lhs, new Times(), rhs);
        }
        public static AbstractExpr operator /(AbstractExpr lhs, IAbstractExpr rhs) {
            return lhs.Equals(Polynomial.ZERO)
                ? (rhs.Equals(Polynomial.ZERO) ? Polynomial.CreateConstant(double.NaN) : Polynomial.ZERO).C
                : new BinaryExpression(lhs, new Divide(), rhs);
        }
        public static AbstractExpr operator -(AbstractExpr inner) {
            return inner.Equals(Polynomial.ZERO)
                ? Polynomial.ZERO.C
                : new UnaryExpression(inner, new UnaryMinus());
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
                get { return 0; } // Also if constant is 0 ... in math, it is -infinity then.
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
                return _name.CompareTo(((Variable)obj).Name);
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

        private class GeneralPolynomial : Polynomial, IGeneralPolynomial {
            private readonly IVariable _variable;
            private readonly double[] _coefficients;
            private readonly int _degree;
            internal GeneralPolynomial(IVariable variable,
                    IEnumerable<double> coefficients) {
                _coefficients = coefficients.ToArray();
                if (_coefficients[0].Near(0)) {
                    throw new ArgumentException("Top coefficient must not be 0");
                }
                _degree = _coefficients.Length - 1;
                _variable = variable;
            }

            public override IVariable Var {
                get { return _variable; }
            }

            public override int Degree { get { return _degree; } }

            public override double Coefficient(int power) {
                return _coefficients[_degree - power];
            }

            public override bool Equals(object obj) {
                return Equals(obj as GeneralPolynomial);
            }

            private bool Equals(GeneralPolynomial other) {
                return other != null
                    && _variable.Equals(other._variable)
                    && _coefficients.SequenceEqual(other._coefficients);
            }

            public override int GetHashCode() {
                // Divide coefficient hashcode by degree to avoid overflow.
                return _coefficients.Sum(c => c.GetHashCode() / (_degree + 1));
            }

            public override IEnumerable<double> Coefficients {
                get { return _coefficients; }
            }

            public override TResult Accept<TParameter, TResult>(
                    ISolverModelExprVisitor<TParameter, TResult> visitor,
                    TParameter p) {
                return visitor.Visit(this, p);
            }
        }

        public static IVariable CreateAnchorVariable(Anchor anchor,
                                        Anchor.Coordinate coordinate) {
            return new AnchorVariable(anchor, coordinate);
        }

        public static INamedVariable CreateNamedVariable(string name) {
            return new NamedVariable(name);
        }

        // We cache ZERO to ease, in the future, many comparisons with it.
        public static readonly IConstant ZERO = new Constant(0);

        public static IConstant CreateConstant(double value) {
            return value.Near(0) ? ZERO : new Constant(value);
        }

        public abstract IVariable Var { get; }
        public abstract int Degree { get; }
        public abstract double Coefficient(int power);
        public abstract IEnumerable<double> Coefficients { get; }
        public Polynomial P { get { return this; } }

        private static IVariable SameVarOrNull(IPolynomial lhs, IPolynomial rhs) {
            return lhs.Var.Equals(rhs.Var) ? lhs.Var
                : lhs is IConstant ? rhs.Var
                : rhs is IConstant ? lhs.Var
                : null;
        }

        public static Polynomial operator +(Polynomial p, IPolynomial q) {
            IVariable commonVar = SameVarOrNull(p, q);
            if (commonVar == null) {
                throw new ArgumentException("operator+ works only for polynomials with same variable, not " + p.Var + " and " + q.Var);
            }
            int resultDegree = Math.Max(p.Degree, q.Degree);
            var resultCoefficients = new double[resultDegree + 1];
            // d               2 1 0
            //                 v v v
            // powers: 6 5 4 3 2 1 0, i.e., resultDeg = 6
            // [i]:    0 1 2 3 4 5 6
            // =>
            //    6 - d
            for (int d = 0; d <= p.Degree; d++) {
                resultCoefficients[resultDegree - d] = p.Coefficient(d);
            }
            for (int d = 0; d <= q.Degree; d++) {
                resultCoefficients[resultDegree - d] += q.Coefficient(d);
            }
            return CreatePolynomial(commonVar, resultCoefficients).P;
        }

        public static Polynomial operator *(Polynomial lhs, IPolynomial rhs) {
            IVariable commonVar = SameVarOrNull(lhs, rhs);
            if (commonVar == null) {
                throw new ArgumentException("operator+ works only for polynomials with same variable, not " + lhs.Var + " and " + rhs.Var);
            }

            int deg = lhs.Degree + rhs.Degree;
            var resultCoefficients = new double[deg + 1];
            for (int ld = 0; ld <= lhs.Degree; ld++) {
                for (int rd = 0; rd <= rhs.Degree; rd++) {
                    resultCoefficients[ld + rd] += lhs.Coefficients.ElementAt(ld) * rhs.Coefficients.ElementAt(rd);
                }
            }

            return CreatePolynomial(commonVar, resultCoefficients).P;
        }

        public static Polynomial operator *(Polynomial p, double d) {
            return CreatePolynomial(p.Var, p.Coefficients.Select(c => c * d)).P;
        }

        public static Polynomial operator *(double d, Polynomial p) {
            return p * d;
        }

    public static Polynomial operator -(Polynomial p) {
        return CreatePolynomial(p.Var, p.Coefficients.Select(c => -c)).P;
    }

        public static IPolynomial CreatePolynomial(IVariable variable, params double[] coefficients) {
            return CreatePolynomial(variable, (IEnumerable<double>)coefficients);
        }

        public static IPolynomial CreatePolynomial(IVariable variable,
                                        IEnumerable<double> coefficients) {
            // Skip all leading zero coefficients
            int zeroPrefixLength = 0;
            foreach (var c in coefficients) {
                if (!c.Near(0)) {
                    break;
                }
                zeroPrefixLength++;
            }
            double[] normalizedCoefficients =
                coefficients.Skip(zeroPrefixLength).ToArray();

            // Create intermediate coefficient for zero polynomial.
            if (normalizedCoefficients.Length == 0) {
                normalizedCoefficients = new[] { 0.0 };
            }

            // Create result based on degree and coefficients.
            var deg = normalizedCoefficients.Length - 1;
            if (deg == 0) {
                return new Constant(normalizedCoefficients[0]);
            } else if (deg == 1
                       && normalizedCoefficients[0].Near(1)
                       && normalizedCoefficients[1].Near(0)) {
                return variable;
            } else {
                return new GeneralPolynomial(variable, normalizedCoefficients);
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
