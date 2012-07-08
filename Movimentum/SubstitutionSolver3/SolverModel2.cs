using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    #region Input constraints

    public abstract partial class AbstractConstraint {
        public abstract RewriteSet CreateRewritesFromSingleConstraint(
            IEnumerable<AbstractConstraint> allOtherConstraints,
            IDictionary<Variable, VariableRangeRestriction> previousValues,
            IDictionary<Variable, VariableRangeRestriction> restrictions,
            int maxNeededRank);

        //protected static VariableRangeRestriction GetVariableRangeRestriction(
        //        Variable variable,
        //        IDictionary<Variable, VariableRangeRestriction> variableRangeRestrictions) {
        //    VariableRangeRestriction result;
        //    return !variableRangeRestrictions.TryGetValue(variable, out result)
        //        ? new VariableRangeRestriction(variable, Double.NegativeInfinity, Double.PositiveInfinity)
        //        : result;
        //}
    }

    public abstract partial class ScalarConstraint {
        //private static readonly ConstantFoldingVisitor CONSTANT_FOLDING_VISITOR = new ConstantFoldingVisitor();

        //protected ScalarConstraint(AbstractExpr expr) {
        //    _expr = expr.Accept(CONSTANT_FOLDING_VISITOR, Ig.nore);
        //}
    }

    public partial class EqualsZeroConstraint {
        ////public override RewriteSet CreateRewritesFromSingleConstraint(IEnumerable<AbstractConstraint> allOtherConstraints, IDictionary<Variable, VariableRangeRestriction> previousValues, IDictionary<Variable, VariableRangeRestriction> restrictions, int maxNeededRank) {
        ////    if (maxNeededRank >= 1) {
        ////        // 0 = c
        ////        var c = new Catch<Constant>();
        ////        if (Expr.Unify(c)) {
        ////            return new TrueFalseRewrite(this, c.Match.Value.Near(0)).AsSet(1);
        ////        }
        ////    }
        ////    if (maxNeededRank >= 2) {
        ////        // 0 = v
        ////        var v = new Catch<Variable>();
        ////        if (Expr.Unify(v)) {
        ////            Variable variable = v.Match;
        ////            return new ReplaceRewrite(this,
        ////                variable,
        ////                new Constant(0),
        ////                cs => cs.AddVariableRestriction(new VariableRangeRestriction(variable, 0))).AsSet(2);
        ////        }
        ////    }
        ////    if (maxNeededRank >= 3) {
        ////        // 0 = v + c
        ////        var v = new Catch<Variable>();
        ////        var c = new Catch<Constant>();
        ////        if (Expr.Unify(new BinaryExpression(v, new Plus(), c))) {
        ////            Variable variable = v.Match;
        ////            double value = -c.Match.Value;
        ////            return new ReplaceRewrite(this,
        ////                variable,
        ////                new Constant(value),
        ////                cs => cs.AddVariableRestriction(new VariableRangeRestriction(variable, value))).AsSet(3);
        ////        }
        ////    }
        ////    if (maxNeededRank >= 4) {
        ////        // Split constraint with formal square root
        ////        var findFormalSquarerootVisitor = new FindFormalSquarerootVisitor();
        ////        Expr.Accept(findFormalSquarerootVisitor, Ig.nore);
        ////        if (findFormalSquarerootVisitor.InnermostFormalSquarerootExpressions.Any()) {
        ////            IEnumerable<ReplaceRewrite> formalSquarerootRewrites = CreateFormalSquarerootRewrites(findFormalSquarerootVisitor.InnermostFormalSquarerootExpressions);
        ////            return new RewriteSet(formalSquarerootRewrites, 4);
        ////        }
        ////    }
        ////    if (maxNeededRank >= 5) {
        ////        var dv = new VariableDegreeVisitor();
        ////        Dictionary<Variable, VariableDegree> degrees = Expr.Accept(dv, Ig.nore);
        ////        if (degrees.Count == 1 && degrees.First().Value == VariableDegree.One) {
        ////            // Linear expression with a single variable!
        ////            Variable variable = degrees.First().Key;
        ////            double y0 = Expr.Accept(new EvaluationVisitor(variable, 0), Ig.nore);
        ////            double y1 = Expr.Accept(new EvaluationVisitor(variable, 1), Ig.nore);
        ////            if (y0.Near(y1)) {
        ////                // Horizontal line
        ////                return new TrueFalseRewrite(this, y0.Near(0)).AsSet(5);
        ////            } else {
        ////                // Compute solution
        ////                double x0 = y0 / (y0 - y1);
        ////                return new ReplaceRewrite(this,
        ////                    variable,
        ////                    new Constant(x0),
        ////                    cs => cs.AddVariableRestriction(new VariableRangeRestriction(variable, x0))).AsSet(5);
        ////            }
        ////        }
        ////    }
        ////    if (maxNeededRank >= 6) {
        ////        // 0 = v + (expr without v)
        ////        var v = new Catch<Variable>();
        ////        var e = new Catch<AbstractExpr>();
        ////        if (Expr.Unify(new BinaryExpression(v, new Plus(), e))) {
        ////            Variable variable = v.Match;
        ////            AbstractExpr expr = e.Match.UnaryMinus();
        ////            return new BacksubstitutionRewrite(this,
        ////                variable,
        ////                expr
        ////                ).AsSet(6);
        ////        }
        ////    }
        ////    // rank 7 is used for creating VariableRestrictions from AtLeastZeroConstraint - BEFORE we go into solving (with square roots!)!
        ////    if (maxNeededRank >= 8) {
        ////        // Solve equations with only a single variable
        ////        ISolver solver = new CompoundSolver(Expr, 0.1);
        ////        if (solver.IsSolvable) {
        ////            #region TEMPORARY!!!

        ////            if (solver.Variable.Name == "x") { }
        ////            // x for our first test case is always 10.
        ////            double value = 10;
        ////            return new ReplaceRewrite(this,
        ////                                      solver.Variable,
        ////                                      new Constant(value),
        ////                                      cs => cs.AddVariableRestriction(new VariableRangeRestriction(solver.Variable, value))).AsSet(8);
        ////            #endregion TEMPORARY!!!


        ////            //VariableRangeRestriction previousValue = GetVariableRangeRestriction(solver.Variable, previousValues);
        ////            //VariableRangeRestriction restriction = GetVariableRangeRestriction(solver.Variable, restrictions);
        ////            //double value = solver.Solve(previousValue, restriction);
        ////            //if (double.IsNaN(value)) {
        ////            //    return new TrueFalseRewrite(this, false).AsSet(8);
        ////            //} else {
        ////            //    return new ReplaceRewrite(this,
        ////            //                              solver.Variable,
        ////            //                              new Constant(value),
        ////            //                              cs => cs.AddVariableRestriction(new VariableRangeRestriction(solver.Variable, value))).AsSet(8);
        ////            //}
        ////        }
        ////    }

        ////    return new NoRewrite(this).AsSet(int.MaxValue - 2);
        ////}

        public override RewriteSet CreateRewritesFromSingleConstraint(IEnumerable<AbstractConstraint> allOtherConstraints, IDictionary<Variable, VariableRangeRestriction> previousValues, IDictionary<Variable, VariableRangeRestriction> restrictions, int maxNeededRank) {
            if (maxNeededRank >= 1) {
                // 0 = c
                var c = new TypeMatchTemplate<Constant>();
                ExpressionMatcher m = c.CreateMatcher();
                if (m.TryMatch(Expr)) {
                    return new TrueFalseRewrite(this, m.Match(c).Value.Near(0)).AsSet(1);
                }
            }
            if (maxNeededRank >= 2) {
                // 0 = v
                var v = new TypeMatchTemplate<Variable>();
                ExpressionMatcher m = v.CreateMatcher();
                if (m.TryMatch(Expr)) {
                    Variable variable = m.Match(v);
                    return new ReplaceRewrite(this,
                        variable,
                        new Constant(0),
                        cs => cs.AddVariableRestriction(new VariableValueRestriction(variable, 0))).AsSet(2);
                }
            }
            if (maxNeededRank >= 3) {
                // 0 = v + c
                var v = new TypeMatchTemplate<Variable>();
                var c = new TypeMatchTemplate<Constant>();
                ExpressionMatcher b = (v + c).CreateMatcher();
                if (b.TryMatch(Expr)) {
                    Variable variable = b.Match(v);
                    double value = -(b.Match(c)).Value;
                    return new ReplaceRewrite(this,
                        variable,
                        new Constant(value),
                        cs => cs.AddVariableRestriction(new VariableValueRestriction(variable, value))).AsSet(3);
                }
            }
            if (maxNeededRank >= 4) {
                // Split constraint with formal square root
                var findFormalSquarerootVisitor = new FindFormalSquarerootVisitor();
                Expr.Accept(findFormalSquarerootVisitor, Ig.nore);
                if (findFormalSquarerootVisitor.InnermostFormalSquarerootExpressions.Any()) {
                    IEnumerable<ReplaceRewrite> formalSquarerootRewrites = CreateFormalSquarerootRewrites(findFormalSquarerootVisitor.InnermostFormalSquarerootExpressions);
                    return new RewriteSet(formalSquarerootRewrites, 4);
                }
            }
            if (maxNeededRank >= 5) {
                var dv = new VariableDegreeVisitor();
                Dictionary<Variable, VariableDegree> degrees = Expr.Accept(dv, Ig.nore);
                if (degrees.Count == 1 && degrees.First().Value == VariableDegree.One) {
                    // Linear expression with a single variable!
                    Variable variable = degrees.First().Key;
                    double y0 = Expr.Accept(new EvaluationVisitor(variable, 0), Ig.nore);
                    double y1 = Expr.Accept(new EvaluationVisitor(variable, 1), Ig.nore);
                    if (y0.Near(y1)) {
                        // Horizontal line
                        return new TrueFalseRewrite(this, y0.Near(0)).AsSet(5);
                    } else {
                        // Compute solution
                        double x0 = y0 / (y0 - y1);
                        return new ReplaceRewrite(this,
                            variable,
                            new Constant(x0),
                            cs => cs.AddVariableRestriction(new VariableValueRestriction(variable, x0))).AsSet(5);
                    }
                }
            }
            if (maxNeededRank >= 6) {
                // 0 = v + (expr without v)
                var v = new TypeMatchTemplate<Variable>();
                var e = new TypeMatchTemplate<AbstractExpr>();
                ExpressionMatcher m = (v + e).CreateMatcher();
                if (m.TryMatch(Expr)) {
                    Variable variable = m.Match(v);
                    AbstractExpr expr = -m.Match(e);
                    return new BacksubstitutionRewrite(this,
                        variable,
                        expr
                        ).AsSet(6);
                }
            }
            // rank 7 is used for creating VariableRestrictions from AtLeastZeroConstraint - BEFORE we go into solving (with square roots!)!
            if (maxNeededRank >= 8) {
                // Solve equations with only a single variable
                ISolver solver = new CompoundSolver(Expr, 0.1);
                if (solver.IsSolvable) {
                    #region TEMPORARY!!!

                    if (solver.Variable.Name == "x") { }
                    // x for our first test case is always 10.
                    double value = 10;
                    return new ReplaceRewrite(this,
                                              solver.Variable,
                                              new Constant(value),
                                              cs => cs.AddVariableRestriction(new VariableValueRestriction(solver.Variable, value))).AsSet(8);
                    #endregion TEMPORARY!!!


                    //VariableRangeRestriction previousValue = GetVariableRangeRestriction(solver.Variable, previousValues);
                    //VariableRangeRestriction restriction = GetVariableRangeRestriction(solver.Variable, restrictions);
                    //double value = solver.Solve(previousValue, restriction);
                    //if (double.IsNaN(value)) {
                    //    return new TrueFalseRewrite(this, false).AsSet(8);
                    //} else {
                    //    return new ReplaceRewrite(this,
                    //                              solver.Variable,
                    //                              new Constant(value),
                    //                              cs => cs.AddVariableRestriction(new VariableRangeRestriction(solver.Variable, value))).AsSet(8);
                    //}
                }
            }

            return new NoRewrite(this).AsSet(int.MaxValue - 2);
        }

        private IEnumerable<ReplaceRewrite> CreateFormalSquarerootRewrites(IEnumerable<UnaryExpression> innermostFormalSquarerootExpressions) {
            // Take first and create the two rewrites.
            UnaryExpression first = innermostFormalSquarerootExpressions.First();
            IEnumerable<UnaryExpression> rest = innermostFormalSquarerootExpressions.Skip(1);

            UnaryExpression positiveRoot = new UnaryExpression(first.Inner, new PositiveSquareroot());
            AbstractExpr negativeRoot = positiveRoot.UnaryMinus();

            Action<OLDSolverNode> addRootArgIsAtLeastZeroConstraint = cs => cs.AddConstraint(new AtLeastZeroConstraint(first.Inner));

            // If there are more, "multiply" the two rewrites with each rewrite from the other ones.
            // We get 2^k rewrites for k formal square roots.
            if (rest.Any()) {
                var restResult = CreateFormalSquarerootRewrites(rest);
                return restResult.SelectMany(r =>
                    new[] {
                              new ReplaceRewrite(this, Union(r.Rewrites, first, positiveRoot), r.Action + addRootArgIsAtLeastZeroConstraint),
                              new ReplaceRewrite(this, Union(r.Rewrites, first, negativeRoot), r.Action + addRootArgIsAtLeastZeroConstraint),
                          }
                    );
            } else {
                return new[] {
                               new ReplaceRewrite(this, first, positiveRoot, addRootArgIsAtLeastZeroConstraint),
                               new ReplaceRewrite(this, first, negativeRoot, addRootArgIsAtLeastZeroConstraint)
                           };
            }
        }

        private IDictionary<AbstractExpr, AbstractExpr> Union(IDictionary<AbstractExpr, AbstractExpr> rewrites, AbstractExpr from, AbstractExpr to) {
            return rewrites.ContainsKey(from)
                ? rewrites // what if to's differ??                
                : new Dictionary<AbstractExpr, AbstractExpr>(rewrites) { { from, to } };
        }
    }

    public partial class MoreThanZeroConstraint {
        public override RewriteSet CreateRewritesFromSingleConstraint(IEnumerable<AbstractConstraint> allOtherConstraints, IDictionary<Variable, VariableRangeRestriction> previousValues, IDictionary<Variable, VariableRangeRestriction> restrictions, int maxNeededRank) {
            throw new NotImplementedException("No rewrite implemented for " + this);
        }
    }

    public partial class AtLeastZeroConstraint {
        public override RewriteSet CreateRewritesFromSingleConstraint(IEnumerable<AbstractConstraint> allOtherConstraints, IDictionary<Variable, VariableRangeRestriction> previousValues, IDictionary<Variable, VariableRangeRestriction> restrictions, int maxNeededRank) {
            if (maxNeededRank >= 1) {
                // 0 <= c
                var c = new TypeMatchTemplate<Constant>();
                ExpressionMatcher m = c.CreateMatcher();
                if (m.TryMatch(Expr)) {
                    return new TrueFalseRewrite(this, m.Match(c).Value >= 0).AsSet(1);
                }
            }
            if (maxNeededRank >= 2) {
                // 0 <= v
                var v = new TypeMatchTemplate<Variable>();
                ExpressionMatcher m = v.CreateMatcher();
                if (m.TryMatch(Expr)) {
                    return new ActionRewrite(this, cs => cs.AddVariableRestriction(
                        new VariableMultipleRangeRestriction(m.Match(v), 0, double.PositiveInfinity)
                    )).AsSet(2);
                }
            }
            if (maxNeededRank >= 7) {
                // We try to solve for the rhs.
                ISolver solver = new CompoundSolver(Expr, 0.1);
                if (solver.IsSolvable) {
                    #region TEMPORARY!!!

                    if (solver.Variable.Name == "x") { }

                    return new ActionRewrite(this, cs => cs.AddVariableRestriction(
                        // x for our first test case is always 10.
                        new VariableMultipleRangeRestriction(solver.Variable, 0, 20))).AsSet(7);
                    #endregion TEMPORARY!!!



                    //VariableDegreeVisitor visitor = new VariableDegreeVisitor();
                    //Dictionary<Variable, VariableDegree> degree = Expr.Accept(visitor, Ig.nore);
                    //VariableDegree d;
                    //degree.TryGetValue(solver.Variable, out d);
                    //switch (d) {
                    //    case VariableDegree.Zero:
                    //        break;
                    //    case VariableDegree.One: {
                    //            // Linear function
                    //            VariableRangeRestriction previousValue = GetVariableRangeRestriction(solver.Variable, previousValues);
                    //            VariableRangeRestriction restriction = GetVariableRangeRestriction(solver.Variable, restrictions);
                    //            double zero = solver.Solve(previousValue, restriction);
                    //            var slope = Slope(solver.Variable, Expr, zero);
                    //            if (slope > 0) {
                    //                throw new NotImplementedException("Not yet completely implemented");
                    //            } else if (slope < 0) {
                    //                throw new NotImplementedException("Not yet completely implemented");
                    //            }
                    //        }
                    //        break;
                    //    case VariableDegree.Two: {
                    //            // Quadratic function
                    //            VariableRangeRestriction previousValue = GetVariableRangeRestriction(solver.Variable, previousValues);
                    //            ////VariableRangeRestriction restriction = GetVariableRangeRestriction(solver.Variable, restrictions);
                    //            VariableRangeRestriction restriction = new VariableRangeRestriction(solver.Variable, double.NegativeInfinity, double.PositiveInfinity);
                    //            double zero = solver.Solve(previousValue, restriction);
                    //            AbstractExpr expr2 = Expr / (solver.Variable + new Constant(-zero));
                    //            ISolver solver2 = new CompoundSolver(expr2, 0.1);
                    //            double zero2 = solver2.Solve(previousValue, restriction);

                    //            if (double.IsNaN(zero) && double.IsNaN(zero2)) {
                    //                zero = double.NegativeInfinity;
                    //                zero2 = double.PositiveInfinity;
                    //            }

                    //            return new ActionRewrite(this, cs => cs.AddVariableRestriction(
                    //                new VariableRangeRestriction(solver.Variable, Math.Min(zero, zero2), Math.Max(zero, zero2))
                    //                                                     )).AsSet(7);
                    //        }
                    //    case VariableDegree.Other:
                    //        break;
                    //    default:
                    //        throw new ArgumentOutOfRangeException();
                    //}
                }

            }
            return new NoRewrite(this).AsSet(int.MaxValue - 3);
        }

        private static double Slope(Variable variable, AbstractExpr expr, double value) {
            double f1 = expr.Accept(new EvaluationVisitor(variable, value - 1e-6), Ig.nore);
            double f2 = expr.Accept(new EvaluationVisitor(variable, value), Ig.nore);
            if (double.IsNaN(f1)) {
                f1 = f2;
                f2 = expr.Accept(new EvaluationVisitor(variable, value + 1e-6), Ig.nore);
            }
            return (f2 - f1) / 1e-6;
        }
    }

    #endregion Input constraints

    #region Expressions

    public abstract partial class AbstractExpr {
        public virtual AbstractExpr UnaryMinus() {
            return new UnaryExpression(this, new UnaryMinus());
        }
        public EqualsZeroConstraint EqualsZero() {
            return new EqualsZeroConstraint(this);
        }
        public MoreThanZeroConstraint MoreThanZero() {
            return new MoreThanZeroConstraint(this);
        }

        ////public abstract bool Unify(AbstractExpr other);
    }

    ////public abstract class Catch : AbstractExpr { }

    ////public class Catch<TCatch> : Catch where TCatch : AbstractExpr {
    ////    private TCatch _match;

    ////    public override string ToString() {
    ////        return "Catch<" + typeof(TCatch).Name + ">(" + (_match == null ? "?" : _match.ToString()) + ")";
    ////    }

    ////    public override TResult Accept<TParameter, TResult>(ISolverModelExprVisitor<TParameter, TResult> visitor, TParameter p) {
    ////        throw new NotSupportedException();
    ////    }

    ////    public override bool Unify(AbstractExpr other) {
    ////        // if (other is Catch) { ??? }
    ////        if (other is TCatch) {
    ////            _match = (TCatch)other;
    ////            return true;
    ////        } else {
    ////            return false;
    ////        }
    ////    }

    ////    public TCatch Match {
    ////        get { return _match; }
    ////    }
    ////}

    public abstract partial class AbstractOperator { }

    public partial class Constant {
        //public override bool Unify(AbstractExpr other) {
        //    if (other is Catch) {
        //        return other.Unify(this);
        //    } else if (other is Constant) {
        //        return ((Constant)other).Value.Near(Value); // ?? or Equals??
        //    } else {
        //        return false;
        //    }
        //}
    }

    public abstract partial class Variable {

        ////public override bool Unify(AbstractExpr other) {
        ////    if (other is Catch) {
        ////        return other.Unify(this);
        ////    } else if (other is Variable) {
        ////        return ((Variable)other).Name == Name;
        ////    } else {
        ////        return false;
        ////    }
        ////}
    }

    public partial class NamedVariable {
    }

    public partial class AnchorVariable {
    }

    public partial class UnaryExpression {
        //public override AbstractExpr UnaryMinus() {
        //    // -(-x) == x
        //    return _op is UnaryMinus ? _inner : base.UnaryMinus();
        //}

        ////public override bool Unify(AbstractExpr other) {
        ////    if (other is Catch) {
        ////        return other.Unify(this);
        ////    } else if (other is UnaryExpression) {
        ////        var otherUnary = (UnaryExpression)other;
        ////        return Op.Equals(otherUnary.Op) && Inner.Unify(otherUnary.Inner);
        ////    } else {
        ////        return false;
        ////    }
        ////}
    }

    public abstract partial class UnaryOperator {
        public abstract double Evaluate(double value);
    }

    public partial class UnaryMinus {
        public override double Evaluate(double value) {
            return -value;
        }
    }
    public partial class Square {
        public override double Evaluate(double value) {
            return value * value;
        }
    }
    public partial class FormalSquareroot {
        public override double Evaluate(double value) {
            throw new NotSupportedException("FormalSquareroot.Evaluate(double)");
        }
        //public override bool Equals(object obj) {
        //    // All formal square roots are not equal to each other - this is necessary for the ReplaceRewriters, so that they
        //    // treat each occurring formal root as a different expression. (Hopefully this works)
        //    return ReferenceEquals(this, obj);
        //}
        //public override int GetHashCode() {
        //    return base.GetHashCode();
        //}
    }

    public partial class PositiveSquareroot {
        public const double CUTOFF_ROOT = 1e-5;
        public const double CUTOFF = CUTOFF_ROOT * CUTOFF_ROOT;
        public override double Evaluate(double value) {
            if (value < -CUTOFF) {
                throw new NotSupportedException("sqrt(" + value + ")");
            } else if (value < CUTOFF) {
                // Linear fall of sqrt near zero --> 
                return (value + CUTOFF) * CUTOFF_ROOT;
            } else {
                return Math.Sqrt(value);
            }
        }
    }
    //public partial class Integral : UnaryOperator {
    //    public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
    //        return visitor.Visit(this, innerResult, p);
    //    }
    //}
    //public partial class Differential : UnaryOperator {
    //    public override TResult Accept<TExpression, TParameter, TResult>(ISolverModelUnaryOpVisitor<TExpression, TParameter, TResult> visitor, TExpression innerResult, TParameter p) {
    //        return visitor.Visit(this, innerResult, p);
    //    }
    //}
    public partial class Sin {
        public override double Evaluate(double value) {
            return Math.Sin(value / 180 * Math.PI);
        }
    }
    public partial class Cos {
        public override double Evaluate(double value) {
            return Math.Cos(value / 180 * Math.PI);
        }
    }

    public partial class BinaryExpression {

        ////public override bool Unify(AbstractExpr other) {
        ////    if (other is Catch) {
        ////        return other.Unify(this);
        ////    } else if (other is BinaryExpression) {
        ////        var otherBinary = (BinaryExpression)other;
        ////        return Op.Equals(otherBinary.Op) && Lhs.Unify(otherBinary.Lhs) && Rhs.Unify(otherBinary.Rhs);
        ////    } else {
        ////        return false;
        ////    }
        ////}
    }

    public abstract partial class BinaryOperator {
        public abstract double Evaluate(double lhs, double rhs);
    }

    public partial class Plus {
        public override double Evaluate(double lhs, double rhs) {
            return lhs + rhs;
        }
    }

    public partial class Times {
        public override double Evaluate(double lhs, double rhs) {
            return lhs * rhs;
        }
    }

    public partial class Divide {
        public override double Evaluate(double lhs, double rhs) {
            double result = lhs / rhs;
            if (double.IsNaN(result) | double.IsInfinity(result)) {
                throw new NotSupportedException(lhs + "/" + rhs);
            } else {
                return result;
            }
        }
    }
    // some more to come


    public partial class RangeExpr {
        ////public override bool Unify(AbstractExpr other) {
        ////    throw new NotImplementedException();
        ////}
    }

    #endregion Expressions

}
