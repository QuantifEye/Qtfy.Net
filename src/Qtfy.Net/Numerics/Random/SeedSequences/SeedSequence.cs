// <copyright file="SeedSequence.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.SeedSequences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// a <see cref="uint"/> seed sequence. <see cref="ISeedSequence"/>.
    /// </summary>
    public sealed class SeedSequence : ISeedSequence
    {
        private readonly uint[] entropy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedSequence"/> class.
        /// </summary>
        /// <param name="seeds">
        /// The seeds to construct the seed sequence with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="seeds"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="seeds"/> is empty.
        /// </exception>
        public SeedSequence(IEnumerable<uint> seeds)
        {
            if (seeds is null)
            {
                throw new ArgumentNullException(nameof(seeds));
            }

            this.entropy = seeds.ToArray();
            if (this.entropy.Length == 0)
            {
                throw new ArgumentException("Must provide entropy.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedSequence"/> class.
        /// </summary>
        /// <param name="seeds">
        /// The seeds to construct the seed sequence with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="seeds"/> is null.
        /// </exception>
        public SeedSequence(params uint[] seeds)
            : this(seeds.AsEnumerable())
        {
        }

        /// <inheritdoc />
        public void Generate(uint[] buffer)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            unsafe
            {
                fixed (uint* bufferPin = buffer, seedsPin = this.entropy)
                {
                    GenerateImpl(bufferPin, (uint)buffer.Length, seedsPin, (uint)this.entropy.Length);
                }
            }
        }

        /// <inheritdoc />
        public void Generate(ulong[] buffer)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            unsafe
            {
                fixed (ulong* bufferPin = buffer)
                fixed (uint* seedsPin = this.entropy)
                {
                    GenerateImpl((uint*)bufferPin, (uint)buffer.Length * 2U, seedsPin, (uint)this.entropy.Length);
                }
            }
        }

        private static unsafe void GenerateImpl(uint* buffer, uint bufferLenght, uint* seeds, uint seedSize)
        {
            unchecked
            {
                for (var i = 0U; i < bufferLenght; ++i)
                {
                    buffer[i] = 0x8b8b8b8bu;
                }

                var t = bufferLenght >= 623U ? 11U
                    : bufferLenght >= 68U ? 7U
                    : bufferLenght >= 39U ? 5U
                    : bufferLenght >= 7U ? 3U
                    : (bufferLenght - 1U) / 2U;
                var p = (bufferLenght - t) / 2U;
                var q = p + t;

                // k == 0
                var a = buffer[(0U - 1U) % bufferLenght] ^ buffer[0U] ^ buffer[p % bufferLenght];
                var r1 = (a ^ (a >> 27)) * 1664525U;
                var r2 = r1 + seedSize;
                buffer[p % bufferLenght] += r1;
                buffer[q % bufferLenght] += r2;
                buffer[0U] = r2;

                // k <= seedSize
                var k = 1u;
                for (; k <= seedSize; ++k)
                {
                    a = buffer[(k - 1U) % bufferLenght] ^ buffer[k % bufferLenght] ^ buffer[(k + p) % bufferLenght];
                    r1 = (a ^ (a >> 27)) * 1664525U;
                    r2 = r1 + k % bufferLenght + seeds[k - 1U];
                    buffer[(k + p) % bufferLenght] += r1;
                    buffer[(k + q) % bufferLenght] += r2;
                    buffer[k % bufferLenght] = r2;
                }

                // k < maxSize
                var m = Math.Max(seedSize + 1U, bufferLenght);
                for (; k < m; ++k)
                {
                    a = buffer[(k - 1U) % bufferLenght] ^ buffer[k % bufferLenght] ^ buffer[(k + p) % bufferLenght];
                    r1 = (a ^ (a >> 27)) * 1664525U;
                    r2 = r1 + k % bufferLenght;
                    buffer[(k + p) % bufferLenght] += r1;
                    buffer[(k + q) % bufferLenght] += r2;
                    buffer[k % bufferLenght] = r2;
                }

                // k < m + resultSize
                m = m + bufferLenght;
                for (; k < m; ++k)
                {
                    a = buffer[(k - 1U) % bufferLenght] + buffer[k % bufferLenght] + buffer[(k + p) % bufferLenght];
                    r1 = (a ^ (a >> 27)) * 1566083941U;
                    r2 = r1 - k % bufferLenght;
                    buffer[(k + p) % bufferLenght] ^= r1;
                    buffer[(k + q) % bufferLenght] ^= r2;
                    buffer[k % bufferLenght] = r2;
                }
            }
        }
    }
}
