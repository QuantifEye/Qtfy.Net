// <copyright file="BigRational.Rounding.cs" company="QuantifEye">
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
        private static readonly BigInteger BigIntegerTwo = new (2);

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
    }
}
