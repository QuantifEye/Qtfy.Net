// <copyright file="BigRationalTests.Conversions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
    using System.Globalization;
    using System.Numerics;
    using NUnit.Framework;

    public partial class BigRationalTests
    {
        private static decimal ParseDecimal(string str)
        {
            return decimal.Parse(str, CultureInfo.InvariantCulture);
        }

        [TestCase("0.125", "1/8")]
        [TestCase("0.0125", "1/80")]
        [TestCase("-0.125", "-1/8")]
        [TestCase("-0.0125", "-1/80")]
        public void DecimalToRational(string dec, string expected)
        {
            AssertEqual(
                BigRational.Parse(expected),
                (BigRational)ParseDecimal(dec));
        }

        [TestCase("1005/10", "100.5")]
        [TestCase("80", "80")]
        [TestCase("1/8", "0.125")]
        [TestCase("1/80", "0.0125")]
        [TestCase("-1/8", "-0.125")]
        [TestCase("-1/80", "-0.0125")]
        [TestCase("-1", "-1")]
        [TestCase("1", "1")]
        [TestCase("2", "2")]
        public void RationalToDecimalExact(string rational, string expected)
        {
            Assert.AreEqual(
                ParseDecimal(expected),
                (decimal)BigRational.Parse(rational));
        }

        [TestCase("2/3", "0.6666666666666666666666666667")]
        [TestCase("-2/3", "-0.6666666666666666666666666667")]
        [TestCase("1/3", "0.3333333333333333333333333333")]
        [TestCase("-1/3", "-0.3333333333333333333333333333")]
        public void RationalToDecimalWithRecurringDigit(string rational, string expected)
        {
            Assert.AreEqual(
                ParseDecimal(expected),
                (decimal)BigRational.Parse(rational));
        }

        [TestCase("0.0000000000000000000000000005", "0")]
        [TestCase("0.0000000000000000000000000015", "0.0000000000000000000000000002")]
        [TestCase("0.0000000000000000000000000025", "0.0000000000000000000000000002")]
        [TestCase("-0.0000000000000000000000000005", "0")]
        [TestCase("-0.0000000000000000000000000015", "-0.0000000000000000000000000002")]
        [TestCase("-0.0000000000000000000000000025", "-0.0000000000000000000000000002")]
        public void RationalToDecimalWithRounding(string rationalAsDecimal, string expected)
        {
            var r = (BigRational)ParseDecimal(rationalAsDecimal);
            var actual = (decimal)(r / 10);
            Assert.AreEqual(
                ParseDecimal(expected),
                actual);
        }

        [Test]
        public void TestToDecimalOutOfRange()
        {
            decimal dummy;
            BigRational rational = BigInteger.Pow(10, 30);
            Assert.Throws<OverflowException>(
                () => dummy = (decimal)rational);

            BigRational negative = -rational;
            Assert.Throws<OverflowException>(
                () => dummy = (decimal)negative);
        }

        [TestCase(0.125d, 1, 8)]
        [TestCase(0.25d, 1, 4)]
        [TestCase(-0.125d, -1, 8)]
        [TestCase(-0.25d, -1, 4)]
        public void FromDouble(double value, int numerator, int denominator)
        {
            var actual = (BigRational)value;
            var expected = new BigRational(numerator, denominator);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0.125f, 1, 8)]
        [TestCase(0.25f, 1, 4)]
        [TestCase(-0.125f, -1, 8)]
        [TestCase(-0.25f, -1, 4)]
        public void FromFloat(float value, int numerator, int denominator)
        {
            var actual = (BigRational)value;
            var expected = new BigRational(numerator, denominator);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0f)]
        [TestCase(1f)]
        [TestCase(2f)]
        [TestCase(-1f)]
        [TestCase(-2f)]
        [TestCase(0.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.25f)]
        [TestCase(-0.25f)]
        [TestCase(0.1f)]
        [TestCase(-0.1f)]
        [TestCase(0.000000000000000000005f)]
        [TestCase(-0.000000000000000000005f)]
        [TestCase(0.000000000000000000001f)]
        [TestCase(-0.000000000000000000001f)]
        public void CastToFloatRoundTrip(float expected)
        {
            var actual = (float)(BigRational)expected;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0d)]
        [TestCase(1d)]
        [TestCase(2d)]
        [TestCase(-1d)]
        [TestCase(-2d)]
        [TestCase(0.5d)]
        [TestCase(-0.5d)]
        [TestCase(0.000000000000000000005d)]
        [TestCase(-0.000000000000000000005d)]
        [TestCase(0.000000000000000000001d)]
        [TestCase(-0.000000000000000000001d)]
        public void CastToDoubleRoundTrip(double expected)
        {
            var actual = (double)(BigRational)expected;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(0f)]
        [TestCase(1f)]
        [TestCase(2f)]
        [TestCase(-1f)]
        [TestCase(-2f)]
        [TestCase(0.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.000000000000000000005f)]
        [TestCase(-0.000000000000000000005f)]
        [TestCase(0.000000000000000000001f)]
        [TestCase(-0.000000000000000000001f)]
        public void CastToFloatRoundTrip(double expected)
        {
            var actual = (double)(BigRational)expected;
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1.0000000000000002, 4)]
        [TestCase(1.0000000000000002, 3)]
        [TestCase(1d, 2)]
        [TestCase(1d, 1)]
        [TestCase(1d, 0)]
        [TestCase(1d, -1)]
        [TestCase(0.99999999999999989, -2)]
        [TestCase(0.99999999999999978, -3)]
        [TestCase(0.99999999999999978, -4)]
        public void CastToDoubleRounding(double expected, int shift)
        {
            var two54 = BigInteger.Pow(2, 54);
            Assert.AreEqual(expected, (double)new BigRational(two54 + shift, two54));
        }

        [Test]
        public void CastToDoubleRoundToInfinity()
        {
            BigRational toRound = BigInteger.Pow(2, 1024) - BigInteger.One;
            Assert.AreEqual(
                double.PositiveInfinity,
                (double)toRound);

            Assert.AreEqual(
                double.NegativeInfinity,
                (double)-toRound);
        }

        [Test]
        public void CastToDoubleOverflow()
        {
            BigRational doubleMax = double.MaxValue;
            BigRational positiveOverflowValue = doubleMax * doubleMax;
            BigRational negativeOverflowValue = -positiveOverflowValue;
            AssertBitEqual(double.PositiveInfinity, (double)positiveOverflowValue);
            AssertBitEqual(double.NegativeInfinity, (double)negativeOverflowValue);
        }

        [Test]
        public void CastToFloatOverflow()
        {
            BigRational floatMax = float.MaxValue;
            BigRational positiveOverflowValue = floatMax * floatMax;
            BigRational negativeOverflowValue = -positiveOverflowValue;
            AssertBitEqual(float.PositiveInfinity, (float)positiveOverflowValue);
            AssertBitEqual(float.NegativeInfinity, (float)negativeOverflowValue);
        }

        [Test]
        public void CastToDoubleUnderflow()
        {
            BigRational doubleEpsilon = double.Epsilon;
            BigRational positiveUnderflow = doubleEpsilon * doubleEpsilon;
            BigRational negativeUnderflow = -positiveUnderflow;
            AssertBitEqual(0.0d, (double)positiveUnderflow);
            AssertBitEqual(-0.0d, (double)negativeUnderflow);
        }

        [Test]
        public void CastToFloatUnderflow()
        {
            BigRational floatEpsilon = float.Epsilon;
            BigRational positiveUnderflow = floatEpsilon * floatEpsilon;
            BigRational negativeUnderflow = -positiveUnderflow;
            AssertBitEqual(0.0f, (float)positiveUnderflow);
            AssertBitEqual(-0.0f, (float)negativeUnderflow);
        }

        private static void AssertBitEqual(double left, double right)
        {
            Assert.AreEqual(BitConverter.DoubleToInt64Bits(left), BitConverter.DoubleToInt64Bits(right));
        }

        private static void AssertBitEqual(float left, float right)
        {
            Assert.AreEqual(BitConverter.SingleToInt32Bits(left), BitConverter.SingleToInt32Bits(right));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(-2)]
        public void FromSignedInteger(int value)
        {
            var expected = new BigRational(value);
            AssertEqual(expected, (long)value);
            AssertEqual(expected, value);
            AssertEqual(expected, (short)value);
            AssertEqual(expected, (sbyte)value);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void FromUnsignedInteger(int value)
        {
            var expected = new BigRational(value);
            AssertEqual(expected, (uint)value);
            AssertEqual(expected, (ushort)value);
            AssertEqual(expected, (byte)value);
            AssertEqual(expected, (sbyte)value);
        }
    }
}
