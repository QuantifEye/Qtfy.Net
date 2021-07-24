// <copyright file="PermutedCongruentialGenerator.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    using System;

    /// <summary>
    /// The Permuted Congruential Generator.
    /// <see href="https://www.pcg-random.org/index.html" />.
    /// </summary>
    public sealed class PermutedCongruentialGenerator : UIntRandomNumberEngine
    {
        private readonly ulong inc;

        private ulong state;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutedCongruentialGenerator"/> class.
        /// </summary>
        /// <param name="stateInitializer">
        /// The state initializer.
        /// </param>
        /// <param name="streamId">
        /// The stream id. Must be in range [0, 2^63).
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="streamId"/> is not in [0, 2^63).
        /// </exception>
        public PermutedCongruentialGenerator(ulong stateInitializer, ulong streamId)
        {
            unchecked
            {
                const ulong maxStreamId = 1UL << 63;
                if (streamId >= maxStreamId)
                {
                    throw new ArgumentException(
                        $"{nameof(streamId)} must be less than {maxStreamId}.",
                        nameof(streamId));
                }

                this.state = 0U;
                this.inc = (streamId << 1) | 1UL;
                _ = this.NextUInt();
                this.state += stateInitializer;
                _ = this.NextUInt();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermutedCongruentialGenerator"/> class.
        /// This method uses deterministic default values.
        /// </summary>
        public PermutedCongruentialGenerator()
        {
            this.state = 0x853c49e6748fea9bUL;
            this.inc = 0xda3e39cb94b95bdbUL;
        }

        /// <inheritdoc />
        public override uint NextUInt()
        {
            unchecked
            {
                var oldState = this.state;
                this.state = oldState * 6364136223846793005UL + this.inc;
                var xorShifted = (uint)(((oldState >> 18) ^ oldState) >> 27);
                var rot = (int)(oldState >> 59);
                return (xorShifted >> rot) | (xorShifted << (-rot & 31));
            }
        }
    }
}
