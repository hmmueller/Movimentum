﻿using System.Collections.Generic;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestTemplateMatching {

        [Test]
        public void TestMatchConstant() {
            Constant input = new Constant(1.5);
            var m = new TypeMatchTemplate<Constant>();
            var result = m.TryMatch(input);
            Assert.AreSame(result[m], input);
        }

        [Test]
        public void TestDontMatchConstant() {
            Constant input = new Constant(1.5);
            var m = new TypeMatchTemplate<NamedVariable>();
            var result = m.TryMatch(input);
            Assert.IsNull(result);
        }

        [Test]
        public void TestDoMatchAssignment() {
            // Set up expression a + 1 * 2
            var v = new NamedVariable("a");
            var e = new Constant(1) * new Constant(2);
            AbstractExpr input = v + e;

            // Set up template vt + et
            var vt = new TypeMatchTemplate<Variable>();
            var et = new TypeMatchTemplate<AbstractExpr>();
            BinaryExpressionTemplate template = vt + et;

            // Do matching
            IDictionary<AbstractExpressionTemplate, AbstractExpr> result =
                template.TryMatch(input);

            // Check that 
            // * vt matched to a 
            // * et matched to 1 * 2
            // * complete template matched to input.
            Assert.AreSame(result[vt], v);
            Assert.AreEqual("a", ((Variable)result[vt]).Name);
            Assert.AreSame(result[et], e);
            Assert.AreSame(result[template], input);
        }

        [Test]
        public void TestDontMatchAssignment() {
            AbstractExpr input = new Constant(-1) + new Constant(1) * new Constant(2);

            BinaryExpressionTemplate template = new TypeMatchTemplate<Variable>() + new TypeMatchTemplate<AbstractExpr>();

            IDictionary<AbstractExpressionTemplate, AbstractExpr> result = template.TryMatch(input);
            Assert.IsNull(result);
        }

        [Test]
        public void TestDoMatchTwice() {
            var three = new Constant(3);
            AbstractExpr input = three + three;

            var ct = new TypeMatchTemplate<Constant>();
            BinaryExpressionTemplate template = ct + ct;

            IDictionary<AbstractExpressionTemplate, AbstractExpr> result = template.TryMatch(input);
            Assert.AreSame(result[ct], three);
            Assert.AreSame(result[template], input);
        }

        [Test]
        public void TestDoMatchTwice2() {
            AbstractExpr input = new Constant(3) + new Constant(3);

            var ct = new TypeMatchTemplate<Constant>();
            BinaryExpressionTemplate template = ct + ct;

            IDictionary<AbstractExpressionTemplate, AbstractExpr> result = template.TryMatch(input);
            Assert.AreEqual(result[ct], new Constant(3));
            Assert.AreSame(result[template], input);
        }

        [Test]
        public void TestDontMatchTwice() {
            AbstractExpr input = new Constant(3) + new Constant(2);

            var ct = new TypeMatchTemplate<Constant>();
            BinaryExpressionTemplate template = ct + ct;

            IDictionary<AbstractExpressionTemplate, AbstractExpr> result = template.TryMatch(input);
            Assert.IsNull(result);
        }

        //[Test]
        //public void TestDontFoldConstant() {
        //    AbstractExpr input = (new Constant(1) + new NamedVariable("b")) * new Constant(4);
        //    AbstractExpr result = input.Accept(visitor, Ig.nore);
        //    Assert.AreEqual(input, result);
        //    Assert.AreSame(input, result);
        //}

        //    [Test]
        //    public void TestDoFoldConstantLeftAndRightOfBinaryOp() {
        //        AbstractExpr input = (new Constant(1) + new Constant(2)) / (new Constant(3) + new Constant(4));
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(new Constant((1.0+2.0)/(3.0+4.0)), result);
        //    }

        //    [Test]
        //    public void TestDoFoldConstantOnlyOnLeftSideOfBinaryOp() {
        //        AbstractExpr input = (new Constant(1) + new Constant(2)) * new NamedVariable("c");
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(new Constant(3) * new NamedVariable("c"), result);
        //    }

        //    [Test]
        //    public void TestDoFoldConstantOnlyOnRightSideOfBinaryOp() {
        //        AbstractExpr input = new NamedVariable("a") * (new Constant(2) + new Constant(3));
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(new NamedVariable("a") * new Constant(5), result);
        //    }

        //    [Test]
        //    public void TestDontFoldConstantInBinaryOp() {
        //        AbstractExpr input = new NamedVariable("a") * (new Constant(2) + new NamedVariable("c"));
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(input, result);
        //        Assert.AreSame(input, result);
        //    }

        //    [Test]
        //    public void TestDontFoldConstantInFormalSquareRoot() {
        //        AbstractExpr input = new UnaryExpression(new Constant(4), new FormalSquareroot());
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(input, result);
        //        Assert.AreSame(input, result);
        //    }

        //    [Test]
        //    public void TestDoFoldConstantInPositiveSquareRoot() {
        //        AbstractExpr input = new UnaryExpression(new Constant(4), new PositiveSquareroot());
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreNotEqual(input, result);
        //        Assert.AreEqual(new Constant(2), result);
        //    }

        //[Test]
        //public void TestDoFoldConstantInUnaryMinus() {
        //    AbstractExpr input = -new Constant(4);
        //    AbstractExpr result = input.Accept(visitor, Ig.nore);
        //    Assert.AreNotEqual(input, result);
        //    Assert.AreEqual(new Constant(-4), result);
        //}

        //    [Test]
        //    public void TestFoldingsInSimpleSums() {
        //        AbstractExpr input1 = new NamedVariable("a") + new Constant(1) + new Constant(2);
        //        AbstractExpr input2 = new Constant(1) + new NamedVariable("a") + new Constant(2);
        //        AbstractExpr input3 = new Constant(1) + new Constant(2) + new NamedVariable("a");
        //        AbstractExpr input4 = new NamedVariable("a") + (new Constant(1) + new Constant(2));
        //        AbstractExpr result1 = input1.Accept(visitor, Ig.nore);
        //        AbstractExpr result2 = input2.Accept(visitor, Ig.nore);
        //        AbstractExpr result3 = input3.Accept(visitor, Ig.nore);
        //        AbstractExpr result4 = input4.Accept(visitor, Ig.nore);
        //        Assert.AreSame(input1, result1);
        //        Assert.AreSame(input2, result2);
        //        Assert.AreEqual(new Constant(3) + new NamedVariable("a"), result3);
        //        Assert.AreEqual(new NamedVariable("a") + new Constant(3), result4);
        //    }

    }
}