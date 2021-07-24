// <copyright file="ThreeFry4X64.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    /// <summary>
    /// The ThreeFry4X64 counter based random number generator that performs 20 rounds.
    /// See the paper <see href="http://www.thesalmons.org/john/random123/papers/random123sc11.pdf"/>.
    /// There are 2^256 possible keyed generators, each with a period of 2^258.
    /// </summary>
    public sealed class ThreeFry4X64 : ULongRandomNumberEngine
    {
        private const ulong Parity = 0x1BD11BDAA9FC1A22UL;

        private nint index = 3;

        private ulong spare1;

        private ulong spare2;

        private ulong spare3;

        private readonly ulong key0;

        private readonly ulong key1;

        private readonly ulong key2;

        private readonly ulong key3;

        private readonly ulong key4;

        private ulong ctr0;

        private ulong ctr1;

        private ulong ctr2;

        private ulong ctr3;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreeFry4X64"/> class.
        /// </summary>
        /// <param name="key0">
        /// The first key to the generator.
        /// </param>
        /// <param name="key1">
        /// The second key to the generator.
        /// </param>
        /// <param name="key2">
        /// The third key to the generator.
        /// </param>
        /// <param name="key3">
        /// The fourth key to the generator.
        /// </param>
        /// <remarks>
        /// Conceptually the key is equal to
        /// key0 + (key1 * 2^64) + (key2 * 2^128) + (key3 * 2^196)
        /// resulting in 2^256 possible keys.
        /// </remarks>
        public ThreeFry4X64(ulong key0, ulong key1 = 0UL, ulong key2 = 0UL, ulong key3 = 0UL)
        {
            this.key0 = key0;
            this.key1 = key1;
            this.key2 = key2;
            this.key3 = key3;
            this.key4 = key0 ^ key1 ^ key2 ^ key3 ^ Parity;
        }

        /// <inheritdoc />
        public override ulong NextULong()
        {
            unchecked
            {
                switch (++this.index)
                {
                    case 1:
                        return this.spare1;
                    case 2:
                        return this.spare2;
                    case 3:
                        return this.spare3;
                    default:
                        this.index = 0;

                        var k0 = this.key0;
                        var k1 = this.key1;
                        var k2 = this.key2;
                        var k3 = this.key3;
                        var k4 = this.key4;
                        var c0 = this.ctr0;
                        var c1 = this.ctr1;
                        var c2 = this.ctr2;
                        var c3 = this.ctr3;

                        if (++this.ctr0 == 0UL)
                        {
                            if (++this.ctr1 == 0UL)
                            {
                                if (++this.ctr2 == 0UL)
                                {
                                    ++this.ctr3;
                                }
                            }
                        }

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
                        c0 += k1;
                        c1 += k2;
                        c2 += k3;
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
                        c0 += k2;
                        c1 += k3;
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
                        c0 += k3;
                        c1 += k4;
                        c2 += k0;
                        c3 += k1 + 3UL;
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
                        c2 += k1;
                        c3 += k2 + 4UL;
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
                        c1 += k1;
                        c2 += k2;
                        c3 += k3 + 5UL;

                        this.spare1 = c1;
                        this.spare2 = c2;
                        this.spare3 = c3;
                        return c0;
                }
            }
        }
    }
}
