using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

// This file contains only the "boilerplate code" for the model classes, which is:
// * field definitions for model fields.
// * property definitions to access model fields.
// * constructors
// * Equals() and GetHashcode() methods
// * ToString() methods
// If a class has other methods or properties, it is made partial, and those methods
// and properties go to a file with the class's name.

// This file contains boilerplate code for the model operator classes, which is
// * A constructor
// * Declaration of the operator instances

namespace Movimentum.Model {
    #region Script

    public partial class Script {
        private readonly Config _config;
        private readonly IEnumerable<Thing> _things;
        private readonly IEnumerable<Step> _steps;
        public Script(Config config, IEnumerable<Thing> things,
                      IEnumerable<Step> steps) {
            _config = config;
            _things = things;
            _steps = steps;
        }

        public Config Config { get { return _config; } }
        public IEnumerable<Thing> Things { get { return _things; } }
        public IEnumerable<Step> Steps { get { return _steps; } }
    }

    public class Config {
        private readonly double _framesPerTimeunit;
        public Config(double framesPerTimeunit) {
            _framesPerTimeunit = framesPerTimeunit;
        }

        public double FramesPerTimeunit { get { return _framesPerTimeunit; } }
    }

    #endregion Script

    #region Thingdefinition

    public class Thing {
        private readonly string _name;
        private readonly Image _image;
        private readonly IReadOnlyDictionary<string, ConstVector> _anchors;

        public Thing(string name, Image image, Dictionary<string, ConstVector> anchors) {
            _name = name;
            _image = image;
            _anchors = new ReadOnlyDictionary<string, ConstVector>(anchors);
        }

        public string Name { get { return _name; } }
        public Image Image { get { return _image; } }
        public IReadOnlyDictionary<string, ConstVector> Anchors { get { return _anchors; } }
    }

    public partial class ConstVector { // mhm ... shouldn't the doubles be scalar expressions?
        private readonly double _x;
        private readonly double _y;
        private readonly double? _z;
        public ConstVector(double x, double y, double z) {
            _x = x;
            _y = y;
            _z = z;
        }

        public ConstVector(double x, double y) {
            _x = x;
            _y = y;
            _z = null;
        }

        public double X { get { return _x; } }
        public double Y { get { return _y; } }
        public double Z { get { return _z ?? 0; } }

        public bool Equals(ConstVector obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            ConstVector o = obj as ConstVector;
            return o != null && o._x.Equals(_x) && o._y.Equals(_y) && o.Z.Equals(Z);
        }
        public override int GetHashCode() {
            return _x.GetHashCode() + _y.GetHashCode() + _z.GetHashCode();
        }
    }

    #endregion Thingdefinition

    #region Step

    public partial class Step {
        private readonly double _time;
        private readonly List<Constraint> _constraints;
        public Step(double time, List<Constraint> constraints) {
            _time = time;
            _constraints = constraints;
        }

        public double Time { get { return _time; } }
        public IEnumerable<Constraint> Constraints {
            get { return _constraints; }
        }
    }

    abstract public class Constraint {
        public abstract string Key { get; }
    }

    public class VectorEqualityConstraint : Constraint {
        private readonly Anchor _variable;
        private readonly VectorExpr _rhs;
        public VectorEqualityConstraint(Anchor variable, VectorExpr rhs) {
            _variable = variable;
            _rhs = rhs;
        }

        public Anchor Variable { get { return _variable; } }
        public VectorExpr Rhs { get { return _rhs; } }

        public override string ToString() {
            return _variable + "#=#" + _rhs;
        }

        public override string Key {
            get { return _variable.Thing + "_" + _variable.Name; }
        }
    }

    public class ScalarEqualityConstraint : Constraint {
        private readonly string _variable;
        private readonly ScalarExpr _rhs;
        public ScalarEqualityConstraint(string variable, ScalarExpr rhs) {
            _variable = variable;
            _rhs = rhs;
        }

        public string Variable { get { return _variable; } }
        public ScalarExpr Rhs { get { return _rhs; } }

        public override string ToString() {
            return _variable + "_=_" + _rhs;
        }

        public override string Key {
            get { return _variable; }
        }
    }

    public class ScalarInequalityConstraint : Constraint {
        private readonly string _variable;
        private readonly ScalarInequalityOperator _operator;
        private readonly ScalarExpr _rhs;
        public ScalarInequalityConstraint(string variable, ScalarInequalityOperator @operator, ScalarExpr rhs) {
            _variable = variable;
            _operator = @operator;
            _rhs = rhs;
        }

        public string Variable { get { return _variable; } }
        public ScalarInequalityOperator Operator { get { return _operator; } }
        public ScalarExpr Rhs { get { return _rhs; } }

