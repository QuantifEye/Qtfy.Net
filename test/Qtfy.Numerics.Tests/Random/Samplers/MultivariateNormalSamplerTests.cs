// <copyright file="MultivariateNormalSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.Random.Samplers;

using Qtfy.Numerics.Random.RandomNumberEngines;
using Qtfy.Numerics.Random.Samplers;

internal sealed class MultivariateNormalSamplerTests
{
    private const int Trials = 1000000;

    [Test]
    public void TestLength()
    {
        double[] mean = [0d, 0d];
        var sigma = new[,]
        {
            { 1d, 0.5 },
            { 0.5, 1d },
        };
        var engine = MersenneTwister32Bit19937.InitGenRand(1);
        var sampler = new MultivariateNormalSampler.Builder(mean, sigma).Build(engine);

        Assert.That(sampler.Length, Is.EqualTo(2));
    }

    [Test]
    public void TestInvalidMean()
    {
        var inputCovariance = new[,]
        {
            { 1.0, 0.5 },
            { 0.5, 1.0 },
        };

        Assert.Throws<ArgumentException>(
            () => _ = new MultivariateNormalSampler.Builder([0d], inputCovariance));

        Assert.Throws<ArgumentException>(
            () => _ = new MultivariateNormalSampler.Builder([0d, double.NaN], inputCovariance));

        Assert.Throws<ArgumentNullException>(
            () => _ = new MultivariateNormalSampler.Builder(null!, inputCovariance));

        Assert.Throws<ArgumentNullException>(
            () => _ = new MultivariateNormalSampler.Builder([0d], null!));
    }

    [Test]
    public void TestConstructValid()
    {
        var covarianceMatrix = new[,]
        {
            { 1.0, 0.5 },
            { 0.5, 1.0 },
        };

        double[] mean = [0.0, 0.0];
        var engine = MersenneTwister32Bit19937.InitGenRand(1);
        Assert.DoesNotThrow(() => _ = new MultivariateNormalSampler.Builder(mean, covarianceMatrix).Build(engine));
    }

    [TestCaseSource(typeof(IntegrateDistributionCases))]
    public void TestIntegrateDistribution(double[] x, double[] mu, double[,] sigma, double expected, double error)
    {
        var sampler = new MultivariateNormalSampler.Builder(mu, sigma).Build(new ReducedThreeFry4X64(1));
        var actual = SamplerTester.IntegrateMultivariateCdf(sampler, x, Trials);
        Assert.That(actual, Is.EqualTo(expected).Within(error));
    }

    private sealed class IntegrateDistributionCases : IEnumerable
    {
        private static object[] Case(double[] x, double[] mu, double[,] sigma, double expected, double error)
            => [x, mu, sigma, expected, error];

        public IEnumerator GetEnumerator()
        {
            const double error = 0.001;
            double[] x = [0.5, 0.5];
            yield return Case(
                x: [0.5, 0.5],
                mu: [0.0, 0.0],
                sigma: new[,]
                {
                    { 1d, 0.5 },
                    { 0.5, 1d },
                },
                expected: 0.5462444438570895,
                error: error);
            yield return Case(
                x: [0.5, 0.5],
                mu: [0.0, 0.0],
                sigma: new[,]
                {
                    { 1d, -0.5 },
                    { -0.5, 1d },
                },
                expected: 0.41922310903660254,
                error: error);
            yield return Case(
                x: [0.5, 0.5],
                mu: [0.3, 0.7],
                sigma: new[,]
                {
                    { 1d, 0.5 },
                    { 0.5, 1d },
                },
                expected: 0.3225238066199577,
                error: error);
        }
    }
}
