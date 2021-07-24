// <copyright file="PiecewiseConstantDistributionTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Distributions
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Distributions;

    public class PiecewiseConstantDistributionTests
    {
        private static PiecewiseConstantDistribution MonotonicTestDistribution()
        {
            var boundaries = new[] { 1d, 2d, 3d };
            var weights = new[] { 1d, 1d };
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
            ConstructInvalid(new[] { 1d }, new[] { 1d });
            ConstructInvalid(new[] { 1d, 1d }, new[] { 1d, 1d });
            ConstructInvalid(new[] { 1d, 1d }, new[] { 1d });
            ConstructInvalid(new[] { 1d, 2d }, new[] { -1d });
        }

        [Test]
        public void TestCumulativeDistribution()
        {
            var dist = MonotonicTestDistribution();

            Assert.AreEqual(0, dist.CumulativeDistribution(-1d));
            Assert.AreEqual(0, dist.CumulativeDistribution(1d));
            Assert.AreEqual(0.5, dist.CumulativeDistribution(2d));
            Assert.AreEqual(1.0, dist.CumulativeDistribution(3.0));
            Assert.AreEqual(1.0, dist.CumulativeDistribution(4.0));
            Assert.AreEqual(0.75, dist.CumulativeDistribution(2.5));
            Assert.AreEqual(0.25, dist.CumulativeDistribution(1.5));
            Assert.AreEqual(double.NaN, dist.CumulativeDistribution(double.NaN));
        }

        [Test]
        public void TestQuantile()
        {
            var dist = MonotonicTestDistribution();

            Assert.AreEqual(1, dist.Quantile(0d));
            Assert.AreEqual(2, dist.Quantile(0.5));
            Assert.AreEqual(3, dist.Quantile(1d));
            Assert.AreEqual(1.5, dist.Quantile(0.25));
            Assert.AreEqual(2.5, dist.Quantile(0.75));
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
}
