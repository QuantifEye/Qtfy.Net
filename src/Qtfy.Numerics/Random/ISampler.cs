// <copyright file="ISampler.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Random;

/// <summary>
/// An object used to transform the values produced by a random number engine
/// into values from the desired distribution.
/// </summary>
/// <typeparam name="T">
/// The type of the generated values.
/// </typeparam>
public interface ISampler<T>
{
    /// <summary>
    /// Gets the next random number.
    /// </summary>
    /// <returns>
    /// The next random number.
    /// </returns>
    T GetNext();
}
