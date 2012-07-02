using System;
using System.Collections.Generic;
using System.Linq;
using Movimentum.Model;

namespace Movimentum.SubstitutionSolver3 {
    #region Intermediate constraints

    public partial class BacksubstitutionConstraint : AbstractConstraint {
        private readonly Variable _variable;
        private readonly AbstractExpr _expr;

        public BacksubstitutionConstraint(Variable variable, AbstractExpr expr) {
            _variable = variable;
            _expr = expr;
        }

        public AbstractExpr Expr {
            get { return _expr; }
        }

        public Variable Variable {
            get { return _variable; }
        }

        #region Overrides of AbstractConstraint

        public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
            return visitor.Visit(this, p);
        }

        public override RewriteSet CreateRewritesFromSingleConstraint(IEnumerable<AbstractConstraint> allOtherConstraints, IDictionary<Variable, VariableRangeRestriction> previousValues, IDictionary<Variable, VariableRangeRestriction> restrictions, int maxNeededRank) {
            if (maxNeededRank >= 98) {
                //var c = new Catch<Constant>();
                //if (Expr.Unify(c)) {
                //    double value = c.Match.Value;



                var c = new TypeMatchTemplate<Constant>();
                var m = c.CreateMatcher();
                if (m.TryMatch(Expr)) {
                    double value = m.Match(c).Value;
                    return new ReplaceRewrite(this,
                        Variable,
                        new Constant(value),
                        cs => {
                            cs.AddVariableRestriction(new VariableValueRestriction(Variable, value));
                            cs.RemoveConstraint(this);
                        }).AsSet(97);
                }
            }
            return new NoRewrite(this).AsSet(int.MaxValue - 3);
        }

        #endregion
    }

    //public partial class VariableEqualsConstantConstraint : AbstractConstraint {
    //    private readonly Variable _variable;
    //    private readonly double _value;

    //    public VariableEqualsConstantConstraint(Variable variable, double value) {
    //        _variable = variable;
    //        _value = value;
    //    }

    //    public Variable Variable {
    //        get { return _variable; }
    //    }

    //    public double Value {
    //        get { return _value; }
    //    }

    //    public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
    //        return visitor.Visit(this, p);
    //    }

    //    public override Rank CreateRank(IEnumerable<AbstractConstraint> allConstraints) {
    //        return new Rank(6, 0);
    //    }

    //    public override IEnumerable<AbstractRewrite> CreateRewritesFromSingleConstraint() {
    //        yield return new ReplaceRewrite(_variable, new Constant(_value), "From " + this);
    //    }
    //}

    public partial class VariableInRangeKnowledge /*: AbstractConstraint*/ {
        private readonly Variable _variable;
        private readonly double _lo;
        private readonly double _hi;

        public VariableInRangeKnowledge(Variable variable, double lo, double hi) {
            _variable = variable;
            _lo = lo;
            _hi = hi;
        }

        public Variable Variable {
            get { return _variable; }
        }

        public double Lo {
            get { return _lo; }
        }

        public double Hi {
            get { return _hi; }
        }

        //public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
        //    return visitor.Visit(this, p);
        //}

        //public override Rank CreateRank(IEnumerable<AbstractConstraint> allConstraints) {
        //    return new Rank(1, 0);
        //}

        //public override IEnumerable<AbstractRewrite> CreateRewritesFromSingleConstraint() {
        //    throw new NotImplementedException();
        //}

        public override bool Equals(object obj) {
            return Equals(obj as VariableInRangeKnowledge);
        }

        public bool Equals(VariableInRangeKnowledge other) {
            return other != null && other._variable.Equals(_variable) && other._lo.Near(_lo) && other._hi.Near(_hi); // == ??
        }

        public override int GetHashCode() {
            return _variable.GetHashCode();
        }
    }

    //public partial class VariableEqualsExpressionKnowledge /*: AbstractConstraint*/ {
    //    private static readonly ConstantFoldingVisitor CONSTANT_FOLDING_VISITOR = new ConstantFoldingVisitor();

    //    private readonly Variable _variable;
    //    private readonly AbstractExpr _expr;

    //    public VariableEqualsExpressionKnowledge(Variable variable, AbstractExpr expr) {
    //        _variable = variable;
    //        _expr = expr.Accept(CONSTANT_FOLDING_VISITOR, Ig.nore);
    //        // Precondition: Variable does not occur in expr
    //        // Precondition: Variable does not occur in lower-numbered constraints (not checked)
    //    }

    //    public Variable Variable {
    //        get { return _variable; }
    //    }

    //    public AbstractExpr Expr {
    //        get { return _expr; }
    //    }

    //    //public override TResult Accept<TParameter, TResult>(ISolverModelConstraintVisitor<TParameter, TResult> visitor, TParameter p) {
    //    //    return visitor.Visit(this, p);
    //    //}

    //    //public override Rank CreateRank(IEnumerable<AbstractConstraint> allConstraints) {
    //    //    return new Rank(6, 0);
    //    //}

    //    //public override IEnumerable<AbstractRewrite> CreateRewritesFromSingleConstraint() {
    //    //    throw new NotImplementedException();
    //    //}

    //    public override bool Equals(object obj) {
    //        return Equals(obj as VariableEqualsExpressionKnowledge);
    //    }

    //    public bool Equals(VariableEqualsExpressionKnowledge other) {
    //        return other != null && other._variable.Equals(_variable) && other._expr.Equals(_expr);
    //    }

    //    public override int GetHashCode() {
    //        unchecked {
    //            return _variable.GetHashCode() + _expr.GetHashCode();
    //        }
    //    }
    //}

    #endregion Intermediate constraints
}
