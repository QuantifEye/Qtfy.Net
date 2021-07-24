// <copyright file="ISeedSequence.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random
{
    /// <summary>
    /// A seed sequence is constructed with integer-valued data and produces a requested number of unsigned integer values i, [0, 2^32)
    /// based on the data provided to the constructor data.
    /// The produced values are distributed over the entire 32-bit range even if the provided seed values are close.
    /// It provides a way to seed a large number of random number engines or to seed a generator that requires a lot of entropy,
    /// given a small seed or a poorly distributed initial seed sequence.
    /// </summary>
    public interface ISeedSequence
    {
        /// <summary>
        /// Fills the provided buffer with integer values in [0, 2^32) based on the original data provided in the
        /// constructor. The produced values are distributed over the entire range of unsigned 32 bit integers
        /// even if initial values were strongly biased.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to seed/initialize.
        /// </param>
        void Generate(uint[] buffer);

        /// <summary>
        /// Fills the provided buffer with integer values in [0, 2^32) based on the original data provided in the
        /// constructor. The produced values are distributed over the entire range of unsigned 32 bit integers
        /// even if initial values were strongly biased.
        /// </summary>
        /// <param name="buffer">
        /// The buffer to seed/initialize.
        /// </param>
        /// <remarks>
        /// This method shares an implementation with <see cref="Generate(uint[])"/> and casts values to
        /// generated to ulong.
        /// </remarks>
        void Generate(ulong[] buffer);
    }
}
