// <copyright file="NormalSampler.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Random.Samplers;

using Qtfy.Numerics.Distributions;

/// <summary>
/// A random distribution that generates normally distributed values.
/// </summary>
public sealed class NormalSampler : ISampler<double>
{
    private readonly StandardNormalSampler standardNormalSampler;

    /// <summary>
    /// Initializes a new instance of the <see cref="NormalSampler"/> class.
    /// </summary>
    /// <param name="generator">
    /// The underlying bit generator to use.
    /// </param>
    /// <param name="mu">
    /// The mean of the distribution.
    /// </param>
    /// <param name="sigma">
    /// The standard deviation of the distribution.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="generator"/> is null.
    /// If <paramref name="mu"/> is nan or infinite.
    /// If <paramref name="sigma"/> is infinite or nan.
    /// If <paramref name="sigma"/> is less than or equal to 0.
    /// </exception>
    public NormalSampler(IRandomNumberEngine generator, double mu, double sigma)
    {
        ArgumentNullException.ThrowIfNull(generator);

        NormalDistribution.ValidateParameters(mu, sigma);
        this.standardNormalSampler = new StandardNormalSampler(generator);
        this.Mu = mu;
        this.Sigma = sigma;
    }

    /// <summary>
    /// Gets the mean parameter of the distribution.
    /// </summary>
    public double Mu { get; }

    /// <summary>
    /// Gets the standard deviation of the distribution.
    /// </summary>
    public double Sigma { get; }

    /// <inheritdoc/>
    public double GetNext()
    {
        return Math.FusedMultiplyAdd(this.standardNormalSampler.GetNext(), this.Sigma, this.Mu);
    }
}