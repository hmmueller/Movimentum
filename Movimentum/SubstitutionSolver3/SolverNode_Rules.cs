using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        static SolverNode() {
            {
                // 1. 0 = C, 0 <= C, 0 < C
                var z = new TypeMatchTemplate<Constant>();
                new RuleAction<ScalarConstraintMatcher>("0=C",
                    new EqualsZeroConstraintTemplate(z).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        matcher.Match(z).Value.Near(0) // NEAR!
                            ? new SolverNode(
                                currNode.Constraints
                                        .Except(matchedConstraint),
                                currNode)
                            : null);
                new RuleAction<ScalarConstraintMatcher>("0<=C",
                    new AtLeastZeroConstraintTemplate(z).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) => {
                        double value = matcher.Match(z).Value;
                        return value.Near(0) | value >= 0
                                    ? new SolverNode(
                                            currNode.Constraints
                                                .Except(matchedConstraint),
                                            currNode)
                                    : null;
                    });
                ////new RuleAction<ScalarConstraintMatcher>("0<C",
                ////    new MoreThanZeroConstraintTemplate(z).GetMatchDelegate(),
                ////    matcher => matcher != null,
                ////    (currNode, matcher, matchedConstraint) =>
                ////        matcher.Match(z).Value > 0
                ////            ? new SolverNode(
                ////                currNode.Constraints
                ////                    .Except(matchedConstraint),
                ////                currNode)
                ////            : null);
            }
            {
                // 2. 0 = V
                var v = new TypeMatchTemplate<Variable>();
                new RuleAction<ScalarConstraintMatcher>("0=V",
                    new EqualsZeroConstraintTemplate(v).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        currNode.RememberAndSubstituteVariable(
                            matcher.Match(v), 0));
            }

            {
                // 3. 0 = V + C
                var v = new TypeMatchTemplate<Variable>();
                var e = new TypeMatchTemplate<Constant>();
                new RuleAction<ScalarConstraintMatcher>("0=V+C",
                    new EqualsZeroConstraintTemplate(v + e).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        currNode.RememberAndSubstituteVariable(
                            matcher.Match(v),
                            -matcher.Match(e).Value));
            }
            {
                // 4. Match constraints with formal square roots
                new RuleAction<FindFormalSquarerootVisitor>("root",
                    constraint =>
                        constraint.Expr.Accept(new FindFormalSquarerootVisitor(), Ig.nore),
                    formalRootFinder =>
                        formalRootFinder.SomeFormalSquareroot != null,
                    (node, formalRootFinder, constraint) =>
                        RewriteFormalSquareRoot(node, formalRootFinder.SomeFormalSquareroot)
                    );
            }
            ////{
            ////    // 5. Solve linear expression with single variable
            ////    new RuleAction<Dictionary<Variable, VariableDegree>>("lin",
            ////        constraint => constraint is EqualsZeroConstraint ? constraint.Expr.Accept(new VariableDegreeVisitor(), Ig.nore) : null,
            ////        variabledegrees => variabledegrees.Count() == 1 && variabledegrees.Single().Value == VariableDegree.One,
            ////        (node, variabledegrees, constraint) => SolveLinearEquation(node, variabledegrees.Single().Key, constraint));
            ////}
            ////{
            ////    // 6. Substitute variable with expression (that does not contain the variable)
            ////    var v = new TypeMatchTemplate<Variable>();
            ////    var e = new TypeMatchTemplate<AbstractExpr>();
            ////    new RuleAction<ScalarConstraintMatcher>("V->E",
            ////        constraint => {
            ////            ScalarConstraintMatcher m = new ScalarConstraintMatcher(new EqualsZeroConstraintTemplate(v + e)).TryMatch(constraint);
            ////            if (m != null) {
            ////                var degrees = m.Match(e).Accept(new VariableDegreeVisitor(), Ig.nore);
            ////                return degrees.ContainsKey(m.Match(v)) ? null : m;
            ////            } else {
            ////                return null;
            ////            }
            ////        },
            ////        m => m != null,
            ////        (currNode, matcher, matchedConstraint) =>
            ////            currNode.SubstituteVariable(matcher.Match(v), -matcher.Match(e))
            ////    );
            ////}
            ////{
            ////    // 7. Solve quadratic constraint
            ////    // ...
            ////}
        }

        private static IEnumerable<SolverNode> RewriteFormalSquareRoot(SolverNode origin, UnaryExpression someFormalSquareroot) {
            UnaryExpression positiveRoot = new UnaryExpression(someFormalSquareroot.Inner, new PositiveSquareroot());
            UnaryExpression negativeRoot = -positiveRoot;

            ScalarConstraint argumentIsAtLeastZeroConstraint = new AtLeastZeroConstraint(someFormalSquareroot.Inner);

            return new[] {
            CreateSolverNodeWithOneFormalSquareRootExpanded(origin, positiveRoot, argumentIsAtLeastZeroConstraint, someFormalSquareroot),
            CreateSolverNodeWithOneFormalSquareRootExpanded(origin, negativeRoot, argumentIsAtLeastZeroConstraint, someFormalSquareroot)
        };
        }

        private static SolverNode CreateSolverNodeWithOneFormalSquareRootExpanded(SolverNode origin, UnaryExpression root, ScalarConstraint additionalConstraint, UnaryExpression first) {
            // TODO: The rewriting here is wrong: Only one formal square root must be rewritten, EVEN IN THE CASE OF DIAMONDS!!!
            // Example: 
            return new SolverNode(
                origin.Constraints.Select(c =>
                                          c.Accept(new RewritingVisitor(new Dictionary<AbstractExpr, AbstractExpr> { { first, root } }), Ig.nore))
                    .Concat(new[] { additionalConstraint }), origin
                );
        }

        private static SolverNode SolveLinearEquation(SolverNode origin, Variable variable, AbstractConstraint constraint) {
            // Linear expression with a single variable!
            var expr = ((ScalarConstraint)constraint).Expr;
            double y0 = expr.Accept(new EvaluationVisitor(variable, 0), Ig.nore);
            double y1 = expr.Accept(new EvaluationVisitor(variable, 1), Ig.nore);
            if (y0.Near(y1)) {
                // Horizontal line
                return y0.Near(0) ? new SolverNode(origin.Constraints.Except(constraint), origin) : null; // EXCEPT!
            } else {
                // Compute solution
                double x0 = y0 / (y0 - y1);
                return origin.RememberAndSubstituteVariable(variable, x0);
            }
        }
    }
}