// <copyright file="TestUtils.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests;

internal static class TestUtils
{
    /// <summary>
    /// The smallest deviation from 1.0 toward zero.
    /// That is <c>1.0 - Math.BitDecrement(1.0)</c>, or <c>Math.ScaleB(1, -53)</c>.
    /// </summary>
    public const double Error = 1.1102230246251565E-16;

    public static void IsClose(double expected, double actual, double error = Error * 3)
    {
        if (expected != actual)
        {
            if (double.IsNaN(expected))
            {
                Assert.That(actual, Is.NaN);
            }
            else if (double.IsPositiveInfinity(expected))
            {
                Assert.That(double.IsPositiveInfinity(expected), Is.True, "Expected Positive Infinity");
            }
            else if (double.IsNegativeInfinity(expected))
            {
                Assert.That(double.IsNegativeInfinity(expected), Is.True, "Expected Negative Infinity");
            }
            else
            {
                if (!double.IsFinite(actual))
                {
                    Assert.Fail($"Expected number that is not nan and not infinite, actual: {actual}.");
                }
                else if ((expected < 0d && actual > 0d) || (expected > 0d && actual < 0d))
                {
                    Assert.Fail("actual and expected must have same sign");
                }
                else if (actual == 0d || expected == 0d)
                {
                    Assert.That(
                        actual: actual,
                        expression: Is.EqualTo(expected).Within(error),
                        message: "Actual and expected must have same values");
                }
                else
                {
                    var q = expected > actual ? actual / expected : expected / actual;
                    var e = expected > actual ? expected - actual : actual - expected;
                    Assert.That(
                        actual: q,
                        expression: Is.EqualTo(1d).Within(error),
                        message: $"\n      expected: {expected}\n" +
                                 $"        actual: {actual}\n" +
                                 $"     abs error: {e}\n" +
                                 $"relative error: {1d - q}\n");
                }
            }
        }
    }
}
