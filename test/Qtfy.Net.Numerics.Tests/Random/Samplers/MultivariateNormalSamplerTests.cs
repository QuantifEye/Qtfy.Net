// <copyright file="MultivariateNormalSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using System.Collections;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;
    using Qtfy.Net.Numerics.Random.Samplers;

    public class MultivariateNormalSamplerTests
    {
        private const int Trials = 1000000;

        [Test]
        public void TestLength()
        {
            var mean = new[] { 0d, 0d };
            var sigma = new[,]
            {
                { 1d, 0.5 },
                { 0.5, 1d },
            };
            var engine = MersenneTwister32Bit19937.InitGenRand(1);
            var sampler = new MultivariateNormalSampler.Builder(mean, sigma).Build(engine);
            Assert.AreEqual(2, sampler.Length);
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
                () => _ = new MultivariateNormalSampler.Builder(new[] { 0d }, inputCovariance));

            Assert.Throws<ArgumentException>(
                () => _ = new MultivariateNormalSampler.Builder(new[] { 0d, double.NaN }, inputCovariance));

            Assert.Throws<ArgumentNullException>(
                () => _ = new MultivariateNormalSampler.Builder(null, inputCovariance));

            Assert.Throws<ArgumentNullException>(
                () => _ = new MultivariateNormalSampler.Builder(new[] { 0d }, null));
        }

        [Test]
        public void TestConstructValid()
        {
            var covarianceMatrix = new[,]
            {
                { 1.0, 0.5 },
                { 0.5, 1.0 },
            };

            var mean = new[] { 0.0, 0.0 };
            var engine = MersenneTwister32Bit19937.InitGenRand(1);
            Assert.DoesNotThrow(() => _ = new MultivariateNormalSampler.Builder(mean, covarianceMatrix).Build(engine));
        }

        [TestCaseSource(typeof(IntegrateDistributionCases))]
        public void TestIntegrateDistribution(double[] x, double[] mu, double[,] sigma, double expected, double error)
        {
            var sampler = new MultivariateNormalSampler.Builder(mu, sigma).Build(new ReducedThreeFry4X64(1));
            var actual = SamplerTester.IntegrateMultivariateCdf(sampler, x, Trials);
            Assert.AreEqual(actual, expected, error);
        }

        private class IntegrateDistributionCases : IEnumerable
        {
            private static object[] Case(double[] x, double[] mu, double[,] sigma, double expected, double error)
                => new object[] { x, mu, sigma, expected, error };

            public IEnumerator GetEnumerator()
            {
                const double error = 0.001;
                double[] x = { 0.5, 0.5 };
                yield return Case(
                    x: new[] { 0.5, 0.5 },
                    mu: new[] { 0.0, 0.0 },
                    sigma: new[,]
                    {
                        { 1d, 0.5 },
                        { 0.5, 1d },
                    },
                    expected: 0.5462444438570895,
                    error: error);
                yield return Case(
                    x: new[] { 0.5, 0.5 },
                    mu: new[] { 0.0, 0.0 },
                    sigma: new[,]
                    {
                        { 1d, -0.5 },
                        { -0.5, 1d },
                    },
                    expected: 0.41922310903660254,
                    error: error);
                yield return Case(
                    x: new[] { 0.5, 0.5 },
                    mu: new[] { 0.3, 0.7 },
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
}
