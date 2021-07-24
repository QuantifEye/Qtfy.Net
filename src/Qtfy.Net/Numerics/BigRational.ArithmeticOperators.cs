// <copyright file="BigRational.ArithmeticOperators.cs" company="QuantifEye">
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
    public partial struct BigRational
    {
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
    }
}
