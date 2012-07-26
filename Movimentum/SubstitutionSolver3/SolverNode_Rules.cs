using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        static SolverNode() {
            {
                // 1. 0 = C, 0 <= C
                var z = new TypeMatchTemplate<IConstant>();
                new RuleAction<ScalarConstraintMatcher>("0=C",
                    new EqualsZeroConstraintTemplate(z).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        matcher.Match(z).Value.Near(0) // NEAR!
                            ? new SolverNode(
                                currNode.Constraints
                                        .Except(matchedConstraint),
                                currNode)
                            : currNode.MarkDefinitelyDead());
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
                                    : currNode.MarkDefinitelyDead();
                    });
            }
            {
                // 2. 0 = V
                var v = new TypeMatchTemplate<IVariable>();
                new RuleAction<ScalarConstraintMatcher>("0=V",
                    new EqualsZeroConstraintTemplate(v).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        currNode.CloseVariable(
                            matcher.Match(v), Polynomial.CreateConstant(0), matchedConstraint));
            }

            {
                // 3. 0 = V + C
                var v = new TypeMatchTemplate<IVariable>();
                var e = new TypeMatchTemplate<IConstant>();
                new RuleAction<ScalarConstraintMatcher>("0=V+C",
                    new EqualsZeroConstraintTemplate(v + e).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        currNode.CloseVariable(
                            matcher.Match(v),
                            -matcher.Match(e).C,
                            matchedConstraint));
            }
            {
                // 3a. 0 = P[V]
                var p = new TypeMatchTemplate<IPolynomial>();
                new RuleAction<ScalarConstraintMatcher>("0=P[V]",
                    new EqualsZeroConstraintTemplate(p).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) =>
                        FindZeros(matcher.Match(p)).Select(zero =>
                            currNode.CloseVariable(
                            matcher.Match(p).Var,
                            Polynomial.CreateConstant(zero),
                            matchedConstraint))
                        );
            }
            {
                // 4. Match constraints with formal square roots
                new RuleAction<FindFormalSquarerootVisitor>("root",
                    constraint =>
                        constraint.Expr.Accept(new FindFormalSquarerootVisitor()),
                    formalRootFinder =>
                        formalRootFinder.SomeFormalSquareroot != null,
                    (node, formalRootFinder, constraint) =>
                        RewriteFormalSquareroot(node, formalRootFinder.SomeFormalSquareroot)
                    );
            }
            {
                // 5. 0 = V + E(without V)
                //    Substitute variable with expression that does not
                //    contain the variable and has no formal square root.
                var v = new TypeMatchTemplate<IVariable>();
                var e = new TypeMatchTemplate<AbstractExpr>();
                new RuleAction<ScalarConstraintMatcher>("V->E",
                    constraint => {
                        // Check for v+e match.
                        ScalarConstraintMatcher m =
                            new ScalarConstraintMatcher(
                                new EqualsZeroConstraintTemplate(v + e))
                            .TryMatch(constraint);
                        if (m == null) {
                            return null;
                        }
                        // Check that no formal square root exists.
                        FindFormalSquarerootVisitor f =
                            constraint.Expr.Accept(
                                new FindFormalSquarerootVisitor());
                        if (f.SomeFormalSquareroot != null) {
                            return null;
                        }
                        // Check that v is not in e.
                        Dictionary<IVariable, VariableDegree> degrees =
                            m.Match(e).Accept(new VariableDegreeVisitor());
                        IVariable variable = m.Match(v);
                        if (degrees.ContainsKey(variable)
                            && degrees[variable] != VariableDegree.Zero) {
                            return null;
                        }
                        return m;
                    },
                    m => m != null,
                    (currNode, matcher, matchedConstraint) =>
                        currNode.CloseVariable(matcher.Match(v),
                                                   -matcher.Match(e), matchedConstraint)
                );
            }
            {
                // STEPE
                // 5a. 0 = P[V](of degree 1) + E(without V)
                //    Substitute variable with expression that does not
                //    contain the variable and has no formal square root.
                var p = new TypeMatchTemplate<IPolynomial>();
                var e = new TypeMatchTemplate<AbstractExpr>();
                new RuleAction<ScalarConstraintMatcher>("P[V]->E",
                    constraint => {
                        // Check for p+e match.
                        ScalarConstraintMatcher m =
                            new ScalarConstraintMatcher(
                                new EqualsZeroConstraintTemplate(p + e))
                            .TryMatch(constraint);
                        if (m == null) {
                            return null;
                        }
                        // Check that no formal square root exists.
                        FindFormalSquarerootVisitor f =
                            constraint.Expr.Accept(
                                new FindFormalSquarerootVisitor());
                        if (f.SomeFormalSquareroot != null) {
                            return null;
                        }
                        // Check that p is of degree 1.
                        IPolynomial poly = m.Match(p);
                        if (poly.Degree != 1) {
                            return null;
                        }
                        // Check that v is not in e.
                        Dictionary<IVariable, VariableDegree> degrees =
                            m.Match(e).Accept(new VariableDegreeVisitor());
                        IVariable variable = poly.Var;
                        if (degrees.ContainsKey(variable)
                            && degrees[variable] != VariableDegree.Zero) {
                            return null;
                        }
                        return m;
                    },
                    m => m != null,
                    // 0 = ax + b + E --> x = -(b + E)/a

                    (currNode, matcher, matchedConstraint) => {
                        IPolynomial poly = matcher.Match(p);
                        IAbstractExpr expr = matcher.Match(e);
                        IConstant a = Polynomial.CreateConstant(poly.Coefficient(1));
                        IConstant b = Polynomial.CreateConstant(poly.Coefficient(0));
                        AbstractExpr replacement = -(b.C + expr) / a;
                        return currNode.CloseVariable(matcher.Match(p).Var,
                                               replacement, matchedConstraint);
                    }
                );
            }
            {
                // STEPF
                // 6. 0 = P[V] + sqrt(E)
                //    rewrite to
                //    0 = E - P[V]² and 0 <= -P[V]
                var p = new TypeMatchTemplate<IPolynomial>();
                var e = new TypeMatchTemplate<AbstractExpr>();
                var t = p + new UnaryExpressionTemplate(new PositiveSquareroot(), e);
                new RuleAction<ScalarConstraintMatcher>("0=P[V]+sqrt(E)",
                    new EqualsZeroConstraintTemplate(t).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) => {
                        ScalarConstraint c1 = new EqualsZeroConstraint(matcher.Match(e) + -new UnaryExpression(matcher.Match(p), new Square()));
                        ScalarConstraint c2 = new AtLeastZeroConstraint(-matcher.Match(p).C);
                        return new SolverNode(currNode.Constraints.Except(matchedConstraint).Union(new[] { c1, c2 }), currNode.ClosedVariables,
                                                currNode);
                    });
            }
            {
                // STEPF
                // 6a. 0 = P[V] + -sqrt(E)
                //    rewrite to
                //    0 = E - P[V]² and 0 <= P[V]
                var p = new TypeMatchTemplate<IPolynomial>();
                var e = new TypeMatchTemplate<AbstractExpr>();
                var t = p + -new UnaryExpressionTemplate(new PositiveSquareroot(), e);
                new RuleAction<ScalarConstraintMatcher>("0=P[V]-sqrt(E)",
                    new EqualsZeroConstraintTemplate(t).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) => {
                        ScalarConstraint c1 = new EqualsZeroConstraint(matcher.Match(e) + -new UnaryExpression(matcher.Match(p), new Square()));
                        ScalarConstraint c2 = new AtLeastZeroConstraint(matcher.Match(p));
                        return new SolverNode(currNode.Constraints.Except(matchedConstraint).Union(new[] { c1, c2 }), currNode.ClosedVariables,
                                                currNode);
                    });
            }
            {
                // STEPM
                // 7. 0 = sqrt(E)
                //    rewrite to
                //    0 = E
                var e = new TypeMatchTemplate<AbstractExpr>();
                var t = new UnaryExpressionTemplate(new PositiveSquareroot(), e);
                new RuleAction<ScalarConstraintMatcher>("0=sqrt(E)",
                    new EqualsZeroConstraintTemplate(t).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) => {
                        ScalarConstraint c1 = new EqualsZeroConstraint(matcher.Match(e));
                        return new SolverNode(currNode.Constraints.Except(matchedConstraint).Union(new[] { c1 }), currNode.ClosedVariables,
                                                currNode);
                    });
            }
            {
                // STEPM
                // 7. 0 = -E
                //    rewrite to
                //    0 = E
                var e = new TypeMatchTemplate<AbstractExpr>();
                var t = -e;
                new RuleAction<ScalarConstraintMatcher>("0=-E",
                    new EqualsZeroConstraintTemplate(t).GetMatchDelegate(),
                    matcher => matcher != null,
                    (currNode, matcher, matchedConstraint) => {
                        ScalarConstraint c1 = new EqualsZeroConstraint(matcher.Match(e));
                        return new SolverNode(currNode.Constraints.Except(matchedConstraint).Union(new[] { c1 }), currNode.ClosedVariables,
                                                currNode);
                    });
            }
        }

        private static IEnumerable<double> FindZeros(IPolynomial polynomial) {
            switch (polynomial.Degree) {
                case 0:
                    throw new ArgumentException("Cannot find zeros for constant polynomial");
                case 1: {
                        // 0 = ax + b --> x = -b/a                
                        var a = polynomial.Coefficient(1);
                        var b = polynomial.Coefficient(0);
                        return new[] { -b / a };
                    }
                case 2: {
                        // 0 = ax² + bx + c --> with D = b² - 4ac, x1/2 = (-b+-sqrt D)/2a
                        // These formulas are not good from a numerical standpoint! - see Wikipedia ...
                        var a = polynomial.Coefficient(2);
                        var b = polynomial.Coefficient(1);
                        var c = polynomial.Coefficient(0);
                        var sqrtD = Math.Sqrt(b * b - 4 * a * c);
                        return new[] { (-b + sqrtD) / (2 * a), (-b - sqrtD) / (2 * a) };
                    }
                default:
                    throw new NotImplementedException("Cannot find zeros for polynomial of degree " + polynomial.Degree);
            }
        }

        private static IEnumerable<SolverNode> RewriteFormalSquareroot(SolverNode origin, UnaryExpression someFormalSquareroot) {
            UnaryExpression positiveRoot = new UnaryExpression(someFormalSquareroot.Inner, new PositiveSquareroot());
            AbstractExpr negativeRoot = -positiveRoot;

            ScalarConstraint argumentIsAtLeastZeroConstraint = new AtLeastZeroConstraint(someFormalSquareroot.Inner);

            return new[] {
            CreateSolverNodeWithOneFormalSquareRootExpanded(origin, positiveRoot, argumentIsAtLeastZeroConstraint, someFormalSquareroot),
            CreateSolverNodeWithOneFormalSquareRootExpanded(origin, negativeRoot, argumentIsAtLeastZeroConstraint, someFormalSquareroot)
        };
        }

        private static SolverNode CreateSolverNodeWithOneFormalSquareRootExpanded(SolverNode origin, AbstractExpr root, ScalarConstraint additionalConstraint, UnaryExpression first) {
            // TODO: The rewriting here is wrong: Only one formal square root must be rewritten, EVEN IN THE CASE OF DIAMONDS!!!
            // Example: 
            //var rewritingVisitor = new RewritingVisitor(new Dictionary<IAbstractExpr, IAbstractExpr> { { first, root } });
            var rewritingVisitor = new RewritingVisitor(first, root);
            return new SolverNode(
                origin.Constraints.Select(c => c.Accept(rewritingVisitor))
                    .Concat(new[] { additionalConstraint }),
                    origin
                );
        }
    }
}