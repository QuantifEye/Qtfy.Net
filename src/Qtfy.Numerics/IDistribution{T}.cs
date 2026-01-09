// <copyright file="IDistribution{T}.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

/// <summary>
/// A base interface for statistical distributions with a quantile function.
/// </summary>
/// <typeparam name="T">
/// The numeric type of values the distribution can return.
/// </typeparam>
public interface IDistribution<T> : IDistribution
{
    /// <summary>
    /// Calculates the quantile of the distribution for a provided probability.
    /// </summary>
    /// <param name="probability">
    /// A probability in the interval [0, 1].
    /// </param>
    /// <returns>
    /// The quantile of the distribution for the provided probability.
    /// </returns>
    T Quantile(double probability);
}
