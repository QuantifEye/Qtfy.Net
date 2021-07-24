// <copyright file="NormalDistributionTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Distributions
{
    using System;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Distributions;
    using static Tests.TestUtils;

    public class NormalDistributionTests
    {
        [TestCase(2d, 0d)]
        [TestCase(2d, -1d)]
        [TestCase(2d, double.NegativeInfinity)]
        [TestCase(2d, double.PositiveInfinity)]
        [TestCase(2d, double.NaN)]
        [TestCase(double.NegativeInfinity, 2d)]
        [TestCase(double.PositiveInfinity, 2d)]
        [TestCase(double.NaN, 2d)]
        public void ConstructInvalid(double mu, double sigma)
        {
            Assert.Throws<ArgumentException>(
                () => _ = new NormalDistribution(mu, sigma));
        }

        [Test]
        public void TestMu()
        {
            var mu = 2.3;
            var distribution = new NormalDistribution(mu, 1);
            Assert.AreEqual(mu, distribution.Mu);
        }

        [TestCase(1d)]
        [TestCase(2d)]
        public void TestSigma(double sigma)
        {
            var distribution = new NormalDistribution(1, sigma);
            Assert.AreEqual(sigma, distribution.Sigma);
            Assert.AreEqual(sigma, distribution.StandardDeviation);
            Assert.AreEqual(sigma * sigma, distribution.Variance);
        }

        [TestCase(1, 1, 1)]
        public void TestMean(double mu, double sigma, double expected)
        {
            Assert.AreEqual(expected, new NormalDistribution(mu, sigma).Mean);
        }

        [TestCase(1.5, 1.2, 0.5, 1.5)]
        [TestCase(1.5, 1.2, 0.0, double.NegativeInfinity)]
        [TestCase(1.5, 1.2, 1.0, double.PositiveInfinity)]
        public void TestQuantile(double mu, double sigma, double probability, double expected)
        {
            IsClose(expected, new NormalDistribution(mu, sigma).Quantile(probability));
            IsClose(expected, NormalDistribution.QuantileFunction(probability, mu, sigma));
        }

        [TestCase(0, 1, -0.1)]
        [TestCase(0, 1, 1.1)]
        [TestCase(0, 1, double.NaN)]
        public void TestInvalidQuantile(double mu, double sigma, double expected)
        {
            Assert.Throws<ArgumentException>(
                () => _ = new NormalDistribution(mu, sigma).Quantile(expected));
        }

        [TestCase(1.5, 1.2, 2.0, 0.30481030534500203)]
        [TestCase(1.5, 1.2, 1.0, 0.30481030534500203)]
        [TestCase(1.5, 1.2, double.NegativeInfinity, 0d)]
        [TestCase(1.5, 1.2, double.PositiveInfinity, 0d)]
        [TestCase(1.5, 1.2, double.NaN, double.NaN)]
        public void TestDensity(double mu, double sigma, double x, double expected)
        {
            IsClose(expected, new NormalDistribution(mu, sigma).Density(x));
        }

        [TestCase(1.5, 1.2, 10, -26.18806564555419)]
        [TestCase(1.5, 1.2, -7.0, -26.18806564555419)]
        [TestCase(1.5, 1.2, double.NegativeInfinity, double.NegativeInfinity)]
        [TestCase(1.5, 1.2, double.PositiveInfinity, double.NegativeInfinity)]
        [TestCase(1.5, 1.2, double.NaN, double.NaN)]
        public void TestDensityLn(double mu, double sigma, double x, double expected)
        {
            IsClose(expected, new NormalDistribution(mu, sigma).DensityLn(x));
        }

        [TestCase(1.5, 1.2, 2, 0.6615388804893103)]
        [TestCase(1.5, 1.2, double.NegativeInfinity, 0d)]
        [TestCase(1.5, 1.2, double.PositiveInfinity, 1d)]
        [TestCase(1.5, 1.2, double.NaN, double.NaN)]
        public void TestCumulativeDistribution(double mu, double sigma, double x, double expected)
        {
            IsClose(expected, new NormalDistribution(mu, sigma).CumulativeDistribution(x));
            IsClose(expected, NormalDistribution.CumulativeDistributionFunction(x, mu, sigma));
        }
    }
}
