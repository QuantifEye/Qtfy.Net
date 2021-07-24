// <copyright file="Philox4X32.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The Philox4x32 counter based random number generator that performs 10 rounds.
    /// See the paper <see href="http://www.thesalmons.org/john/random123/papers/random123sc11.pdf"/>.
    /// There are 2^64 possible keyed generators, each with a period of 2^130.
    /// </summary>
    public sealed class Philox4X32 : UIntRandomNumberEngine
    {
        private readonly uint key0;

        private readonly uint key1;

        private nint index;

        private uint spare0;

        private uint spare1;

        private uint spare2;

        private uint ctr0;

        private uint ctr1;

        private uint ctr2;

        private uint ctr3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Philox4X32"/> class.
        /// </summary>
        /// <param name="key">
        /// The key to construct the engine with.
        /// </param>
        public Philox4X32(ulong key)
        {
            this.key0 = (uint)key;
            this.key1 = (uint)(key >> 32);
            this.index = 3;
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public override uint NextUInt()
        {
            unchecked
            {
                switch (this.index++)
                {
                    case 0:
                        return this.spare0;
                    case 1:
                        return this.spare1;
                    case 2:
                        return this.spare2;
                    default:
                    {
                        const ulong multiplier0 = 0xD2511F53;
                        const ulong multiplier1 = 0xCD9E8D57;
                        const uint bump0 = 0x9E3779B9;
                        const uint bump1 = 0xBB67AE85;

                        this.index = 0;

                        var c0 = this.ctr0;
                        var c1 = this.ctr1;
                        var c2 = this.ctr2;
                        var c3 = this.ctr3;

                        if (++this.ctr0 == 0U)
                        {
                            if (++this.ctr1 == 0U)
                            {
                                if (++this.ctr2 == 0U)
                                {
                                    ++this.ctr3;
                                }
                            }
                        }

                        var k0 = this.key0;
                        var k1 = this.key1;

                        var p0 = multiplier0 * c0;
                        var p1 = multiplier1 * c2;
                        c0 = (uint)(p1 >> 32) ^ c1 ^ k0;
                        c1 = (uint)p1;
                        c2 = (uint)(p0 >> 32) ^ c3 ^ k1;
                        c3 = (uint)p0;

                        for (nint i = 0; i < 9; ++i)
                        {
                            k0 += bump0;
                            k1 += bump1;
                            p0 = multiplier0 * c0;
                            p1 = multiplier1 * c2;
                            c0 = (uint)(p1 >> 32) ^ c1 ^ k0;
                            c1 = (uint)p1;
                            c2 = (uint)(p0 >> 32) ^ c3 ^ k1;
                            c3 = (uint)p0;
                        }

                        this.spare0 = c1;
                        this.spare1 = c2;
                        this.spare2 = c3;
                        return c0;
                    }
                }
            }
        }
    }
}
