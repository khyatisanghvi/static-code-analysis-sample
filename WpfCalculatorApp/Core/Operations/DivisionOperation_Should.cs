﻿using System;
using FluentAssertions;
using NDepend.Attributes;
using NEdifis;
using NEdifis.Attributes;
using NUnit.Framework;
using WpfCalculatorApp.Exceptions;

namespace WpfCalculatorApp.Core.Operations
{
    [TestFixtureFor(typeof(DivisionOperation))]
    [UncoverableByTest]
    //[IsNotDeadCode]
    // ReSharper disable once InconsistentNaming
    internal sealed class DivisionOperation_Should
    {

        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<DivisionOperation>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }

        [TestCase(3, 3, 1)]
        [TestCase(-3, 3, -1)]
        [TestCase(2100, -300, -7)]
        [TestCase(555555.00, -555.00, -1001.00)]
        [TestCase(double.MinValue - 1, 1, double.MinValue - 1)]
        [TestCase(double.MinValue, -10, double.MinValue / -10)]
        [TestCase(double.MaxValue + 1, -1, (double.MaxValue + 1) / -1)]
        [TestCase(double.MaxValue, 10, double.MaxValue / 10)]
        public void Do_Divisions(double firstOperand, double secondOperand, double expectedResult)
        {
            var ctx = new ContextFor<DivisionOperation>();
            var sut = ctx.BuildSut();

            sut.Calculate(firstOperand, secondOperand).Should().Be(expectedResult);
        }

        [Test]
        public void Do_Division_By_Zero()
        {
            var ctx = new ContextFor<DivisionOperation>();
            var sut = ctx.BuildSut();

            Action act = () => sut.Calculate(222, 0);
            act.Should().Throw<CalculatorException>();
        }


    }
}
