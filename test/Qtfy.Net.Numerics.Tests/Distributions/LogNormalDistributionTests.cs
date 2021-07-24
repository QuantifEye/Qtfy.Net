// <copyright file="LogNormalDistributionTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Distributions
{
    using System;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Distributions;
    using static TestUtils;

    public class LogNormalDistributionTests
    {
        [TestCase(2d, 0d)]
        [TestCase(2d, -1d)]
        [TestCase(2d, double.NegativeInfinity)]
        [TestCase(2d, double.PositiveInfinity)]
        [TestCase(2d, double.NaN)]
        [TestCase(double.NegativeInfinity, 2d)]
        [TestCase(double.PositiveInfinity, 2d)]
        [TestCase(double.NaN, 2d)]
        public void ConstructInvalid(double mean, double sigma)
        {
            Assert.Throws<ArgumentException>(
                () => _ = new LogNormalDistribution(mean, sigma));
        }

        [Test]
        public void TestMu()
        {
            var mu = 2.3;
            var distribution = new LogNormalDistribution(mu, 1);
            Assert.AreEqual(mu, distribution.Mu);
        }

        [Test]
        public void TestSigma()
        {
            var sigma = 2.3;
            var distribution = new LogNormalDistribution(1, sigma);
            Assert.AreEqual(sigma, distribution.Sigma);
        }

        [TestCase(1.5, 1.2, 9.207330865882250958792)]
        [TestCase(-1.5, 1.2, 0.4584060113052235451173)]
        public void TestMean(double mu, double sigma, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).Mean);
        }

        [TestCase(1.5, 1.2, 273.03430003502444)]
        [TestCase(-1.5, 1.2, 0.6767843655163925)]
        public void TestVariance(double mu, double sigma, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).Variance);
        }

        [TestCase(1.5, 1.2, 16.52374957553595)]
        [TestCase(-1.5, 1.2, 0.8226690498106711)]
        public void TestStandardDeviation(double mu, double sigma, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).StandardDeviation);
        }

        [TestCase(1.5, 1.2, 0d, 0d)]
        [TestCase(1.5, 1.2, 0.25, 1.9949366586042043)]
        [TestCase(1.5, 1.2, 0.50, 4.4816890703380645)]
        [TestCase(1.5, 1.2, 0.75, 10.068257975288748)]
        [TestCase(1.5, 1.2, 1d, double.PositiveInfinity)]
        public void TestQuantile(double mu, double sigma, double probability, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).Quantile(probability));
        }

        [TestCase(0, 1, -0.1)]
        [TestCase(0, 1, 1.1)]
        public void TestInvalidQuantile(double mu, double sigma, double expected)
        {
            Assert.Throws<ArgumentException>(
                () => _ = new LogNormalDistribution(mu, sigma).Quantile(expected));
        }

        [TestCase(1.5, 1.2, 0d, 0d)]
        [TestCase(1.5, 1.2, 1d, 0.15220757115751826)]
        [TestCase(1.5, 1.2, 2d, 0.13259539628602582)]
        [TestCase(1.5, 1.2, 5d, 0.0662144498764099)]
        [TestCase(1.5, 1.2, -double.Epsilon, 0d)]
        [TestCase(1.5, 1.2, double.PositiveInfinity, 0d)]
        [TestCase(1.5, 1.2, double.NaN, double.NaN)]
        public void TestDensity(double mu, double sigma, double x, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).Density(x));
        }

        [TestCase(1.5, 1.2, 1d, -1.8825100899986273)]
        [TestCase(1.5, 1.2, 2d, -2.0204529206413384)]
        [TestCase(1.5, 1.2, 10d, -3.627505888373319)]
        [TestCase(1.5, 1.2, 0d, double.NegativeInfinity)]
        [TestCase(1.5, 1.2, -double.Epsilon, double.NegativeInfinity)]
        [TestCase(1.5, 1.2, double.PositiveInfinity, double.NegativeInfinity)]
        [TestCase(1.5, 1.2, double.NaN, double.NaN)]
        public void TestDensityLn(double mu, double sigma, double x, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).DensityLn(x));
        }

        [TestCase(1.5, 1.2, 2, 0.250671749381456)]
        [TestCase(1.5, 1.2, -2d, 0d)]
        [TestCase(1.5, 1.2, -1d, 0d)]
        [TestCase(1.5, 1.2, 0d, 0d)]
        [TestCase(1.5, 1.2, double.PositiveInfinity, 1d)]
        [TestCase(1.5, 1.2, double.NaN, double.NaN)]
        public void TestCumulativeDistribution(double mu, double sigma, double x, double expected)
        {
            IsClose(expected, new LogNormalDistribution(mu, sigma).CumulativeDistribution(x));
            IsClose(expected, LogNormalDistribution.CumulativeDistributionFunction(x, mu, sigma));
        }
    }
}
