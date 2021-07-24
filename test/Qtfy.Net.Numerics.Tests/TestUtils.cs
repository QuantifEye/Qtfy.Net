// <copyright file="TestUtils.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using NUnit.Framework;

    public static class TestUtils
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
                    Assert.IsNaN(actual);
                }
                else if (double.IsPositiveInfinity(expected))
                {
                    Assert.True(double.IsPositiveInfinity(actual), "Expected Positive Infinity");
                }
                else if (double.IsNegativeInfinity(expected))
                {
                    Assert.True(double.IsNegativeInfinity(actual), "Expected Negative Infinity");
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
                        Assert.AreEqual(expected, actual, error);
                    }
                    else
                    {
                        var q = expected > actual ? actual / expected : expected / actual;
                        var e = expected > actual ? expected - actual : actual - expected;
                        Assert.AreEqual(
                            1d,
                            q,
                            error,
                            "\n" +
                            $"      expected: {expected}\n" +
                            $"        actual: {actual}\n" +
                            $"     abs error: {e}\n" +
                            $"relative error: {1d - q}\n");
                    }
                }
            }
        }
    }
}
