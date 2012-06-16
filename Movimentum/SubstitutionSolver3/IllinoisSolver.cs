using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public class CompoundSolver : ISolver {
        private readonly IllinoisSolver _illinois;
        private readonly IterationSolver _iteration;

        public CompoundSolver(AbstractExpr expr, double delta) {
            _illinois = new IllinoisSolver(expr, delta);
            _iteration = new IterationSolver(expr, delta);
        }

        public Variable Variable {
            get { return _illinois.Variable; }
        }

        public bool IsSolvable {
            get { return _illinois.IsSolvable || _iteration.IsSolvable; }
        }

        public double Solve(VariableRangeRestriction previousValue, VariableRangeRestriction restriction) {
            double result = _illinois.Solve(previousValue, restriction);
            if (double.IsNaN(result)) {
                result = _iteration.Solve(previousValue, restriction);
            }
            return result;
        }
    }

    public class IllinoisSolver : ISolver {
        private readonly AbstractExpr _expr;
        private readonly Variable _variable;
        private readonly bool _isSolvable;

        public IllinoisSolver(AbstractExpr expr, double ignore) {
            // Precondition: No formal sqrt in expr.
            var countingVisitor = new VariableCountingVisitor();
            expr.Accept(countingVisitor, Ig.nore);
            bool singleVariable = countingVisitor.Variables().Count() == 1;
            if (singleVariable) {
                _variable = countingVisitor.Variables().First();
                _expr = expr;
                _isSolvable = true;
            }
        }

        public bool IsSolvable {
            get { return _isSolvable; }
        }

        public Variable Variable {
            get { return _variable; }
        }

        public double Solve(VariableRangeRestriction previousValue, VariableRangeRestriction restriction) {
            foreach (var range in restriction.PossibleRanges) {
                double x1 = range.LoApproximateDouble;
                double x2 = range.HiApproximateDouble;
                var f1 = f(x1);
                var f2 = f(x2);
                if (f1 * f2 <= 0) {
                    double result = Illinois(x1, x2, f1, f2);
                    if (!double.IsNaN(result)) {
                        return result;
                    }
                }
            }
            return double.NaN;
        }

        private double f(double x) {
            EvaluationVisitor evaluationVisitor = new EvaluationVisitor(new Dictionary<Variable, double> { { _variable, x } });
            return _expr.Accept(evaluationVisitor, Ig.nore);
        }

        public double Illinois(double x1, double x2, double f1, double f2) {
            // Copied from p.269 of http://www.tu-harburg.de/ins/lehre/material/grnummath.pdf
            int iterationCt = 0;
            do {
                var x3 = x2 - (x2 - x1) * f2 / (f2 - f1);
                var f3 = f(x3);
                if (f2 * f3 < 0) {
                    x1 = x2;
                    f1 = f2;
                    x2 = x3;
                    f2 = f3;
                } else {
                    x2 = x3;
                    f2 = f3;
                    f1 = 0.5 * f1;
                }
                if (iterationCt++ > 200) {
                    throw new Exception("Cannot solve 0 = " + _expr + " after 200 iterations");
                }
            } while (!0.0.Near(f2)); // Math.Abs(x1 - x2) > eps);
            return x2;
        }
    }
}
