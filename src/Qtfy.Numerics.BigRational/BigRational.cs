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

/// <summary>
/// A structure that represents a rational number with an arbitrarily large numerator and denominator.
/// </summary>
public partial struct BigRational :
    IEquatable<BigRational>,
    IEquatable<BigInteger>,
    IEquatable<ulong>,
    IEquatable<long>,
    IEquatable<uint>,
    IEquatable<int>,
    IEquatable<ushort>,
    IEquatable<short>,
    IEquatable<byte>,
    IEquatable<sbyte>,
    IComparable<BigRational>,
    IComparable<BigInteger>,
    IComparable<ulong>,
    IComparable<long>,
    IComparable<uint>,
    IComparable<int>,
    IComparable<ushort>,
    IComparable<short>,
    IComparable<byte>,
    IComparable<sbyte>
{
    /// <summary>
    /// A value representing 1/1.
    /// </summary>
    public static readonly BigRational One = new BigRational(1);

    /// <summary>
    /// A value representing 0/1.
    /// </summary>
    public static readonly BigRational Zero = new BigRational(0);

    /// <summary>
    /// A value representing -1/1.
    /// </summary>
    public static readonly BigRational MinusOne = new BigRational(-1);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Numerator.Sign;
    }

    /// <summary>
    /// Gets a value indicating whether the numerator is positive.
    /// </summary>
    public bool IsPositive
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Numerator.Sign == 1;
    }

    /// <summary>
    /// Gets a value indicating whether the numerator is negative.
    /// </summary>
    public bool IsNegative
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Numerator.Sign == -1;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to zero (0 / 1).
    /// </summary>
    public bool IsZero
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Numerator.IsZero;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to one (1/1).
    /// </summary>
    public bool IsOne
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Numerator.IsOne && this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> is equal to minus one (-1/1).
    /// </summary>
    public bool IsMinusOne
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.Numerator == BigInteger.MinusOne && this.Denominator.IsOne;
    }

    /// <summary>
    /// Gets a value indicating whether this <see cref="BigRational"/> can be represented as an integer (x/1).
    /// </summary>
    public bool IsInteger
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    /// <summary>
    /// Calculates the absolute value of a <see cref="BigRational"/>.
    /// </summary>
    /// <param name="value">
    /// A <see cref="BigRational"/> value.
    /// </param>
    /// <returns>
    /// The absolute value of <paramref name="value"/>.
    /// </returns>
    public static BigRational Abs(BigRational value)
    {
        return value.IsNegative ? -value : value;
    }

    /// <summary>
    /// Returns the greater of two <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// The greater of <paramref name="left"/> and <paramref name="right"/>.
    /// </returns>
    public static BigRational Max(BigRational left, BigRational right)
    {
        return left < right ? right : left;
    }

    /// <summary>
    /// Returns the lesser of two <see cref="BigRational"/> values.
    /// </summary>
    /// <param name="left">
    /// The first value to compare.
    /// </param>
    /// <param name="right">
    /// The second value to compare.
    /// </param>
    /// <returns>
    /// The greater of <paramref name="left"/> and <paramref name="right"/>.
    /// </returns>
    public static BigRational Min(BigRational left, BigRational right)
    {
        return left > right ? right : left;
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
        ArgumentNullException.ThrowIfNull(value);

        var s = value.Split('/');
        switch (s.Length)
        {
            case 1:
                return new BigRational(BigInteger.Parse(value, CultureInfo.InvariantCulture));
            case 2:
                var n = BigInteger.Parse(s[0], CultureInfo.InvariantCulture);
                var d = BigInteger.Parse(s[1], CultureInfo.InvariantCulture);
                if (d.IsZero)
                {
                    break;
                }

                return new BigRational(n, d);
        }

        throw new FormatException($"Could not parse \"{value}\" as a BigRational.");
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
        if (value is null)
        {
            rational = default;
            return false;
        }

        var s = value.Split('/');
        switch (s.Length)
        {
            case 1 when BigInteger.TryParse(value, out var bigint):
                rational = new BigRational(bigint);
                return true;
            case 2 when BigInteger.TryParse(s[0], out var num) && BigInteger.TryParse(s[1], out var den):
                if (den.IsZero)
                {
                    rational = default;
                    return false;
                }

                rational = new BigRational(num, den);
                return true;
            default:
                rational = default;
                return false;
        }
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

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{this.Numerator}/{this.Denominator}";
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
