// <copyright file="NormalSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.Random.Samplers;

using Qtfy.Numerics.Distributions;
using Qtfy.Numerics.Random;
using Qtfy.Numerics.Random.RandomNumberEngines;
using Qtfy.Numerics.Random.Samplers;

internal sealed class NormalSamplerTests
{
    private const double Mu = 12d;

    private const double Sigma = 1d;

    [TestCase(0.5, 0d, 1d,  0.001)]
    [TestCase(0.0, 0d, 1d,  0.001)]
    [TestCase(-0.5, 0d, 1d, 0.001)]
    public void TestGetIntegrateDistribution(double x, double mean, double sigma, double error)
    {
        var sampler = new NormalSampler(new ReducedThreeFry4X64(1), mean, sigma);
        var referenceDistribution = new NormalDistribution(mean, sigma);
        SamplerTester.TestIntegrateDistribution(x, sampler, referenceDistribution, error);
    }

    [Test]
    public void TestProperties()
    {
        var sampler = new NormalSampler(new ReducedThreeFry4X64(1), Mu, Sigma);
        Assert.That(sampler.Mu, Is.EqualTo(Mu));
        Assert.That(sampler.Sigma, Is.EqualTo(Sigma));
    }

    private static void TestInvalidThrows<TException>(IRandomNumberEngine engine, double mu, double sigma)
        where TException : Exception
    {
        Assert.Throws<TException>(
            () => _ = new NormalSampler(engine, mu, sigma));
    }

    [Test]
    public void TestNanParameter()
    {
        var engine = MersenneTwister32Bit19937.InitGenRand(1);
        TestInvalidThrows<ArgumentException>(engine, double.NaN, 1d);
        TestInvalidThrows<ArgumentException>(engine, 1d, double.NaN);
    }

    [Test]
    public void TestConstructInvalidGenerator()
    {
        TestInvalidThrows<ArgumentNullException>(null!, 1d, 1d);
    }
}
