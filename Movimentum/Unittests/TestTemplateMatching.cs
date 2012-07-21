using System.Collections.Generic;
using Movimentum.SubstitutionSolver3;
using NUnit.Framework;

namespace Movimentum.Unittests {
    [TestFixture]
    class TestTemplateMatching {

        [Test]
        public void TestMatchConstant() {
            IConstant input = Polynomial.CreateConstant(1.5);
            var m = new TypeMatchTemplate<IConstant>();
            var result = m.TryMatch(input);
            Assert.AreSame(result[m], input);
        }

        [Test]
        public void TestDontMatchConstant() {
            IConstant input = Polynomial.CreateConstant(1.5);
            var m = new TypeMatchTemplate<INamedVariable>();
            var result = m.TryMatch(input);
            Assert.IsNull(result);
        }

        [Test]
        public void TestDoMatchAssignment() {
            // Set up expression a + 1 * 2
            var v = Polynomial.CreateNamedVariable("a");
            var e = Polynomial.CreateConstant(1).C * Polynomial.CreateConstant(2);
            AbstractExpr input = v.C + e;

            // Set up template vt + et
            var vt = new TypeMatchTemplate<IVariable>();
            var et = new TypeMatchTemplate<AbstractExpr>();
            BinaryExpressionTemplate template = vt + et;

            // Do matching
            IDictionary<AbstractExpressionTemplate, IAbstractExpr> result =
                template.TryMatch(input);

            // Check that 
            // * vt matched to a 
            // * et matched to 1 * 2
            // * complete template matched to input.
            Assert.AreSame(result[vt], v);
            Assert.AreEqual("a", ((IVariable)result[vt]).Name);
            Assert.AreSame(result[et], e);
            Assert.AreSame(result[template], input);
        }

        [Test]
        public void TestDontMatchAssignment() {
            AbstractExpr input = Polynomial.CreateConstant(-1).C + Polynomial.CreateConstant(1).C * Polynomial.CreateConstant(2);

            BinaryExpressionTemplate template = new TypeMatchTemplate<IVariable>() + new TypeMatchTemplate<AbstractExpr>();

            IDictionary<AbstractExpressionTemplate, IAbstractExpr> result = template.TryMatch(input);
            Assert.IsNull(result);
        }

        [Test]
        public void TestDoMatchTwice() {
            var three = Polynomial.CreateConstant(3);
            AbstractExpr input = three.C + three;

            var ct = new TypeMatchTemplate<IConstant>();
            BinaryExpressionTemplate template = ct + ct;

            IDictionary<AbstractExpressionTemplate, IAbstractExpr> result = template.TryMatch(input);
            Assert.AreSame(result[ct], three);
            Assert.AreSame(result[template], input);
        }

        [Test]
        public void TestDoMatchTwice2() {
            AbstractExpr input = Polynomial.CreateConstant(3).C + Polynomial.CreateConstant(3);

            var ct = new TypeMatchTemplate<IConstant>();
            BinaryExpressionTemplate template = ct + ct;

            IDictionary<AbstractExpressionTemplate, IAbstractExpr> result = template.TryMatch(input);
            Assert.AreEqual(result[ct], Polynomial.CreateConstant(3));
            Assert.AreSame(result[template], input);
        }

        [Test]
        public void TestDontMatchTwice() {
            AbstractExpr input = Polynomial.CreateConstant(3).C + Polynomial.CreateConstant(2);

            var ct = new TypeMatchTemplate<IConstant>();
            BinaryExpressionTemplate template = ct + ct;

            IDictionary<AbstractExpressionTemplate, IAbstractExpr> result = template.TryMatch(input);
            Assert.IsNull(result);
        }

        //[Test]
        //public void TestDontFoldConstant() {
        //    AbstractExpr input = (Polynomial.CreateConstant(1) + Polynomial.CreateNamedVariable("b")) * Polynomial.CreateConstant(4);
        //    AbstractExpr result = input.Accept(visitor, Ig.nore);
        //    Assert.AreEqual(input, result);
        //    Assert.AreSame(input, result);
        //}

