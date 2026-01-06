// <copyright file="StandardNormalDistributionTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.Distributions;

using NUnit.Framework;
using Qtfy.Numerics.Distributions;

internal sealed class StandardNormalDistributionTests
{
    [Test]
    public void TestMean()
    {
        Assert.That(StandardNormalDistribution.Instance.Mean, Is.Zero);
    }

    [Test]
    public void TestVariance()
    {
        Assert.That(StandardNormalDistribution.Instance.Variance, Is.EqualTo(1d));
    }

    [Test]
    public void TestStandardDeviation()
    {
        Assert.That(StandardNormalDistribution.Instance.StandardDeviation, Is.EqualTo(1d));
    }

    [TestCase(1.0, 0.8413447460685429485852d)]
    [TestCase(double.PositiveInfinity, 1.0)]
    [TestCase(double.NegativeInfinity, 0.0)]
    [TestCase(double.NaN, double.NaN)]
    public void TestCumulativeDistributionFunction(double x, double expected)
    {
        Assert.That(StandardNormalDistribution.Instance.CumulativeDistribution(x), Is.EqualTo(expected));
        Assert.That(StandardNormalDistribution.CumulativeDistributionFunction(x), Is.EqualTo(expected));
    }

    [TestCase(1.0, double.PositiveInfinity)]
    [TestCase(0.0, double.NegativeInfinity)]
    [TestCase(0.5, 0.0)]
    public void TestQuantileFunction(double probability, double expected)
    {
        Assert.That(StandardNormalDistribution.Instance.Quantile(probability), Is.EqualTo(expected));
    }

    [TestCase(1.0, -1.41893853320467274178045451569708215806201947)]
    [TestCase(double.PositiveInfinity, double.NegativeInfinity)]
    [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
    [TestCase(double.NaN, double.NaN)]
    public void TestDensityLn(double x, double expected)
    {
        Assert.That(StandardNormalDistribution.Instance.DensityLn(x), Is.EqualTo(expected));
    }

    [TestCase(1.0, 0.2419707245191433497978)]
    [TestCase(double.PositiveInfinity, 0.0)]
    [TestCase(double.NegativeInfinity, 0.0)]
    [TestCase(double.NaN, double.NaN)]
    public void TestDensity(double x, double expected)
    {
        Assert.That(StandardNormalDistribution.Instance.Density(x), Is.EqualTo(expected));
    }
}
