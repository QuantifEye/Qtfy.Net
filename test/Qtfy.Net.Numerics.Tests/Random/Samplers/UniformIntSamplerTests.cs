// <copyright file="UniformIntSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;
    using Qtfy.Net.Numerics.Random.Samplers;

    public class UniformIntSamplerTests
    {
        private const int Min = 7;

        private const int Max = 12;

        private static UniformIntSampler GetSampler(int min, int max)
        {
            return new (new ReducedThreeFry4X64(1), min, max);
        }

        [Test]
        public void TestConstructInvalid()
        {
            Assert.Throws<ArgumentException>(
                () => _ = GetSampler(12, 7));

            Assert.Throws<ArgumentNullException>(
                () => _ = new UniformIntSampler(null, 1, 2));
        }

        [Test]
        public void TestMinMax()
        {
            Assert.AreEqual(Min, GetSampler(Min, Max).Min);
            Assert.AreEqual(Max, GetSampler(Min, Max).Max);
        }

        [TestCase(1, 3)]
        public void IntegrateCdf(int min, int max)
        {
            const int trials = 1000000;
            const double error = 0.0005;
            var sampler = new UniformIntSampler(new ReducedThreeFry4X64(1), min, max);

            for (var x = min; x <= max; ++x)
            {
                var expected = (double)(x - min + 1) / (max - min + 1);
                var success = 0;
                for (var i = 0; i < trials; ++i)
                {
                    if (sampler.GetNext() <= x)
                    {
                        ++success;
                    }
                }

                var actual = (double)success / trials;
                Assert.AreEqual(expected, actual, error);
            }
        }
    }
}
