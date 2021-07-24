// <copyright file="StandardNormalDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Distributions
{
    using System;

    /// <summary>
    /// A standard normal distribution object.
    /// </summary>
    public class StandardNormalDistribution : IContinuousDistribution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardNormalDistribution"/> class.
        /// </summary>
        private StandardNormalDistribution()
        {
        }

        /// <summary>
        /// Gets the singleton instance of the standard normal distribution.
        /// </summary>
        public static StandardNormalDistribution Instance { get; } = new ();

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public double Mean
        {
            get => 0d;
        }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public double Variance
        {
            get => 1d;
        }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation
        {
            get => 1d;
        }

        /// <summary>
        /// Calculates the probability that a standard normal random variable is less than <paramref name="x"/>.
        /// </summary>
        /// <param name="x">
        /// The value at which to evaluate the standard normal CDF.
        /// </param>
        /// <returns>
        /// The probability that a random variable is less than or equal to <paramref name="x"/>.
        /// </returns>
        public static double CumulativeDistributionFunction(double x)
        {
            return Math.FusedMultiplyAdd(SpecialFunctions.Erf(x / Constants.SqrtTwo), 0.5d, 0.5d);
        }

        /// <inheritdoc />
        public double CumulativeDistribution(double x)
        {
            return CumulativeDistributionFunction(x);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        /// If <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public double Quantile(double probability)
        {
            return QuantileFunction(probability);
        }

        /// <summary>
        /// Calculates the quantile of function of the distribution.
        /// </summary>
        /// <param name="probability">
        /// The value at which to evaluate the function.
        /// </param>
        /// <returns>
        /// The required quantile of the standard normal distribution.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public static double QuantileFunction(double probability)
        {
            if (probability >= 0d && probability <= 1d)
            {
                return Constants.SqrtTwo * SpecialFunctions.ErfInv(Math.FusedMultiplyAdd(2d, probability, -1d));
            }

            throw new ArgumentException("invalid probability");
        }

        /// <inheritdoc />
        public double Density(double x)
        {
            return Math.Exp(-0.5 * x * x) / Constants.SqrtTwoPi;
        }

        /// <inheritdoc />
        public double DensityLn(double x)
        {
            return -0.5 * x * x - Constants.LogSqrtTwoPi;
        }
    }
}
