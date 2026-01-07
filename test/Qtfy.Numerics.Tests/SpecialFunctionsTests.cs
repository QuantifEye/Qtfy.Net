// <copyright file="SpecialFunctionsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests;

internal sealed class SpecialFunctionsTests
{
    private static void TestHelper(
        double min,
        double max,
        double inc,
        double error,
        Func<double, double> expectedFunction,
        Func<double, double> actualFunction)
    {
        for (var x = min; x < max; x += inc)
        {
            var expected = expectedFunction(x);
            var actual = actualFunction(x);
            if (Math.Abs(expected - actual) > error)
            {
                Assert.That(expected, Is.EqualTo(actual).Within(error), $"Expected {expected} but got {actual}");
            }
        }
    }

    [Test]
    public void TestErrorFunction()
    {
        TestHelper(
            -120d,
            120d,
            0.01,
            TestUtils.Error * 2,
            MathNet.Numerics.SpecialFunctions.Erf,
            Qtfy.Numerics.SpecialFunctions.Erf);
    }

    [Test]
    public void TestInverseErrorFunctionRange()
    {
        TestHelper(
            -1d,
            1d,
            0.00001,
            TestUtils.Error * 4,
            MathNet.Numerics.SpecialFunctions.ErfInv,
            Qtfy.Numerics.SpecialFunctions.ErfInv);
    }

    [TestCase(1d - TestUtils.Error, TestUtils.Error)]
    public void TestInverseErrorFunctionValue(double input, double error)
    {
        Assert.That(
            actual: MathNet.Numerics.SpecialFunctions.ErfInv(input),
            expression: Is.EqualTo(Qtfy.Numerics.SpecialFunctions.ErfInv(input)).Within(error));
    }

    [Test]
    public void TestInverseErrorFunctionLimits()
    {
        Assert.That(SpecialFunctions.ErfInv(1d), Is.EqualTo(double.PositiveInfinity));
        Assert.That(SpecialFunctions.ErfInv(-1d), Is.EqualTo(double.NegativeInfinity));
        Assert.That(SpecialFunctions.ErfInv(Math.BitDecrement(-1d)), Is.NaN);
        Assert.That(SpecialFunctions.ErfInv(Math.BitIncrement(1d)), Is.NaN);
    }
}
