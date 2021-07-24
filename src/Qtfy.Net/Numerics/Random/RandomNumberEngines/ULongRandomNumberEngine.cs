// <copyright file="ULongRandomNumberEngine.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.RandomNumberEngines
{
    using System;

    /// <summary>
    /// A base class for all random bit generators that generate uniformly distributed values.
    /// </summary>
    public abstract class ULongRandomNumberEngine : IRandomNumberEngine
    {
        /// <inheritdoc />
        public abstract ulong NextULong();

        /// <inheritdoc />
        public uint NextUInt()
        {
            unchecked
            {
                const ulong range = uint.MaxValue + 1UL;
                const ulong scaling = ulong.MaxValue / range;
                const ulong last = range * scaling;
                ulong result;
                do
                {
                    result = this.NextULong();
                }
                while (result >= last);

                return (uint)(result / scaling);
            }
        }

        private ulong NextULongImpl(ulong maxExclusive)
        {
            unchecked
            {
                var scaling = ulong.MaxValue / maxExclusive;
                var last = maxExclusive * scaling;
                ulong result;
                do
                {
                    result = this.NextULong();
                }
                while (result >= last);

                return result / scaling;
            }
        }

        /// <inheritdoc />
        public ulong NextULong(ulong max)
        {
            unchecked
            {
                return max == ulong.MaxValue
                    ? this.NextULong()
                    : this.NextULongImpl(max + 1UL);
            }
        }

        /// <inheritdoc />
        public double NextCanonical()
        {
            return RandomFunctions.Canonical(this.NextULong());
        }

        /// <inheritdoc />
        public double NextIncrementedCanonical()
        {
            return RandomFunctions.IncrementedCanonical(this.NextULong());
        }

        /// <inheritdoc />
        public double NextStandardUniform()
        {
            unchecked
            {
                const ulong maxInclusive = 1UL << 53;
                const ulong maxExclusive = maxInclusive + 1UL;
                const ulong scaling = ulong.MaxValue / maxExclusive;
                const ulong last = maxExclusive * scaling;
                ulong result;
                do
                {
                    result = this.NextULong();
                }
                while (result >= last);

                return Math.ScaleB(result / scaling, -53);
            }
        }

        /// <inheritdoc/>
        public uint NextUInt(uint max)
        {
            unchecked
            {
                return (uint)this.NextULongImpl(max + 1UL);
            }
        }
    }
}
