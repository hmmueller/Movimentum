using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public class RewriteSet {
        private readonly IEnumerable<AbstractRewrite> _rewrites;
        private readonly int _rank;

        public RewriteSet(IEnumerable<AbstractRewrite> rewrites, int rank) {
            _rewrites = rewrites.ToArray();
            _rank = rank;
        }

        public int Rank {
            get { return _rank; }
        }

        public IEnumerable<AbstractRewrite> Rewrites {
            get { return _rewrites; }
        }

        public override string ToString() {
            return "RewriteSet <" + string.Join(",", _rewrites) + ">";
        }
    }

    public abstract class AbstractRewrite {
        protected readonly AbstractConstraint _source;
        private readonly Action<ConstraintSet> _action;

        protected AbstractRewrite(AbstractConstraint source, Action<ConstraintSet> action) {
            _source = source;
            _action = action;
        }

        public RewriteSet AsSet(int rank) {
            return new RewriteSet(new[] { this }, rank);
        }

        public abstract AbstractConstraint Rewrite(AbstractConstraint constraint);
        public Action<ConstraintSet> Action { get { return _action ?? (cs => { }); } }
    }

    public class NoRewrite : AbstractRewrite {
        public NoRewrite(AbstractConstraint source) : base(source, null) { }

        public override AbstractConstraint Rewrite(AbstractConstraint constraint) {
            throw new NotSupportedException("NoRewrite encountered - we did not find a better rewrite; source: " + _source);
        }
    }

    public class ReplaceRewrite : AbstractRewrite {
        private readonly RewritingVisitor _visitor;

        public ReplaceRewrite(AbstractConstraint source, AbstractExpr from, AbstractExpr to, Action<ConstraintSet> action = null)
            : this(source, new Dictionary<AbstractExpr, AbstractExpr> { { from, to } }, action) {
        }

        public ReplaceRewrite(AbstractConstraint source, IDictionary<AbstractExpr, AbstractExpr> rewrites, Action<ConstraintSet> action = null)
            : base(source, action) {
            _visitor = new RewritingVisitor(rewrites);
        }

        public IDictionary<AbstractExpr, AbstractExpr> Rewrites { get { return _visitor.Rewrites; } }

        public override AbstractConstraint Rewrite(AbstractConstraint constraint) {
            // We could throw out the constraint if it is equal to _source ... but we do this not yet - it tests more the way it is.
            return constraint.Accept(_visitor, Ig.nore);
        }

        public override string ToString() {
            return "ReplaceRewrite " + string.Join(",", Rewrites.Select(kvp => kvp.Key + "->" + kvp.Value));
        }
    }

    public class BacksubstitutionRewrite : ReplaceRewrite {
        private readonly BacksubstitutionConstraint _backsubstitutionConstraint;

        public BacksubstitutionRewrite(AbstractConstraint source, Variable from, AbstractExpr to)
            : base(source, from, to) {
            _backsubstitutionConstraint = new BacksubstitutionConstraint(from, to);
        }

        public override AbstractConstraint Rewrite(AbstractConstraint constraint) {
            if (constraint == _source) {
                // We replace the original constraint with the backsubstitution;
                return _backsubstitutionConstraint;
            } else {
                return base.Rewrite(constraint);
            }
        }

    }

    public class TrueFalseRewrite : AbstractRewrite {
        private readonly bool _b;

        public TrueFalseRewrite(AbstractConstraint source, bool b)
            : base(source, cs => { if (!b) { cs.MarkAsDeadEnd(); } }) {
            _b = b;
        }

        public override AbstractConstraint Rewrite(AbstractConstraint constraint) {
            return constraint == _source ? null : constraint;
        }

        public override string ToString() {
            return "TrueFalseRewrite " + _b;
        }
    }

    public class ActionRewrite : AbstractRewrite {
        public ActionRewrite(AbstractConstraint source, Action<ConstraintSet> action)
            : base(source, action) {
        }

        public override AbstractConstraint Rewrite(AbstractConstraint constraint) {
            return constraint == _source ? null : constraint;
        }

        public override string ToString() {
            return "ActionRewrite";
        }
    }
}
