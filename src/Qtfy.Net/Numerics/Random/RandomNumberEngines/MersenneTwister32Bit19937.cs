// <copyright file="MersenneTwister32Bit19937.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

/*
Original code's copyright and license:
Copyright (C) 1997 - 2002, Makoto Matsumoto and Takuji Nishimura,
All rights reserved.
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
 1. Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.
 2. Redistributions in binary form must reproduce the above copyright
    notice, this list of conditions and the following disclaimer in the
    documentation and/or other materials provided with the distribution.
 3. The names of its contributors may not be used to endorse or promote
    products derived from this software without specific prior written
    permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
Any feedback is very welcome.
http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/emt.html
email: m-mat @ math.sci.hiroshima-u.ac.jp (remove space)
*/

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    using System;

    /// <summary>
    /// The Mersenne Twister random number generator.
    /// <see href="http://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/emt19937ar.html" />.
    /// </summary>
    public sealed class MersenneTwister32Bit19937 : UIntRandomNumberEngine
    {
        private const int N = 624;

        private readonly uint[] state;

        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister32Bit19937"/> class.
        /// </summary>
        /// <param name="state">
        /// The initial state.
        /// </param>
        /// <param name="index">
        /// The initial index.
        /// </param>
        private MersenneTwister32Bit19937(uint[] state, int index)
        {
            this.state = state;
            this.index = index;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister32Bit19937"/> class.
        /// </summary>
        /// <param name="seedSequence">
        /// The seed source.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="seedSequence"/> is null.
        /// </exception>
        public MersenneTwister32Bit19937(ISeedSequence seedSequence)
        {
            if (seedSequence is null)
            {
                throw new ArgumentNullException(nameof(seedSequence));
            }

            var temp = new uint[N];
            seedSequence.Generate(temp);
            this.state = temp;
            this.index = N;
        }

        /// <inheritdoc />
        public override uint NextUInt()
        {
            unchecked
            {
                if (this.index == N)
                {
                    this.UpdateState();
                }

                var y = this.state[this.index++];
                y ^= y >> 11;
                y ^= (y << 7) & 0x9d2c5680U;
                y ^= (y << 15) & 0xefc60000U;
                return y ^ (y >> 18);
            }
        }

        /// <summary>
        /// The implementation of <see cref="UpdateState"/>.
        /// </summary>
        /// <param name="mt">
        /// A pointer to the first element in the state. The name mt is retained from the original c code.
        /// </param>
        private static unsafe void UpdateStateImpl(uint* mt)
        {
            const int m = 397;
            const uint matrixA = 0x9908b0dfU;
            const uint upperMask = 0x80000000U;
            const uint lowerMask = 0x7fffffffU;
            unchecked
            {
                var p0 = mt;
                var p1 = mt + 1U;
                var p2 = mt + m;
                var end = mt + N;
                uint y;

                do
                {
                    y = (*p0 & upperMask) | (*p1 & lowerMask);
                    *p0 = *p2 ^ (y >> 1) ^ ((y & 0x1U) * matrixA);
                    ++p0;
                    ++p1;
                    ++p2;
                }
                while (p2 != end);

                p2 = mt;

                do
                {
                    y = (*p0 & upperMask) | (*p1 & lowerMask);
                    *p0 = *p2 ^ (y >> 1) ^ ((y & 0x1U) * matrixA);
                    ++p0;
                    ++p1;
                    ++p2;
                }
                while (p1 != end);

                y = (*p0 & upperMask) | (*mt & lowerMask);
                *p0 = *p2 ^ (y >> 1) ^ ((y & 0x1U) * matrixA);
            }
        }

        /// <summary>
        /// Advances the state by recalculating it. This is used when the end of the state has been reached in
        /// order to update state values.
        /// </summary>
        private void UpdateState()
        {
            unsafe
            {
                fixed (uint* s = this.state)
                {
                    UpdateStateImpl(s);
                    this.index = 0;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister32Bit19937"/> class.
        /// </summary>
        /// <param name="seed">
        /// The seed used to seed the initial state.
        /// </param>
        /// <returns>
        /// A new instance of a Mersenne Twister PRNG.
        /// </returns>
        /// <remarks>
        /// This method uses the initialization procedure called init_by_array in the original
        /// c code.
        /// <see href="http://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/CODES/mt19937ar.c" />.
        /// </remarks>
        public static MersenneTwister32Bit19937 InitGenRand(uint seed)
        {
            unsafe
            {
                var state = new uint[N];
                fixed (uint* mt = state)
                {
                    InitGenRandImpl(mt, seed);
                }

                return new MersenneTwister32Bit19937(state, N);
            }
        }

        /// <summary>
        /// Private backend to the constructor function, i.e. the actions on the
        /// generator's state are performed here.
        /// </summary>
        /// <param name="mt">
        /// The generator state.
        /// </param>
        /// <param name="seed">
        /// The seed.
        /// </param>
        private static unsafe void InitGenRandImpl(uint* mt, uint seed)
        {
            unchecked
            {
                mt[0] = seed;
                var t = seed;
                for (uint mti = 1; mti < N; mti++)
                {
                    t = 1812433253U * (t ^ (t >> 30)) + mti;
                    mt[mti] = t;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister32Bit19937"/> class.
        /// </summary>
        /// <param name="seeds">
        /// The seeds used to seed the initial state.
        /// </param>
        /// <returns>
        /// A new instance of a Mersenne Twister PRNG.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="seeds"/> is null.
        /// </exception>
        /// <remarks>
        /// This method uses the initialization procedure called init_by_array in the original
        /// c code.
        /// <see href="http://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/CODES/mt19937ar.c" />.
        /// </remarks>
        public static MersenneTwister32Bit19937 InitByArray(uint[] seeds)
        {
            if (seeds is null)
            {
                throw new ArgumentNullException(nameof(seeds));
            }

            var state = new uint[N];
            unsafe
            {
                fixed (uint* mt = state, initKey = seeds)
                {
                    InitByArrayImpl(mt, initKey, (uint)seeds.Length);
                }
            }

            return new MersenneTwister32Bit19937(state, N);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister32Bit19937"/> class,
        /// i.e. private backend to the function before.
        /// </summary>
        /// <param name="mt">
        /// The generator's state.
        /// </param>
        /// <param name="initKey">
        /// The initial key.
        /// </param>
        /// <param name="keyLength">
        /// The key length.
        /// </param>
        private static unsafe void InitByArrayImpl(uint* mt, uint* initKey, uint keyLength)
        {
            unchecked
            {
                const uint mostSignificantBit = 0x1U << 31;
                InitGenRandImpl(mt, 19650218U);
                var i = 1U;
                var j = 0U;
                var k = Math.Max(keyLength, N);
                for (; k != 0; --k)
                {
                    mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1664525U)) + initKey[j] + j;
                    if (++i >= N)
                    {
                        mt[0] = mt[N - 1];
                        i = 1;
                    }

                    if (++j >= keyLength)
                    {
                        j = 0;
                    }
                }

                for (k = N - 1; k != 0; k--)
                {
                    mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 30)) * 1566083941U)) - i;
                    if (++i >= N)
                    {
                        mt[0] = mt[N - 1];
                        i = 1;
                    }
                }

                mt[0] = mostSignificantBit;
            }
        }
    }
}
