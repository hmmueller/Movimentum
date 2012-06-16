using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public interface ISolver {
        Variable Variable { get; }
        bool IsSolvable { get; }
        double Solve(VariableRangeRestriction previousValue, VariableRangeRestriction restriction);
    }

    public class IterationSolver : ISolver {
        private readonly AbstractExpr _expr;
        private readonly Variable _variable;
        private readonly bool _isSolvable;
        private readonly double _delta;

        public IterationSolver(AbstractExpr expr, double delta) {
            // Precondition: No formal sqrt in expr.
            var countingVisitor = new VariableCountingVisitor();
            expr.Accept(countingVisitor, Ig.nore);
            bool singleVariable = countingVisitor.Variables().Count() == 1;
            if (singleVariable) {
                _variable = countingVisitor.Variables().First();
                _expr = expr;
                _delta = delta;
                _isSolvable = true;
            }
        }

        public bool IsSolvable {
            get { return _isSolvable; }
        }

        public Variable Variable {
            get { return _variable; }
        }

        public double Solve(VariableRangeRestriction startValue, VariableRangeRestriction restriction) {
            double x0 = startValue.GetSomeValue();
            double delta = _delta;
            for (int i = 0; ; i++) {
                Range[] possibleRanges = Range.CreateClosed(x0 - delta, x0 + delta).Intersect(restriction.PossibleRanges).ToArray();
                if (possibleRanges.Length == 0) {
                    return double.NaN;
                }
                double x1 = possibleRanges[0].LoApproximateDouble; // TODO: But what if the range is open??
                double x2 = possibleRanges[0].HiApproximateDouble;
                for (int j = 1; j < possibleRanges.Length && x1 == x2; j++) {
                    x2 = possibleRanges[j].HiApproximateDouble;
                }

                double f1 = f(x1);
                double f2 = f(x2);
                // TODO: What if fL or fH is NaN? Should not happen, because we restrict ourselves to "restriction", but still ...

                double k = (f2 - f1) / (x2 - x1);
                double d = f2 - k * x2;

                x0 = -d / k;
                if (!restriction.Contains(x0)) {
                    // The secant intersects the x-axis in an undefined segment.
                    // So we have to replace x0 with some valid x; we use the nearest defined x.
                    double bestReplacement = 1e100;
                    foreach (var r in restriction.PossibleRanges) {
                        bestReplacement = BetterReplacement(bestReplacement, x0, r.LoApproximateDouble);
                        bestReplacement = BetterReplacement(bestReplacement, x0, r.HiApproximateDouble);
                    }
                    x0 = bestReplacement;
                }

                double f0 = f(x0);
                // TODO: Again, f0 could be NaN? Should not happen, because we restrict ourselves to "restriction", but still ...

                delta /= 2.0;

                if (x1.Near(x2) && f0.Near(0)) {
                    return (x1 + x2) / 2;
                }
                if ((1e6 * f0).Near(0)) {
                    return (x1 + x2) / 2;
                }
                if (double.IsNaN(f0) || i > 100) {
                    return double.NaN;
                }
            }
        }

        private static double BetterReplacement(double bestReplacement, double x0, double candidate) {
            if (Math.Abs(candidate - x0) <= Math.Abs(bestReplacement - x0)) {
                bestReplacement = candidate;
            }
            return bestReplacement;
        }

        //public double Solve(double startValue) {
        //    double x0 = startValue;
        //    double delta = _delta;
        //    for (int i = 0; ; i++) {
        //        double xM = x0;
        //        double xL = x0 - delta;
        //        double xR = x0 + delta;

        //        double fM = f(xM);
        //        double fL = f(xL);
        //        double fR = f(xR);
        //        double f1, f2, x1, x2;
        //        if (double.IsNaN(fL)) {
        //            // use fM, fR
        //            f1 = fM;
        //            f2 = fR;
        //            x1 = xM;
        //            x2 = xR;
        //        } else if (double.IsNaN(fR)) {
        //            // use fL, fM
        //            f1 = fL;
        //            f2 = fM;
        //            x1 = xL;
        //            x2 = xM;
        //        } else {
        //            // use fL, fR
        //            f1 = fL;
        //            f2 = fR;
        //            x1 = xL;
        //            x2 = xR;
        //        }

        //        double k = (f2 - f1) / (x2 - x1);
        //        double d = f2 - k * x2;

        //        x0 = -d / k;
        //        double f0 = f(x0);
        //        if (double.IsNaN(f0)) {
        //            if (x0 > xR) {
        //                x0 = xR;
        //            } else if (x0 < xL) {
        //                x0 = xL;
        //            } else {
        //                x0 = (x1 + x2) / 2;
        //            }
        //            f0 = f(x0);
        //        }

        //        delta /= 2.0;

        //        if (x1.Near(x2) && f0.Near(0)) {
        //            return (x1 + x2) / 2;
        //        }
        //        if ((1e6 * f0).Near(0)) {
        //            return (x1 + x2) / 2;
        //        }
        //        if (double.IsNaN(f0) || i > 100) {
        //            return double.NaN;
        //        }
        //    }
        //}
        private double f(double x) {
            return _expr.Accept(new EvaluationVisitor(_variable, x), Ig.nore);
        }
    }
}
