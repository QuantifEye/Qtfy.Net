// <copyright file="LogNormalSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Distributions;
    using Qtfy.Net.Numerics.Random;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;
    using Qtfy.Net.Numerics.Random.Samplers;

    public class LogNormalSamplerTests
    {
        [TestCase(0.5, 0d, 1d, 0.001)]
        [TestCase(0.0, 0d, 1d, 0.001)]
        [TestCase(-0.5, 0d, 1d, 0.001)]
        public void TestGetIntegrateDistribution(double x, double mean, double sigma, double error)
        {
            var sampler = new LogNormalSampler(new ReducedThreeFry4X64(1), mean, sigma);
            var referenceDistribution = new LogNormalDistribution(mean, sigma);
            SamplerTester.TestIntegrateDistribution(x, sampler, referenceDistribution, error);
        }

        [Test]
        public void TestProperties()
        {
            const double mu = 12d;
            const double sigma = 1d;
            var sampler = new LogNormalSampler(new ReducedThreeFry4X64(1), mu, sigma);
            Assert.AreEqual(mu, sampler.Mu);
            Assert.AreEqual(sigma, sampler.Sigma);
        }

        private static void TestInvalidThrows<TException>(IRandomNumberEngine engine, double mu, double sigma)
            where TException : Exception
        {
            Assert.Throws<TException>(
                () => _ = new LogNormalSampler(engine, mu, sigma));
        }

        [Test]
        public void TestNanParameter()
        {
            var engine = new ReducedThreeFry4X64(1);
            TestInvalidThrows<ArgumentException>(engine, double.NaN, 1d);
            TestInvalidThrows<ArgumentException>(engine, 1d, double.NaN);
        }

        [Test]
        public void TestConstructInvalidGenerator()
        {
            TestInvalidThrows<ArgumentNullException>(null, 1d, 1d);
        }
    }
}
