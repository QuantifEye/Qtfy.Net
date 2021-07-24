// <copyright file="MersenneTwister64Bit19937Tests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

// The following comment relates to the code found in the methods
//  - MersenneTwister64Bit19937Tests.Original
//  - MersenneTwister64Bit19937Tests.OriginalInitGenRand
//  - MersenneTwister64Bit19937Tests.OriginalInitByArray

/*
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
 email: m-mat @ math.sci.hiroshima-u.ac.jp (remove spaces) */

namespace Qtfy.Net.Numerics.Tests.Random.RandomNumberEngines
{
    using System;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;

    public class MersenneTwister64Bit19937Tests
    {
        private enum SeedMethod
        {
            InitGenRand,
            InitByArray,
        }

        [TestCase(1234U)]
        public void InitGenRandTest(uint seed)
        {
            // the number of random numbers to test
            // This is chosen so that the state is cycled twice.
            const int size = 1400;

            var generator = MersenneTwister64Bit19937.InitGenRand(seed);
            Assert.AreEqual(
                OriginalInitGenRand(seed, size),
                GetRandomValues(generator, size));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(622)]
        [TestCase(623)]
        [TestCase(624)]
        [TestCase(625)]
        [TestCase(626)]
        public void InitByArrayTest(int seedSize)
        {
            // the number of random numbers to test
            // This is chosen so that the state is cycled twice.
            const int size = 1400;

            var seeds = OriginalInitGenRand(1, seedSize);
            var generator = MersenneTwister64Bit19937.InitByArray(seeds);
            var actual = GetRandomValues(generator, size);
            var expected = OriginalInitByArray(seeds, size);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void InitByArrayNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => MersenneTwister64Bit19937.InitByArray(null));
        }

        [Test]
        public void TestConstructWithSeedSequence()
        {
        }

        [Test]
        public void TestConstructWithNullSeedSequence()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = new MersenneTwister64Bit19937(null));
        }

        private static ulong[] GetRandomValues(MersenneTwister64Bit19937 generator, int size)
        {
            var actual = new ulong[size];
            for (var i = 0; i < actual.Length; ++i)
            {
                actual[i] = generator.NextULong();
            }

            return actual;
        }

        private static ulong[] OriginalInitGenRand(ulong seed, int outputSize)
        {
            return Original(
                outputSize,
                seed,
                method: SeedMethod.InitGenRand);
        }

        private static ulong[] OriginalInitByArray(ulong[] seeds, int outputSize)
        {
            return Original(
                outputSize,
                seeds: seeds,
                method: SeedMethod.InitByArray);
        }

#pragma warning disable
        /// <summary>
        /// Generates an array of generated numbers with the provided output size.
        /// if SeedMethod is SeedMethod.InitGenRand, the seed parameter must also be provided.
        /// if SeedMethod is SeedMethod.InitByArray, the seeds parameter must also be provided.
        /// </summary>
        /// <remarks>
        /// This code in this method is taken from the c code published by the authors of the mersenne twister.
        /// It is edited as little as possible, (just enough to make it work in c#). Hence the warning disable pragma
        /// It is intended to test the c# implementation of the generator.
        /// </remarks>
        private static ulong[] Original(
            int outputSize,
            ulong seed = default,
            ulong[] seeds = default,
            SeedMethod method = default)
        {
            /*
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
             email: m-mat @ math.sci.hiroshima-u.ac.jp (remove spaces) */

            const int NN = 312;
            const int MM = 156;
            const ulong MATRIX_A = 0xB5026F5AA96619E9UL;
            const ulong UM = 0xFFFFFFFF80000000UL; /* Most significant 33 bits */
            const ulong LM = 0x7FFFFFFFUL; /* Least significant 31 bits */

            /* The array for the state vector */
            var mt = new ulong[NN];

            /* mti==NN+1 means mt[NN] is not initialized */
            ulong mti = NN + 1;

            /* initializes mt[NN] with a seed */
            void init_genrand64(ulong seed)
            {
                mt[0] = seed;
                for (mti = 1; mti < NN; mti++)
                    mt[mti] = 6364136223846793005UL * (mt[mti - 1] ^ (mt[mti - 1] >> 62)) + mti;
            }

            /* initialize by an array with array-length */
            /* init_key is the array for initializing keys */
            /* key_length is its length */
            void init_by_array64(ulong[] init_key, ulong key_length)
            {
                ulong i, j, k;
                init_genrand64(19650218UL);
                i = 1;
                j = 0;
                k = NN > key_length ? NN : key_length;
                for (; k != 0; k--)
                {
                    mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 62)) * 3935559000370003845UL))
                            + init_key[j] + j; /* non linear */
                    i++;
                    j++;
                    if (i >= NN)
                    {
                        mt[0] = mt[NN - 1];
                        i = 1;
                    }

                    if (j >= key_length) j = 0;
                }

                for (k = NN - 1; k != 0; k--)
                {
                    mt[i] = (mt[i] ^ ((mt[i - 1] ^ (mt[i - 1] >> 62)) * 2862933555777941757UL))
                            - i; /* non linear */
                    i++;
                    if (i >= NN)
                    {
                        mt[0] = mt[NN - 1];
                        i = 1;
                    }
                }

                mt[0] = 1UL << 63; /* MSB is 1; assuring non-zero initial array */
            }

            /* generates a random number on [0, 2^64-1]-interval */
            ulong genrand64_int64()
            {
                int i;
                ulong x;
                ulong[] mag01 = { 0UL, MATRIX_A };

                if (mti >= NN)
                {
                    /* generate NN words at one time */

                    /* if init_genrand64() has not been called, */
                    /* a default initial seed is used     */
                    if (mti == NN + 1)
                        init_genrand64(5489UL);

                    for (i = 0; i < NN - MM; i++)
                    {
                        x = (mt[i] & UM) | (mt[i + 1] & LM);
                        mt[i] = mt[i + MM] ^ (x >> 1) ^ mag01[(int)(x & 1UL)];
                    }

                    for (; i < NN - 1; i++)
                    {
                        x = (mt[i] & UM) | (mt[i + 1] & LM);
                        mt[i] = mt[i + (MM - NN)] ^ (x >> 1) ^ mag01[(int)(x & 1UL)];
                    }

                    x = (mt[NN - 1] & UM) | (mt[0] & LM);
                    mt[NN - 1] = mt[MM - 1] ^ (x >> 1) ^ mag01[(int)(x & 1UL)];

                    mti = 0;
                }

                x = mt[mti++];

                x ^= (x >> 29) & 0x5555555555555555UL;
                x ^= (x << 17) & 0x71D67FFFEDA60000UL;
                x ^= (x << 37) & 0xFFF7EEE000000000UL;
                x ^= x >> 43;

                return x;
            }


            switch (method)
            {
                case SeedMethod.InitGenRand:
                    init_genrand64(seed);
                    break;
                case SeedMethod.InitByArray:
                    init_by_array64(seeds, (ulong)seeds.Length);
                    break;
            }

            var result = new ulong[outputSize];
            for (var i = 0; i < result.Length; ++i)
            {
                result[i] = genrand64_int64();
            }

            return result;
        }
#pragma warning restore
    }
}
