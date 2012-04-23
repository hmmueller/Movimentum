using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

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

    public class ConstVector { // mhm ... shouldn't the doubles be scalar expressions?
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        public ConstVector(double x, double y, double z) {
            _x = x;
            _y = y;
            _z = z;
        }

        public double X { get { return _x; } }
        public double Y { get { return _y; } }
        public double Z { get { return _z; } }

        public bool Equals(ConstVector obj) {
            return Equals((object)obj);
        }
        public override bool Equals(object obj) {
            ConstVector o = obj as ConstVector;
            return o != null && o._x.Equals(_x) && o._y.Equals(_y) && o._z.Equals(_z);
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

    abstract public class Constraint {}

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
            return "(" + _variable + OpString() + _rhs + ")";
        }
        private string OpString() {
            switch (_operator) {
                case ScalarInequalityOperator.LT:
                    return "_<_";
                case ScalarInequalityOperator.LE:
                    return "_<=_";
                case ScalarInequalityOperator.GT:
                    return "_>_";
                case ScalarInequalityOperator.GE:
                    return "_>=_";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum ScalarInequalityOperator { LT, LE, GT, GE }

    #endregion Step

    #region VectorExpr

    abstract public class VectorExpr { }

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
            return _lhs.GetHashCode() + (int)_operator + _rhs.GetHashCode();
        }

        public override string ToString() {
            return "(" + _lhs + OpString() + _rhs + ")";
        }
        private string OpString() {
            switch (_operator) {
                case BinaryVectorOperator.PLUS:
                    return "#+#";
                case BinaryVectorOperator.MINUS:
                    return "#-#";
                //case BinaryVectorOperator.TIMES:
                //    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum BinaryVectorOperator { PLUS, MINUS /*, TIMES*/ }

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
            return _lhs.GetHashCode() + (int)_operator + _rhs.GetHashCode();
        }

        public override string ToString() {
            return "(" + _lhs + OpString() + _rhs + ")";
        }
        private string OpString() {
            switch (_operator) {
                case VectorScalarOperator.ROTATE:
                    return "#ROT_";
                case VectorScalarOperator.TIMES:
                    return "#*_";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum VectorScalarOperator { ROTATE, TIMES }

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
            return _inner.GetHashCode() + (int)_operator;
        }

        public override string ToString() {
            return "(" + OpString() + _inner + ")";
        }
        private string OpString() {
            switch (_operator) {
                case UnaryVectorOperator.MINUS:
                    return "#-";
                case UnaryVectorOperator.INTEGRAL:
                    return "#I:";
                case UnaryVectorOperator.DIFFERENTIAL:
                    return "#D:";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum UnaryVectorOperator { MINUS, INTEGRAL, DIFFERENTIAL }

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

        public override string ToString() {
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

        public override string ToString() {
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

        public override string ToString() {
            return "#" + _thing + "." + _name;
        }
    }

    #endregion VectorExpr

    #region ScalarExpr

    abstract public class ScalarExpr { }

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
            return _lhs.GetHashCode() + (int)_operator + _rhs.GetHashCode();
        }

        public override string ToString() {
            return "(" + _lhs + OpString() + _rhs + ")";
        }
        private string OpString() {
            switch (_operator) {
                case BinaryScalarOperator.PLUS:
                    return "_+_";
                case BinaryScalarOperator.MINUS:
                    return "_-_";
                case BinaryScalarOperator.TIMES:
                    return "_*_";
                case BinaryScalarOperator.DIVIDE:
                    return "_/_";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum BinaryScalarOperator { PLUS, MINUS, TIMES, DIVIDE }

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
            return _inner.GetHashCode() + (int)_operator;
        }
        public override string ToString() {
            return "(" + OpString() + _inner + ")";
        }
        private string OpString() {
            switch (_operator) {
                case UnaryScalarOperator.MINUS:
                    return "_-";
                case UnaryScalarOperator.INTEGRAL:
                    return "_i";
                case UnaryScalarOperator.DIFFERENTIAL:
                    return "_d";
                case UnaryScalarOperator.SQUARED:
                    return "_²";
                case UnaryScalarOperator.CUBED:
                    return "_³";
                case UnaryScalarOperator.SQUAREROOT:
                    return "_q";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum UnaryScalarOperator { MINUS, INTEGRAL, DIFFERENTIAL, SQUARED, CUBED, SQUAREROOT }

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
            return Equals((object) obj);
        }
        public override bool Equals(object obj) {
            BinaryScalarVectorExpr o = obj as BinaryScalarVectorExpr;
            return o != null && o._lhs.Equals(_lhs) && o._operator.Equals(_operator) && o._rhs.Equals(_rhs);
        }
        public override int GetHashCode() {
            return _lhs.GetHashCode() + (int)_operator + _rhs.GetHashCode();
        }

        public override string ToString() {
            return "(" + _lhs + OpString() + _rhs + ")";
        }
        private string OpString() {
            switch (_operator) {
                case BinaryScalarVectorOperator.ANGLE:
                    return "#a#";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum BinaryScalarVectorOperator { ANGLE }

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
            return _inner.GetHashCode() + (int) _operator;
        }
        public override string ToString() {
            return "(" + _inner + OpString() + ")";
        }
        private string OpString() {
            switch (_operator) {
                case UnaryScalarVectorOperator.LENGTH:
                    return ".L";
                case UnaryScalarVectorOperator.X:
                    return ".X";
                case UnaryScalarVectorOperator.Y:
                    return ".Y";
                case UnaryScalarVectorOperator.Z:
                    return ".Z";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum UnaryScalarVectorOperator { LENGTH, X, Y, Z }

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

        public override string ToString() {
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

        public override string ToString() {
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

        public override string ToString() {
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

        public override string ToString() {
            return ".IV";
        }
    }

    #endregion ScalarExpr
}
