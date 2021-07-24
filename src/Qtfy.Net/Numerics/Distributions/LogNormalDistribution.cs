// <copyright file="LogNormalDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Distributions
{
    using System;
    using static System.Math;
    using static SpecialFunctions;

    /// <summary>
    /// A Log-Normal statistical distribution object.
    /// </summary>
    public class LogNormalDistribution : IContinuousDistribution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogNormalDistribution"/> class.
        /// </summary>
        /// <param name="mu">
        /// The mean parameter of the related normal distribution.
        /// </param>
        /// <param name="sigma">
        /// The standard deviation of the related normal distribution.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="mu"/> is infinite or nan.
        /// If <paramref name="sigma"/> is infinite or nan.
        /// If <paramref name="sigma"/> is less than or equal to zero.
        /// </exception>
        public LogNormalDistribution(double mu, double sigma)
        {
            NormalDistribution.ValidateParameters(mu, sigma);
            this.Mu = mu;
            this.Sigma = sigma;
        }

        /// <summary>
        /// Gets the mean parameter of the related normal distribution.
        /// </summary>
        public double Mu { get; }

        /// <summary>
        /// Gets the standard deviation parameter of the related normal distribution.
        /// </summary>
        public double Sigma { get; }

        /// <summary>
        /// Gets the mean of the distribution.
        /// </summary>
        public double Mean
        {
            get => Exp(this.Mu + this.Sigma * this.Sigma / 2d);
        }

        /// <summary>
        /// Gets the variance of the distribution.
        /// </summary>
        public double Variance
        {
            get
            {
                var v = this.Sigma * this.Sigma;
                return (Exp(v) - 1d) * Exp(2d * this.Mu + v);
            }
        }

        /// <summary>
        /// Gets the standard deviation of the distribution.
        /// </summary>
        public double StandardDeviation
        {
            get => Sqrt(this.Variance);
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">
        /// if <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public double Quantile(double probability)
        {
            if (probability >= 0d && probability <= 1d)
            {
                var erfInv = ErfInv(FusedMultiplyAdd(2d, probability, -1d));
                return Exp(this.Mu + Constants.SqrtTwo * this.Sigma * erfInv);
            }

            throw new ArgumentException("invalid probability", nameof(probability));
        }

        /// <inheritdoc />
        public double Density(double x)
        {
            if (x <= 0d)
            {
                return 0d;
            }
            else
            {
                var z = (Log(x) - this.Mu) / this.Sigma;
                return Exp(-0.5 * z * z) / (x * this.Sigma * Constants.SqrtTwoPi);
            }
        }

        /// <inheritdoc />
        public double DensityLn(double x)
        {
            if (x <= 0d)
            {
                return double.NegativeInfinity;
            }
            else
            {
                var z = (Log(x) - this.Mu) / this.Sigma;
                return -0.5 * z * z - Log(x * this.Sigma * Constants.SqrtTwoPi);
            }
        }

        /// <inheritdoc />
        public double CumulativeDistribution(double x)
        {
            return CumulativeDistributionFunctionImpl(x, this.Mu, this.Sigma);
        }

        /// <summary>
        /// The cumulative distribution function for the log-normal distribution.
        /// </summary>
        /// <param name="x">
        /// The point at which to evaluate the function.
        /// </param>
        /// <param name="mu">
        /// The mu parameter of the related normal distribution.
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter of the related normal distribution.
        /// </param>
        /// <returns>
        /// The probability that a random variable is less than or equal to <paramref name="x"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="mu"/> is infinite or nan.
        /// If <paramref name="sigma"/> is infinite or nan.
        /// If <paramref name="sigma"/> is less than or equal to zero.
        /// </exception>
        public static double CumulativeDistributionFunction(double x, double mu, double sigma)
        {
            NormalDistribution.ValidateParameters(mu, sigma);
            return CumulativeDistributionFunctionImpl(x, mu, sigma);
        }

        /// <summary>
        /// The cumulative distribution function for the log-normal distribution, i.e.
        /// private backend to function before.
        /// </summary>
        /// <param name="x">
        /// The point at which to evaluate the function.
        /// </param>
        /// <param name="mu">
        /// The mu parameter of the related normal distribution.
        /// </param>
        /// <param name="sigma">
        /// The sigma parameter of the related normal distribution.
        /// </param>
        /// <returns>
        /// The probability that a random variable is less than or equal to <paramref name="x"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="mu"/> is infinite or nan.
        /// If <paramref name="sigma"/> is infinite or nan.
        /// If <paramref name="sigma"/> is less than or equal to zero.
        /// </exception>
        private static double CumulativeDistributionFunctionImpl(double x, double mu, double sigma)
        {
            return x <= 0d
                ? 0d
                : FusedMultiplyAdd(Erf((Log(x) - mu) / (Constants.SqrtTwo * sigma)), 0.5, 0.5);
        }
    }
}
