// <copyright file="BigRational.SeriesExpansions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    using System;
    using System.Numerics;

    /// <summary>
    /// A structure that represents a rational number with an arbitrarily large numerator and denominator.
    /// </summary>
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
        public static BigRational Exp(BigRational power, int terms)
        {
            if (terms < 0)
            {
                throw new ArgumentException("terms must be non-negative");
            }

            if (terms == 0)
            {
                return 0;
            }

            if (terms == 1)
            {
                return 1;
            }

            var xn = new BigRational(BigInteger.One);
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
        /// Approximates the natural (base e) logarithm of a specified number using a series expansion of a specified (default = 1000) number of terms.
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
        public static BigRational Log(BigRational x, int terms)
        {
            if (terms < 0)
            {
                throw new ArgumentException("terms must be non-negative");
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
}
