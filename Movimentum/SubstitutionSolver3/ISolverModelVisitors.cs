﻿namespace Movimentum.SubstitutionSolver3 {
    public interface ISolverModelConstraintVisitor<in TParameter, out TResult> {
        TResult Visit(EqualsZeroConstraint equalsZero, TParameter p);
        TResult Visit(MoreThanZeroConstraint moreThanZero, TParameter p);
        TResult Visit(AtLeastZeroConstraint atLeastZero, TParameter p);
        //TResult Visit(VariableEqualsConstantConstraint variableEqualsConstant, TParameter p);
        //TResult Visit(VariableInRangeConstraint variableInRange, TParameter p);
        //TResult Visit(VariableEqualsExpressionConstraint variableEqualsExpression, TParameter p);

        // BacksubstitutionConstraints are created internally
        TResult Visit(BacksubstitutionConstraint backsubstitutionConstraint, TParameter p);
    }

    public interface ISolverModelConstraintVisitor<out TResult> : ISolverModelConstraintVisitor<Ignore, TResult> { }

    public interface ISolverModelExprVisitor<in TParameter, out TResult> {
        TResult Visit(Constant constant, TParameter p);
        TResult Visit(NamedVariable namedVariable, TParameter p);
        TResult Visit(AnchorVariable anchorVariable, TParameter p);
        TResult Visit(UnaryExpression unaryExpression, TParameter p);
        TResult Visit(BinaryExpression binaryExpression, TParameter p);
        TResult Visit(RangeExpr rangeExpr, TParameter p);
    }

    public interface ISolverModelExprVisitor<out TResult> : ISolverModelExprVisitor<Ignore, TResult> { }

    public interface ISolverModelUnaryOpVisitor<in TExpression, in TParameter, out TResult> {
        TResult Visit(UnaryMinus op, TExpression inner, TParameter p);
        TResult Visit(Square op, TExpression inner, TParameter p);
        TResult Visit(FormalSquareroot op, TExpression inner, TParameter p);
        TResult Visit(PositiveSquareroot op, TExpression inner, TParameter p);
        //TResult Visit(Integral op, TExpression e, TParameter p);
        //TResult Visit(Differential op, TExpression e, TParameter p);
        TResult Visit(Sin op, TExpression inner, TParameter p);
        TResult Visit(Cos op, TExpression inner, TParameter p);
    }

    public interface ISolverModelUnaryOpVisitor<in TExpression, out TResult> : ISolverModelUnaryOpVisitor<TExpression, Ignore, TResult> { }

    public interface ISolverModelBinaryOpVisitor<in TExpression, in TParameter, out TResult> {
        TResult Visit(Plus op, TExpression lhs, TExpression rhs, TParameter p);
        TResult Visit(Times op, TExpression lhs, TExpression rhs, TParameter p);
        TResult Visit(Divide op, TExpression lhs, TExpression rhs, TParameter p);
    }

    public interface ISolverModelBinaryOpVisitor<in TExpression, out TResult> : ISolverModelBinaryOpVisitor<TExpression, Ignore, TResult> { }
}
