// <copyright file="BigRational.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

/// <summary>
/// A structure that represents a rational number with an arbitrarily large numerator and denominator.
/// </summary>
/// <remarks>
/// Values are normalized to lowest terms with a positive denominator.
/// </remarks>
public readonly struct BigRational :
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
    IComparable<sbyte>,
    IComparable,
    IComparable<BigRational>
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
            ThrowDenominatorZero();
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

    /// <summary>
    /// Gets a value that represents negative one (-1).
    /// </summary>
    public static BigRational MinusOne => NegativeOne;

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static int Radix => 2;

    /// <summary>
    /// Gets the denominator of this <see cref="BigRational" />.
    /// </summary>
    /// <remarks>
    /// This is a computed property because default struct initialization leaves the denominator as zero.
    /// This ensures that a default-initialized <see cref="BigRational"/> is treated as zero with a denominator of one.
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
    /// Gets a number that indicates the sign of this <see cref="BigRational"/>.
    /// </summary>
    /// <returns>
    /// -1 if the value is negative,
    /// 0 if the value is zero,
    /// 1 if the value is positive.
    /// </returns>
    public int Sign
    {
        get => this.Numerator.Sign;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is greater than zero.
    /// </summary>
    public bool IsGreaterThanZero
    {
        get => this.Numerator.Sign == 1;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is less than zero.
    /// </summary>
    public bool IsLessThanZero
    {
        get => this.Numerator.Sign == -1;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is less than or equal to zero.
    /// </summary>
    public bool IsLessThanOrEqualZero
    {
        get => this.Numerator.Sign <= 0;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is greater than or equal to zero.
    /// </summary>
    public bool IsGreaterThanOrEqualZero
    {
        get => this.Numerator.Sign >= 0;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is positive or zero.
    /// </summary>
    public bool IsPositiveOrZero
    {
        get => this.IsGreaterThanOrEqualZero;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is negative or zero.
    /// </summary>
    public bool IsNegativeOrZero
    {
        get => this.IsLessThanOrEqualZero;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to zero.
    /// </summary>
    public bool IsZero
    {
        get => this.Numerator.IsZero;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to one.
    /// </summary>
    public bool IsOne
    {
        get => this.Numerator.IsOne && this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to minus one.
    /// </summary>
    public bool IsMinusOne
    {
        get => this.Numerator == BigInteger.MinusOne && this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> can be represented as an integer.
    /// </summary>
    public bool IsInteger
    {
        get => this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is a positive integer power of two.
    /// </summary>
    public bool IsPowerOfTwo
    {
        get => this.IsInteger && this.Numerator.IsPowerOfTwo;
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
    /// Converts a <see cref="BigRational"/> to a <see cref="double"/>. If the value is halfway between two
    /// representable double values, it is rounded to the nearest even value. Values that exceed the range
    /// of <see cref="double"/> are converted to <see cref="double.PositiveInfinity"/> or
    /// <see cref="double.NegativeInfinity"/>, and values too small to represent round to signed zero.
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
    /// a <see cref="BigRational"/> to a double. Values that exceed the range of <see cref="float"/> are
    /// converted to infinity, and values too small to represent round to signed zero.
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
    /// a <see cref="BigRational"/> to a double. Values that exceed the range of <see cref="Half"/> are
    /// converted to infinity, and values too small to represent round to signed zero.
    /// </remarks>
    public static explicit operator Half(BigRational value)
    {
        return (Half)(double)value;
    }

    /// <summary>
    /// Converts a <see cref="double"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="double"/> to convert.
    /// </param>
    /// <remarks>
    /// The resulting <see cref="BigRational"/> represents the exact value of <paramref name="value"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// If <paramref name="value"/> is not finite.
    /// </exception>
    public static implicit operator BigRational(double value)
    {
        if (!double.IsFinite(value))
        {
            ThrowValueMustBeFinite();
        }

        if (value == 0d)
        {
            return Zero;
        }

        ulong bits;
        unsafe
        {
            bits = *(ulong*)&value;
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
                magnitude = new BigRational(significand << shift);
            }
            else
            {
                magnitude = new BigRational(significand, BigInteger.One << -shift);
            }
        }

        return value < 0d ? -magnitude : magnitude;
    }

    /// <summary>
    /// Converts a <see cref="float"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="float"/> to convert.
    /// </param>
    /// <remarks>
    /// The resulting <see cref="BigRational"/> represents the exact value of <paramref name="value"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// If <paramref name="value"/> is not finite.
    /// </exception>
    public static implicit operator BigRational(float value)
    {
        return (double)value;
    }

    /// <summary>
    /// Converts a <see cref="Half"/> to a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="Half"/> to convert.
    /// </param>
    /// <remarks>
    /// The resulting <see cref="BigRational"/> represents the exact value of <paramref name="value"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// If <paramref name="value"/> is not finite.
    /// </exception>
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
    /// <remarks>
    /// The resulting <see cref="BigRational"/> represents the exact value of <paramref name="value"/>.
    /// </remarks>
    public static implicit operator BigRational(decimal value)
    {
        var bits = decimal.GetBits(value);
        var low = (uint)bits[0];
        var mid = (uint)bits[1];
        var high = (uint)bits[2];
        var isNegative = (bits[3] & int.MinValue) != 0;
        var scale = (bits[3] >> 16) & 0xFF;

        var numerator = ((BigInteger)high << 64) | ((BigInteger)mid << 32) | low;
        if (isNegative)
        {
            numerator = -numerator;
        }

        var denominator = BigInteger.Pow(10, scale);
        return new BigRational(numerator, denominator);
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
    /// <remarks>
    /// If <paramref name="value"/> cannot be represented exactly, it is rounded to fit decimal precision
    /// using midpoint-to-even rounding.
    /// </remarks>
    public static explicit operator decimal(BigRational value)
    {
        if (value < DecimalMin || value > DecimalMax)
        {
            ThrowDecimalValueOutOfRange();
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
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    public static bool operator !=(BigRational left, ulong right)
    {
        return left.Denominator != BigInteger.One || left.Numerator != right;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    public static bool operator !=(ulong left, BigRational right)
    {
        return right.Denominator != BigInteger.One || left != right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is not equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    public static bool operator >(BigRational left, ulong right)
    {
        return left.Numerator > right * left.Denominator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    public static bool operator >(ulong left, BigRational right)
    {
        return left * right.Denominator > right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    public static bool operator <=(BigRational left, ulong right)
    {
        return left.Numerator <= right * left.Denominator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    public static bool operator <=(ulong left, BigRational right)
    {
        return left * right.Denominator <= right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    public static bool operator <(ulong left, BigRational right)
    {
        return left * right.Denominator < right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    public static bool operator <(BigRational left, ulong right)
    {
        return left.Numerator < right * left.Denominator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is less than <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    public static bool operator >=(BigRational left, ulong right)
    {
        return left.Numerator >= right * left.Denominator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    public static bool operator >=(ulong left, BigRational right)
    {
        return left * right.Denominator >= right.Numerator;
    }

    /// <summary>
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    /// Returns a value indicating whether <paramref name="left"/> is greater than or equal to <paramref name="right"/>.
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
    /// The value of <paramref name="value"/> + 1.
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
    /// The value of <paramref name="value"/> - 1.
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
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(BigRational dividend, BigRational divisor)
    {
        return new BigRational(dividend.Numerator * divisor.Denominator, dividend.Denominator * divisor.Numerator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by a <see cref="BigInteger"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigInteger"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(BigRational dividend, BigInteger divisor)
    {
        return new BigRational(dividend.Numerator, dividend.Denominator * divisor);
    }

    /// <summary>
    /// Divides a <see cref="BigInteger"/> value by a <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigInteger"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(BigInteger dividend, BigRational divisor)
    {
        return new BigRational(dividend * divisor.Denominator, divisor.Numerator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="ulong"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(BigRational dividend, ulong divisor)
    {
        return new BigRational(dividend.Numerator, dividend.Denominator * divisor);
    }

    /// <summary>
    /// Divides a <see cref="ulong"/> value by a <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="ulong"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(ulong dividend, BigRational divisor)
    {
        return new BigRational(dividend * divisor.Denominator, divisor.Numerator);
    }

    /// <summary>
    /// Divides a <see cref="BigRational"/> value by a <see cref="long"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="long"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(BigRational dividend, long divisor)
    {
        return new BigRational(dividend.Numerator, dividend.Denominator * divisor);
    }

    /// <summary>
    /// Divides a <see cref="long"/> value by a <see cref="BigRational"/> value.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="long"/> to be divided (the dividend).
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> to divide by (the divisor).
    /// </param>
    /// <returns>
    /// The quotient of <paramref name="dividend"/> and <paramref name="divisor"/>.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator /(long dividend, BigRational divisor)
    {
        return new BigRational(dividend * divisor.Denominator, divisor.Numerator);
    }

    /// <summary>
    /// Calculates the remainder that results from division with two specified <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(BigRational dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with a <see cref="BigRational"/> and a
    /// <see cref="BigInteger"/>.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigInteger"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(BigRational dividend, BigInteger divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with a <see cref="BigInteger"/> and a
    /// <see cref="BigRational"/>.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigInteger"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(BigInteger dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with a <see cref="BigRational"/> and a
    /// <see cref="ulong"/>.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="ulong"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(BigRational dividend, ulong divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with a <see cref="ulong"/> and a
    /// <see cref="BigRational"/>.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="ulong"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(ulong dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with a <see cref="BigRational"/> and a
    /// <see cref="long"/>.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="BigRational"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="long"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(BigRational dividend, long divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <summary>
    /// Calculates the remainder that results from division with a <see cref="long"/> and a
    /// <see cref="BigRational"/>.
    /// </summary>
    /// <param name="dividend">
    /// The <see cref="long"/> value to be divided.
    /// </param>
    /// <param name="divisor">
    /// The <see cref="BigRational"/> value to divide by.
    /// </param>
    /// <returns>
    /// The remainder that results from the division.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// If <paramref name="divisor"/> is zero.
    /// </exception>
    public static BigRational operator %(long dividend, BigRational divisor)
    {
        var temp = dividend / divisor;
        return dividend - ((temp.Numerator / temp.Denominator) * divisor);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Abs(BigRational value)
    {
        return value.IsLessThanZero ? -value : value;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    static int INumber<BigRational>.Sign(BigRational value)
    {
        return value.Sign;
    }

    /// <inheritdoc cref="INumber{BigRational}" />
    public static BigRational CopySign(BigRational value, BigRational sign)
    {
        return sign.IsLessThanZero ? -Abs(value) : Abs(value);
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
    public static bool IsNegative(BigRational value)
    {
        return value.IsLessThanZero;
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

    /// <summary>
    /// Returns a value indicating whether the specified value is a power of two.
    /// </summary>
    /// <param name="value">
    /// The value to test.
    /// </param>
    /// <returns>
    /// true if <paramref name="value"/> is a positive integer power of two; otherwise, false.
    /// </returns>
    public static bool IsPow2(BigRational value)
    {
        return value.IsPowerOfTwo;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool IsPositive(BigRational value)
    {
        return value.IsGreaterThanZero;
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
    /// <exception cref="ArgumentException">
    /// If <paramref name="min"/> is greater than <paramref name="max"/>.
    /// </exception>
    public static BigRational Clamp(BigRational value, BigRational min, BigRational max)
    {
        if (min > max)
        {
            ThrowMinMustBeLessThanOrEqualToMax();
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
                ThrowZeroToNegativePower();
            }

            return Zero;
        }

        if (exp > 0)
        {
            return new BigRational(
                numerator: BigInteger.Pow(value.Numerator, exp),
                denominator: BigInteger.Pow(value.Denominator, exp));
        }

        if (exp == int.MinValue)
        {
            var numerator = value.Denominator;
            var denominator = value.Numerator;
            var powNumerator = BigInteger.Pow(numerator, int.MaxValue);
            var powDenominator = BigInteger.Pow(denominator, int.MaxValue);
            return new BigRational(powNumerator * numerator, powDenominator * denominator);
        }

        exp = -exp;
        return new BigRational(
            numerator: BigInteger.Pow(value.Denominator, exp),
            denominator: BigInteger.Pow(value.Numerator, exp));
    }

    /// <summary>
    /// Calculates the Taylor-series approximation of e raised to <paramref name="power"/>,
    /// using the specified number of terms.
    /// </summary>
    /// <param name="power">
    /// The exponent to apply to Euler's number.
    /// </param>
    /// <param name="terms">
    /// The number of terms to compute. Must be positive.
    /// </param>
    /// <returns>
    /// The Taylor-series approximation of e raised to <paramref name="power"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="terms"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Exp(BigRational power, int terms)
    {
        if (terms <= 0)
        {
            ThrowTermsMustBePositive();
        }

        if (terms == 1)
        {
            return One;
        }

        var xn = One;
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
    /// Approximates the natural (base e) logarithm of a specified number using a series expansion
    /// with the specified number of terms.
    /// </summary>
    /// <param name="x">
    /// The number whose logarithm is to be approximated. Must be positive.
    /// </param>
    /// <param name="terms">
    /// The number of terms to compute. Must be positive.
    /// </param>
    /// <returns>
    /// The approximation of the natural (base e) logarithm of the specified number.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="terms"/> is less than or equal to zero.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="x"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Log(BigRational x, int terms)
    {
        if (terms <= 0)
        {
            ThrowTermsMustBePositive();
        }

        if (x.IsNegativeOrZero)
        {
            ThrowValueMustBePositive();
        }

        if (x.IsOne)
        {
            return Zero;
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

    /// <summary>
    /// Converts the string representation of an integer or a numerator/denominator fraction
    /// to its <see cref="BigRational"/> equivalent.
    /// </summary>
    /// <param name="value">
    /// A string that contains an integer or a numerator/denominator fraction to convert.
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
        ArgumentNullException.ThrowIfNull(value);
        return Parse(value, NumberStyles.Integer, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Converts the string representation of an integer or a numerator/denominator fraction
    /// to its <see cref="BigRational"/> equivalent using the invariant culture.
    /// </summary>
    /// <param name="value">
    /// A string that contains an integer or a numerator/denominator fraction to convert.
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
    public static BigRational ParseInvariant(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
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

        if (!TryParseCore(s.AsSpan(), style, provider, out var result))
        {
            ThrowCouldNotParseBigRationalString(s);
        }

        return result;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        if (!TryParseCore(s, style, provider, out var result))
        {
            ThrowCouldNotParseBigRationalSpan();
        }

        return result;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static BigRational Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider)
    {
        var text = Encoding.UTF8.GetString(utf8Text);
        return Parse(text, style, provider);
    }

    /// <summary>
    /// Tries to convert the string representation of an integer or a numerator/denominator fraction
    /// to its <see cref="BigRational"/> equivalent,
    /// and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of an integer or a numerator/denominator fraction.
    /// </param>
    /// <param name="rational">
    /// When this method returns, contains the <see cref="BigRational"/> equivalent to
    /// the number that is contained in value, or zero (0) if the conversion fails.
    /// The conversion fails if <paramref name="value"/> is null or is not in the correct format.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// true if value was converted successfully; otherwise, false.
    /// </returns>
    public static bool TryParse(string? value, out BigRational rational)
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
        var text = Encoding.UTF8.GetString(utf8Text);
        return TryParse(text, style, provider, out result);
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    /// <exception cref="OverflowException">
    /// If <paramref name="value"/> is not representable by <see cref="BigRational"/>.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// If <paramref name="value"/> cannot be converted to <see cref="BigRational"/>.
    /// </exception>
    public static BigRational CreateChecked<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (!TryConvertFromChecked(value, out var result))
        {
            ThrowCannotConvertFrom<TOther>();
        }

        return result;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    /// <exception cref="NotSupportedException">
    /// If <paramref name="value"/> cannot be converted to <see cref="BigRational"/>.
    /// </exception>
    public static BigRational CreateSaturating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (!TryConvertFromSaturating(value, out var result))
        {
            ThrowCannotConvertFrom<TOther>();
        }

        return result;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    /// <exception cref="NotSupportedException">
    /// If <paramref name="value"/> cannot be converted to <see cref="BigRational"/>.
    /// </exception>
    public static BigRational CreateTruncating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (!TryConvertFromTruncating(value, out var result))
        {
            ThrowCannotConvertFrom<TOther>();
        }

        return result;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    /// <exception cref="OverflowException">
    /// If <paramref name="value"/> is not representable by <see cref="BigRational"/>.
    /// </exception>
    public static bool TryConvertFromChecked<TOther>(TOther value, out BigRational result)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(BigRational))
        {
            result = Unsafe.As<TOther, BigRational>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            var bigInteger = Unsafe.As<TOther, BigInteger>(ref value);
            result = new BigRational(bigInteger);
            return true;
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            result = Unsafe.As<TOther, sbyte>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            result = Unsafe.As<TOther, byte>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = Unsafe.As<TOther, short>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = Unsafe.As<TOther, ushort>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = Unsafe.As<TOther, int>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = Unsafe.As<TOther, uint>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = Unsafe.As<TOther, long>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = Unsafe.As<TOther, ulong>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            var int128Value = Unsafe.As<TOther, Int128>(ref value);
            result = new BigRational((BigInteger)int128Value);
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            var uint128Value = Unsafe.As<TOther, UInt128>(ref value);
            result = new BigRational((BigInteger)uint128Value);
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            var nativeValue = Unsafe.As<TOther, nint>(ref value);
            result = (long)nativeValue;
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            var nativeValue = Unsafe.As<TOther, nuint>(ref value);
            result = (ulong)nativeValue;
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            result = Unsafe.As<TOther, char>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            var floatValue = Unsafe.As<TOther, float>(ref value);
            if (!float.IsFinite(floatValue))
            {
                ThrowValueNotRepresentableByBigRational();
            }

            result = floatValue;
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            var doubleValue = Unsafe.As<TOther, double>(ref value);
            if (!double.IsFinite(doubleValue))
            {
                ThrowValueNotRepresentableByBigRational();
            }

            result = doubleValue;
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            var halfValue = Unsafe.As<TOther, Half>(ref value);
            if (!Half.IsFinite(halfValue))
            {
                ThrowValueNotRepresentableByBigRational();
            }

            result = (double)halfValue;
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            result = Unsafe.As<TOther, decimal>(ref value);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertFromSaturating<TOther>(TOther value, out BigRational result)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(float))
        {
            var floatValue = Unsafe.As<TOther, float>(ref value);
            if (!float.IsFinite(floatValue))
            {
                result = default;
                return false;
            }
        }
        else if (typeof(TOther) == typeof(double))
        {
            var doubleValue = Unsafe.As<TOther, double>(ref value);
            if (!double.IsFinite(doubleValue))
            {
                result = default;
                return false;
            }
        }
        else if (typeof(TOther) == typeof(Half))
        {
            var halfValue = Unsafe.As<TOther, Half>(ref value);
            if (!Half.IsFinite(halfValue))
            {
                result = default;
                return false;
            }
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
    /// <exception cref="OverflowException">
    /// If <paramref name="value"/> is not representable by <typeparamref name="TOther"/>.
    /// </exception>
    public static bool TryConvertToChecked<TOther>(BigRational value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        result = default!;

        if (typeof(TOther) == typeof(BigRational))
        {
            result = Unsafe.As<BigRational, TOther>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            if (!value.IsInteger)
            {
                ThrowValueNotInteger();
            }

            var numerator = value.Numerator;
            result = Unsafe.As<BigInteger, TOther>(ref numerator);
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            var floatValue = (float)value;
            if (!float.IsFinite(floatValue))
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            result = Unsafe.As<float, TOther>(ref floatValue);
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            var doubleValue = (double)value;
            if (!double.IsFinite(doubleValue))
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            result = Unsafe.As<double, TOther>(ref doubleValue);
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            var halfValue = (Half)(double)value;
            if (!Half.IsFinite(halfValue))
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            result = Unsafe.As<Half, TOther>(ref halfValue);
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            var decimalValue = (decimal)value;
            result = Unsafe.As<decimal, TOther>(ref decimalValue);
            return true;
        }

        var integer = value.Numerator;
        var isInteger = value.IsInteger;

        if (typeof(TOther) == typeof(sbyte))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < sbyte.MinValue || integer > sbyte.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var sbyteValue = (sbyte)integer;
            result = Unsafe.As<sbyte, TOther>(ref sbyteValue);
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < byte.MinValue || integer > byte.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var byteValue = (byte)integer;
            result = Unsafe.As<byte, TOther>(ref byteValue);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < short.MinValue || integer > short.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var shortValue = (short)integer;
            result = Unsafe.As<short, TOther>(ref shortValue);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < ushort.MinValue || integer > ushort.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var ushortValue = (ushort)integer;
            result = Unsafe.As<ushort, TOther>(ref ushortValue);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < int.MinValue || integer > int.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var intValue = (int)integer;
            result = Unsafe.As<int, TOther>(ref intValue);
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < uint.MinValue || integer > uint.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var uintValue = (uint)integer;
            result = Unsafe.As<uint, TOther>(ref uintValue);
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < long.MinValue || integer > long.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var longValue = (long)integer;
            result = Unsafe.As<long, TOther>(ref longValue);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < ulong.MinValue || integer > ulong.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var ulongValue = (ulong)integer;
            result = Unsafe.As<ulong, TOther>(ref ulongValue);
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < Int128.MinValue || integer > Int128.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var int128Value = (Int128)integer;
            result = Unsafe.As<Int128, TOther>(ref int128Value);
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < UInt128.MinValue || integer > UInt128.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var uint128Value = (UInt128)integer;
            result = Unsafe.As<UInt128, TOther>(ref uint128Value);
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < nint.MinValue || integer > nint.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var nintValue = (nint)integer;
            result = Unsafe.As<nint, TOther>(ref nintValue);
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < nuint.MinValue || integer > nuint.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var nuintValue = (nuint)integer;
            result = Unsafe.As<nuint, TOther>(ref nuintValue);
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            if (!isInteger)
            {
                ThrowValueNotInteger();
            }

            if (integer < char.MinValue || integer > char.MaxValue)
            {
                ThrowValueOutsideDestinationTypeRange();
            }

            var charValue = (char)integer;
            result = Unsafe.As<char, TOther>(ref charValue);
            return true;
        }

        return false;
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowCannotConvertFrom<TOther>()
    {
        throw new NotSupportedException($"Cannot convert from {typeof(TOther)}.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowValueNotRepresentableByBigRational()
    {
        throw new OverflowException("Value is not representable by BigRational.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowValueOutsideDestinationTypeRange()
    {
        throw new OverflowException("Value is outside the range of the destination type.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowValueNotInteger()
    {
        throw new OverflowException("Value is not an integer.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowDenominatorZero()
    {
        throw new DivideByZeroException("The denominator of a BigRational cannot be zero.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowValueMustBeFinite()
    {
        throw new ArgumentException("Value must be finite.", "value");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowDecimalValueOutOfRange()
    {
        throw new OverflowException("Value outside of range of valid decimal values.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowMinMustBeLessThanOrEqualToMax()
    {
        throw new ArgumentException("min must be less than or equal to max.", "min");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowZeroToNegativePower()
    {
        throw new DivideByZeroException("Cannot raise zero to a negative power.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowCouldNotParseBigRationalSpan()
    {
        throw new FormatException("Could not parse value as a BigRational.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowCouldNotParseBigRationalString(string value)
    {
        throw new FormatException($"Could not parse \"{value}\" as a BigRational.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowTermsMustBePositive()
    {
        throw new ArgumentOutOfRangeException("terms", "Terms must be positive.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowValueMustBePositive()
    {
        throw new ArgumentOutOfRangeException("x", "Value must be positive.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static T ThrowObjectMustBeBigRational<T>()
    {
        throw new ArgumentException("Object must be of type BigRational.", "obj");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static T ThrowInvalidRoundingMode<T>()
    {
        throw new ArgumentOutOfRangeException("mode", "Invalid rounding mode.");
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowTickMustBeGreaterThanZero()
    {
        throw new ArgumentOutOfRangeException("tick", "Tick must be greater than zero.");
    }

    /// <inheritdoc cref="INumberBase{BigRational}" />
    public static bool TryConvertToSaturating<TOther>(BigRational value, out TOther result)
        where TOther : INumberBase<TOther>
    {
        result = default!;

        if (typeof(TOther) == typeof(BigRational))
        {
            result = Unsafe.As<BigRational, TOther>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            var truncated = TruncateToBigInteger(value);
            result = Unsafe.As<BigInteger, TOther>(ref truncated);
            return true;
        }

        var integer = TruncateToBigInteger(value);

        if (typeof(TOther) == typeof(sbyte))
        {
            sbyte sbyteValue;
            if (integer < sbyte.MinValue)
            {
                sbyteValue = sbyte.MinValue;
            }
            else if (integer > sbyte.MaxValue)
            {
                sbyteValue = sbyte.MaxValue;
            }
            else
            {
                sbyteValue = (sbyte)integer;
            }

            result = Unsafe.As<sbyte, TOther>(ref sbyteValue);
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            byte byteValue;
            if (integer < byte.MinValue)
            {
                byteValue = byte.MinValue;
            }
            else if (integer > byte.MaxValue)
            {
                byteValue = byte.MaxValue;
            }
            else
            {
                byteValue = (byte)integer;
            }

            result = Unsafe.As<byte, TOther>(ref byteValue);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            short shortValue;
            if (integer < short.MinValue)
            {
                shortValue = short.MinValue;
            }
            else if (integer > short.MaxValue)
            {
                shortValue = short.MaxValue;
            }
            else
            {
                shortValue = (short)integer;
            }

            result = Unsafe.As<short, TOther>(ref shortValue);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            ushort ushortValue;
            if (integer < ushort.MinValue)
            {
                ushortValue = ushort.MinValue;
            }
            else if (integer > ushort.MaxValue)
            {
                ushortValue = ushort.MaxValue;
            }
            else
            {
                ushortValue = (ushort)integer;
            }

            result = Unsafe.As<ushort, TOther>(ref ushortValue);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            int intValue;
            if (integer < int.MinValue)
            {
                intValue = int.MinValue;
            }
            else if (integer > int.MaxValue)
            {
                intValue = int.MaxValue;
            }
            else
            {
                intValue = (int)integer;
            }

            result = Unsafe.As<int, TOther>(ref intValue);
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            uint uintValue;
            if (integer < uint.MinValue)
            {
                uintValue = uint.MinValue;
            }
            else if (integer > uint.MaxValue)
            {
                uintValue = uint.MaxValue;
            }
            else
            {
                uintValue = (uint)integer;
            }

            result = Unsafe.As<uint, TOther>(ref uintValue);
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            long longValue;
            if (integer < long.MinValue)
            {
                longValue = long.MinValue;
            }
            else if (integer > long.MaxValue)
            {
                longValue = long.MaxValue;
            }
            else
            {
                longValue = (long)integer;
            }

            result = Unsafe.As<long, TOther>(ref longValue);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            ulong ulongValue;
            if (integer < ulong.MinValue)
            {
                ulongValue = ulong.MinValue;
            }
            else if (integer > ulong.MaxValue)
            {
                ulongValue = ulong.MaxValue;
            }
            else
            {
                ulongValue = (ulong)integer;
            }

            result = Unsafe.As<ulong, TOther>(ref ulongValue);
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            Int128 int128Value;
            if (integer < Int128.MinValue)
            {
                int128Value = Int128.MinValue;
            }
            else if (integer > Int128.MaxValue)
            {
                int128Value = Int128.MaxValue;
            }
            else
            {
                int128Value = (Int128)integer;
            }

            result = Unsafe.As<Int128, TOther>(ref int128Value);
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            UInt128 uint128Value;
            if (integer < UInt128.MinValue)
            {
                uint128Value = UInt128.MinValue;
            }
            else if (integer > UInt128.MaxValue)
            {
                uint128Value = UInt128.MaxValue;
            }
            else
            {
                uint128Value = (UInt128)integer;
            }

            result = Unsafe.As<UInt128, TOther>(ref uint128Value);
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            nint nintValue;
            if (integer < nint.MinValue)
            {
                nintValue = nint.MinValue;
            }
            else if (integer > nint.MaxValue)
            {
                nintValue = nint.MaxValue;
            }
            else
            {
                nintValue = (nint)integer;
            }

            result = Unsafe.As<nint, TOther>(ref nintValue);
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            nuint nuintValue;
            if (integer < nuint.MinValue)
            {
                nuintValue = nuint.MinValue;
            }
            else if (integer > nuint.MaxValue)
            {
                nuintValue = nuint.MaxValue;
            }
            else
            {
                nuintValue = (nuint)integer;
            }

            result = Unsafe.As<nuint, TOther>(ref nuintValue);
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            char charValue;
            if (integer < char.MinValue)
            {
                charValue = char.MinValue;
            }
            else if (integer > char.MaxValue)
            {
                charValue = char.MaxValue;
            }
            else
            {
                charValue = (char)integer;
            }

            result = Unsafe.As<char, TOther>(ref charValue);
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            var floatValue = (float)value;
            result = Unsafe.As<float, TOther>(ref floatValue);
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            var doubleValue = (double)value;
            result = Unsafe.As<double, TOther>(ref doubleValue);
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            var halfValue = (Half)(double)value;
            result = Unsafe.As<Half, TOther>(ref halfValue);
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            decimal decimalValue;
            if (value < DecimalMin)
            {
                decimalValue = decimal.MinValue;
            }
            else if (value > DecimalMax)
            {
                decimalValue = decimal.MaxValue;
            }
            else
            {
                decimalValue = (decimal)value;
            }

            result = Unsafe.As<decimal, TOther>(ref decimalValue);
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
            result = Unsafe.As<BigRational, TOther>(ref value);
            return true;
        }

        if (typeof(TOther) == typeof(BigInteger))
        {
            var truncated = TruncateToBigInteger(value);
            result = Unsafe.As<BigInteger, TOther>(ref truncated);
            return true;
        }

        var integer = TruncateToBigInteger(value);

        if (typeof(TOther) == typeof(sbyte))
        {
            var sbyteValue = (sbyte)WrapToSigned(integer, 8);
            result = Unsafe.As<sbyte, TOther>(ref sbyteValue);
            return true;
        }

        if (typeof(TOther) == typeof(byte))
        {
            var byteValue = (byte)WrapToUnsigned(integer, 8);
            result = Unsafe.As<byte, TOther>(ref byteValue);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            var shortValue = (short)WrapToSigned(integer, 16);
            result = Unsafe.As<short, TOther>(ref shortValue);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            var ushortValue = (ushort)WrapToUnsigned(integer, 16);
            result = Unsafe.As<ushort, TOther>(ref ushortValue);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            var intValue = (int)WrapToSigned(integer, 32);
            result = Unsafe.As<int, TOther>(ref intValue);
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            var uintValue = (uint)WrapToUnsigned(integer, 32);
            result = Unsafe.As<uint, TOther>(ref uintValue);
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            var longValue = (long)WrapToSigned(integer, 64);
            result = Unsafe.As<long, TOther>(ref longValue);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            var ulongValue = (ulong)WrapToUnsigned(integer, 64);
            result = Unsafe.As<ulong, TOther>(ref ulongValue);
            return true;
        }

        if (typeof(TOther) == typeof(Int128))
        {
            var int128Value = (Int128)WrapToSigned(integer, 128);
            result = Unsafe.As<Int128, TOther>(ref int128Value);
            return true;
        }

        if (typeof(TOther) == typeof(UInt128))
        {
            var uint128Value = (UInt128)WrapToUnsigned(integer, 128);
            result = Unsafe.As<UInt128, TOther>(ref uint128Value);
            return true;
        }

        if (typeof(TOther) == typeof(nint))
        {
            var bits = IntPtr.Size * 8;
            var nintValue = (nint)WrapToSigned(integer, bits);
            result = Unsafe.As<nint, TOther>(ref nintValue);
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            var bits = IntPtr.Size * 8;
            var nuintValue = (nuint)WrapToUnsigned(integer, bits);
            result = Unsafe.As<nuint, TOther>(ref nuintValue);
            return true;
        }

        if (typeof(TOther) == typeof(char))
        {
            var charValue = (char)WrapToUnsigned(integer, 16);
            result = Unsafe.As<char, TOther>(ref charValue);
            return true;
        }

        if (typeof(TOther) == typeof(float))
        {
            var floatValue = (float)value;
            result = Unsafe.As<float, TOther>(ref floatValue);
            return true;
        }

        if (typeof(TOther) == typeof(double))
        {
            var doubleValue = (double)value;
            result = Unsafe.As<double, TOther>(ref doubleValue);
            return true;
        }

        if (typeof(TOther) == typeof(Half))
        {
            var halfValue = (Half)(double)value;
            result = Unsafe.As<Half, TOther>(ref halfValue);
            return true;
        }

        if (typeof(TOther) == typeof(decimal))
        {
            decimal decimalValue;
            if (value < DecimalMin)
            {
                decimalValue = decimal.MinValue;
            }
            else if (value > DecimalMax)
            {
                decimalValue = decimal.MaxValue;
            }
            else
            {
                decimalValue = (decimal)value;
            }

            result = Unsafe.As<decimal, TOther>(ref decimalValue);
            return true;
        }

        return false;
    }

    private static bool TryParseCore(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out BigRational result)
    {
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
    /// If this <see cref="BigRational"/> is zero.
    /// </exception>
    public BigRational Reciprocal()
    {
        return new BigRational(this.denominator, this.numerator);
    }

    /// <summary>
    /// Converts the value of this instance to its string representation using the specified format and provider.
    /// </summary>
    /// <param name="format">
    /// A standard or custom format string that is applied to the numerator and denominator.
    /// </param>
    /// <param name="formatProvider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// The string representation in numerator/denominator form.
    /// </returns>
    /// <remarks>
    /// The format string is applied to both the numerator and denominator, and the result is always
    /// represented in numerator/denominator form.
    /// </remarks>
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
        return double.IsFinite(other) && this.Equals((BigRational)other);
    }

    /// <inheritdoc />
    public bool Equals(float other)
    {
        return float.IsFinite(other) && this.Equals((BigRational)other);
    }

    /// <inheritdoc />
    public bool Equals(Half other)
    {
        return Half.IsFinite(other) && this.Equals((BigRational)(double)other);
    }

    /// <inheritdoc />
    public bool Equals(Int128 other)
    {
        return this.Numerator.Equals((BigInteger)other * this.Denominator);
    }

    /// <inheritdoc />
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
    public bool Equals(nuint other)
    {
        return this.Equals((ulong)other);
    }

    /// <inheritdoc />
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
    public bool Equals(sbyte other)
    {
        return this.Numerator.Equals(other * this.Denominator);
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
        if (double.IsNaN(other))
        {
            return 1;
        }

        if (double.IsPositiveInfinity(other))
        {
            return -1;
        }

        if (double.IsNegativeInfinity(other))
        {
            return 1;
        }

        return this.CompareTo((BigRational)other);
    }

    /// <inheritdoc />
    public int CompareTo(float other)
    {
        return this.CompareTo((double)other);
    }

    /// <inheritdoc />
    public int CompareTo(Half other)
    {
        return this.CompareTo((double)other);
    }

    /// <inheritdoc />
    public int CompareTo(Int128 other)
    {
        return this.Numerator.CompareTo((BigInteger)other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(UInt128 other)
    {
        return this.Numerator.CompareTo((BigInteger)other * this.Denominator);
    }

    /// <inheritdoc />
    public int CompareTo(nint other)
    {
        return this.CompareTo((long)other);
    }

    /// <inheritdoc />
    public int CompareTo(nuint other)
    {
        return this.CompareTo((ulong)other);
    }

    /// <inheritdoc />
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
    public int CompareTo(sbyte other)
    {
        return this.Numerator.CompareTo(other * this.Denominator);
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentException">
    /// If <paramref name="obj"/> is not a <see cref="BigRational"/>.
    /// </exception>
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            BigRational other => this.CompareTo(other),
            _ => ThrowObjectMustBeBigRational<int>(),
        };
    }

    /// <inheritdoc />
    public int CompareTo(BigRational other)
    {
        return (this.Numerator * other.Denominator).CompareTo(other.Numerator * this.Denominator);
    }

    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified
    /// <see cref="BigRational"/> number.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> number.
    /// </param>
    /// <returns>
    /// The smallest integral number that is greater than or equal to the specified <see cref="BigRational"/> number.
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
    /// The size of the tick.
    /// </param>
    /// <returns>
    /// The smallest multiple of <paramref name="tick"/> that is greater than or equal to
    /// <paramref name="value"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
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
    /// The largest multiple of <paramref name="tick"/> that is less than or equal to
    /// <paramref name="value"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="tick"/> is less than or equal to zero.
    /// </exception>
    public static BigRational Floor(BigRational value, BigRational tick)
    {
        AssertValidTick(tick);
        var ticks = value / tick;
        return ticks.IsInteger ? value : FloorImpl(ticks) * tick;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest number that is a multiple of <paramref name="tick"/>.
    /// If <paramref name="value"/> is exactly halfway between two such numbers, <paramref name="mode"/>
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
    /// The specification of what to do when <paramref name="value"/> is exactly halfway between two numbers
    /// that are a multiple of <paramref name="tick"/>.
    /// </param>
    /// <returns>
    /// The value of <paramref name="value"/> rounded to a multiple of <paramref name="tick"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="tick"/> is less than or equal to zero.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="mode"/> is not a valid <see cref="MidpointRoundingMode"/> value.
    /// </exception>
    public static BigRational RoundToTick(BigRational value, BigRational tick, MidpointRoundingMode mode)
    {
        AssertValidRationalRounding(mode);
        AssertValidTick(tick);
        var ticks = value / tick;
        return ticks.IsInteger ? value : RoundImpl(ticks, mode) * tick;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest <see cref="BigInteger"/>.
    /// If <paramref name="value"/> is exactly halfway between two such numbers, <paramref name="mode"/>
    /// specifies the rounding method to use.
    /// </summary>
    /// <param name="value">
    /// The value to be rounded.
    /// </param>
    /// <param name="mode">
    /// The specification of what to do when <paramref name="value"/> is exactly halfway between two integer values.
    /// </param>
    /// <returns>
    /// The result of rounding <paramref name="value"/> to the nearest <see cref="BigInteger"/>.
    /// If <paramref name="value"/> is exactly halfway between two such numbers, <paramref name="mode"/>
    /// specifies the rounding method to use.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="mode"/> is not a valid <see cref="MidpointRoundingMode"/> value.
    /// </exception>
    public static BigInteger RoundToInt(BigRational value, MidpointRoundingMode mode)
    {
        AssertValidRationalRounding(mode);
        return value.IsInteger ? value.Numerator : RoundImpl(value, mode);
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral number. If <paramref name="value"/>
    /// is exactly halfway between two such numbers, <paramref name="mode"/> specifies the rounding method to use.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <param name="mode">
    /// The rounding methodology to use if the value is exactly halfway between two integral values.
    /// </param>
    /// <returns>
    /// The nearest integral number. If <paramref name="value"/>
    /// is exactly halfway between two such numbers, <paramref name="mode"/> specifies the rounding method to use.
    /// </returns>
    /// <remarks>
    /// This method assumes that <paramref name="value"/> is not an integral number.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="mode"/> is not a valid <see cref="MidpointRoundingMode"/> value.
    /// </exception>
    private static BigInteger RoundImpl(BigRational value, MidpointRoundingMode mode)
    {
        return mode switch
        {
            MidpointRoundingMode.ToEven => RoundToEvenImpl(value),
            MidpointRoundingMode.Up => RoundUpImpl(value),
            MidpointRoundingMode.Down => RoundDownImpl(value),
            MidpointRoundingMode.AwayFromZero => RoundAwayFromZeroImpl(value),
            MidpointRoundingMode.TowardZero => RoundTowardZeroImpl(value),
            _ => ThrowInvalidRoundingMode<BigInteger>(),
        };
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
        if (value.IsGreaterThanZero)
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
    /// The smallest integral number that is greater than or equal to the specified <see cref="BigRational"/> number.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger CeilingImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        if (value.IsLessThanZero)
        {
            return value.Numerator / value.Denominator;
        }

        return (value.Numerator / value.Denominator) + BigInteger.One;
    }

    /// <summary>
    /// Rounds <paramref name="value"/> to the nearest integral value. If <paramref name="value"/>
    /// is exactly halfway between two integral values, it is rounded up.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly halfway between two integral values, it is rounded up.
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
    /// is exactly halfway between two integral values, it is rounded down.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly halfway between two integral values, it is rounded down.
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
    /// is exactly halfway between two integral values, it is rounded toward zero.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly halfway between two integral values, it is rounded toward zero.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundTowardZeroImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        var fraction = value - floor;
        if (value.IsGreaterThanZero)
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
    /// is exactly halfway between two integral values, it is rounded away from zero.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly halfway between two integral values, it is rounded away from zero.
    /// </returns>
    /// <remarks>
    /// Assumes that <paramref name="value"/> is not an integer.
    /// </remarks>
    private static BigInteger RoundAwayFromZeroImpl(BigRational value)
    {
        Debug.Assert(!value.IsInteger, "value must not be an integer");
        var floor = FloorImpl(value);
        var fraction = value - floor;
        if (value.IsGreaterThanZero)
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
    /// is exactly halfway between two integral values, it is rounded to the nearest even integral value.
    /// </summary>
    /// <param name="value">
    /// The value to round.
    /// </param>
    /// <returns>
    /// The nearest integral value. If <paramref name="value"/>
    /// is exactly halfway between two integral values, it is rounded to the nearest even integral value.
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
    /// Validates a midpoint rounding mode.
    /// </summary>
    /// <param name="mode">
    /// The midpoint rounding mode to validate.
    /// </param>
    private static void AssertValidRationalRounding(MidpointRoundingMode mode)
    {
        if (!Enum.IsDefined(mode))
        {
            ThrowInvalidRoundingMode<object>();
        }
    }

    /// <summary>
    /// Validates a tick value.
    /// </summary>
    /// <param name="tick">
    /// The tick value to validate.
    /// </param>
    private static void AssertValidTick(BigRational tick)
    {
        if (!tick.IsGreaterThanZero)
        {
            ThrowTickMustBeGreaterThanZero();
        }
    }

    /// <summary>
    /// Checks whether the number is greater than or equal to one half.
    /// </summary>
    /// <returns>
    /// true if the number is greater than or equal to one half; otherwise, false.
    /// </returns>
    private bool IsGreaterThanOrEqualToHalf()
    {
        return this.Numerator * BigIntegerTwo >= this.Denominator;
    }

    /// <summary>
    /// Checks whether the number is greater than one half.
    /// </summary>
    /// <returns>
    /// true if the number is greater than one half; otherwise, false.
    /// </returns>
    private bool IsGreaterThanHalf()
    {
        return this.Numerator * BigIntegerTwo > this.Denominator;
    }

    /// <summary>
    /// Compares the number to one half.
    /// </summary>
    /// <returns>
    /// A negative value if the number is less than one half,
    /// zero if the number is equal to one half,
    /// and a positive value if the number is greater than one half.
    /// </returns>
    private int CompareToHalf()
    {
        return (this.Numerator * BigIntegerTwo).CompareTo(this.Denominator);
    }
}
