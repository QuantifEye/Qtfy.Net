// <copyright file="IDistribution{T}.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    /// <summary>
    /// A base interface for statistical distributions with a quantile function.
    /// </summary>
    /// <typeparam name="T">
    /// The numeric type of the possible values that the underlying
    /// random variable can have.
    /// </typeparam>
    public interface IDistribution<T> : IDistribution
    {
        /// <summary>
        /// Calculates the quantile of the distribution for a provided probability.
        /// </summary>
        /// <param name="probability">
        /// The probability of the quantile to calculate.
        /// </param>
        /// <returns>
        /// The quantile of the distribution for a provided probability.
        /// </returns>
        T Quantile(double probability);
    }
}
