// <copyright file="SpecialFunctionsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
    using NUnit.Framework;

    public class SpecialFunctionsTests
    {
        private static unsafe void TestHelper(
            double min,
            double max,
            double inc,
            double error,
            delegate*<double, double> expectedFunction,
            delegate*<double, double> actualFunction)
        {
            for (var x = min; x < max; x += inc)
            {
                var expected = expectedFunction(x);
                var actual = actualFunction(x);
                if (Math.Abs(expected - actual) > error)
                {
                    Assert.AreEqual(expected, actual, error);
                }
            }
        }

        [Test]
        public void TestErrorFunction()
        {
            unsafe
            {
                TestHelper(
                    -120d,
                    120d,
                    0.01,
                    TestUtils.Error * 2,
                    &MathNet.Numerics.SpecialFunctions.Erf,
                    &Qtfy.Net.Numerics.SpecialFunctions.Erf);
            }
        }

        [Test]
        public void TestInverseErrorFunctionRange()
        {
            unsafe
            {
                TestHelper(
                    -1d,
                    1d,
                    0.00001,
                    TestUtils.Error * 4,
                    &MathNet.Numerics.SpecialFunctions.ErfInv,
                    &Qtfy.Net.Numerics.SpecialFunctions.ErfInv);
            }
        }

        [TestCase(1d - TestUtils.Error, TestUtils.Error)]
        public void TestInverseErrorFunctionValue(double input, double error)
        {
            Assert.AreEqual(
                MathNet.Numerics.SpecialFunctions.ErfInv(input),
                Qtfy.Net.Numerics.SpecialFunctions.ErfInv(input),
                error);
        }

        [Test]
        public void TestInverseErrorFunctionLimits()
        {
            Assert.True(double.IsPositiveInfinity(SpecialFunctions.ErfInv(1d)));
            Assert.True(double.IsNegativeInfinity(SpecialFunctions.ErfInv(-1d)));
            Assert.IsNaN(SpecialFunctions.ErfInv(Math.BitDecrement(-1d)));
            Assert.IsNaN(SpecialFunctions.ErfInv(Math.BitIncrement(1d)));
        }
    }
}
