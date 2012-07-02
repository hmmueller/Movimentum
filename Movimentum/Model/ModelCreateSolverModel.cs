using System;
using System.Linq;
using Movimentum.SubstitutionSolver3;

namespace Movimentum.Model {
    #region Step

    public abstract partial class Constraint {
        public abstract AbstractConstraint[] CreateSolverConstraints(double t, double iv);
    }

    public partial class VectorEqualityConstraint {
        public override AbstractConstraint[] CreateSolverConstraints(double t, double iv) {
            AbstractExpr[] anchorExpr = _anchor.CreateSolverExpressions(t, iv);
            AbstractExpr[] rhsExpr = _rhs.CreateSolverExpressions(t, iv);
            return new[] {
                CreateEqualsZero(anchorExpr[0], rhsExpr[0]),
                CreateEqualsZero(anchorExpr[1], rhsExpr[1]),
                CreateEqualsZero(anchorExpr[2], rhsExpr[2]),
            };
        }

        private EqualsZeroConstraint CreateEqualsZero(AbstractExpr lhs, AbstractExpr rhs) {
            return new EqualsZeroConstraint(BinaryScalarOperator.MINUS.CreateSolverExpression(lhs, rhs));
        }
    }

    public partial class ScalarEqualityConstraint {
        public override AbstractConstraint[] CreateSolverConstraints(double t, double iv) {
            return new[] { new EqualsZeroConstraint(
                            BinaryScalarOperator.MINUS.CreateSolverExpression(
                                new NamedVariable(_variable), 
                                _rhs.CreateSolverExpression(t, iv)))
            };
        }
    }

    public partial class ScalarInequalityConstraint {
        public override AbstractConstraint[] CreateSolverConstraints(double t, double iv) {
            if (_operator == ScalarInequalityOperator.LT) {
                return new[] { new MoreThanZeroConstraint(BinaryScalarOperator.MINUS.CreateSolverExpression(_rhs.CreateSolverExpression(t, iv), new NamedVariable(_variable))) };
            } else if (_operator == ScalarInequalityOperator.LE) {
                return new[] { new AtLeastZeroConstraint(BinaryScalarOperator.MINUS.CreateSolverExpression(_rhs.CreateSolverExpression(t, iv), new NamedVariable(_variable))) };
            } else if (_operator == ScalarInequalityOperator.GT) {
                return new[] { new MoreThanZeroConstraint(BinaryScalarOperator.MINUS.CreateSolverExpression(new NamedVariable(_variable), _rhs.CreateSolverExpression(t, iv))) };
            } else if (_operator == ScalarInequalityOperator.GE) {
                return new[] { new AtLeastZeroConstraint(BinaryScalarOperator.MINUS.CreateSolverExpression(new NamedVariable(_variable), _rhs.CreateSolverExpression(t, iv))) };
            } else {
                throw new Exception("Unexpected Operator in ScalarInequalityConstraint");
            }
        }
    }


    #endregion Step

    #region VectorExpr

    public abstract partial class VectorExpr {
        public abstract AbstractExpr[] CreateSolverExpressions(double t, double iv);
    }

    public partial class BinaryVectorExpr {
        public override AbstractExpr[] CreateSolverExpressions(double t, double iv) {
            AbstractExpr[] lhsExpr = _lhs.CreateSolverExpressions(t, iv);
            AbstractExpr[] rhsExpr = _rhs.CreateSolverExpressions(t, iv);
            return _operator.CreateSolverExpressions(lhsExpr, rhsExpr);
        }
    }

    public partial class BinaryVectorOperator {
        private Func<AbstractExpr[], AbstractExpr[], AbstractExpr[]> _create;
        public AbstractExpr[] CreateSolverExpressions(AbstractExpr[] lhsExpr, AbstractExpr[] rhsExpr) {
            return _create(lhsExpr, rhsExpr);
        }

        static void BinaryVectorOperatorInit() {
            PLUS._create = (lhsExpr, rhsExpr) => new[] {
                BinaryScalarOperator.PLUS.CreateSolverExpression(lhsExpr[0], rhsExpr[0]),
                BinaryScalarOperator.PLUS.CreateSolverExpression(lhsExpr[1], rhsExpr[1]),
                BinaryScalarOperator.PLUS.CreateSolverExpression(lhsExpr[2], rhsExpr[2])
            };
            MINUS._create = (lhsExpr, rhsExpr) => new[] {
                BinaryScalarOperator.MINUS.CreateSolverExpression(lhsExpr[0], rhsExpr[0]),
                BinaryScalarOperator.MINUS.CreateSolverExpression(lhsExpr[1], rhsExpr[1]),
                BinaryScalarOperator.MINUS.CreateSolverExpression(lhsExpr[2], rhsExpr[2])
            };
        }
    }

    public partial class BinaryScalarVectorExpr {
        public override AbstractExpr[] CreateSolverExpressions(double t, double iv) {
            AbstractExpr[] lhsExpr = _lhs.CreateSolverExpressions(t, iv);
            AbstractExpr rhsExpr = _rhs.CreateSolverExpression(t, iv);
            return _operator.CreateSolverExpressions(lhsExpr, rhsExpr);
        }
    }

