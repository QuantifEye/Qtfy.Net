// <copyright file="StandardUniformDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Distributions
{
    using System;

    /// <summary>
    /// A standard uniform distribution. That is a continuous uniform distribution on [0, 1].
    /// </summary>
    public class StandardUniformDistribution : IContinuousDistribution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardUniformDistribution"/> class.
        /// </summary>
        private StandardUniformDistribution()
        {
        }

        /// <summary>
        /// Gets the singleton instance of this distribution.
        /// </summary>
        public static StandardUniformDistribution Instance { get; } = new ();

        /// <inheritdoc />
        /// <exception cref="ArgumentException">
        /// If <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public double Quantile(double probability)
        {
            if (probability >= 0d && probability <= 1d)
            {
                return probability;
            }

            throw new ArgumentException("invalid probability");
        }

        /// <inheritdoc />
        public double CumulativeDistribution(double x)
        {
            return CumulativeDistributionFunction(x);
        }

        /// <summary>
        /// The cumulative distribution function of the standard uniform distribution.
        /// </summary>
        /// <param name="x">
        /// The value at which to evaluate the function.
        /// </param>
        /// <returns>
        /// The probability that a random variable is less than or equal to <paramref name="x"/>.
        /// </returns>
        public static double CumulativeDistributionFunction(double x)
        {
            if (x <= 0d)
            {
                return 0d;
            }

            if (x >= 1d)
            {
                return 1d;
            }

            return x;
        }

        /// <inheritdoc />
        public double Density(double x)
        {
            if (double.IsNaN(x))
            {
                return double.NaN;
            }

            return x >= 0d && x <= 1d ? 1d : 0d;
        }

        /// <inheritdoc />
        public double DensityLn(double x)
        {
            if (double.IsNaN(x))
            {
                return double.NaN;
            }

            return x >= 0 && x <= 1d ? 0d : double.NegativeInfinity;
        }
    }
}
