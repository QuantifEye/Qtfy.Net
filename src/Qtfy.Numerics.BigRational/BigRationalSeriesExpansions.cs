// <copyright file="BigRationalSeriesExpansions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

public partial struct BigRational
{
    /// <summary>
    /// Calculates the taylor approximation of Eulers constant raised to <paramref name="power"/>,
    /// with the specified number of terms.
    /// </summary>
    /// <param name="power">
    /// The power to raise Eulers constant to.
    /// </param>
    /// <param name="terms">
    /// The number of terms to compute.
    /// </param>
    /// <returns>
    /// The taylor approximation of Eulers constant raised to <paramref name="power"/>,
    /// with the specified number of terms.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="terms"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Exp(BigRational power, int terms)
    {
        if (terms <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(terms), "Terms must be positive.");
        }

        if (terms == 1)
        {
            return One;
        }

        var xn = One;
        var sum = xn;
        var factorial = BigInteger.One;
        for (var t = 1; t != terms; ++t)
        {
            xn *= power;
            factorial *= t;
            sum += xn / factorial;
        }

        return sum;
    }

    /// <summary>
    /// Approximates the natural (base e) logarithm of a specified number using a series expansion with the specified number of terms.
    /// </summary>
    /// <param name="x">
    /// The number whose logarithm is to be approximated.
    /// </param>
    /// <param name="terms">
    /// The number of terms to compute.
    /// </param>
    /// <returns>
    /// The approximation of the natural (base e) logarithm of a specified number.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="terms"/> is less than or equal to zero.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="x"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Log(BigRational x, int terms)
    {
        if (terms <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(terms), "Terms must be positive.");
        }

        if (x.IsNegativeOrZero)
        {
            throw new ArgumentOutOfRangeException(nameof(x), "Value must be positive.");
        }

        if (x.IsOne)
        {
            return Zero;
        }

        var n = 1 / (x - 1);
        var factor = 1 / ((2 * n) + 1);
        var factorSquared = factor * factor;
        var total = factor;
        for (int term = 1, power = 3; term < terms; ++term, power += 2)
        {
            factor *= factorSquared;
            total += factor / power;
        }

        return 2 * total;
    }
}
