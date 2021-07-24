// <copyright file="IRandomNumberEngine.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random
{
    /// <summary>
    /// The common interface that all bit generators that produce uniform values share.
    /// </summary>
    public interface IRandomNumberEngine
    {
        /// <summary>
        /// Generates a <see cref="uint"/> that is uniformly distributed across all possible <see cref="uint"/> values,
        /// this is [0, 2^32).
        /// </summary>
        /// <returns>
        /// An <see cref="uint"/> that is uniformly distributed across all possible <see cref="uint"/> values.
        /// </returns>
        uint NextUInt();

        /// <summary>
        /// Generates a <see cref="uint"/> that is uniformly distributed across all possible <see cref="uint"/> values,
        /// this is [0, max].
        /// </summary>
        /// <param name="max">
        /// The largest possible number that will be returned.
        /// </param>
        /// <returns>
        /// A <see cref="uint"/> that is uniformly distributed across all possible <see cref="uint"/> values,
        /// this is [0, max].
        /// </returns>
        uint NextUInt(uint max);

        /// <summary>
        /// Generates a <see cref="ulong"/> that is uniformly distributed across all possible <see cref="ulong"/> values,
        /// this is [0, 2^64).
        /// </summary>
        /// <returns>
        /// An <see cref="ulong"/> that is uniformly distributed across all possible <see cref="ulong"/> values.
        /// </returns>
        ulong NextULong();

        /// <summary>
        /// Generates a <see cref="ulong"/> that is uniformly distributed across all possible <see cref="ulong"/> values,
        /// this is [0, max].
        /// </summary>
        /// <param name="max">
        /// The largest possible number that will be returned.
        /// </param>
        /// <returns>
        /// A <see cref="ulong"/> that is uniformly distributed across all possible <see cref="ulong"/> values,
        /// this is [0, max].
        /// </returns>
        ulong NextULong(ulong max);

        /// <summary>
        /// Generates a standard real uniform random variable distributed on the interval [0, 1),
        /// rounded to the nearest multiple of 2^-53.
        /// </summary>
        /// <returns>
        /// A standard real uniform random variable distributed on the interval [0, 1).
        /// </returns>
        double NextCanonical();

        /// <summary>
        /// Creates a double in the interval (0, 1] by creating an integer the range (0, 2^53],
        /// converting this integer to a double, and dividing this number by 2^53.
        /// </summary>
        /// <returns>
        /// A double in the interval (0, 1].
        /// </returns>
        double NextIncrementedCanonical();

        /// <summary>
        /// Generates a standard real uniform random variable distributed on the interval [0, 1],
        /// rounded to the nearest multiple of 2^-53.
        /// </summary>
        /// <returns>
        /// A double in the interval [0, 1].
        /// </returns>
        public double NextStandardUniform();
    }
}
