// <copyright file="UniformRealSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.Random.Samplers;

using Qtfy.Numerics.Distributions;
using Qtfy.Numerics.Random.RandomNumberEngines;
using Qtfy.Numerics.Random.Samplers;

internal sealed class UniformRealSamplerTests
{
    private const double Min = 0.5;

    private const double Max = 1.5;

    private static UniformRealSampler GetSampler(double min, double max)
    {
        return new (new ReducedThreeFry4X64(1), min, max);
    }

    [TestCase(0.5, 0.0, 1.0, 0.0001)]
    [TestCase(0.4, 0.1, 0.5, 0.0001)]
    public void TestGetIntegrateDistribution(double x, double min, double max, double error)
    {
        var sampler = new UniformRealSampler(new ReducedThreeFry4X64(1), min, max);
        var referenceDistribution = new UniformRealDistribution(min, max);
        SamplerTester.TestIntegrateDistribution(x, sampler, referenceDistribution, error);
    }

    [Test]
    public void TestProperties()
    {
        var sampler = new UniformRealSampler(new ReducedThreeFry4X64(1), Min, Max);
        Assert.That(sampler.Min, Is.EqualTo(Min));
        Assert.That(sampler.Max, Is.EqualTo(Max));
    }

    [Test]
    public void TestConstructInvalidEngine()
    {
        Assert.Throws<ArgumentNullException>(
            () => _ = new UniformRealSampler(null, 5d, 10d));
    }

    private static void TestInvalidConstruction<TException>(double min, double max)
        where TException : Exception
        => Assert.Throws<TException>(() => _ = GetSampler(min, max));

    [TestCase(double.NegativeInfinity)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(double.NaN)]
    public void TestInvalidDoubleInput(double invalidValue)
    {
        TestInvalidConstruction<ArgumentException>(invalidValue, 1d);
        TestInvalidConstruction<ArgumentException>(1d, invalidValue);
    }

    [Test]
    public void TestMinGreaterThanMax()
    {
        Assert.Throws<ArgumentException>(
            () => _ = GetSampler(Max, Min));
    }
}
