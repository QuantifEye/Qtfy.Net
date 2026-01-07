// <copyright file="BigRational.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

using System;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

/// <summary>
/// A structure that represents a rational number with an arbitrarily large numerator and denominator.
/// </summary>
public partial struct BigRational :
    INumber<BigRational>,
    ISignedNumber<BigRational>,
    IEquatable<BigRational>,
    IEquatable<BigInteger>,
    IEquatable<decimal>,
    IEquatable<double>,
    IEquatable<float>,
    IEquatable<Half>,
    IEquatable<Int128>,
    IEquatable<UInt128>,
    IEquatable<nint>,
    IEquatable<nuint>,
    IEquatable<ulong>,
    IEquatable<long>,
    IEquatable<uint>,
    IEquatable<int>,
    IEquatable<ushort>,
    IEquatable<char>,
    IEquatable<short>,
    IEquatable<byte>,
    IEquatable<sbyte>,
    IComparable,
    IComparable<BigRational>,
    IComparable<BigInteger>,
    IComparable<decimal>,
    IComparable<double>,
    IComparable<float>,
    IComparable<Half>,
    IComparable<Int128>,
    IComparable<UInt128>,
    IComparable<nint>,
    IComparable<nuint>,
    IComparable<ulong>,
    IComparable<long>,
    IComparable<uint>,
    IComparable<int>,
    IComparable<ushort>,
    IComparable<char>,
    IComparable<short>,
    IComparable<byte>,
    IComparable<sbyte>
{
    /// <summary>
    /// The greatest value a <see cref="decimal"/> value can have as a <see cref="BigInteger"/>.
    /// </summary>
    private static readonly BigInteger DecimalMax = (BigInteger)decimal.MaxValue;

    /// <summary>
    /// The smallest value a <see cref="decimal"/> value can have as a <see cref="BigInteger"/>.
    /// </summary>
    private static readonly BigInteger DecimalMin = (BigInteger)decimal.MinValue;

    private static readonly BigInteger BigIntegerTwo = new (2);

    /// <summary>
    /// The denominator value of this <see cref="BigRational"/>.
    /// </summary>
    private readonly BigInteger denominator;

    /// <summary>
    /// The numerator value of this <see cref="BigRational"/>.
    /// </summary>
    private readonly BigInteger numerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BigRational"/> struct.
    /// </summary>
    /// <param name="numerator">
    /// The numerator.
    /// </param>
    /// <remarks>
    /// Sets the <see cref="Denominator"/> to <see cref="BigInteger.One"/>.
    /// </remarks>
    public BigRational(BigInteger numerator)
    {
        this.numerator = numerator;
        this.denominator = BigInteger.One;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BigRational"/> struct.
    /// </summary>
    /// <param name="numerator">
    /// The numerator.
    /// </param>
    /// <param name="denominator">
    /// The denominator.
    /// </param>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="denominator"/> is zero.
    /// </exception>
    public BigRational(BigInteger numerator, BigInteger denominator)
    {
        if (denominator.IsZero)
        {
            throw new DivideByZeroException("The denominator of a BigRational cannot be zero.");
        }

        if (numerator.IsZero)
        {
            this.numerator = BigInteger.Zero;
            this.denominator = BigInteger.One;
        }
        else
        {
            var gcd = denominator < BigInteger.Zero
                ? -BigInteger.GreatestCommonDivisor(numerator, denominator)
                : BigInteger.GreatestCommonDivisor(numerator, denominator);

            this.numerator = numerator / gcd;
            this.denominator = denominator / gcd;
        }
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational One { get; } = new BigRational(1);

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Zero { get; } = new BigRational(0);

    /// <inheritdoc cref="IAdditiveIdentity{BigRational, BigRational}" />
    public static BigRational AdditiveIdentity => Zero;

    /// <inheritdoc cref="IMultiplicativeIdentity{BigRational, BigRational}" />
    public static BigRational MultiplicativeIdentity => One;

    /// <inheritdoc cref="ISignedNumber{BigRational}" />
    public static BigRational NegativeOne { get; } = new BigRational(-1);

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static int Radix => 2;

    /// <summary>
    /// Gets the denominator of this <see cref="BigRational" />.
    /// </summary>
    /// <remarks>
    /// This is currently a computed property because c# does not provide a default constructor.
    /// This ensures that a default constructed <see cref="BigRational"/> is equal to (0 / 1).
    /// </remarks>
    public BigInteger Denominator
    {
        get => this.denominator.IsZero
            ? BigInteger.One
            : this.denominator;
    }

    /// <summary>
    /// Gets the numerator of this <see cref="BigRational"/>.
    /// </summary>
    public BigInteger Numerator
    {
        get => this.numerator;
    }

    /// <summary>
    /// Gets a number that indicates if <see cref="Numerator"/> is negative, positive, or zero.
    /// </summary>
    /// <returns>
    /// -1 if the value of the numerator is negative,
    /// 0 if the value of the numerator is zero,
    /// 1 if the value of the numerator is positive.
    /// </returns>
    public int Sign
    {
        get => this.Numerator.Sign;
    }

    /// <summary>
    /// Gets a value indicating whether the numerator is positive.
    /// </summary>
    public bool IsPositive
    {
        get => this.Numerator.Sign == 1;
    }

    /// <summary>
    /// Gets a value indicating whether the numerator is negative.
    /// </summary>
    public bool IsNegative
    {
        get => this.Numerator.Sign == -1;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to zero (0 / 1).
    /// </summary>
    public bool IsZero
    {
        get => this.Numerator.IsZero;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to one (1/1).
    /// </summary>
    public bool IsOne
    {
        get => this.Numerator.IsOne && this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to minus one (-1/1).
    /// </summary>
    public bool IsMinusOne
    {
        get => this.Numerator == BigInteger.MinusOne && this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> can be represented as an integer (x/1).
    /// </summary>
    public bool IsInteger
    {
        get => this.Denominator.IsOne;
    }

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
    /// Converts a <see cref="BigRational"/> to a <see cref="Half"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The value of the provided <see cref="BigRational"/> converted to a <see cref="Half"/>.
    /// </returns>
    /// <remarks>
    /// The implementation relies on the implementation of the conversion operator that converts
    /// a <see cref="BigRational"/> to a double.
    /// </remarks>
    public static explicit operator Half(BigRational value)
    {
        return (Half)(double)value;
    }

    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="d">
    /// The <see cref="double"/> to convert.
    /// </param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="d"/> is not finite.
    /// </exception>
    public static implicit operator BigRational(double d)
    {
        if (!double.IsFinite(d))
        {
            throw new ArgumentException("value must be finite", nameof(d));
        }

        if (d == 0d)
        {
            return Zero;
        }

        ulong bits;
        unsafe
        {
            bits = *(ulong*)&d;
        }

        var exponent = (int)((bits >> 52) & 0x7FFUL);
        var mantissa = bits & 0xFFFFFFFFFFFFFUL;

        BigRational magnitude;
        if (exponent == 0)
        {
            var denominator = BigInteger.One << 1074;
            magnitude = new BigRational(new BigInteger(mantissa), denominator);
        }
        else
        {
            var significand = new BigInteger(mantissa | (1UL << 52));
            var shift = exponent - 1075;
            if (shift >= 0)
            {
                magnitude = new BigRational(significand << shift, BigInteger.One);
            }
            else
            {
                magnitude = new BigRational(significand, BigInteger.One << -shift);
            }
        }

        return d < 0d ? -magnitude : magnitude;
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
    /// Converts a <see cref="Half"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="Half"/> to convert.
    /// </param>
    public static implicit operator BigRational(Half value)
    {
        return (double)value;
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
    /// Converts a <see cref="BigRational"/> to a <see cref="BigInteger"/> by truncating toward zero.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="BigInteger"/> value.
    /// </returns>
    public static explicit operator BigInteger(BigRational value)
    {
        return TruncateToBigInteger(value);
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="Int128"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="Int128"/> value.
    /// </returns>
    public static explicit operator Int128(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer < (BigInteger)Int128.MinValue)
        {
            return Int128.MinValue;
        }

        if (integer > (BigInteger)Int128.MaxValue)
        {
            return Int128.MaxValue;
        }

        return (Int128)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="UInt128"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="UInt128"/> value.
    /// </returns>
    [CLSCompliant(false)]
    public static explicit operator UInt128(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer.Sign < 0)
        {
            return 0;
        }

        if (integer > (BigInteger)UInt128.MaxValue)
        {
            return UInt128.MaxValue;
        }

        return (UInt128)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="nint"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="nint"/> value.
    /// </returns>
    public static explicit operator nint(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        var min = (long)nint.MinValue;
        var max = (long)nint.MaxValue;
        if (integer < min)
        {
            return (nint)min;
        }

        if (integer > max)
        {
            return (nint)max;
        }

        return (nint)(long)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="nuint"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="nuint"/> value.
    /// </returns>
    [CLSCompliant(false)]
    public static explicit operator nuint(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        var max = (ulong)nuint.MaxValue;
        if (integer.Sign < 0)
        {
            return 0;
        }

        if (integer > (BigInteger)max)
        {
            return (nuint)max;
        }

        return (nuint)(ulong)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="long"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="long"/> value.
    /// </returns>
    public static explicit operator long(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer < (BigInteger)long.MinValue)
        {
            return long.MinValue;
        }

        if (integer > (BigInteger)long.MaxValue)
        {
            return long.MaxValue;
        }

        return (long)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="ulong"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="ulong"/> value.
    /// </returns>
    [CLSCompliant(false)]
    public static explicit operator ulong(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer.Sign < 0)
        {
            return 0;
        }

        if (integer > (BigInteger)ulong.MaxValue)
        {
            return ulong.MaxValue;
        }

        return (ulong)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="int"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="int"/> value.
    /// </returns>
    public static explicit operator int(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer < (BigInteger)int.MinValue)
        {
            return int.MinValue;
        }

        if (integer > (BigInteger)int.MaxValue)
        {
            return int.MaxValue;
        }

        return (int)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="uint"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="uint"/> value.
    /// </returns>
    [CLSCompliant(false)]
    public static explicit operator uint(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer.Sign < 0)
        {
            return 0;
        }

        if (integer > (BigInteger)uint.MaxValue)
        {
            return uint.MaxValue;
        }

        return (uint)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="short"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="short"/> value.
    /// </returns>
    public static explicit operator short(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer < (BigInteger)short.MinValue)
        {
            return short.MinValue;
        }

        if (integer > (BigInteger)short.MaxValue)
        {
            return short.MaxValue;
        }

        return (short)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="ushort"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="ushort"/> value.
    /// </returns>
    [CLSCompliant(false)]
    public static explicit operator ushort(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer.Sign < 0)
        {
            return 0;
        }

        if (integer > (BigInteger)ushort.MaxValue)
        {
            return ushort.MaxValue;
        }

        return (ushort)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="byte"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="byte"/> value.
    /// </returns>
    public static explicit operator byte(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer.Sign < 0)
        {
            return 0;
        }

        if (integer > (BigInteger)byte.MaxValue)
        {
            return byte.MaxValue;
        }

        return (byte)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="sbyte"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="sbyte"/> value.
    /// </returns>
    [CLSCompliant(false)]
    public static explicit operator sbyte(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer < (BigInteger)sbyte.MinValue)
        {
            return sbyte.MinValue;
        }

        if (integer > (BigInteger)sbyte.MaxValue)
        {
            return sbyte.MaxValue;
        }

        return (sbyte)integer;
    }

    /// <summary>
    /// Converts a <see cref="BigRational"/> to a <see cref="char"/> by truncating toward zero and clamping to the destination range.
    /// </summary>
    /// <param name="value">
    /// The <see cref="BigRational"/> to convert.
    /// </param>
    /// <returns>
    /// The truncated <see cref="char"/> value.
    /// </returns>
    public static explicit operator char(BigRational value)
    {
        var integer = TruncateToBigInteger(value);
        if (integer.Sign < 0)
        {
            return '\0';
        }

        if (integer > (BigInteger)(int)char.MaxValue)
        {
            return char.MaxValue;
        }

        return (char)(ushort)integer;
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
    /// Converts a <see cref="Int128"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="Int128"/> to convert.
    /// </param>
    public static implicit operator BigRational(Int128 value)
    {
        return new BigRational((BigInteger)value);
    }

    /// <summary>
    /// Converts a <see cref="UInt128"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="UInt128"/> to convert.
    /// </param>
    [CLSCompliant(false)]
    public static implicit operator BigRational(UInt128 value)
    {
        return new BigRational((BigInteger)value);
    }

    /// <summary>
    /// Converts a <see cref="nint"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="nint"/> to convert.
    /// </param>
    public static implicit operator BigRational(nint value)
    {
        return new BigRational((long)value);
    }

    /// <summary>
    /// Converts a <see cref="nuint"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="nuint"/> to convert.
    /// </param>
    [CLSCompliant(false)]
    public static implicit operator BigRational(nuint value)
    {
        return new BigRational((ulong)value);
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
    /// Converts a <see cref="char"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="char"/> to convert.
    /// </param>
    public static implicit operator BigRational(char value)
    {
        return new BigRational((int)value);
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
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    public static bool operator ==(BigRational left, BigRational right)
    {
        return left.Numerator == right.Numerator && left.Denominator == right.Denominator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    public static bool operator ==(BigRational left, BigInteger right)
    {
        return left.IsInteger && left.Numerator == right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    public static bool operator ==(BigInteger left, BigRational right)
    {
        return right.IsInteger && left == right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator ==(BigRational left, ulong right)
    {
        return left.IsInteger && left.Numerator == right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator ==(ulong left, BigRational right)
    {
        return right.IsInteger && left == right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    public static bool operator ==(BigRational left, long right)
    {
        return left.IsInteger && left.Numerator == right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, false.
    /// </returns>
    public static bool operator ==(long left, BigRational right)
    {
        return right.IsInteger && left == right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    public static bool operator !=(BigRational left, BigRational right)
    {
        return left.Numerator != right.Numerator || left.Denominator != right.Denominator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    public static bool operator !=(BigRational left, BigInteger right)
    {
        return left.Denominator != BigInteger.One || left.Numerator != right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    public static bool operator !=(BigInteger left, BigRational right)
    {
        return right.Denominator != BigInteger.One || left != right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator !=(BigRational left, ulong right)
    {
        return left.Denominator != BigInteger.One || left.Numerator != right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator !=(ulong left, BigRational right)
    {
        return right.Denominator != BigInteger.One || left != right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    public static bool operator !=(BigRational left, long right)
    {
        return left.Denominator != BigInteger.One || left.Numerator != right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is unequal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// false if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, true.
    /// </returns>
    public static bool operator !=(long left, BigRational right)
    {
        return right.Denominator != BigInteger.One || left != right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >(BigRational left, BigRational right)
    {
        return left.Numerator * right.Denominator > right.Numerator * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >(BigRational left, BigInteger right)
    {
        return left.Numerator > right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >(BigInteger left, BigRational right)
    {
        return left * right.Denominator > right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator >(BigRational left, ulong right)
    {
        return left.Numerator > right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator >(ulong left, BigRational right)
    {
        return left * right.Denominator > right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >(BigRational left, long right)
    {
        return left.Numerator > right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >(long left, BigRational right)
    {
        return left * right.Denominator > right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <=(BigRational left, BigRational right)
    {
        return left.Numerator * right.Denominator <= right.Numerator * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <=(BigRational left, BigInteger right)
    {
        return left.Numerator <= right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <=(BigInteger left, BigRational right)
    {
        return left * right.Denominator <= right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator <=(BigRational left, ulong right)
    {
        return left.Numerator <= right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator <=(ulong left, BigRational right)
    {
        return left * right.Denominator <= right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <=(BigRational left, long right)
    {
        return left.Numerator <= right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <=(long left, BigRational right)
    {
        return left * right.Denominator <= right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <(BigRational left, BigRational right)
    {
        return left.Numerator * right.Denominator < right.Numerator * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <(BigRational left, BigInteger right)
    {
        return left.Numerator < right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator <(ulong left, BigRational right)
    {
        return left * right.Denominator < right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator <(BigRational left, ulong right)
    {
        return left.Numerator < right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <(long left, BigRational right)
    {
        return left * right.Denominator < right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <(BigRational left, long right)
    {
        return left.Numerator < right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is less than <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator <(BigInteger left, BigRational right)
    {
        return left * right.Denominator < right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >=(BigRational left, BigRational right)
    {
        return left.Numerator * right.Denominator >= right.Numerator * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >=(BigRational left, BigInteger right)
    {
        return left.Numerator >= right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >=(BigInteger left, BigRational right)
    {
        return left * right.Denominator >= right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator >=(BigRational left, ulong right)
    {
        return left.Numerator >= right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    [CLSCompliant(false)]
    public static bool operator >=(ulong left, BigRational right)
    {
        return left * right.Denominator >= right.Numerator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >=(BigRational left, long right)
    {
        return left.Numerator >= right * left.Denominator;
    }

    /// <summary>
    /// Returns an indication whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// true if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, false.
    /// </returns>
    public static bool operator >=(long left, BigRational right)
    {
        return left * right.Denominator >= right.Numerator;
    }

    /// <summary>
    /// Negates a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The value to negate.
    /// </param>
    /// <returns>
    /// The result of multiplying <paramref name="value"/> by negative one (-1).
    /// </returns>
    public static BigRational operator -(BigRational value)
    {
        return new BigRational(-value.Numerator, value.Denominator);
    }

    /// <summary>
    /// Returns the value of the <see cref="BigRational"/> operand. (The sign of the operand is unchanged.)
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <paramref name="value"/> operand.
    /// </returns>
    public static BigRational operator +(BigRational value)
    {
        return value;
    }

    /// <summary>
    /// Adds one to <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// The value to add one to.
    /// </param>
    /// <returns>
    /// The value of <paramref name="value"/> + (1 / 1).
    /// </returns>
    public static BigRational operator ++(BigRational value)
    {
        var denominator = value.Denominator;
        return new BigRational(value.Numerator + denominator, denominator);
    }

    /// <summary>
    /// Subtracts one from <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// The value to subtract one from.
    /// </param>
    /// <returns>
    /// The value of <paramref name="value"/> - (1 / 1).
    /// </returns>
    public static BigRational operator --(BigRational value)
    {
        var denominator = value.Denominator;
        return new BigRational(value.Numerator - denominator, denominator);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    public static BigRational operator +(BigRational augend, BigRational addend)
    {
        var leftDen = augend.Denominator;
        var rightDen = addend.Denominator;
        return new BigRational((augend.Numerator * rightDen) + (addend.Numerator * leftDen), leftDen * rightDen);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    public static BigRational operator +(BigRational augend, BigInteger addend)
    {
        var leftDen = augend.Denominator;
        return new BigRational(augend.Numerator + (addend * leftDen), leftDen);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    public static BigRational operator +(BigInteger augend, BigRational addend)
    {
        var rightDen = addend.Denominator;
        return new BigRational((augend * rightDen) + addend.Numerator, rightDen);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator +(BigRational augend, ulong addend)
    {
        var leftDen = augend.Denominator;
        return new BigRational(augend.Numerator + (addend * leftDen), leftDen);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator +(ulong augend, BigRational addend)
    {
        var rightDen = addend.Denominator;
        return new BigRational((augend * rightDen) + addend.Numerator, rightDen);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    public static BigRational operator +(BigRational augend, long addend)
    {
        var leftDen = augend.Denominator;
        return new BigRational(augend.Numerator + (addend * leftDen), leftDen);
    }

    /// <summary>
    /// Calculates the sum of the <paramref name="augend"/> and <paramref name="addend"/>.
    /// </summary>
    /// <param name="augend">
    /// The first number to add (the augend).
    /// </param>
    /// <param name="addend">
    /// The second number to add (the addend).
    /// </param>
    /// <returns>
    /// The sum of <paramref name="augend"/> and <paramref name="addend"/>.
    /// </returns>
    public static BigRational operator +(long augend, BigRational addend)
    {
        var rightDen = addend.Denominator;
        return new BigRational((augend * rightDen) + addend.Numerator, rightDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    public static BigRational operator -(BigRational minuend, BigRational subtrahend)
    {
        var leftDen = minuend.Denominator;
        var rightDen = subtrahend.Denominator;
        return new BigRational((minuend.Numerator * rightDen) - (subtrahend.Numerator * leftDen), leftDen * rightDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    public static BigRational operator -(BigRational minuend, BigInteger subtrahend)
    {
        var leftDen = minuend.Denominator;
        return new BigRational(minuend.Numerator - (subtrahend * leftDen), leftDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    public static BigRational operator -(BigInteger minuend, BigRational subtrahend)
    {
        var rightDen = subtrahend.Denominator;
        return new BigRational((minuend * rightDen) - subtrahend.Numerator, rightDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator -(BigRational minuend, ulong subtrahend)
    {
        var leftDen = minuend.Denominator;
        return new BigRational(minuend.Numerator - (subtrahend * leftDen), leftDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator -(ulong minuend, BigRational subtrahend)
    {
        var rightDen = subtrahend.Denominator;
        return new BigRational((minuend * rightDen) - subtrahend.Numerator, rightDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    public static BigRational operator -(BigRational minuend, long subtrahend)
    {
        var leftDen = minuend.Denominator;
        return new BigRational(minuend.Numerator - (subtrahend * leftDen), leftDen);
    }

    /// <summary>
    /// Subtracts a <paramref name="subtrahend"/> value from a <paramref name="minuend"/> value.
    /// </summary>
    /// <param name="minuend">
    /// The value to subtract from (the minuend).
    /// </param>
    /// <param name="subtrahend">
    /// The value to subtract (the subtrahend).
    /// </param>
    /// <returns>
    /// The result of subtracting <paramref name="subtrahend"/> from <paramref name="minuend"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator -(long minuend, BigRational subtrahend)
    {
        var rightDen = subtrahend.Denominator;
        return new BigRational((minuend * rightDen) - subtrahend.Numerator, rightDen);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator *(BigRational multiplicand, BigRational multiplier)
    {
        return new BigRational(
            multiplicand.Numerator * multiplier.Numerator,
            multiplicand.Denominator * multiplier.Denominator);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    public static BigRational operator *(BigRational multiplicand, BigInteger multiplier)
    {
        return new BigRational(multiplicand.Numerator * multiplier, multiplicand.Denominator);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    public static BigRational operator *(BigInteger multiplicand, BigRational multiplier)
    {
        return new BigRational(multiplicand * multiplier.Numerator, multiplier.Denominator);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator *(BigRational multiplicand, ulong multiplier)
    {
        return new BigRational(multiplicand.Numerator * multiplier, multiplicand.Denominator);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static BigRational operator *(ulong multiplicand, BigRational multiplier)
    {
        return new BigRational(multiplicand * multiplier.Numerator, multiplier.Denominator);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    public static BigRational operator *(BigRational multiplicand, long multiplier)
    {
        return new BigRational(multiplicand.Numerator * multiplier, multiplicand.Denominator);
    }

    /// <summary>
    /// Calculates the product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </summary>
    /// <param name="multiplicand">
    /// The first value to multiply.
    /// </param>
    /// <param name="multiplier">
    /// The second value to multiply.
    /// </param>
    /// <returns>
    /// The product of <paramref name="multiplicand"/> and <paramref name="multiplier"/>.
    /// </returns>
    public static BigRational operator *(long multiplicand, BigRational multiplier)
    {
        return new BigRational(multiplicand * multiplier.Numerator, multiplier.Denominator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    public static BigRational operator /(BigRational dividend, BigRational divisor)
    {
        return new BigRational(dividend.Numerator * divisor.Denominator, dividend.Denominator * divisor.Numerator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    public static BigRational operator /(BigRational dividend, BigInteger divisor)
    {
        return new BigRational(dividend.Numerator, dividend.Denominator * divisor);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    public static BigRational operator /(BigInteger dividend, BigRational divisor)
    {
        return new BigRational(dividend * divisor.Denominator, divisor.Numerator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    [CLSCompliant(false)]
    public static BigRational operator /(BigRational dividend, ulong divisor)
    {
        return new BigRational(dividend.Numerator, dividend.Denominator * divisor);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    [CLSCompliant(false)]
    public static BigRational operator /(ulong dividend, BigRational divisor)
    {
        return new BigRational(dividend * divisor.Denominator, divisor.Numerator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    public static BigRational operator /(BigRational dividend, long divisor)
    {
        return new BigRational(dividend.Numerator, dividend.Denominator * divisor);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by another <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is equal to zero (0/1).
    /// </exception>
    public static BigRational operator /(long dividend, BigRational divisor)
    {
        return new BigRational(dividend * divisor.Denominator, divisor.Numerator);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    public static BigRational operator %(BigRational dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    public static BigRational operator %(BigRational dividend, BigInteger divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    public static BigRational operator %(BigInteger dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    [CLSCompliant(false)]
    public static BigRational operator %(BigRational dividend, ulong divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    [CLSCompliant(false)]
    public static BigRational operator %(ulong dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    [CLSCompliant(false)]
    public static BigRational operator %(BigRational dividend, long divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero (1/0).
    /// </exception>
    public static BigRational operator %(long dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Abs(BigRational value)
    {
        return value.IsNegative ? -value : value;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    static int INumber<BigRational>.Sign(BigRational value)
    {
        return value.Sign;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational CopySign(BigRational value, BigRational sign)
    {
        return sign.IsNegative ? -Abs(value) : Abs(value);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsCanonical(BigRational value)
    {
        return true;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsComplexNumber(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsEvenInteger(BigRational value)
    {
        return value.IsInteger && value.Numerator.IsEven;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsFinite(BigRational value)
    {
        return true;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsImaginaryNumber(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsInfinity(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsNaN(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    static bool INumberBase<BigRational>.IsInteger(BigRational value)
    {
        return value.IsInteger;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    static bool INumberBase<BigRational>.IsNegative(BigRational value)
    {
        return value.IsNegative;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsNegativeInfinity(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsNormal(BigRational value)
    {
        return !value.IsZero;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsOddInteger(BigRational value)
    {
        return value.IsInteger && !value.Numerator.IsEven;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    static bool INumberBase<BigRational>.IsPositive(BigRational value)
    {
        return value.Sign >= 0;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsPositiveInfinity(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsRealNumber(BigRational value)
    {
        return true;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsSubnormal(BigRational value)
    {
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    static bool INumberBase<BigRational>.IsZero(BigRational value)
    {
        return value.IsZero;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational Max(BigRational left, BigRational right)
    {
        return left < right ? right : left;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational MaxNumber(BigRational left, BigRational right)
    {
        return Max(left, right);
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational MaxNative(BigRational left, BigRational right)
    {
        return Max(left, right);
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational Min(BigRational left, BigRational right)
    {
        return left > right ? right : left;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational MinNumber(BigRational left, BigRational right)
    {
        return Min(left, right);
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational MinNative(BigRational left, BigRational right)
    {
        return Min(left, right);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational MaxMagnitude(BigRational x, BigRational y)
    {
        return Abs(x) >= Abs(y) ? x : y;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational MaxMagnitudeNumber(BigRational x, BigRational y)
    {
        return MaxMagnitude(x, y);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational MinMagnitude(BigRational x, BigRational y)
    {
        return Abs(x) <= Abs(y) ? x : y;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational MinMagnitudeNumber(BigRational x, BigRational y)
    {
        return MinMagnitude(x, y);
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational Clamp(BigRational value, BigRational min, BigRational max)
    {
        if (min > max)
        {
            throw new ArgumentException("min must be less than or equal to max.", nameof(min));
        }

        if (value < min)
        {
            return min;
        }

        return value > max ? max : value;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational ClampNative(BigRational value, BigRational min, BigRational max)
    {
        return Clamp(value, min, max);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational MultiplyAddEstimate(BigRational left, BigRational right, BigRational addend)
    {
        return (left * right) + addend;
    }

    /// <summary>
    /// Raises a <see cref="BigRational"/> to an <see cref="int"/> power.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/>.
    /// </param>
    /// <param name="exp">
    /// The <see cref="int"/> exponent.
    /// </param>
    /// <returns>
    /// <paramref name="value"/> raised to the power <paramref name="exp"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="value"/> is zero and <paramref name="exp"/> is less than zero.
    /// </exception>
    public static BigRational Pow(BigRational value, int exp)
    {
        if (exp == 0)
        {
            return One;
        }

        if (value.IsZero)
        {
            if (exp < 0)
            {
                throw new DivideByZeroException("Cannot raise zero to a negative power.");
            }

            return Zero;
        }

        if (exp > 0)
        {
            return new BigRational(
                numerator: BigInteger.Pow(value.Numerator, exp),
                denominator: BigInteger.Pow(value.Denominator, exp));
        }

        exp = -exp;
        return new BigRational(
            numerator: BigInteger.Pow(value.Denominator, exp),
            denominator: BigInteger.Pow(value.Numerator, exp));
    }

    /// <summary>
    /// Converts the string representation of a number to its <see cref="BigRational"/> equivalent.
    /// </summary>
    /// <param name="value">
    /// A string that contains the number to convert.
    /// </param>
    /// <returns>
    /// A value that is equivalent to the number specified in the <paramref name="value"/> parameter.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="value"/> is null.
    /// </exception>
    /// <exception cref="FormatException">
    /// If <paramref name="value"/> cannot be interpreted as a <see cref="BigRational"/>.
    /// </exception>
    public static BigRational Parse(string value)
    {
        return Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture);
    }

    /// <inheritdoc cref="IParsable{BigRational}" />
    public static BigRational Parse(string s, IFormatProvider? provider)
    {
        return Parse(s, NumberStyles.Integer, provider);
    }

    /// <inheritdoc cref="ISpanParsable{BigRational}" />
    public static BigRational Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return Parse(s, NumberStyles.Integer, provider);
    }

    /// <inheritdoc cref="IUtf8SpanParsable{BigRational}" />
    public static BigRational Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider)
    {
        return Parse(utf8Text, NumberStyles.Integer, provider);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        ArgumentNullException.ThrowIfNull(s);

        if (TryParseCore(s.AsSpan(), style, provider, out var result))
        {
            return result;
        }

        throw new FormatException($"Could not parse \"{s}\" as a BigRational.");
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        if (TryParseCore(s, style, provider, out var result))
        {
            return result;
        }

        throw new FormatException("Could not parse value as a BigRational.");
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider)
    {
        var text = Encoding.UTF8.GetString(utf8Text);
        return Parse(text, style, provider);
    }

    /// <summary>
    /// Tries to convert the string representation of a number to its <see cref="BigRational"/> equivalent,
    /// and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number.
    /// </param>
    /// <param name="rational">
    /// When this method returns, contains the <see cref="BigRational"/> equivalent to
    /// the number that is contained in value, or zero (0) if the conversion fails.
    /// The conversion fails if the value <paramref name="value"/> is null or is not of the correct format.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// true if value was converted successfully; otherwise, false.
    /// </returns>
    public static bool TryParse(string value, out BigRational rational)
    {
        return TryParse(value, NumberStyles.Integer, null, out rational);
    }

    /// <inheritdoc cref="IParsable{BigRational}" />
    public static bool TryParse(string? s, IFormatProvider? provider, out BigRational result)
    {
        return TryParse(s, NumberStyles.Integer, provider, out result);
    }

    /// <inheritdoc cref="ISpanParsable{BigRational}" />
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out BigRational result)
    {
        return TryParse(s, NumberStyles.Integer, provider, out result);
    }

    /// <inheritdoc cref="IUtf8SpanParsable{BigRational}" />
    public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out BigRational result)
    {
        return TryParse(utf8Text, NumberStyles.Integer, provider, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out BigRational result)
    {
        if (s is null)
        {
            result = default;
            return false;
        }

        return TryParseCore(s.AsSpan(), style, provider, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out BigRational result)
    {
        return TryParseCore(s, style, provider, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out BigRational result)
    {
        if (utf8Text.IsEmpty)
        {
            result = default;
            return false;
        }

        var text = Encoding.UTF8.GetString(utf8Text);
        return TryParse(text, style, provider, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational CreateChecked<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (TryConvertFromChecked(value, out var result))
        {
            return result;
        }

        throw new NotSupportedException($"Cannot convert from {typeof(TOther)}.");
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational CreateSaturating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (TryConvertFromSaturating(value, out var result))
        {
            return result;
        }

        throw new NotSupportedException($"Cannot convert from {typeof(TOther)}.");
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational CreateTruncating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (TryConvertFromTruncating(value, out var result))
        {
            return result;
        }

        throw new NotSupportedException($"Cannot convert from {typeof(TOther)}.");
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertFromChecked<TOther>(TOther value, out BigRational result)
        where TOther : INumberBase<TOther>
    {
        if (value is BigRational rational)
        {
            result = rational;
            return true;
        }

        if (value is BigInteger bigInteger)
        {
            result = new BigRational(bigInteger);
            return true;
        }

        if (value is sbyte sbyteValue)
        {
            result = sbyteValue;
            return true;
        }

        if (value is byte byteValue)
        {
            result = byteValue;
            return true;
        }

        if (value is short shortValue)
        {
            result = shortValue;
            return true;
        }

        if (value is ushort ushortValue)
        {
            result = ushortValue;
            return true;
        }

        if (value is int intValue)
        {
            result = intValue;
            return true;
        }

        if (value is uint uintValue)
        {
            result = uintValue;
            return true;
        }

        if (value is long longValue)
        {
            result = longValue;
            return true;
        }

        if (value is ulong ulongValue)
        {
            result = ulongValue;
            return true;
        }

        if (value is Int128 int128Value)
        {
            result = new BigRational((BigInteger)int128Value);
            return true;
        }

        if (value is UInt128 uint128Value)
        {
            result = new BigRational((BigInteger)uint128Value);
            return true;
        }

        if (value is nint nintValue)
        {
            result = (long)nintValue;
            return true;
        }

        if (value is nuint nuintValue)
        {
            result = (ulong)nuintValue;
            return true;
        }

        if (value is char charValue)
        {
            result = charValue;
            return true;
        }

        if (value is float floatValue)
        {
            if (!float.IsFinite(floatValue))
            {
                throw new OverflowException("Value is not representable by BigRational.");
            }

            result = floatValue;
            return true;
        }

        if (value is double doubleValue)
        {
            if (!double.IsFinite(doubleValue))
            {
                throw new OverflowException("Value is not representable by BigRational.");
            }

            result = doubleValue;
            return true;
        }

        if (value is Half halfValue)
        {
            if (!Half.IsFinite(halfValue))
            {
                throw new OverflowException("Value is not representable by BigRational.");
            }

            result = (double)halfValue;
            return true;
        }

        if (value is decimal decimalValue)
        {
            result = decimalValue;
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertFromSaturating<TOther>(TOther value, out BigRational result)
        where TOther : INumberBase<TOther>
    {
        if (value is float floatValue && !float.IsFinite(floatValue))
        {
            result = default;
            return false;
        }

        if (value is double doubleValue && !double.IsFinite(doubleValue))
        {
            result = default;
            return false;
        }

        if (value is Half halfValue && !Half.IsFinite(halfValue))
        {
            result = default;
            return false;
        }

        return TryConvertFromChecked(value, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertFromTruncating<TOther>(TOther value, out BigRational result)
        where TOther : INumberBase<TOther>
    {
        return TryConvertFromSaturating(value, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertToChecked<TOther>(BigRational value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        result = default!;

        if (typeof(TOther) == typeof(BigRational))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            if (!value.IsInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            result = (TOther)(object)value.Numerator;
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            var floatValue = (float)value;
            if (!float.IsFinite(floatValue))
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)floatValue;
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            var doubleValue = (double)value;
            if (!double.IsFinite(doubleValue))
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)doubleValue;
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            var halfValue = (Half)(double)value;
            if (!Half.IsFinite(halfValue))
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)halfValue;
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            result = (TOther)(object)(decimal)value;
            return true;
        }

        var integer = value.Numerator;
        var isInteger = value.IsInteger;

        if (typeof(TOther) == typeof(sbyte))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < sbyte.MinValue || integer > sbyte.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(sbyte)integer;
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < byte.MinValue || integer > byte.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(byte)integer;
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < short.MinValue || integer > short.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(short)integer;
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < ushort.MinValue || integer > ushort.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(ushort)integer;
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < int.MinValue || integer > int.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(int)integer;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < uint.MinValue || integer > uint.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(uint)integer;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < long.MinValue || integer > long.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(long)integer;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < ulong.MinValue || integer > ulong.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(ulong)integer;
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < Int128.MinValue || integer > Int128.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(Int128)integer;
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < UInt128.MinValue || integer > UInt128.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(UInt128)integer;
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < nint.MinValue || integer > nint.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(nint)integer;
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < nuint.MinValue || integer > nuint.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(nuint)integer;
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            if (!isInteger)
            {
                throw new OverflowException("Value is not an integer.");
            }

            if (integer < char.MinValue || integer > char.MaxValue)
            {
                throw new OverflowException("Value is outside the range of the destination type.");
            }

            result = (TOther)(object)(char)integer;
            return true;
        }

        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertToSaturating<TOther>(BigRational value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        result = default!;

        if (typeof(TOther) == typeof(BigRational))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            result = (TOther)(object)TruncateToBigInteger(value);
            return true;
        }

        var integer = TruncateToBigInteger(value);

        if (typeof(TOther) == typeof(sbyte))
        {
            if (integer < sbyte.MinValue)
            {
                result = (TOther)(object)sbyte.MinValue;
                return true;
            }

            if (integer > sbyte.MaxValue)
            {
                result = (TOther)(object)sbyte.MaxValue;
                return true;
            }

            result = (TOther)(object)(sbyte)integer;
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            if (integer < byte.MinValue)
            {
                result = (TOther)(object)byte.MinValue;
                return true;
            }

            if (integer > byte.MaxValue)
            {
                result = (TOther)(object)byte.MaxValue;
                return true;
            }

            result = (TOther)(object)(byte)integer;
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            if (integer < short.MinValue)
            {
                result = (TOther)(object)short.MinValue;
                return true;
            }

            if (integer > short.MaxValue)
            {
                result = (TOther)(object)short.MaxValue;
                return true;
            }

            result = (TOther)(object)(short)integer;
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            if (integer < ushort.MinValue)
            {
                result = (TOther)(object)ushort.MinValue;
                return true;
            }

            if (integer > ushort.MaxValue)
            {
                result = (TOther)(object)ushort.MaxValue;
                return true;
            }

            result = (TOther)(object)(ushort)integer;
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            if (integer < int.MinValue)
            {
                result = (TOther)(object)int.MinValue;
                return true;
            }

            if (integer > int.MaxValue)
            {
                result = (TOther)(object)int.MaxValue;
                return true;
            }

            result = (TOther)(object)(int)integer;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            if (integer < uint.MinValue)
            {
                result = (TOther)(object)uint.MinValue;
                return true;
            }

            if (integer > uint.MaxValue)
            {
                result = (TOther)(object)uint.MaxValue;
                return true;
            }

            result = (TOther)(object)(uint)integer;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            if (integer < long.MinValue)
            {
                result = (TOther)(object)long.MinValue;
                return true;
            }

            if (integer > long.MaxValue)
            {
                result = (TOther)(object)long.MaxValue;
                return true;
            }

            result = (TOther)(object)(long)integer;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            if (integer < ulong.MinValue)
            {
                result = (TOther)(object)ulong.MinValue;
                return true;
            }

            if (integer > ulong.MaxValue)
            {
                result = (TOther)(object)ulong.MaxValue;
                return true;
            }

            result = (TOther)(object)(ulong)integer;
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            if (integer < Int128.MinValue)
            {
                result = (TOther)(object)Int128.MinValue;
                return true;
            }

            if (integer > Int128.MaxValue)
            {
                result = (TOther)(object)Int128.MaxValue;
                return true;
            }

            result = (TOther)(object)(Int128)integer;
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            if (integer < UInt128.MinValue)
            {
                result = (TOther)(object)UInt128.MinValue;
                return true;
            }

            if (integer > UInt128.MaxValue)
            {
                result = (TOther)(object)UInt128.MaxValue;
                return true;
            }

            result = (TOther)(object)(UInt128)integer;
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            if (integer < nint.MinValue)
            {
                result = (TOther)(object)nint.MinValue;
                return true;
            }

            if (integer > nint.MaxValue)
            {
                result = (TOther)(object)nint.MaxValue;
                return true;
            }

            result = (TOther)(object)(nint)integer;
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            if (integer < nuint.MinValue)
            {
                result = (TOther)(object)nuint.MinValue;
                return true;
            }

            if (integer > nuint.MaxValue)
            {
                result = (TOther)(object)nuint.MaxValue;
                return true;
            }

            result = (TOther)(object)(nuint)integer;
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            if (integer < char.MinValue)
            {
                result = (TOther)(object)char.MinValue;
                return true;
            }

            if (integer > char.MaxValue)
            {
                result = (TOther)(object)char.MaxValue;
                return true;
            }

            result = (TOther)(object)(char)integer;
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            result = (TOther)(object)(float)value;
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            result = (TOther)(object)(double)value;
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            result = (TOther)(object)(Half)(double)value;
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            if (value < DecimalMin)
            {
                result = (TOther)(object)decimal.MinValue;
                return true;
            }

            if (value > DecimalMax)
            {
                result = (TOther)(object)decimal.MaxValue;
                return true;
            }

            result = (TOther)(object)(decimal)value;
            return true;
        }

        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertToTruncating<TOther>(BigRational value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        result = default!;

        if (typeof(TOther) == typeof(BigRational))
        {
            result = (TOther)(object)value;
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            result = (TOther)(object)TruncateToBigInteger(value);
            return true;
        }

        var integer = TruncateToBigInteger(value);

        if (typeof(TOther) == typeof(sbyte))
        {
            result = (TOther)(object)(sbyte)WrapToSigned(integer, 8);
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            result = (TOther)(object)(byte)WrapToUnsigned(integer, 8);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = (TOther)(object)(short)WrapToSigned(integer, 16);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = (TOther)(object)(ushort)WrapToUnsigned(integer, 16);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = (TOther)(object)(int)WrapToSigned(integer, 32);
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = (TOther)(object)(uint)WrapToUnsigned(integer, 32);
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = (TOther)(object)(long)WrapToSigned(integer, 64);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = (TOther)(object)(ulong)WrapToUnsigned(integer, 64);
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            result = (TOther)(object)(Int128)WrapToSigned(integer, 128);
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            result = (TOther)(object)(UInt128)WrapToUnsigned(integer, 128);
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            var bits = IntPtr.Size * 8;
            result = (TOther)(object)(nint)WrapToSigned(integer, bits);
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            var bits = IntPtr.Size * 8;
            result = (TOther)(object)(nuint)WrapToUnsigned(integer, bits);
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            result = (TOther)(object)(char)WrapToUnsigned(integer, 16);
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            result = (TOther)(object)(float)value;
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            result = (TOther)(object)(double)value;
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            result = (TOther)(object)(Half)(double)value;
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            if (value < DecimalMin)
            {
                result = (TOther)(object)decimal.MinValue;
                return true;
            }

            if (value > DecimalMax)
            {
                result = (TOther)(object)decimal.MaxValue;
                return true;
            }

            result = (TOther)(object)(decimal)value;
            return true;
        }

        return false;
    }

    private static bool TryParseCore(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out BigRational result)
    {
        if (s.IsEmpty)
        {
            result = default;
            return false;
        }

        var separatorIndex = s.IndexOf('/');
        if (separatorIndex < 0)
        {
            if (BigInteger.TryParse(s, style, provider, out var integer))
            {
                result = new BigRational(integer);
                return true;
            }

            result = default;
            return false;
        }

        var numeratorSpan = s[..separatorIndex];
        var denominatorStart = separatorIndex + 1;
        var denominatorSpan = s[denominatorStart..];

        if (!BigInteger.TryParse(numeratorSpan, style, provider, out var numerator)
            || !BigInteger.TryParse(denominatorSpan, style, provider, out var denominator)
            || denominator.IsZero)
        {
            result = default;
            return false;
        }

        result = new BigRational(numerator, denominator);
        return true;
    }

    private static BigInteger TruncateToBigInteger(BigRational value)
    {
        return value.Numerator / value.Denominator;
    }

    private static BigInteger WrapToUnsigned(BigInteger value, int bits)
    {
        var modulus = BigInteger.One << bits;
        var result = value % modulus;
        if (result.Sign < 0)
        {
            result += modulus;
        }

        return result;
    }

    private static BigInteger WrapToSigned(BigInteger value, int bits)
    {
        var modulus = BigInteger.One << bits;
        var result = value % modulus;
        if (result.Sign < 0)
        {
            result += modulus;
        }

        var signBit = modulus >> 1;
        if (result >= signBit)
        {
            result -= modulus;
        }

        return result;
    }

    /// <summary>
    /// Deconstructs this <see cref="BigRational"/> into a numerator and a denominator.
    /// </summary>
    /// <param name="numerator">
    /// The numerator of this <see cref="BigRational"/>.
    /// </param>
    /// <param name="denominator">
    /// The denominator of this <see cref="BigRational"/>.
    /// </param>
    public void Deconstruct(out BigInteger numerator, out BigInteger denominator)
    {
        numerator = this.Numerator;
        denominator = this.Denominator;
    }

    /// <summary>
    /// Calculates the reciprocal of this <see cref="BigRational"/> instance
    /// (1 divided by this <see cref="BigRational"/> value).
    /// </summary>
    /// <returns>
    /// The reciprocal value.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If this <see cref="BigRational"/> is zero (0/1).
    /// </exception>
    public BigRational Reciprocal()
    {
        return new BigRational(this.denominator, this.numerator);
    }

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var numeratorText = this.Numerator.ToString(format, formatProvider);
        var denominatorText = this.Denominator.ToString(format, formatProvider);
        return string.Concat(numeratorText, "/", denominatorText);
    }

    /// <inheritdoc />
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        var text = this.ToString(format.IsEmpty ? null : format.ToString(), provider);
        if (text.Length > destination.Length)
        {
            charsWritten = 0;
            return false;
        }

        text.AsSpan().CopyTo(destination);
        charsWritten = text.Length;
        return true;
    }

    /// <inheritdoc />
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        var text = this.ToString(format.IsEmpty ? null : format.ToString(), provider);
        var byteCount = Encoding.UTF8.GetByteCount(text);
        if (byteCount > utf8Destination.Length)
        {
            bytesWritten = 0;
            return false;
        }

        bytesWritten = Encoding.UTF8.GetBytes(text.AsSpan(), utf8Destination);
        return true;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return this.ToString(null, CultureInfo.CurrentCulture);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is BigRational bigRational && this.Equals(bigRational);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            return (this.Numerator.GetHashCode() * 137) + this.Denominator.GetHashCode();
        }
    }

    /// <inheritdoc />
    public bool Equals(BigRational other)
    {
        return (this.Numerator * other.Denominator).Equals(other.Numerator * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(BigInteger other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(decimal other)
    {
        return this.Equals((BigRational)other);
    }

    /// <inheritdoc />
    public bool Equals(double other)
    {
        if (!double.IsFinite(other))
        {
            return false;
        }

        return this.Equals((BigRational)other);
    }

    /// <inheritdoc />
    public bool Equals(float other)
    {
        if (!float.IsFinite(other))
        {
            return false;
        }

        return this.Equals((BigRational)other);
    }

    /// <inheritdoc />
    public bool Equals(Half other)
    {
        if (!Half.IsFinite(other))
        {
            return false;
        }

        return this.Equals((BigRational)(double)other);
    }

    /// <inheritdoc />
    public bool Equals(Int128 other)
    {
        return this.Numerator.Equals((BigInteger)other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public bool Equals(UInt128 other)
    {
        return this.Numerator.Equals((BigInteger)other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(nint other)
    {
        return this.Equals((long)other);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public bool Equals(nuint other)
    {
        return this.Equals((ulong)other);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public bool Equals(ulong other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(long other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public bool Equals(uint other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(int other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public bool Equals(ushort other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(char other)
    {
        return this.Numerator.Equals((BigInteger)(int)other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(short other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    public bool Equals(byte other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public bool Equals(sbyte other)
    {
        return this.Numerator.Equals(other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj switch
        {
            BigRational other => this.CompareTo(other),
            BigInteger other => this.CompareTo(other),
            decimal other => this.CompareTo(other),
            double other => this.CompareTo(other),
            float other => this.CompareTo(other),
            Half other => this.CompareTo(other),
            Int128 other => this.CompareTo(other),
            UInt128 other => this.CompareTo(other),
            nint other => this.CompareTo(other),
            nuint other => this.CompareTo(other),
            ulong other => this.CompareTo(other),
            long other => this.CompareTo(other),
            uint other => this.CompareTo(other),
            int other => this.CompareTo(other),
            ushort other => this.CompareTo(other),
            char other => this.CompareTo(other),
            short other => this.CompareTo(other),
            byte other => this.CompareTo(other),
            sbyte other => this.CompareTo(other),
            _ => throw new ArgumentException("Object must be a BigRational or numeric value.", nameof(obj)),
        };
    }

    /// <inheritdoc />
    public int CompareTo(BigRational other)
    {
        return (this.Numerator * other.Denominator).CompareTo(other.Numerator * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(BigInteger other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(decimal other)
    {
        return this.CompareTo((BigRational)other);
    }

    /// <inheritdoc />
    public int CompareTo(double other)
    {
        return this.CompareTo((BigRational)other);
    }

    /// <inheritdoc />
    public int CompareTo(float other)
    {
        return this.CompareTo((BigRational)other);
    }

    /// <inheritdoc />
    public int CompareTo(Half other)
    {
        return this.CompareTo((BigRational)(double)other);
    }

    /// <inheritdoc />
    public int CompareTo(Int128 other)
    {
        return this.CompareTo((BigInteger)other);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public int CompareTo(UInt128 other)
    {
        return this.CompareTo((BigInteger)other);
    }

    /// <inheritdoc />
    public int CompareTo(nint other)
    {
        return this.CompareTo((long)other);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public int CompareTo(nuint other)
    {
        return this.CompareTo((ulong)other);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public int CompareTo(ulong other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(long other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public int CompareTo(uint other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(int other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public int CompareTo(ushort other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(char other)
    {
        return this.Numerator.CompareTo((BigInteger)(int)other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(short other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(byte other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    [CLSCompliant(false)]
    public int CompareTo(sbyte other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified
    /// <see cref="BigRational"/> number.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> number.
    /// </param>
    /// <returns>
    /// The smallest <see cref="BigRational"/> value that is greater than or equal to <paramref name="value"/>.
    /// </returns>
    public static BigRational Ceiling(BigRational value)
    {
        return value.IsInteger ? value : CeilingImpl(value);
    }

    /// <summary>
    /// Returns the largest integral number that is less than or equal to the specified
    /// <see cref="BigRational"/> number.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> number.
    /// </param>
    /// <returns>
    /// The largest integral number that is less than or equal to the specified <see cref="BigRational"/> number.
    /// </returns>
    public static BigRational Floor(BigRational value)
    {
        return value.IsInteger ? value : FloorImpl(value);
    }

    /// <summary>
    /// Returns the smallest number greater than or equal to <paramref name="value"/> that is
    /// a whole number of ticks away from zero.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <param name="tick">
    /// The size of the tickSize.
    /// </param>
    /// <returns>
    /// The smallest number greater than or equal to <paramref name="value"/> that is
    /// a whole number of ticks away from zero.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="tick"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Ceiling(BigRational value, BigRational tick)
    {
        AssertValidTick(tick);
        var ticks = value / tick;
        return ticks.IsInteger ? value : CeilingImpl(ticks) * tick;
    }

    /// <summary>
    /// Returns the largest number less than or equal to <paramref name="value"/> that is a
    /// multiple of <paramref name="tick"/>.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> number.
    /// </param>
    /// <param name="tick">
    /// Multiples of <paramref name="tick"/> define the set of values that
    /// <paramref name="value"/> can be rounded to.
    /// </param>
    /// <returns>
    /// The largest number less than or equal to <paramref name="value"/> that is a
    /// multiple of <paramref name="tick"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="tick"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Floor(BigRational value, BigRational tick)
    {
        AssertValidTick(tick);
        var ticks = value / tick;
        return ticks.IsInteger ? value : FloorImpl(ticks) * tick;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to a nearest number that is multiple of <paramref name="tick"/>.
    /// If <paramref name="value"/> is exactly half way between two such numbers, <paramref name="mode"/>
    /// specifies the rounding method to use.
    /// </summary>
    /// <param name="value">
    /// The value to be rounded.
    /// </param>
    /// <param name="tick">
    /// Multiples of <paramref name="tick"/> define the set of values that <paramref name="value"/>
    /// can be rounded to.
    /// </param>
    /// <param name="mode">
    /// The specification of what to do when <paramref name="value"/> is exactly half way between two numbers
    /// that are a multiple if <paramref name="tick"/>.
    /// </param>
    /// <returns>
    /// Rounds<paramref name="value"/> to a nearest number that is multiple of<paramref name= "tick" />.
    /// If <paramref name= "value" /> is exactly half way between two such numbers, <paramref name="mode"/>
    /// specifies the rounding method to use.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="tick"/> is less than or equal to zero.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="mode"/> is mode is not valid <see cref="MidpointRoundingMode"/> value.
    /// </exception>
    public static BigRational RoundToTick(BigRational value, BigRational tick, MidpointRoundingMode mode)
    {
        AssertValidRationalRounding(mode);
        AssertValidTick(tick);
        var ticks = value / tick;
        return ticks.IsInteger ? value : RoundImpl(ticks, mode) * tick;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to a the nearest <see cref="BigInteger"/>
    /// If <paramref name="value"/> is exactly half way between two such numbers, <paramref name="mode"/>
    /// specifies the rounding method to use <see cref="MidpointRoundingMode"/>.
    /// </summary>
    /// <param name="value">
    /// The value to be rounded.
    /// </param>
    /// <param name="mode">
    /// The specification of what to do when <paramref name="value"/> is exactly half way between two integer values.
    /// </param>
    /// <returns>
    /// The result of rounding <paramref name="value"/> to a the nearest <see cref="BigInteger"/>
    /// If <paramref name="value"/> is exactly half way between two such numbers, <paramref name="mode"/>
    /// specifies the rounding method to use <see cref="MidpointRoundingMode"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="mode"/> is mode is not valid <see cref="MidpointRoundingMode"/> value.
    /// </exception>
    public static BigInteger RoundToInt(BigRational value, MidpointRoundingMode mode)
    {
        AssertValidRationalRounding(mode);
        return value.IsInteger ? value.Numerator : RoundImpl(value, mode);
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to a nearest integral number. If <paramref name="value"/>
    /// is exactly half way between two such numbers, <paramref name="mode"/> specifies the rounding method to use.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <param name="mode">
    /// The rounding methodology to use if value is exactly half way between two integral values.
    /// </param>
    /// <returns>
    /// The nearest integral number. If <paramref name="value"/>
    /// is exactly half way between two such numbers, <paramref name="mode"/> specifies the rounding method to use.
    /// </returns>
    /// <remarks>
    /// This method assumes that <paramref name="value"/> is not an integral number.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// If <paramref name="mode"/> is mode is not valid <see cref="MidpointRoundingMode"/> value.
    /// </exception>
    private static BigInteger RoundImpl(BigRational value, MidpointRoundingMode mode)
    {
        switch (mode)
        {
            case MidpointRoundingMode.ToEven:
                return RoundToEvenImpl(value);
            case MidpointRoundingMode.Up:
                return RoundUpImpl(value);
            case MidpointRoundingMode.Down:
                return RoundDownImpl(value);
            case MidpointRoundingMode.AwayFromZero:
                return RoundAwayFromZeroImpl(value);
            case MidpointRoundingMode.TowardZero:
                return RoundTowardZeroImpl(value);
            default:
                throw new ArgumentException("Invalid RationalRounding.");
        }
    }

    /// <summary>
    /// Returns the largest integral number that is less than or equal to the specified
    /// <see cref="BigRational"/> number.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> number.
    /// </param>
    /// <returns>
    /// The largest integral number that is less than or equal to the specified <see cref="BigRational"/> number.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger FloorImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        if (value.IsPositive)
        {
            return value.Numerator / value.Denominator;
        }

        return (value.Numerator / value.Denominator) - BigInteger.One;
    }

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified
    /// <see cref="BigRational"/> number.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> number.
    /// </param>
    /// <returns>
    /// The smallest <see cref="BigRational"/> value that is greater than or equal to <paramref name="value"/>.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger CeilingImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        if (value.IsNegative)
        {
            return value.Numerator / value.Denominator;
        }

        return (value.Numerator / value.Denominator) + BigInteger.One;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded up.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded up.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundUpImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        return (value - floor).IsGreaterThanOrEqualToHalf()
            ? ++floor
            : floor;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded down.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded down.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundDownImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        return (value - floor).IsGreaterThanHalf()
            ? ++floor
            : floor;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded toward zero.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded toward zero.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundTowardZeroImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        var fraction = value - floor;
        if (value.IsPositive)
        {
            if (fraction.IsGreaterThanHalf())
            {
                ++floor;
            }
        }
        else
        {
            if (fraction.IsGreaterThanOrEqualToHalf())
            {
                ++floor;
            }
        }

        return floor;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded away from zero.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded away from zero.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundAwayFromZeroImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        var fraction = value - floor;
        if (value.IsPositive)
        {
            if (fraction.IsGreaterThanOrEqualToHalf())
            {
                ++floor;
            }
        }
        else
        {
            if (fraction.IsGreaterThanHalf())
            {
                ++floor;
            }
        }

        return floor;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded to the nearest even integral value.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly half way between two integral values, it is rounded to the nearest even integral value.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundToEvenImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        switch ((value - floor).CompareToHalf())
        {
            case 0:
                return floor.IsEven ? floor : ++floor;
            case 1:
                return ++floor;
            default:
                return floor;
        }
    }

    /// <summary>
    /// Assert a valid rational rounding.
    /// </summary>
    /// <param name="mode">
    /// The midpoint rounding mode to be assessed.
    /// </param>
    private static void AssertValidRationalRounding(MidpointRoundingMode mode)
    {
        if (!Enum.IsDefined(mode))
        {
            throw new ArgumentException("Invalid RationalRounding.");
        }
    }

    /// <summary>
    /// Assert a valid tick.
    /// </summary>
    /// <param name="tick">
    /// The big rational to be assessed.
    /// </param>
    private static void AssertValidTick(BigRational tick)
    {
        if (!tick.IsPositive)
        {
            throw new ArgumentException("Invalid tick size. Tick must be greater than zero.");
        }
    }

    /// <summary>
    /// Check whether number is greater than or equal to one half.
    /// </summary>
    /// <returns>
    /// Returns <c>true</c>, if number is greater than or equal to one half, <c>false</c> otherwise.
    /// </returns>
    private bool IsGreaterThanOrEqualToHalf()
    {
        return this.Numerator * BigIntegerTwo >= this.Denominator;
    }

    /// <summary>
    /// Check whether number is greater than one half.
    /// </summary>
    /// <returns>
    /// Returns <c>true</c>, if number is greater than one half, <c>false</c> otherwise.
    /// </returns>
    private bool IsGreaterThanHalf()
    {
        return this.Numerator * BigIntegerTwo > this.Denominator;
    }

    /// <summary>
    /// Compare number to one half.
    /// </summary>
    /// <returns>
    /// Returns zero if number is equal to one half.
    /// </returns>
    private int CompareToHalf()
    {
        return (this.Numerator * BigIntegerTwo).CompareTo(this.Denominator);
    }

    /// <summary>
    /// Calculates the taylor approximation of Eulers constant raised to <paramref name="power"/>,
    /// with the specified number of terms.
    /// </summary>
    /// <param name="power">
    /// The power to raise Eulers constant to.
    /// </param>
    /// <param name="terms">
    /// The number of terms to compute.
    /// </param>
    /// <returns>
    /// The taylor approximation of Eulers constant raised to <paramref name="power"/>,
    /// with the specified number of terms.
    /// </returns>
    public static BigRational Exp(BigRational power, int terms)
    {
        if (terms < 0)
        {
            throw new ArgumentException("terms must be non-negative");
        }

        if (terms == 0)
        {
            return 0;
        }

        if (terms == 1)
        {
            return 1;
        }

        var xn = new BigRational(BigInteger.One);
        var sum = xn;
        var factorial = BigInteger.One;
        for (var t = 1; t != terms; ++t)
        {
            xn *= power;
            factorial *= t;
            sum += xn / factorial;
        }

        return sum;
    }

    /// <summary>
    /// Approximates the natural (base e) logarithm of a specified number using a series expansion of a specified (default = 1000) number of terms.
    /// </summary>
    /// <param name="x">
    /// The number whose logarithm is to be approximated.
    /// </param>
    /// <param name="terms">
    /// The number of terms to compute.
    /// </param>
    /// <returns>
    /// The approximation of the natural (base e) logarithm of a specified number.
    /// </returns>
    public static BigRational Log(BigRational x, int terms)
    {
        if (terms < 0)
        {
            throw new ArgumentException("terms must be non-negative");
        }

        var n = 1 / (x - 1);
        var factor = 1 / ((2 * n) + 1);
        var factorSquared = factor * factor;
        var total = factor;
        for (int term = 1, power = 3; term < terms; ++term, power += 2)
        {
            factor *= factorSquared;
            total += factor / power;
        }

        return 2 * total;
    }
}
