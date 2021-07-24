// <copyright file="UIntRandomNumberEngine.cs" company="QuantifEye">
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
    public abstract class UIntRandomNumberEngine : IRandomNumberEngine
    {
        /// <inheritdoc />
        public abstract uint NextUInt();

        /// <inheritdoc/>
        public ulong NextULong()
        {
            return ((ulong)this.NextUInt() << 32) + this.NextUInt();
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
            const ulong two32 = 1UL << 32;
            const ulong two53 = 1UL << 53;
            const uint upperMax = (uint)(two53 >> 32);
            const uint scaling = uint.MaxValue / (upperMax + 1U);
            const uint last = (upperMax + 1U) * scaling;
            ulong result;
            uint temp;
            do
            {
                do
                {
                    temp = this.NextUInt();
                }
                while (temp >= last);

                result = ((temp / scaling) * two32) + this.NextUInt();
            }
            while (result > two53);

            return Math.ScaleB(result, -53);
        }

        /// <inheritdoc />
        public ulong NextULong(ulong max)
        {
            ulong result;
            if (max == ulong.MaxValue)
            {
                result = this.NextULong();
            }
            else if (max <= uint.MaxValue)
            {
                result = this.NextUInt((uint)max);
            }
            else
            {
                var upperMax = (uint)(max >> 32);
                if (upperMax != uint.MaxValue)
                {
                    var range = upperMax + 1UL;
                    var scaling = uint.MaxValue / range;
                    var last = range * scaling;
                    ulong temp;
                    do
                    {
                        do
                        {
                            temp = this.NextUInt();
                        }
                        while (temp >= last);

                        result = ((temp / scaling) << 32) + this.NextUInt();
                    }
                    while (result > max);
                }
                else
                {
                    do
                    {
                        result = this.NextULong();
                    }
                    while (result > max);
                }
            }

            return result;
        }

        /// <inheritdoc />
        public uint NextUInt(uint max)
        {
            if (max == uint.MaxValue)
            {
                return this.NextUInt();
            }
            else
            {
                var range = max + 1u;
                var scaling = uint.MaxValue / range;
                var last = range * scaling;
                uint temp;
                do
                {
                    temp = this.NextUInt();
                }
                while (temp >= last);

                return temp / scaling;
            }
        }
    }
}
