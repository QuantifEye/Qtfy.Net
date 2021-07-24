// <copyright file="BigRational.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    using System;
    using System.Globalization;
    using System.Numerics;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// A structure that represents a rational number with an arbitrarily large numerator and denominator.
    /// </summary>
    public partial struct BigRational
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
        public static BigRational Pow(BigRational value, int exp)
        {
            if (value.IsZero)
            {
                return exp == 0
                    ? throw new ArgumentException("Cannot calculate 0 to the power zero.")
                    : new BigRational(BigInteger.One);
            }
            else if (exp == 0)
            {
                return new BigRational(BigInteger.One);
            }
            else if (exp > 0)
            {
                return new BigRational(
                    numerator: BigInteger.Pow(value.Numerator, exp),
                    denominator: BigInteger.Pow(value.Denominator, exp));
            }
            else
            {
                exp = -exp;
                return new BigRational(
                    numerator: BigInteger.Pow(value.Denominator, exp),
                    denominator: BigInteger.Pow(value.Numerator, exp));
            }
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
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

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
    }
}
