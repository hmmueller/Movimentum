using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public abstract class AbstractBoundary {
        public readonly double Value;

        protected AbstractBoundary(double value) {
            Value = value;
        }

        public abstract string Prefix { get; }
        public abstract string Postfix { get; }

        public abstract bool Below(double d);
        public abstract bool Above(double d);
        public abstract AbstractBoundary Min(AbstractBoundary other);
        public abstract AbstractBoundary Max(AbstractBoundary other);
        internal abstract AbstractBoundary MinOpen(OpenBoundary other);
        internal abstract AbstractBoundary MaxOpen(OpenBoundary other);
        internal abstract AbstractBoundary MinClosed(ClosedBoundary other);
        internal abstract AbstractBoundary MaxClosed(ClosedBoundary other);

        public static AbstractBoundary CreateFrom(double value) {
            return double.IsInfinity(value) ? (AbstractBoundary)new OpenBoundary(value) : new ClosedBoundary(value);
        }

        public double ApproximateDouble() {
            return double.IsNegativeInfinity(Value)
                       ? -1e50
                       : double.IsPositiveInfinity(Value)
                             ? 1e50
                             : Value;
        }
    }

    public class OpenBoundary : AbstractBoundary {
        public OpenBoundary(double value) : base(value) { }

        public override string Prefix {
            get { return "("; }
        }

        public override string Postfix {
            get { return ")"; }
        }

        public override bool Below(double d) {
            return Value < d;
        }

        public override bool Above(double d) {
            return Value > d;
        }

        public override AbstractBoundary Min(AbstractBoundary other) {
            return other.MinOpen(this);
        }

        public override AbstractBoundary Max(AbstractBoundary other) {
            return other.MaxOpen(this);
        }

        internal override AbstractBoundary MinOpen(OpenBoundary other) {
            return Value < other.Value ? this : other;
        }

        internal override AbstractBoundary MaxOpen(OpenBoundary other) {
            return Value > other.Value ? this : other;
        }

        internal override AbstractBoundary MinClosed(ClosedBoundary other) {
            return Value < other.Value ? (AbstractBoundary)this : other;
        }

        internal override AbstractBoundary MaxClosed(ClosedBoundary other) {
            return Value > other.Value ? (AbstractBoundary)this : other;
        }
    }

    public class ClosedBoundary : AbstractBoundary {
        public ClosedBoundary(double value) : base(value) { }
        public override string Prefix {
            get { return "["; }
        }

        public override string Postfix {
            get { return "]"; }
        }

        public override bool Below(double d) {
            return Value <= d;
        }

        public override bool Above(double d) {
            return Value >= d;
        }

        public override AbstractBoundary Min(AbstractBoundary other) {
            return other.MinClosed(this);
        }

        public override AbstractBoundary Max(AbstractBoundary other) {
            return other.MaxClosed(this);
        }

        internal override AbstractBoundary MinOpen(OpenBoundary other) {
            return other.Value < Value ? (AbstractBoundary)other : this;
        }

        internal override AbstractBoundary MaxOpen(OpenBoundary other) {
            return other.Value > Value ? (AbstractBoundary)other : this;
        }

        internal override AbstractBoundary MinClosed(ClosedBoundary other) {
            return Value < other.Value ? this : other;
        }

        internal override AbstractBoundary MaxClosed(ClosedBoundary other) {
            return Value > other.Value ? this : other;
        }
    }

    public class Range {
        private readonly AbstractBoundary _lo;
        private readonly AbstractBoundary _hi;

        public static Range CreateClosed(double lo, double hi) {
            return new Range(new ClosedBoundary(lo), new ClosedBoundary(hi));
        }

        public static Range CreateOpen(double lo, double hi) {
            return new Range(new OpenBoundary(lo), new OpenBoundary(hi));
        }

        public static Range CreateLeftOpen(double lo, double hi) {
            return new Range(new OpenBoundary(lo), new ClosedBoundary(hi));
        }

        public static Range CreateRightOpen(double lo, double hi) {
            return new Range(new ClosedBoundary(lo), new OpenBoundary(hi));
        }

        public Range(AbstractBoundary lo, AbstractBoundary hi) {
            _lo = lo;
            _hi = hi;
        }

        public double Lo {
            get { return _lo.Value; }
        }

        public double Hi {
            get { return _hi.Value; }
        }

        public double LoApproximateDouble {
            get { return _lo.ApproximateDouble(); }
        }

        public double HiApproximateDouble {
            get { return _hi.ApproximateDouble(); }
        }

        public bool IsEmpty() {
            return !_lo.Below(_hi.Value);
        }

        public bool Contains(double x) {
            return _lo.Below(x) && _hi.Above(x);
        }

        public Range Intersect(Range other) {
            return new Range(_lo.Max(other._lo), _hi.Min(other._hi));
        }

        public double GetSomeValue() {
            return (_lo.ApproximateDouble() + _hi.ApproximateDouble()) / 2;
        }

        public IEnumerable<Range> Intersect(IEnumerable<Range> otherRanges) {
            return otherRanges.Select(r => Intersect(r)).Where(r => !r.IsEmpty());
        }

        public override string ToString() {
            return _lo.Prefix + Lo + ".." + Hi + _hi.Postfix;
        }
    }

    public abstract class VariableRangeRestriction {
        private readonly Variable _variable;

        protected VariableRangeRestriction(Variable variable) {
            _variable = variable;
        }

        public Variable Variable { get { return _variable; } }

        public abstract bool Contains(double x0);
        public abstract IEnumerable<Range> PossibleRanges { get; }
        public abstract double GetSomeValue();
        public abstract VariableRangeRestriction Intersect(IEnumerable<Range> possibleRanges);
    }

    public class VariableValueRestriction : VariableRangeRestriction {
        private readonly double _value;

        public VariableValueRestriction(Variable variable, double value)
            : base(variable) {
            _value = value;
        }

        public double Value { get { return _value; } }

        public override string ToString() {
            return Variable.Name + ":=" + Value;
        }

        #region Overrides of VariableRangeRestriction

        public override bool Contains(double x0) {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<Range> PossibleRanges {
            get { throw new System.NotImplementedException(); }
        }

        public override double GetSomeValue() {
            return Value;
        }

        public override VariableRangeRestriction Intersect(IEnumerable<Range> possibleRanges) {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    public class VariableMultipleRangeRestriction : VariableRangeRestriction {
        private readonly Range[] _possibleRanges;
        private VariableMultipleRangeRestriction(Variable variable, IEnumerable<Range> possibleRanges)
            : base(variable) {
            _possibleRanges = possibleRanges.Where(r => !r.IsEmpty()).ToArray();
        }
        public VariableMultipleRangeRestriction(Variable variable, double lo, double hi)
            : this(variable, new[] {
                new Range(AbstractBoundary.CreateFrom(lo), AbstractBoundary.CreateFrom(hi)) }) { }

        public VariableMultipleRangeRestriction(Variable variable, double value)
            : this(variable, value, value) { }

        public override IEnumerable<Range> PossibleRanges {
            get { return _possibleRanges; }
        }

        public override bool Contains(double x) {
            return _possibleRanges.Any(r => r.Contains(x));
        }

        public override VariableRangeRestriction Intersect(IEnumerable<Range> restrictingRanges) {
            return new VariableMultipleRangeRestriction(Variable, _possibleRanges.SelectMany(pr => restrictingRanges.Select(rr => rr.Intersect(pr))));
        }

        //public double? GetSingleValue() {
        //    if (_possibleRanges.Length == 1) {
        //        Range range = _possibleRanges[0];
        //        // ReSharper disable CompareOfFloatsByEqualityOperator
        //        return range.Lo == range.Hi && range.Contains(range.Lo) ? range.Lo : (double?)null;
        //        // ReSharper restore CompareOfFloatsByEqualityOperator
        //    } else {
        //        return null;
        //    }
        //}

        public override double GetSomeValue() {
            return _possibleRanges.Length == 0 ? double.NaN : _possibleRanges[0].GetSomeValue();
        }

        public override string ToString() {
            return Variable.Name + " in {" + string.Join(",", (object[])_possibleRanges) + "}";
        }
    }


}