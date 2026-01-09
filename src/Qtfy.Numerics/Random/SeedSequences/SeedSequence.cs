// <copyright file="SeedSequence.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Random.SeedSequences;

/// <summary>
/// A seed sequence based on <see cref="uint"/> values that implements <see cref="ISeedSequence"/>.
/// </summary>
public sealed class SeedSequence : ISeedSequence
{
    private readonly uint[] entropy;

    /// <summary>
    /// Initializes a new instance of the <see cref="SeedSequence"/> class.
    /// </summary>
    /// <param name="seeds">
    /// The seed values used to construct the seed sequence.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="seeds"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="seeds"/> is empty.
    /// </exception>
    public SeedSequence(IEnumerable<uint> seeds)
    {
        ArgumentNullException.ThrowIfNull(seeds);

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
    /// The seed values used to construct the seed sequence.
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
        ArgumentNullException.ThrowIfNull(buffer);
        if (buffer.Length == 0)
        {
            return;
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
        ArgumentNullException.ThrowIfNull(buffer);
        if (buffer.Length == 0)
        {
            return;
        }

        if (BitConverter.IsLittleEndian)
        {
            unsafe
            {
                fixed (ulong* bufferPin = buffer)
                fixed (uint* seedsPin = this.entropy)
                {
                    GenerateImpl((uint*)bufferPin, (uint)buffer.Length * 2U, seedsPin, (uint)this.entropy.Length);
                }
            }
        }
        else
        {
            var temp = new uint[buffer.Length * 2];
            unsafe
            {
                fixed (uint* tempPin = temp, seedsPin = this.entropy)
                {
                    GenerateImpl(tempPin, (uint)temp.Length, seedsPin, (uint)this.entropy.Length);
                }
            }

            for (var i = 0; i < temp.Length; i += 2)
            {
                var lower = temp[i];
                var upper = temp[i + 1];
                buffer[i / 2] = ((ulong)upper << 32) | lower;
            }
        }
    }

    private static unsafe void GenerateImpl(uint* buffer, uint bufferLength, uint* seeds, uint seedSize)
    {
        unchecked
        {
            for (var i = 0U; i < bufferLength; ++i)
            {
                buffer[i] = 0x8b8b8b8bu;
            }

            var t = bufferLength >= 623U ? 11U
                : bufferLength >= 68U ? 7U
                : bufferLength >= 39U ? 5U
                : bufferLength >= 7U ? 3U
                : (bufferLength - 1U) / 2U;
            var p = (bufferLength - t) / 2U;
            var q = p + t;

            // k == 0
            var a = buffer[(0U - 1U) % bufferLength] ^ buffer[0U] ^ buffer[p % bufferLength];
            var r1 = (a ^ (a >> 27)) * 1664525U;
            var r2 = r1 + seedSize;
            buffer[p % bufferLength] += r1;
            buffer[q % bufferLength] += r2;
            buffer[0U] = r2;

            // k <= seedSize
            var k = 1u;
            for (; k <= seedSize; ++k)
            {
                a = buffer[(k - 1U) % bufferLength] ^ buffer[k % bufferLength] ^ buffer[(k + p) % bufferLength];
                r1 = (a ^ (a >> 27)) * 1664525U;
                r2 = r1 + k % bufferLength + seeds[k - 1U];
                buffer[(k + p) % bufferLength] += r1;
                buffer[(k + q) % bufferLength] += r2;
                buffer[k % bufferLength] = r2;
            }

            // k < maxSize
            var m = Math.Max(seedSize + 1U, bufferLength);
            for (; k < m; ++k)
            {
                a = buffer[(k - 1U) % bufferLength] ^ buffer[k % bufferLength] ^ buffer[(k + p) % bufferLength];
                r1 = (a ^ (a >> 27)) * 1664525U;
                r2 = r1 + k % bufferLength;
                buffer[(k + p) % bufferLength] += r1;
                buffer[(k + q) % bufferLength] += r2;
                buffer[k % bufferLength] = r2;
            }

            // k < m + resultSize
            m = m + bufferLength;
            for (; k < m; ++k)
            {
                a = buffer[(k - 1U) % bufferLength] + buffer[k % bufferLength] + buffer[(k + p) % bufferLength];
                r1 = (a ^ (a >> 27)) * 1566083941U;
                r2 = r1 - k % bufferLength;
                buffer[(k + p) % bufferLength] ^= r1;
                buffer[(k + q) % bufferLength] ^= r2;
                buffer[k % bufferLength] = r2;
            }
        }
    }
}
