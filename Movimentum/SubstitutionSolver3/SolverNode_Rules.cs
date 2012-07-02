using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum.SubstitutionSolver3 {
    public partial class SolverNode {
        static SolverNode() {
            {
                // 1. 0 = C
                var z = new TypeMatchTemplate<Constant>();
                new MatcherRuleAction(
                    new EqualsZeroConstraintTemplate(z),
                    (currNode, matcher, matchedConstraint) =>
                        matcher.Match(z).Value == 0
                            ? new SolverNode(currNode.Constraints.Except(new[] { matchedConstraint }), currNode)
                            : null);
            }
            { // NEU
                // 1a. 0 = V
                var v = new TypeMatchTemplate<Variable>();
                new MatcherRuleAction(
                    new EqualsZeroConstraintTemplate(v),
                    (currNode, matcher, matchedConstraint) =>
                        RememberAndSubstituteVariable(currNode, matcher.Match(v), 0));
            }
            {
                // 2. 0 = V + C
                var v = new TypeMatchTemplate<Variable>();
                var e = new TypeMatchTemplate<Constant>();
                new MatcherRuleAction(
                    new EqualsZeroConstraintTemplate(v + e),
                    (currNode, matcher, matchedConstraint) =>
                        RememberAndSubstituteVariable(currNode, matcher.Match(v), -matcher.Match(e).Value));
            }
            {
                
            }

        }
    }
}