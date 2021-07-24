// <copyright file="MersenneTwister64Bit19937.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

/*
Original code's copyright and license:
A C-program for MT19937-64 (2004/9/29 version).
Coded by Takuji Nishimura and Makoto Matsumoto.

This is a 64-bit version of Mersenne Twister pseudorandom number
generator.

Before using, initialize the state by using init_genrand64(seed)
or init_by_array64(init_key, key_length).

Copyright (C) 2004, Makoto Matsumoto and Takuji Nishimura,
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

References:
T. Nishimura, ``Tables of 64-bit Mersenne Twisters''
 ACM Transactions on Modeling and
 Computer Simulation 10. (2000) 348--357.
M. Matsumoto and T. Nishimura,
 ``Mersenne Twister: a 623-dimensionally equidistributed
   uniform pseudorandom number generator''
 ACM Transactions on Modeling and
 Computer Simulation 8. (Jan. 1998) 3--30.

Any feedback is very welcome.
http://www.math.hiroshima-u.ac.jp/~m-mat/MT/emt.html
email: m-mat @ math.sci.hiroshima-u.ac.jp (remove spaces)
*/

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    using System;

    /// <summary>
    /// The Mersenne Twister random number generator.
    /// <see href="http://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/emt19937ar.html" />.
    /// </summary>
    public sealed class MersenneTwister64Bit19937 : ULongRandomNumberEngine
    {
        private const int N = 312;

        private readonly ulong[] state;

        private int index;

        private MersenneTwister64Bit19937(ulong[] state, int index)
        {
            this.state = state;
            this.index = index;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister64Bit19937"/> class.
        /// </summary>
        /// <param name="seedSequence">
        /// The seed source.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="seedSequence"/> is null.
        /// </exception>
        public MersenneTwister64Bit19937(ISeedSequence seedSequence)
            : this(MakeState(seedSequence), N)
        {
        }

        private static ulong[] MakeState(ISeedSequence seedSequence)
        {
            if (seedSequence is null)
            {
                throw new ArgumentNullException(nameof(seedSequence));
            }

            var state = new ulong[N];
            seedSequence.Generate(state);
            return state;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MersenneTwister64Bit19937"/> class.
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
        public static MersenneTwister64Bit19937 InitGenRand(ulong seed)
        {
            unsafe
            {
                var state = new ulong[N];
                fixed (ulong* mt = state)
                {
                    InitGenRandImpl(mt, seed);
                }

                return new MersenneTwister64Bit19937(state, N);
            }
        }

        private static unsafe void InitGenRandImpl(ulong* mt, ulong seed)
        {
            unchecked
            {
                mt[0] = seed;
                for (ulong mti = 1; mti < N; mti++)
                {
                    var temp = mt[mti - 1];
                    mt[mti] = 6364136223846793005UL * (temp ^ (temp >> 62)) + mti;
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
        /// <remarks>
        /// This method uses the initialization procedure called init_by_array in the original
        /// c code.
        /// <see href="http://www.math.sci.hiroshima-u.ac.jp/m-mat/MT/MT2002/CODES/mt19937ar.c" />.
        /// </remarks>
        public static MersenneTwister64Bit19937 InitByArray(ulong[] seeds)
        {
            if (seeds is null)
            {
                throw new ArgumentNullException(nameof(seeds));
            }

            unsafe
            {
                var state = new ulong[N];
                fixed (ulong* mt = state, initKey = seeds)
                {
                    InitByArrayImpl(mt, initKey, (ulong)seeds.Length);
                }

                return new MersenneTwister64Bit19937(state, N);
            }
        }

        private static unsafe void InitByArrayImpl(ulong* mt, ulong* initKey, ulong keyLength)
        {
            unchecked
            {
                const ulong mostSignificantBit = 1UL << 63;
                InitGenRandImpl(mt, 19650218UL);
                var i = 1UL;
                var j = 0UL;
                var k = Math.Max(N, keyLength);
                for (; k != 0UL; --k)
                {
                    mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 62)) * 3935559000370003845UL)) + initKey[j] + j;
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

                for (k = N - 1UL; k != 0UL; --k)
                {
                    mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 62)) * 2862933555777941757UL)) - i;
                    if (++i >= N)
                    {
                        mt[0] = mt[N - 1];
                        i = 1;
                    }
                }

                mt[0] = mostSignificantBit;
            }
        }

        /// <inheritdoc />
        public override ulong NextULong()
        {
            unchecked
            {
                if (this.index == N)
                {
                    this.UpdateState();
                }

                var y = this.state[this.index++];
                y ^= (y >> 29) & 0x5555555555555555UL;
                y ^= (y << 17) & 0x71D67FFFEDA60000UL;
                y ^= (y << 37) & 0xFFF7EEE000000000UL;
                return y ^ (y >> 43);
            }
        }

        /// <summary>
        /// The implementation of <see cref="UpdateState"/>.
        /// </summary>
        /// <param name="mt">
        /// A pointer to the first element in the state. The name mt is retained from the original c code.
        /// </param>
        private static unsafe void UpdateStateImpl(ulong* mt)
        {
            const int m = 156;
            const ulong matrixA = 0xB5026F5AA96619E9UL;
            const ulong upperMask = 0xFFFFFFFF80000000UL;
            const ulong lowerMask = 0x7FFFFFFFUL;
            unchecked
            {
                var p0 = mt;
                var p1 = mt + 1;
                var p2 = mt + m;
                var end = mt + N;
                ulong y;

                do
                {
                    y = (*p0 & upperMask) | (*p1 & lowerMask);
                    *p0 = *p2 ^ (y >> 1) ^ ((y & 0x1UL) * matrixA);
                    ++p0;
                    ++p1;
                    ++p2;
                }
                while (p2 != end);

                p2 = mt;

                do
                {
                    y = (*p0 & upperMask) | (*p1 & lowerMask);
                    *p0 = *p2 ^ (y >> 1) ^ ((y & 0x1UL) * matrixA);
                    ++p0;
                    ++p1;
                    ++p2;
                }
                while (p1 != end);

                y = (*p0 & upperMask) | (*mt & lowerMask);
                *p0 = *p2 ^ (y >> 1) ^ ((y & 0x1UL) * matrixA);
            }
        }

        /// <summary>
        /// Advances the state by recalculating the state. This is used when the end of the state has been reached in
        /// order to update state values.
        /// </summary>
        private void UpdateState()
        {
            unsafe
            {
                fixed (ulong* mt = this.state)
                {
                    UpdateStateImpl(mt);
                    this.index = 0;
                }
            }
        }
    }
}
