// <copyright file="SamplerTester.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random;

    public static class SamplerTester
    {
        private static double IntegrateCdf(ISampler<double> sampler, double x, int trials)
        {
            var success = 0;
            for (var i = 0; i < trials; i++)
            {
                if (sampler.GetNext() <= x)
                {
                    ++success;
                }
            }

            return (double)success / trials;
        }

        public static void TestIntegrateDistribution(
            double x,
            ISampler<double> sampler,
            IDistribution referenceDistribution,
            double error)
        {
            const int trials = 1000000;
            var actual = IntegrateCdf(sampler, x, trials);
            var expected = referenceDistribution.CumulativeDistribution(x);
            Assert.AreEqual(expected, actual, error);
        }

        public static double IntegrateMultivariateCdf(ISampler<double[]> sampler, double[] x, int trials)
        {
            var success = 0;
            for (var i = 0; i < trials; i++)
            {
                var l = sampler.GetNext();
                if (LessThan(l, x))
                {
                    ++success;
                }
            }

            return (double)success / trials;

            static bool LessThan(double[] l, double[] r)
            {
                for (var i = 0; i < l.Length; ++i)
                {
                    if (l[i] > r[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
