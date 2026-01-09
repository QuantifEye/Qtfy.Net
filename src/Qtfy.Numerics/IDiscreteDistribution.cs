// <copyright file="IDiscreteDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

/// <summary>
/// A base interface for discrete statistical distributions.
/// </summary>
public interface IDiscreteDistribution : IDistribution<int>
{
    /// <summary>
    /// Calculates the probability mass function at <paramref name="x"/>.
    /// </summary>
    /// <param name="x">
    /// The value at which to evaluate the probability mass function.
    /// </param>
    /// <returns>
    /// The probability mass function at <paramref name="x"/>.
    /// </returns>
    double Probability(int x);

    /// <summary>
    /// Calculates the natural logarithm of the probability mass function at <paramref name="x"/>.
    /// </summary>
    /// <param name="x">
    /// The value at which to evaluate the natural logarithm of the probability mass function.
    /// </param>
    /// <returns>
    /// The natural logarithm of the probability mass function evaluated at <paramref name="x"/>.
    /// </returns>
    double ProbabilityLn(int x);
}
