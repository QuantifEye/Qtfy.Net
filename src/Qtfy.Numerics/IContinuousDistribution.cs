// <copyright file="IContinuousDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

/// <summary>
/// A base interface for continuous statistical distributions.
/// </summary>
public interface IContinuousDistribution : IDistribution<double>
{
    /// <summary>
    /// Calculates the probability density function at <paramref name="x"/>.
    /// </summary>
    /// <param name="x">
    /// The value at which to evaluate the probability density function.
    /// </param>
    /// <returns>
    /// The probability density function evaluated at <paramref name="x"/>.
    /// </returns>
    double Density(double x);

    /// <summary>
    /// Calculates the natural logarithm of the probability density function at <paramref name="x"/>.
    /// </summary>
    /// <param name="x">
    /// The value at which to evaluate the probability density function.
    /// </param>
    /// <returns>
    /// The natural logarithm of the probability density function evaluated at <paramref name="x"/>.
    /// </returns>
    double DensityLn(double x);
}
