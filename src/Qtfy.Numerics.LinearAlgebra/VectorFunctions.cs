// <copyright file="VectorFunctions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

using System;

/// <summary>
/// A collection of vector operations.
/// </summary>
public static class VectorFunctions
{
    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    /// <param name="left">
    /// The first vector.
    /// </param>
    /// <param name="right">
    /// The second vector.
    /// </param>
    /// <returns>
    /// The dot product of the two vectors.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="left"/> is null.
    /// If <paramref name="right"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="left"/> and <paramref name="right"/> have different lengths.
    /// </exception>
    public static double Dot(double[] left, double[] right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        if (left.Length != right.Length)
        {
            throw new ArgumentException("Vectors must have the same length.");
        }

        var sum = 0d;
        for (var i = 0; i < left.Length; i++)
        {
            sum += left[i] * right[i];
        }

        return sum;
    }
}
