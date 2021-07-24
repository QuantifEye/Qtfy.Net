// <copyright file="ReducedThreeFry4X64.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    /// <summary>
    /// The ReducedThreeFry4X64 counter based random number generator that performs 20 rounds.
    /// The class is similar to the <see cref="ThreeFry4X64"/> but limits the number of keys that can be used,
    /// and reduces the period of the generator.
    /// There are 2^64 possible keyed generators, each with a period of 2^66.
    /// For any given generator the first 2^66 draws are identical to that of the
    /// equivalently constructed <see cref="ReducedThreeFry4X64"/>, after which they counter is reset.
    /// </summary>
    public sealed class ReducedThreeFry4X64 : ULongRandomNumberEngine
    {
        private const ulong Parity = 0x1BD11BDAA9FC1A22UL;

        private int index = 2;

        private ulong spare1;

        private ulong spare2;

        private ulong spare3;

        private readonly ulong key;

        private ulong ctr = ulong.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReducedThreeFry4X64"/> class.
        /// </summary>
        /// <param name="key">
        /// The key to the generator.
        /// </param>
        public ReducedThreeFry4X64(ulong key)
        {
            this.key = key;
        }

        /// <inheritdoc />
        public override ulong NextULong()
        {
            unchecked
            {
                switch (++this.index)
                {
                    case 0:
                        return this.spare1;
                    case 1:
                        return this.spare2;
                    case 2:
                        return this.spare3;
                    default:
                        this.index = -1;

                        var k0 = this.key;
                        var k4 = k0 ^ Parity;
                        var c0 = ++this.ctr;
                        var c1 = 0UL;
                        var c2 = 0UL;
                        var c3 = 0UL;

                        c0 += c1;
                        c1 = (c1 << 14 | c1 >> 50) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 16 | c3 >> 48) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 52 | c3 >> 12) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 57 | c1 >> 7) ^ c2;
                        c0 += c1;
                        c1 = (c1 << 23 | c1 >> 41) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 40 | c3 >> 24) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 5 | c3 >> 59) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 37 | c1 >> 27) ^ c2;
                        c3 += k4 + 1UL;
                        c0 += c1;
                        c1 = (c1 << 25 | c1 >> 39) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 33 | c3 >> 31) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 46 | c3 >> 18) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 12 | c1 >> 52) ^ c2;
                        c0 += c1;
                        c1 = (c1 << 58 | c1 >> 6) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 22 | c3 >> 42) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 32 | c3 >> 32) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 32 | c1 >> 32) ^ c2;
                        c2 += k4;
                        c3 += k0 + 2UL;
                        c0 += c1;
                        c1 = (c1 << 14 | c1 >> 50) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 16 | c3 >> 48) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 52 | c3 >> 12) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 57 | c1 >> 7) ^ c2;
                        c0 += c1;
                        c1 = (c1 << 23 | c1 >> 41) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 40 | c3 >> 24) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 5 | c3 >> 59) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 37 | c1 >> 27) ^ c2;
                        c1 += k4;
                        c2 += k0;
                        c3 += 3UL;
                        c0 += c1;
                        c1 = (c1 << 25 | c1 >> 39) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 33 | c3 >> 31) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 46 | c3 >> 18) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 12 | c1 >> 52) ^ c2;
                        c0 += c1;
                        c1 = (c1 << 58 | c1 >> 6) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 22 | c3 >> 42) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 32 | c3 >> 32) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 32 | c1 >> 32) ^ c2;
                        c0 += k4;
                        c1 += k0;
                        c3 += 4UL;
                        c0 += c1;
                        c1 = (c1 << 14 | c1 >> 50) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 16 | c3 >> 48) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 52 | c3 >> 12) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 57 | c1 >> 7) ^ c2;
                        c0 += c1;
                        c1 = (c1 << 23 | c1 >> 41) ^ c0;
                        c2 += c3;
                        c3 = (c3 << 40 | c3 >> 24) ^ c2;
                        c0 += c3;
                        c3 = (c3 << 5 | c3 >> 59) ^ c0;
                        c2 += c1;
                        c1 = (c1 << 37 | c1 >> 27) ^ c2;
                        c0 += k0;
                        c3 += 5UL;

                        this.spare1 = c1;
                        this.spare2 = c2;
                        this.spare3 = c3;
                        return c0;
                }
            }
        }
    }
}