        public override string ToString() {
            return "(" + _variable + _operator + _rhs + ")";
        }

        public override string Key {
            get { return _variable + _operator; }
        }
    }

    public class ScalarInequalityOperator : AbstractOperator {
        private ScalarInequalityOperator(int precedence, string asString) : base(precedence, asString) { }
        public static ScalarInequalityOperator LT = new ScalarInequalityOperator(0, " < ");
        public static ScalarInequalityOperator LE = new ScalarInequalityOperator(0, " <= ");
        public static ScalarInequalityOperator GT = new ScalarInequalityOperator(0, " > ");
        public static ScalarInequalityOperator GE = new ScalarInequalityOperator(0, " >= ");
    }

    #endregion Step

    #region VectorExpr

    public abstract class VectorExpr : Expr { }

    public class BinaryVectorExpr : VectorExpr {
        private readonly VectorExpr _lhs;
        private readonly BinaryVectorOperator _operator;
        private readonly VectorExpr _rhs;
        public BinaryVectorExpr(VectorExpr lhs, BinaryVectorOperator @operator, VectorExpr rhs) {
            _lhs = lhs;
            _operator = @operator;
            _rhs = rhs;
        }

        public VectorExpr Lhs { get { return _lhs; } }
        public BinaryVectorOperator Operator { get { return _operator; } }
        public VectorExpr Rhs { get { return _rhs; } }

