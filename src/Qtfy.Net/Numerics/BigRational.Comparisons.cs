// <copyright file="BigRational.Comparisons.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    using System;
    using System.Numerics;

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

        /// <inheritdoc />
        public override bool Equals(object obj)
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
    }
}
