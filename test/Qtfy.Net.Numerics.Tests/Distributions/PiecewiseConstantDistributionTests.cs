// <copyright file="PiecewiseConstantDistributionTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Distributions;

using System;
using System.Collections.Generic;
using NUnit.Framework;
using Qtfy.Net.Numerics.Distributions;

internal sealed class PiecewiseConstantDistributionTests
{
    private static PiecewiseConstantDistribution MonotonicTestDistribution()
    {
        double[] boundaries = [1d, 2d, 3d];
        double[] weights = [1d, 1d];
        return PiecewiseConstantDistribution.Create(boundaries, weights);
    }

    private static void ConstructInvalid(IEnumerable<double> domain, IEnumerable<double> weights)
    {
        Assert.Throws<ArgumentException>(
            () => _ = PiecewiseConstantDistribution.Create(domain, weights));
    }

    [Test]
    public void TestConstructInvalid()
    {
        ConstructInvalid([1d], [1d]);
        ConstructInvalid([1d, 1d], [1d, 1d]);
        ConstructInvalid([1d, 1d], [1d]);
        ConstructInvalid([1d, 2d], [-1d]);
    }

    [TestCase(0, -1d)]
    [TestCase(0, 1d)]
    [TestCase(0.5, 2d)]
    [TestCase(1.0, 3.0)]
    [TestCase(1.0, 4.0)]
    [TestCase(0.75, 2.5)]
    [TestCase(0.25, 1.5)]
    [TestCase(double.NaN, double.NaN)]
    public void TestCumulativeDistribution(double expected, double x)
    {
        var dist = MonotonicTestDistribution();
        Assert.That(dist.CumulativeDistribution(x), Is.EqualTo(expected));
    }

    [TestCase(1.0, 0d)]
    [TestCase(2.0, 0.5)]
    [TestCase(3.0, 1d)]
    [TestCase(1.5, 0.25)]
    [TestCase(2.5, 0.75)]
    public void TestQuantile(double expected, double x)
    {
        var dist = MonotonicTestDistribution();
        Assert.That(dist.Quantile(x), Is.EqualTo(expected));
    }

    [Test]
    public void TestQuantileInvalid()
    {
        var dist = MonotonicTestDistribution();
        Assert.Throws<ArgumentException>(
            () => _ = dist.Quantile(-0.1));
        Assert.Throws<ArgumentException>(
            () => _ = dist.Quantile(1.1));
    }
}
