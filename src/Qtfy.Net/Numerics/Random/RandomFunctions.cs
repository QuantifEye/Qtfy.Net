// <copyright file="RandomFunctions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random
{
    using System;

    /// <summary>
    /// Utility functions used for random number generation.
    /// </summary>
    public static class RandomFunctions
    {
        /// <summary>
        /// Creates a double in the interval [0, 1), rounded to the nearest multiple of 2^-53.
        /// </summary>
        /// <param name="bits">
        /// The bits to use as entropy.
        /// </param>
        /// <returns>
        /// A double in the interval [0, 1), rounded to the nearest multiple of 2^-53.
        /// </returns>
        public static double Canonical(ulong bits)
        {
            return Math.ScaleB(bits >> 11, -53);
        }

        /// <summary>
        /// Creates a double in the interval (0, 1], rounded to the nearest multiple of 2^-53.
        /// </summary>
        /// <param name="bits">
        /// The bits to use as entropy.
        /// </param>
        /// <returns>
        /// A double in the interval (0, 1], rounded to the nearest multiple of 2^-53.
        /// </returns>
        public static double IncrementedCanonical(ulong bits)
        {
            return Math.ScaleB((bits >> 11) + 1UL, -53);
        }
    }
}
