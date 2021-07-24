// <copyright file="GaussianCopulaSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using System.Collections;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Distributions;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;
    using Qtfy.Net.Numerics.Random.Samplers;

    public class GaussianCopulaSamplerTests
    {
        [Test]
        public void TestLength()
        {
            var sigma = new[,]
            {
                { 1d, 0.5 },
                { 0.5, 1d },
            };

            var sampler = new GaussianCopulaSampler.Builder(sigma).Build(new ReducedThreeFry4X64(1));
            Assert.AreEqual(2, sampler.Length);
        }

        [Test]
        public void TestConstructWithNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = new GaussianCopulaSampler.Builder(null));
        }

        [TestCaseSource(typeof(IntegrateDistributionCases))]
        public void TestIntegrateDistribution(double[] x, double[,] sigma, double expected, double error)
        {
            var sampler = new GaussianCopulaSampler.Builder(sigma).Build(new ReducedThreeFry4X64(1));
            var actual = SamplerTester.IntegrateMultivariateCdf(sampler, x, 1000000);
            Assert.AreEqual(expected, actual, error);
        }

        private class IntegrateDistributionCases : IEnumerable
        {
            private static object[] Case(double[] x, double[,] corr, double expected, double error)
                => new object[] { x, corr, expected, error };

            public IEnumerator GetEnumerator()
            {
                const double error = 0.001;
                var xi = StandardNormalDistribution.CumulativeDistributionFunction(0.5);
                yield return Case(
                    x: new[] { xi, xi },
                    corr: new[,]
                    {
                        { 1d, 0.5 },
                        { 0.5, 1d },
                    },
                    expected: 0.5462444438570895,
                    error: error);
                yield return Case(
                    x: new[] { xi, xi },
                    corr: new[,]
                    {
                        { 1d, -0.5 },
                        { -0.5, 1d },
                    },
                    expected: 0.41922310903660254,
                    error: error);
            }
        }
    }
}
