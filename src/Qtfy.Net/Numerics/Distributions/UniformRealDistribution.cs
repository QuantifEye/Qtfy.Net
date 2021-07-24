// <copyright file="UniformRealDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Distributions
{
    using System;

    /// <summary>
    /// A uniform real (continuous) distribution object.
    /// </summary>
    public class UniformRealDistribution : IContinuousDistribution
    {
        private readonly double density;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformRealDistribution"/> class.
        /// </summary>
        /// <param name="min">
        /// The minimum parameter.
        /// </param>
        /// <param name="max">
        /// The maximum parameter.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="min"/> is infinite or nan.
        /// If <paramref name="max"/> is infinite or nan.
        /// or if <paramref name="min"/> is greater-than-or-equal-to <paramref name="max"/>.
        /// </exception>
        public UniformRealDistribution(double min, double max)
        {
            ValidateParameters(min, max);
            var range = max - min;
            var variance = range * range / 12d;
            this.Mean = (max + min) / 2d;
            this.Variance = variance;
            this.StandardDeviation = Math.Sqrt(variance);
            this.Min = min;
            this.Max = max;
            this.density = 1d / range;
        }

        /// <summary>
        /// Gets the minimum parameter.
        /// </summary>
        public double Min { get; }

        /// <summary>
        /// Gets the maximum parameter.
        /// </summary>
        public double Max { get; }

        /// <summary>
        /// Gets the mean if the distribution.
        /// </summary>
        public double Mean { get; }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public double Variance { get; }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation { get; }

        /// <summary>
        /// Checks that min and max are valid parameters to create a uniform continuous distribution.
        /// </summary>
        /// <param name="min">
        /// The smallest value the variable can have.
        /// </param>
        /// <param name="max">
        /// The greatest value the variable can have.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="min"/> is infinite or nan.
        /// If <paramref name="max"/> is infinite or nan.
        /// or if <paramref name="min"/> is greater-than-or-equal-to <paramref name="max"/>.
        /// </exception>
        internal static void ValidateParameters(double min, double max)
        {
            if (!double.IsFinite(min))
            {
                throw new ArgumentException("value must be finite and not NaN", nameof(min));
            }

            if (!double.IsFinite(max))
            {
                throw new ArgumentException("value must be finite and not NaN", nameof(max));
            }

            if (min >= max)
            {
                throw new ArgumentException("min must be less than max");
            }
        }

        /// <inheritdoc />
        public double CumulativeDistribution(double x)
        {
            return CumulativeDistributionFunctionImpl(x, this.Min, this.Max);
        }

        /// <summary>
        /// The cumulative distribution function for the continuous uniform distribution.
        /// </summary>
        /// <param name="x">
        /// The value at which to evaluate the function.
        /// </param>
        /// <param name="min">
        /// The minimum parameter value of the distribution.
        /// </param>
        /// <param name="max">
        /// The parameter value of the distribution.
        /// </param>
        /// <returns>
        /// The probability that a random variable is less than or equal to <paramref name="x"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="min"/> is infinite or nan.
        /// If <paramref name="max"/> is infinite or nan.
        /// or if <paramref name="min"/> is greater-than-or-equal-to <paramref name="max"/>.
        /// </exception>
        public static double CumulativeDistributionFunction(double x, double min, double max)
        {
            ValidateParameters(min, max);
            return CumulativeDistributionFunctionImpl(x, min, max);
        }

        /// <summary>
        /// The cumulative distribution function for the continuous uniform distribution, i.e.
        /// private backend to the function before.
        /// </summary>
        /// <param name="x">
        /// The value at which to evaluate the function.
        /// </param>
        /// <param name="min">
        /// The minimum parameter value of the distribution.
        /// </param>
        /// <param name="max">
        /// The parameter value of the distribution.
        /// </param>
        /// <returns>
        /// The probability that a random variable is less than or equal to <paramref name="x"/>.
        /// </returns>
        private static double CumulativeDistributionFunctionImpl(double x, double min, double max)
        {
            if (x <= min)
            {
                return 0d;
            }

            if (x >= max)
            {
                return 1d;
            }

            return (x - min) / (max - min);
        }

        /// <inheritdoc/>
        public double Density(double x)
        {
            if (double.IsNaN(x))
            {
                return double.NaN;
            }

            return x < this.Min || x > this.Max ? 0d : this.density;
        }

        /// <inheritdoc/>
        public double DensityLn(double x)
        {
            if (double.IsNaN(x))
            {
                return double.NaN;
            }

            return x < this.Min || x > this.Max
                ? double.NegativeInfinity
                : -Math.Log(this.Max - this.Min);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        /// If <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public double Quantile(double probability)
        {
            if (probability >= 0d && probability <= 1d)
            {
                return Math.FusedMultiplyAdd(this.Max - this.Min, probability, this.Min);
            }

            throw new ArgumentException("Invalid probability", nameof(probability));
        }
    }
}