        //    [Test]
        //    public void TestDoFoldConstantLeftAndRightOfBinaryOp() {
        //        AbstractExpr input = (Polynomial.CreateConstant(1) + Polynomial.CreateConstant(2)) / (Polynomial.CreateConstant(3) + Polynomial.CreateConstant(4));
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(Polynomial.CreateConstant((1.0+2.0)/(3.0+4.0)), result);
        //    }

        //    [Test]
        //    public void TestDoFoldConstantOnlyOnLeftSideOfBinaryOp() {
        //        AbstractExpr input = (Polynomial.CreateConstant(1) + Polynomial.CreateConstant(2)) * Polynomial.CreateNamedVariable("c");
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(Polynomial.CreateConstant(3) * Polynomial.CreateNamedVariable("c"), result);
        //    }

        //    [Test]
        //    public void TestDoFoldConstantOnlyOnRightSideOfBinaryOp() {
        //        AbstractExpr input = Polynomial.CreateNamedVariable("a") * (Polynomial.CreateConstant(2) + Polynomial.CreateConstant(3));
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(Polynomial.CreateNamedVariable("a") * Polynomial.CreateConstant(5), result);
        //    }

        //    [Test]
        //    public void TestDontFoldConstantInBinaryOp() {
        //        AbstractExpr input = Polynomial.CreateNamedVariable("a") * (Polynomial.CreateConstant(2) + Polynomial.CreateNamedVariable("c"));
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(input, result);
        //        Assert.AreSame(input, result);
        //    }

        //    [Test]
        //    public void TestDontFoldConstantInFormalSquareRoot() {
        //        AbstractExpr input = new UnaryExpression(Polynomial.CreateConstant(4), new FormalSquareroot());
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreEqual(input, result);
        //        Assert.AreSame(input, result);
        //    }

        //    [Test]
        //    public void TestDoFoldConstantInPositiveSquareRoot() {
        //        AbstractExpr input = new UnaryExpression(Polynomial.CreateConstant(4), new PositiveSquareroot());
        //        AbstractExpr result = input.Accept(visitor, Ig.nore);
        //        Assert.AreNotEqual(input, result);
        //        Assert.AreEqual(Polynomial.CreateConstant(2), result);
        //    }

        //[Test]
        //public void TestDoFoldConstantInUnaryMinus() {
        //    AbstractExpr input = -Polynomial.CreateConstant(4);
        //    AbstractExpr result = input.Accept(visitor, Ig.nore);
        //    Assert.AreNotEqual(input, result);
        //    Assert.AreEqual(Polynomial.CreateConstant(-4), result);
        //}

        //    [Test]
        //    public void TestFoldingsInSimpleSums() {
        //        AbstractExpr input1 = Polynomial.CreateNamedVariable("a") + Polynomial.CreateConstant(1) + Polynomial.CreateConstant(2);
        //        AbstractExpr input2 = Polynomial.CreateConstant(1) + Polynomial.CreateNamedVariable("a") + Polynomial.CreateConstant(2);
        //        AbstractExpr input3 = Polynomial.CreateConstant(1) + Polynomial.CreateConstant(2) + Polynomial.CreateNamedVariable("a");
        //        AbstractExpr input4 = Polynomial.CreateNamedVariable("a") + (Polynomial.CreateConstant(1) + Polynomial.CreateConstant(2));
        //        AbstractExpr result1 = input1.Accept(visitor, Ig.nore);
        //        AbstractExpr result2 = input2.Accept(visitor, Ig.nore);
        //        AbstractExpr result3 = input3.Accept(visitor, Ig.nore);
        //        AbstractExpr result4 = input4.Accept(visitor, Ig.nore);
        //        Assert.AreSame(input1, result1);
        //        Assert.AreSame(input2, result2);
        //        Assert.AreEqual(Polynomial.CreateConstant(3) + Polynomial.CreateNamedVariable("a"), result3);
        //        Assert.AreEqual(Polynomial.CreateNamedVariable("a") + Polynomial.CreateConstant(3), result4);
        //    }

    }
}