    public partial class BinaryScalarVectorOperator {
        private Func<AbstractExpr[], AbstractExpr, AbstractExpr[]> _create;
        public AbstractExpr[] CreateSolverExpressions(AbstractExpr[] lhsExpr, AbstractExpr rhsExpr) {
            return _create(lhsExpr, rhsExpr);
        }

        private static AbstractExpr[] Rotate2D(AbstractExpr[] lhsExpr, AbstractExpr rhsExpr) {
            // X = x cos phi - y sin phi
            // Y = x sin phi + y cos phi
            // Z = z

            var sin = new UnaryExpression(rhsExpr, new Sin());
            var cos = new UnaryExpression(rhsExpr, new Cos());
            var a11 = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[0], cos);
            var a12 = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[1], sin);
            var a21 = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[0], sin);
            var a22 = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[1], cos);

            return new[] {
                BinaryScalarOperator.MINUS.CreateSolverExpression(a11, a12),
                BinaryScalarOperator.PLUS.CreateSolverExpression(a21, a22),
                lhsExpr[2]
            };
        }

        private static AbstractExpr[] Times(AbstractExpr[] lhsExpr, AbstractExpr rhsExpr) {
            return new[] {
                BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[0], rhsExpr),
                BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[1], rhsExpr),
                BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[2], rhsExpr),
            };
        }

        static void BinaryScalarVectorOperatorInit() {
            ROTATE2D._create = Rotate2D;
            TIMES._create = Times;
        }
    }

    public partial class UnaryVectorExpr {
        public override AbstractExpr[] CreateSolverExpressions(double t, double iv) {
            AbstractExpr[] innerExpr = _inner.CreateSolverExpressions(t, iv);
            return _operator.CreateSolverExpressions(innerExpr);
        }
    }

    public partial class UnaryVectorOperator {
        private Func<AbstractExpr[], AbstractExpr[]> _create;
        public AbstractExpr[] CreateSolverExpressions(AbstractExpr[] innerExpr) {
            return _create(innerExpr);
        }

        static void UnaryVectorOperatorInit() {
            MINUS._create = innerExpr => new[] {
                UnaryScalarOperator.MINUS.CreateSolverExpression(innerExpr[0]),
                UnaryScalarOperator.MINUS.CreateSolverExpression(innerExpr[1]),
                UnaryScalarOperator.MINUS.CreateSolverExpression(innerExpr[2]),
            };
            INTEGRAL._create = innerExpr => new[] {
                UnaryScalarOperator.INTEGRAL.CreateSolverExpression(innerExpr[0]),
                UnaryScalarOperator.INTEGRAL.CreateSolverExpression(innerExpr[1]),
                UnaryScalarOperator.INTEGRAL.CreateSolverExpression(innerExpr[2]),
            };
            DIFFERENTIAL._create = innerExpr => new[] {
                UnaryScalarOperator.DIFFERENTIAL.CreateSolverExpression(innerExpr[0]),
                UnaryScalarOperator.DIFFERENTIAL.CreateSolverExpression(innerExpr[1]),
                UnaryScalarOperator.DIFFERENTIAL.CreateSolverExpression(innerExpr[2]),
            };
        }

    }

    public partial class Vector {
        public override AbstractExpr[] CreateSolverExpressions(double t, double iv) {
            return new[] {
                _x.CreateSolverExpression(t, iv),
                _y.CreateSolverExpression(t, iv),
                _z.CreateSolverExpression(t, iv),
            };
        }
    }

    public partial class VectorVariable {
        public override AbstractExpr[] CreateSolverExpressions(double t, double iv) {
            return new[] {
                new NamedVariable(_name + "^X"),
                new NamedVariable(_name + "^Y"),
                new NamedVariable(_name + "^Z"),
            };
        }
    }

    public partial class Anchor {
        public enum Coordinate { X, Y, Z }

        public override AbstractExpr[] CreateSolverExpressions(double t, double iv) {
            return new[] {
                new AnchorVariable(this, Coordinate.X),
                new AnchorVariable(this, Coordinate.Y),
                new AnchorVariable(this, Coordinate.Z),
            };
        }
    }

    #endregion VectorExpr

    #region ScalarExpr

    public abstract partial class ScalarExpr {
        public abstract AbstractExpr CreateSolverExpression(double t, double iv);
    }

    public partial class BinaryScalarExpr {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            AbstractExpr lhsExpr = _lhs.CreateSolverExpression(t, iv);
            AbstractExpr rhsExpr = _rhs.CreateSolverExpression(t, iv);
            return _operator.CreateSolverExpression(lhsExpr, rhsExpr);
        }
    }

    public partial class BinaryScalarOperator {
        private Func<AbstractExpr, AbstractExpr, AbstractExpr> _create;
        public AbstractExpr CreateSolverExpression(AbstractExpr lhsExpr, AbstractExpr rhsExpr) {
            return _create(lhsExpr, rhsExpr);
        }

        static void BinaryScalarOperatorInit() {
            PLUS._create = (lhsExpr, rhsExpr) => lhsExpr +  rhsExpr;
            MINUS._create = (lhsExpr, rhsExpr) => lhsExpr + (-rhsExpr);
            TIMES._create = (lhsExpr, rhsExpr) => lhsExpr *  rhsExpr;
            DIVIDE._create = (lhsExpr, rhsExpr) => lhsExpr / rhsExpr;
        }
    }

    public partial class UnaryScalarExpr {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            AbstractExpr innerExpr = _inner.CreateSolverExpression(t, iv);
            return _operator.CreateSolverExpression(innerExpr);
        }
    }

    public partial class UnaryScalarOperator {
        private Func<AbstractExpr, AbstractExpr> _create;
        public AbstractExpr CreateSolverExpression(AbstractExpr innerExpr) {
            return _create(innerExpr);
        }

        static void UnaryScalarOperatorInit() {
            MINUS._create = innerExpr => new UnaryExpression(innerExpr, new UnaryMinus());
            SQUARED._create = innerExpr => new UnaryExpression(innerExpr, new Square());
            //CUBED._create = innerExpr =>
            //    BinaryScalarOperator.TIMES.CreateSolverExpression(SQUARED.CreateSolverExpression(innerExpr), innerExpr);
            SQUAREROOT._create = innerExpr => new UnaryExpression(innerExpr, new FormalSquareroot());
            //INTEGRAL._create = innerExpr => new UnaryExpression(innerExpr, new Integral());
            //DIFFERENTIAL._create = innerExpr => new UnaryExpression(innerExpr, new Differential());
        }
    }

    public partial class BinaryVectorScalarExpr {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            AbstractExpr[] lhsExpr = _lhs.CreateSolverExpressions(t, iv);
            AbstractExpr[] rhsExpr = _rhs.CreateSolverExpressions(t, iv);
            return _operator.CreateSolverExpression(lhsExpr, rhsExpr);
        }
    }

    public partial class BinaryVectorScalarOperator {
        private Func<AbstractExpr[], AbstractExpr[], AbstractExpr> _create;
        public AbstractExpr CreateSolverExpression(AbstractExpr[] lhsExpr, AbstractExpr[] rhsExpr) {
            return _create(lhsExpr, rhsExpr);
        }

        private static AbstractExpr Angle(AbstractExpr[] lhsExpr, AbstractExpr[] rhsExpr) {
            return BinaryScalarOperator.DIVIDE.CreateSolverExpression(
                    Times(lhsExpr, rhsExpr),
                    BinaryScalarOperator.TIMES.CreateSolverExpression(
                        UnaryVectorScalarOperator.LENGTH.CreateSolverExpression(lhsExpr),
                        UnaryVectorScalarOperator.LENGTH.CreateSolverExpression(rhsExpr))
                );
        }

        private static AbstractExpr Times(AbstractExpr[] lhsExpr, AbstractExpr[] rhsExpr) {
            var x = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[0], rhsExpr[0]);
            var y = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[1], rhsExpr[1]);
            var z = BinaryScalarOperator.TIMES.CreateSolverExpression(lhsExpr[2], rhsExpr[2]);
            return BinaryScalarOperator.PLUS.CreateSolverExpression(x,
                BinaryScalarOperator.PLUS.CreateSolverExpression(y, z));
        }

        static void BinaryVectorScalarOperatorInit() {
            ANGLE._create = Angle;
            TIMES._create = Times;
        }
    }

    public partial class UnaryVectorScalarExpr {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            AbstractExpr[] innerExpr = _inner.CreateSolverExpressions(t, iv);
            return _operator.CreateSolverExpression(innerExpr);
        }
    }

    public partial class UnaryVectorScalarOperator {
        private Func<AbstractExpr[], AbstractExpr> _create;
        public AbstractExpr CreateSolverExpression(AbstractExpr[] innerExpr) {
            return _create(innerExpr);
        }

        static void UnaryVectorScalarOperatorInit() {
            X._create = innerExpr => innerExpr[0];
            Y._create = innerExpr => innerExpr[1];
            Z._create = innerExpr => innerExpr[2];
            LENGTH._create = innerExpr => UnaryScalarOperator.SQUAREROOT.CreateSolverExpression(
                BinaryVectorScalarOperator.TIMES.CreateSolverExpression(innerExpr, innerExpr));
        }
    }

    public partial class RangeScalarExpr {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            return new RangeExpr(_expr.CreateSolverExpression(t, iv),
                _value0.CreateSolverExpression(t, iv),
                _pairs.Select(p => new RangeExpr.Pair(p.MoreThan.CreateSolverExpression(t, iv), p.Value.CreateSolverExpression(t, iv))));
        }
    }

    public partial class ConstScalar {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            return new Constant(_value);
        }
    }

    public partial class ScalarVariable {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            return new NamedVariable(_name);
        }
    }

    public partial class T {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            return new Constant(t);
        }
    }

    public partial class IV {
        public override AbstractExpr CreateSolverExpression(double t, double iv) {
            return new Constant(iv);
        }
    }

    #endregion ScalarExpr
}
