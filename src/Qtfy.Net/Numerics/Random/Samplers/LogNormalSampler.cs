// <copyright file="LogNormalSampler.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using System;
    using Qtfy.Net.Numerics.Distributions;

    /// <summary>
    /// A log normal random distribution.
    /// </summary>
    public sealed class LogNormalSampler : ISampler<double>
    {
        private readonly StandardNormalSampler standardNormalSampler;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogNormalSampler"/> class.
        /// </summary>
        /// <param name="generator">
        /// The underlying bit generator to use.
        /// </param>
        /// <param name="mu">
        /// The mean of the related normal distribution.
        /// </param>
        /// <param name="sigma">
        /// The standard deviation of the related normal distribution.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="generator"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="mu"/> is infinite or nan.
        /// if <paramref name="sigma"/> is infinite or nan.
        /// if <paramref name="sigma"/> is less than or equal to zero.
        /// </exception>
        public LogNormalSampler(IRandomNumberEngine generator, double mu, double sigma)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            NormalDistribution.ValidateParameters(mu, sigma);
            this.standardNormalSampler = new StandardNormalSampler(generator);
            this.Mu = mu;
            this.Sigma = sigma;
        }

        /// <summary>
        /// Gets the mean of the related normal distribution.
        /// </summary>
        public double Mu { get; }

        /// <summary>
        /// Gets the standard deviation parameter of the related normal distribution.
        /// </summary>
        public double Sigma { get; }

        /// <inheritdoc/>
        public double GetNext()
        {
            return Math.Exp(Math.FusedMultiplyAdd(this.standardNormalSampler.GetNext(), this.Sigma, this.Mu));
        }
    }
}