        public bool Equals(BinaryVectorExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            BinaryVectorExpr o = obj as BinaryVectorExpr;
            return o != null && o._lhs.Equals(_lhs) && o._operator.Equals(_operator) && o._rhs.Equals(_rhs);
        }
        public override int GetHashCode() {
            return _lhs.GetHashCode() + _operator.GetHashCode() + _rhs.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_lhs, _operator, _rhs);
        }
    }

    public class BinaryVectorOperator : AbstractOperator {
        private BinaryVectorOperator(int precedence, string asString) : base(precedence, asString) { }
        public static BinaryVectorOperator PLUS = new BinaryVectorOperator(1, " #+ ");
        public static BinaryVectorOperator MINUS = new BinaryVectorOperator(1, " #- ");
    }

    public class VectorScalarExpr : VectorExpr {
        private readonly VectorExpr _lhs;
        private readonly VectorScalarOperator _operator;
        private readonly ScalarExpr _rhs;
        public VectorScalarExpr(VectorExpr lhs, VectorScalarOperator @operator, ScalarExpr rhs) {
            _lhs = lhs;
            _operator = @operator;
            _rhs = rhs;
        }

        public VectorExpr Lhs { get { return _lhs; } }
        public VectorScalarOperator Operator { get { return _operator; } }
        public ScalarExpr Rhs { get { return _rhs; } }

        public bool Equals(VectorScalarExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            VectorScalarExpr o = obj as VectorScalarExpr;
            return o != null && o._lhs.Equals(_lhs) && o._operator.Equals(_operator) && o._rhs.Equals(_rhs);
        }
        public override int GetHashCode() {
            return _lhs.GetHashCode() + _operator.GetHashCode() + _rhs.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_lhs, _operator, _rhs);
        }
    }

    public class VectorScalarOperator : AbstractOperator {
        private VectorScalarOperator(int precedence, string asString) : base(precedence, asString) { }
        public static VectorScalarOperator ROTATE = new VectorScalarOperator(2, ".R");
        public static VectorScalarOperator TIMES = new VectorScalarOperator(2, " *# ");
    }

    public class UnaryVectorExpr : VectorExpr {
        private readonly UnaryVectorOperator _operator;
        private readonly VectorExpr _inner;
        public UnaryVectorExpr(UnaryVectorOperator @operator, VectorExpr inner) {
            _operator = @operator;
            _inner = inner;
        }

        public UnaryVectorOperator Operator { get { return _operator; } }
        public VectorExpr Inner { get { return _inner; } }

        public bool Equals(UnaryVectorExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            UnaryVectorExpr o = obj as UnaryVectorExpr;
            return o != null && o._inner.Equals(_inner) && o._operator.Equals(_operator);
        }
        public override int GetHashCode() {
            return _inner.GetHashCode() + _operator.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_operator, _inner);
        }
    }

    public class UnaryVectorOperator : AbstractOperator {
        private UnaryVectorOperator(int precedence, string asString) : base(precedence, asString) { }
        public static UnaryVectorOperator MINUS = new UnaryVectorOperator(3, "#-");
        public static UnaryVectorOperator INTEGRAL = new UnaryVectorOperator(4, ".#i");
        public static UnaryVectorOperator DIFFERENTIAL = new UnaryVectorOperator(4, ".#d");
    }

    public class Vector : VectorExpr {
        private readonly ScalarExpr _x;
        private readonly ScalarExpr _y;
        private readonly ScalarExpr _z;
        public Vector(ScalarExpr x, ScalarExpr y, ScalarExpr z) {
            _x = x;
            _y = y;
            _z = z;
        }

        public ScalarExpr X { get { return _x; } }
        public ScalarExpr Y { get { return _y; } }
        public ScalarExpr Z { get { return _z; } }

        public bool Equals(Vector obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            Vector o = obj as Vector;
            return o != null && o._x.Equals(_x) && o._y.Equals(_y) && o._z.Equals(_z);
        }
        public override int GetHashCode() {
            return _x.GetHashCode() + _y.GetHashCode() + _z.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return "[" + _x + "," + _y + "," + _z + "]";
        }
    }

    public class VectorVariable : VectorExpr { // Also _
        private readonly string _name;
        public VectorVariable(string name) {
            Debug.Assert(name != null);
            _name = name;
        }

        public string Name { get { return _name; } }

        public bool Equals(VectorVariable obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            VectorVariable o = obj as VectorVariable;
            return o != null && o._name == _name;
        }
        public override int GetHashCode() {
            return _name.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return "#" + _name;
        }
    }

    public class Anchor : VectorExpr {
        private readonly string _thing;
        private readonly string _name;
        public Anchor(string thing, string name) {
            _thing = thing;
            _name = name;
        }

        public string Thing { get { return _thing; } }
        public string Name { get { return _name; } }

        public bool Equals(Anchor obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            Anchor o = obj as Anchor;
            return o != null && o._thing.Equals(_thing) && o._name.Equals(_name);
        }
        public override int GetHashCode() {
            return _thing.GetHashCode() + _name.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return "#" + _thing + "." + _name;
        }
    }

    #endregion VectorExpr

    #region ScalarExpr

    public abstract class ScalarExpr : Expr { }

    public class BinaryScalarExpr : ScalarExpr {
        private readonly ScalarExpr _lhs;
        private readonly BinaryScalarOperator _operator;
        private readonly ScalarExpr _rhs;
        public BinaryScalarExpr(ScalarExpr lhs, BinaryScalarOperator @operator, ScalarExpr rhs) {
            _lhs = lhs;
            _operator = @operator;
            _rhs = rhs;
        }

        public ScalarExpr Lhs { get { return _lhs; } }
        public BinaryScalarOperator Operator { get { return _operator; } }
        public ScalarExpr Rhs { get { return _rhs; } }

        public bool Equals(BinaryScalarExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            BinaryScalarExpr o = obj as BinaryScalarExpr;
            return o != null && o._lhs.Equals(_lhs) && o._operator.Equals(_operator) && o._rhs.Equals(_rhs);
        }
        public override int GetHashCode() {
            return _lhs.GetHashCode() + _operator.GetHashCode() + _rhs.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_lhs, _operator, _rhs);
        }
    }

    public class BinaryScalarOperator : AbstractOperator {
        private BinaryScalarOperator(int precedence, string asString) : base(precedence, asString) { }
        public static BinaryScalarOperator PLUS = new BinaryScalarOperator(1, " + ");
        public static BinaryScalarOperator MINUS = new BinaryScalarOperator(1, " - ");
        public static BinaryScalarOperator TIMES = new BinaryScalarOperator(2, " * ");
        public static BinaryScalarOperator DIVIDE = new BinaryScalarOperator(2, " / ");
    }

    public class UnaryScalarExpr : ScalarExpr {
        private readonly UnaryScalarOperator _operator;
        private readonly ScalarExpr _inner;
        public UnaryScalarExpr(UnaryScalarOperator @operator, ScalarExpr inner) {
            _operator = @operator;
            _inner = inner;
        }

        public UnaryScalarOperator Operator { get { return _operator; } }
        public ScalarExpr Inner { get { return _inner; } }

        public bool Equals(UnaryScalarExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            UnaryScalarExpr o = obj as UnaryScalarExpr;
            return o != null && o._inner.Equals(_inner) && o._operator.Equals(_operator);
        }
        public override int GetHashCode() {
            return _inner.GetHashCode() + _operator.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_operator, _inner);
        }
    }

    public class UnaryScalarOperator : AbstractOperator {
        private UnaryScalarOperator(int precedence, string asString) : base(precedence, asString) { }
        public static UnaryScalarOperator MINUS = new UnaryScalarOperator(3, "._-");
        public static UnaryScalarOperator SQUARED = new UnaryScalarOperator(4, "^2");
        public static UnaryScalarOperator CUBED = new UnaryScalarOperator(4, "^3");
        public static UnaryScalarOperator SQUAREROOT = new UnaryScalarOperator(4, "._q");
        public static UnaryScalarOperator INTEGRAL = new UnaryScalarOperator(5, "._i");
        public static UnaryScalarOperator DIFFERENTIAL = new UnaryScalarOperator(5, "._d");
    }

    public class BinaryScalarVectorExpr : ScalarExpr {
        private readonly VectorExpr _lhs;
        private readonly BinaryScalarVectorOperator _operator;
        private readonly VectorExpr _rhs;
        public BinaryScalarVectorExpr(VectorExpr lhs, BinaryScalarVectorOperator @operator, VectorExpr rhs) {
            _lhs = lhs;
            _operator = @operator;
            _rhs = rhs;
        }

        public VectorExpr Lhs { get { return _lhs; } }
        public BinaryScalarVectorOperator Operator { get { return _operator; } }
        public VectorExpr Rhs { get { return _rhs; } }

        public bool Equals(BinaryScalarVectorExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            BinaryScalarVectorExpr o = obj as BinaryScalarVectorExpr;
            return o != null && o._lhs.Equals(_lhs) && o._operator.Equals(_operator) && o._rhs.Equals(_rhs);
        }
        public override int GetHashCode() {
            return _lhs.GetHashCode() + _operator.GetHashCode() + _rhs.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_lhs, _operator, _rhs);
        }
    }

    public class BinaryScalarVectorOperator : AbstractOperator {
        private BinaryScalarVectorOperator(int precedence, string asString) : base(precedence, asString) { }
        public static BinaryScalarVectorOperator ANGLE = new BinaryScalarVectorOperator(5, " #a# ");
    }

    public class UnaryScalarVectorExpr : ScalarExpr {
        private readonly VectorExpr _inner;
        private readonly UnaryScalarVectorOperator _operator;
        public UnaryScalarVectorExpr(VectorExpr inner, UnaryScalarVectorOperator @operator) {
            _inner = inner;
            _operator = @operator;
        }

        public VectorExpr Inner { get { return _inner; } }
        public UnaryScalarVectorOperator Operator { get { return _operator; } }

        public bool Equals(UnaryScalarVectorExpr obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            UnaryScalarVectorExpr o = obj as UnaryScalarVectorExpr;
            return o != null && o._inner.Equals(_inner) && o._operator.Equals(_operator);
        }
        public override int GetHashCode() {
            return _inner.GetHashCode() - +_operator.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return parentOp.Wrap(_operator, _inner);
        }
    }

    public class UnaryScalarVectorOperator : AbstractOperator {
        private UnaryScalarVectorOperator(int precedence, string asString) : base(precedence, asString) { }
        public static UnaryScalarVectorOperator X = new UnaryScalarVectorOperator(5, ".x");
        public static UnaryScalarVectorOperator Y = new UnaryScalarVectorOperator(5, ".y");
        public static UnaryScalarVectorOperator Z = new UnaryScalarVectorOperator(5, ".z");
        public static UnaryScalarVectorOperator LENGTH = new UnaryScalarVectorOperator(5, ".l");
    }

    public class Constant : ScalarExpr {
        private readonly double _value;
        public Constant(double value) {
            _value = value;
        }

        public double Value { get { return _value; } }

        public bool Equals(Constant obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            return obj is Constant && Math.Abs(((Constant)obj)._value - _value) < 1e-10; // ?? relativer Fehler?
        }
        public override int GetHashCode() {
            return _value.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return "" + _value;
        }
    }

    public class ScalarVariable : ScalarExpr { // Also _
        private readonly string _name;
        public ScalarVariable(string name) {
            Debug.Assert(name != null);
            _name = name;
        }

        public string Name { get { return _name; } }

        public bool Equals(ScalarVariable obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            ScalarVariable o = obj as ScalarVariable;
            return o != null && o._name == _name;
        }
        public override int GetHashCode() {
            return _name.GetHashCode();
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return "_" + _name;
        }
    }

    public class T : ScalarExpr {
        public bool Equals(T obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            return obj is T;
        }
        public override int GetHashCode() {
            return 0;
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return ".T";
        }
    }

    public class IV : ScalarExpr {
        public bool Equals(IV obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            return obj is IV;
        }
        public override int GetHashCode() {
            return 1;
        }

        protected internal override string ToString(AbstractOperator parentOp) {
            return ".IV";
        }
    }

    #endregion ScalarExpr
}
