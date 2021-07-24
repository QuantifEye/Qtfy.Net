// <copyright file="BigRational.Conversions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    using System;
    using System.Diagnostics;
    using System.Numerics;

    /// <summary>
    /// A structure that represents a rational number with an arbitrarily large numerator and denominator.
    /// </summary>
    public partial struct BigRational
    {
        /// <summary>
        /// The greatest value a <see cref="decimal"/> value can have as a <see cref="BigInteger"/>.
        /// </summary>
        private static readonly BigInteger DecimalMax = (BigInteger)decimal.MaxValue;

        /// <summary>
        /// The smallest value a <see cref="decimal"/> value can have as a <see cref="BigInteger"/>.
        /// </summary>
        private static readonly BigInteger DecimalMin = (BigInteger)decimal.MinValue;

        /// <summary>
        /// An array of rationals such that array[i] == Pow(2, -i).
        /// </summary>
        /// <remarks>
        /// A cache of the powers of two needed to convert a floating point number to a
        /// <see cref="BigRational"/>.
        /// </remarks>
        private static readonly BigRational[] NegativePowerOfTwo = BuildNegativePowersOfTwo();

        /// <summary>
        /// Converts a <see cref="BigRational"/> to a <see cref="double"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="BigRational"/> to convert.
        /// </param>
        /// <returns>
        /// The value of the provided <see cref="BigRational"/> converted to a <see cref="double"/>.
        /// </returns>
        /// <remarks>
        /// Converts a <see cref="BigRational"/> to a <see cref="double"/>. If the value is half way between two
        /// prospective double values, the value is rounded to the even value (Bankers Rounding).
        /// </remarks>
        public static explicit operator double(BigRational value)
        {
            const int maxExp = 1023;
            const int minExp = -1022;
            const int exponentBits = 11;
            const int fractionBits = 52;
            const long fractionBitsMask = (1L << fractionBits) - 1L;
            const int extraBits = 8;
            const int extraBitsMask = (1 << extraBits) - 1;

            int sign = value.Sign;

            if (value.IsZero)
            {
                return 0d;
            }

            var a = BigInteger.Abs(value.Numerator);
            var b = value.Denominator;
            var aBits = a.GetBitLength();
            var bBits = b.GetBitLength();

            if (aBits - bBits > maxExp)
            {
                return sign * double.PositiveInfinity;
            }

            if (aBits - bBits < minExp)
            {
                return 0d / sign;
            }

            int shift = (int)(aBits - bBits) - fractionBits - extraBits;
            var x = (long)((shift <= 0 ? a << -shift : a >> shift) / b);
            long extra = x & extraBitsMask;
            x = (x >> extraBits) - (1L << fractionBits);
            Debug.Assert(x <= fractionBitsMask, "1 <= x 2 ^ -52 < 2");

            if ((extra >> (extraBits - 1) == 1) && ((extra > (1 << (extraBits - 1))) || ((x & 1) == 1)))
            {
                ++x;
            }

            if (x >> fractionBits != 0)
            {
                ++aBits;
                if (aBits - bBits > maxExp)
                {
                    return sign * double.PositiveInfinity;
                }
            }

            long dbl = x | ((aBits - bBits - minExp + 1) << fractionBits);
            if (sign == -1)
            {
                dbl |= 1L << (fractionBits + exponentBits);
            }

            return BitConverter.Int64BitsToDouble(dbl);
        }

        /// <summary>
        /// Converts a <see cref="BigRational"/> to a <see cref="float"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="BigRational"/> to convert.
        /// </param>
        /// <returns>
        /// The value of the provided <see cref="BigRational"/> converted to a float.
        /// </returns>
        /// <remarks>
        /// The implementation relies on the implementation of the conversion operator that converts
        /// a <see cref="BigRational"/> to a double.
        /// </remarks>
        public static explicit operator float(BigRational value)
        {
            return (float)(double)value;
        }

        /// <summary>
        /// Converts a <see cref="double"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="d">
        /// The <see cref="double"/> to convert.
        /// </param>
        public static implicit operator BigRational(double d)
        {
            ulong bits;
            unsafe
            {
                bits = *(ulong*)&d;
            }

            var mantissa = new BigRational(BigInteger.One);
            for (var i = 1; i != 53; ++i)
            {
                if ((bits & (0x1UL << (52 - i))) != 0)
                {
                    mantissa += NegativePowerOfTwo[i];
                }
            }

            return d.CompareTo(0) * mantissa * Pow(2, (int)((bits >> 52) & 0b0000_0111_1111_1111UL) - 1023);
        }

        /// <summary>
        /// Converts a <see cref="float"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="d">
        /// The <see cref="float"/> to convert.
        /// </param>
        public static implicit operator BigRational(float d)
        {
            return (double)d;
        }

        /// <summary>
        /// Converts a <see cref="decimal"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="decimal"/> to convert.
        /// </param>
        public static implicit operator BigRational(decimal value)
        {
            // there must be a better way than this.
            static byte[] GetBytes(int[] nums)
            {
                var bytes = new byte[16];
                for (var i = 0; i < 4;)
                {
                    var temp = BitConverter.GetBytes(nums[i]);
                    for (var j = 0; j < 4; i++, j++)
                    {
                        bytes[i] = temp[j];
                    }
                }

                return bytes;
            }

            var intArr = decimal.GetBits(value);
            var intPart = new int[3];
            Array.Copy(intArr, intPart, 3);
            var num = new BigInteger(GetBytes(intPart));
            var den = BigInteger.Pow(10, (intArr[3] >> 16) & 0x000000FF);
            return new BigRational(value >= 0 ? num : -num, den);
        }

        /// <summary>
        /// Converts a <see cref="BigRational"/> to a <see cref="decimal"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="BigRational"/> to convert.
        /// </param>
        /// <exception cref="OverflowException">
        /// A <see cref="OverflowException"/> is raised if the <paramref name="value"/>
        /// is not in the valid range of a <see cref="decimal"/>.
        /// </exception>
        public static explicit operator decimal(BigRational value)
        {
            if (value < DecimalMin || value > DecimalMax)
            {
                throw new OverflowException("Value outside of range of valid decimal values.");
            }

            if (value.IsInteger)
            {
                return (decimal)value.Numerator;
            }

            var rationalWhole = RoundTowardZeroImpl(value);
            var rationalScale = BigInteger.Pow(10, 28 - Digits(rationalWhole));
            var scaledFraction = (value - rationalWhole) * rationalScale;
            var scaledRounded = RoundToInt(scaledFraction, MidpointRoundingMode.ToEven);

            return ((decimal)scaledRounded / (decimal)rationalScale) + (decimal)rationalWhole;

            static int Digits(BigInteger number)
            {
                if (number.IsZero)
                {
                    return 0;
                }

                number = BigInteger.Abs(number);
                var count = 0;
                var ten = new BigInteger(10);
                while (number > 1)
                {
                    ++count;
                    number /= ten;
                }

                return count;
            }
        }

        /// <summary>
        /// Converts a <see cref="BigInteger"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="BigInteger"/> to convert.
        /// </param>
        public static implicit operator BigRational(BigInteger value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="ulong"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="ulong"/> to convert.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator BigRational(ulong value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="long"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="long"/> to convert.
        /// </param>
        public static implicit operator BigRational(long value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="uint"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="uint"/> to convert.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator BigRational(uint value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="int"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="int"/> to convert.
        /// </param>
        public static implicit operator BigRational(int value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="ushort"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="ushort"/> to convert.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator BigRational(ushort value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="short"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="short"/> to convert.
        /// </param>
        public static implicit operator BigRational(short value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="byte"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="byte"/> to convert.
        /// </param>
        public static implicit operator BigRational(byte value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Converts a <see cref="sbyte"/> to a <see cref="BigRational"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="sbyte"/> to convert.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator BigRational(sbyte value)
        {
            return new BigRational(value);
        }

        /// <summary>
        /// Builds a cache of the negative powers of 2.
        /// </summary>
        /// <returns>
        /// An array of rationals such that array[i] == Pow(2, -i).
        /// </returns>
        private static BigRational[] BuildNegativePowersOfTwo()
        {
            var array = new BigRational[53];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = Pow(2, -i);
            }

            return array;
        }
    }
}
