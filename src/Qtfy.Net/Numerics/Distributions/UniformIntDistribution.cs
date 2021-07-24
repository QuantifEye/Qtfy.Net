// <copyright file="UniformIntDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Distributions
{
    using System;

    /// <summary>
    /// A uniform discrete distribution object.
    /// </summary>
    public class UniformIntDistribution : IDiscreteDistribution
    {
        private readonly double n;

        private readonly double m;

        private readonly double pmf;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformIntDistribution"/> class.
        /// </summary>
        /// <param name="min">
        /// The minimum parameter.
        /// </param>
        /// <param name="max">
        /// The maximum parameter.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="min"/> is greater than max.
        /// </exception>
        public UniformIntDistribution(int min, int max)
        {
            if (min < max)
            {
                ulong range = (uint)max - (uint)min;
                var n = range + 1UL;

                this.pmf = 1d / n;
                this.n = n;
                this.m = 1d - min;

                this.Variance = (n * n - 1UL) / 12d;
                this.Mean = ((double)max + min) / 2d;
                this.Min = min;
                this.Max = max;
            }
            else
            {
                throw new ArgumentException("min must be less than or equal to max");
            }
        }

        /// <summary>
        /// Gets the minimum parameter.
        /// </summary>
        public int Min { get; }

        /// <summary>
        /// Gets the maximum parameter.
        /// </summary>
        public int Max { get; }

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public double Mean { get; }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public double Variance { get; }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation
        {
            get => Math.Sqrt(this.Variance);
        }

        /// <inheritdoc />
        public double CumulativeDistribution(double x)
        {
            if (x < this.Min)
            {
                return 0d;
            }

            if (x >= this.Max)
            {
                return 1d;
            }

            return (Math.Floor(x) + this.m) / this.n;
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        /// If <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public int Quantile(double probability)
        {
            if (probability >= 0 && probability <= 1d)
            {
                return (int)Math.Ceiling(this.n * probability);
            }

            throw new ArgumentException("Invalid probability", nameof(probability));
        }

        /// <inheritdoc />
        public double Probability(int x)
        {
            return x < this.Min || x > this.Max ? 0d : this.pmf;
        }

        /// <inheritdoc />
        public double ProbabilityLn(int x)
        {
            return x < this.Min || x > this.Max ? double.NegativeInfinity : -Math.Log(this.n);
        }
    }
}
