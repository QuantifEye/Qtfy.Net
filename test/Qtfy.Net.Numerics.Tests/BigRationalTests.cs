// <copyright file="BigRationalTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
    using System.Numerics;
    using NUnit.Framework;

    public partial class BigRationalTests
    {
        [Test]
        public void TestOneConstant()
        {
            Assert.AreEqual(BigInteger.One, BigRational.One.Numerator);
            Assert.AreEqual(BigInteger.One, BigRational.One.Denominator);
        }

        [Test]
        public void TestMinusOneConstant()
        {
            Assert.AreEqual(BigInteger.MinusOne, BigRational.MinusOne.Numerator);
            Assert.AreEqual(BigInteger.One, BigRational.MinusOne.Denominator);
        }

        [Test]
        public void TestZeroConstant()
        {
            Assert.AreEqual(BigInteger.Zero, BigRational.Zero.Numerator);
            Assert.AreEqual(BigInteger.One, BigRational.Zero.Denominator);
        }

        /// <summary>
        /// Test that the default initialized <see cref="BigRational"/> is equal to (0/1).
        /// </summary>
        [Test]
        public void DefaultInitialize()
        {
            BigRational rational = default;
            Assert.AreEqual(BigInteger.Zero, rational.Numerator);
            Assert.AreEqual(BigInteger.One, rational.Denominator);
        }

        /// <summary>
        /// Test that a <see cref="BigRational"/> is constructed correctly from only a numerator.
        /// </summary>
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void ConstructFromNumerator(int numerator)
        {
            var rational = new BigRational(numerator);
            Assert.AreEqual((BigInteger)numerator, rational.Numerator);
            Assert.AreEqual(BigInteger.One, rational.Denominator);
        }

        /// <summary>
        /// Tests that a positive <see cref="BigRational"/> has a positive numerator and a positive denominator.
        /// </summary>
        [TestCase(1, 2, 1, 2)]
        [TestCase(-1, -2, 1, 2)]
        [TestCase(-1, 2, -1, 2)]
        [TestCase(1, -2, -1, 2)]

        [TestCase(2, 4, 1, 2)]
        [TestCase(-2, -4, 1, 2)]
        [TestCase(-2, 4, -1, 2)]
        [TestCase(2, -4, -1, 2)]

        [TestCase(2, 1, 2, 1)]
        [TestCase(-2, -1, 2, 1)]
        [TestCase(-2, 1, -2, 1)]
        [TestCase(2, -1, -2, 1)]

        [TestCase(4, 2, 2, 1)]
        [TestCase(-4, -2, 2, 1)]
        [TestCase(-4, 2, -2, 1)]
        [TestCase(4, -2, -2, 1)]
        public void Construct(int n1, int d1, int n2, int d2)
        {
            var rational = new BigRational(n1, d1);
            AssertCanonical(rational);
            Assert.AreEqual(rational.Numerator, (BigInteger)n2);
            Assert.AreEqual(rational.Denominator, (BigInteger)d2);
        }

        /// <summary>
        /// Tests that the constructor throws a <see cref="DivideByZeroException"/> if constructed with
        /// a zero denominator.
        /// </summary>
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void ConstructInvalid(int numerator)
        {
            Assert.Throws<DivideByZeroException>(
                () => new BigRational(numerator, 0));
        }

        [TestCase(1, 2, 1)]
        [TestCase(-1, 2, -1)]
        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(-1, 1, -1)]
        public void Sign(int n, int d, int expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).Sign);
        }

        [TestCase(1, 2, true)]
        [TestCase(-1, 2, false)]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(-1, 1, false)]
        public void IsPositive(int n, int d, bool expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).IsPositive);
        }

        [TestCase(1, 2, false)]
        [TestCase(-1, 2, true)]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, false)]
        [TestCase(-1, 1, true)]
        public void IsNegative(int n, int d, bool expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).IsNegative);
        }

        [TestCase(1, 2, false)]
        [TestCase(-1, 2, false)]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, false)]
        [TestCase(-1, 1, false)]
        public void IsZero(int n, int d, bool expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).IsZero);
        }

        [TestCase(1, 2, false)]
        [TestCase(-1, 2, false)]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(-1, 1, false)]
        public void IsOne(int n, int d, bool expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).IsOne);
        }

        [TestCase(1, 2, false)]
        [TestCase(-1, 2, false)]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, false)]
        [TestCase(-1, 1, true)]
        public void IsMinusOne(int n, int d, bool expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).IsMinusOne);
        }

        [TestCase(1, 2, false)]
        [TestCase(-1, 2, false)]
        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(-1, 1, true)]
        public void IsInteger(int n, int d, bool expected)
        {
            Assert.AreEqual(expected, new BigRational(n, d).IsInteger);
        }

        [Test]
        public void Deconstruct()
        {
            var (n, d) = new BigRational(7, 3);
            Assert.AreEqual((BigInteger)7, n);
            Assert.AreEqual((BigInteger)3, d);
        }

        [TestCase(1, 2)]
        [TestCase(-1, 2)]
        public void Abs(int n, int d)
        {
            var actual = BigRational.Abs(new BigRational(n, d));
            var expected = new BigRational(Math.Abs(n), Math.Abs(d));
            AssertEqual(expected, actual);
        }

        [TestCase(1, 2)]
        [TestCase(-1, 2)]
        public void Reciprocal(int n1, int n2)
        {
            var rational = new BigRational(n1, n2);
            var expected = new BigRational(n2, n1);
            AssertEqual(expected, rational.Reciprocal());
        }

        [Test]
        public void ReciprocalZero()
        {
            var rational = new BigRational(0, 1);
            Assert.Throws<DivideByZeroException>(
                () => rational.Reciprocal());
        }

        [TestCase(1, 4, 1, 2)]
        [TestCase(-1, 2, 1, 4)]
        [TestCase(-1, 2, -1, 4)]
        public void MinAndMax(int minNumerator, int minDenominator, int maxNumerator, int maxDenominator)
        {
            var maximum = new BigRational(maxNumerator, maxDenominator);
            var minimum = new BigRational(minNumerator, minDenominator);
            AssertEqual(maximum, BigRational.Max(minimum, maximum));
            AssertEqual(maximum, BigRational.Max(maximum, minimum));
            AssertEqual(minimum, BigRational.Min(minimum, maximum));
            AssertEqual(minimum, BigRational.Min(maximum, minimum));
        }

        [TestCase(1, 1, 1, 1, 1)]
        [TestCase(1, 1, 0, 1, 1)]
        [TestCase(1, 2, 2, 1, 4)]
        [TestCase(-1, 1, 1, -1, 1)]
        [TestCase(-1, 1, 0, 1, 1)]
        [TestCase(-1, 2, 2, 1, 4)]
        [TestCase(-1, 2, 3, -1, 8)]
        [TestCase(-1, 2, 0, 1, 1)]
        [TestCase(0, 1, 1, 1, 1)]
        [TestCase(10, 1, 0, 1, 1)]
        public void Pow(int n, int d, int power, int expectedNumerator, int expectedDenominator)
        {
            var rational = new BigRational(n, d);
            var expected = new BigRational(expectedNumerator, expectedDenominator);
            AssertEqual(expected, BigRational.Pow(rational, power));
        }

        [Test]
        public void PowError()
        {
            Assert.Throws<ArgumentException>(
                () => BigRational.Pow(0, 0));
        }

        [TestCase(1, 2, "1/2")]
        [TestCase(-1, 2, "-1/2")]
        [TestCase(1, -2, "-1/2")]
        public void TestToString(int numerator, int denominator, string expected)
        {
            Assert.AreEqual(expected, new BigRational(numerator, denominator).ToString());
        }

        [TestCase("-123/456", -123, 456)]
        [TestCase("123/456", 123, 456)]
        [TestCase("123", 123, 1)]
        public void TestParseSuccessful(string from, int numerator, int denominator)
        {
            var expected = new BigRational(numerator, denominator);
            var actual = BigRational.Parse(from);
            AssertEqual(expected, actual);
        }

        [Test]
        public void TestParseNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => BigRational.Parse(null));
        }

        [TestCase("xyz")]
        [TestCase("123/0")]
        public void TestParseUnsuccessful(string from)
        {
            Assert.Throws<FormatException>(
                () => BigRational.Parse(from));
        }

        [TestCase("123", 123, 1, true)]
        [TestCase("123/456", 123, 456, true)]
        [TestCase("xyz", 0, 0, false)]
        public void TestTryParse(string input, int numerator, int denominator, bool expectedSuccess)
        {
            var expectedRational = expectedSuccess
                ? new BigRational(numerator, denominator)
                : default;

            var actualSuccess = BigRational.TryParse(input, out var actualRational);

            Assert.AreEqual(expectedSuccess, actualSuccess);
            AssertEqual(expectedRational, actualRational);
        }

        [Test]
        public void TryParseNull()
        {
            if (BigRational.TryParse(null, out _))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetHashCodeEqual()
        {
            Assert.AreEqual(
                new BigRational(1, 2).GetHashCode(),
                new BigRational(2, 4).GetHashCode());

            Assert.AreEqual(
                default(BigRational).GetHashCode(),
                new BigRational(0).GetHashCode());

            Assert.AreNotEqual(
                default(BigRational).GetHashCode(),
                new BigRational(1).GetHashCode());
        }

        private static void AssertCanonical(BigRational rational)
        {
            Assert.True(rational.Denominator > BigRational.Zero);
            var n = BigInteger.Abs(rational.Numerator);
            var d = BigInteger.Abs(rational.Denominator);
            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            Assert.AreEqual(n, n / gcd);
            Assert.AreEqual(d, d / gcd);
        }

        private static void AssertEqual(BigRational left, BigRational right)
        {
            AssertCanonical(left);
            AssertCanonical(right);
            Assert.AreEqual(left.Numerator, right.Numerator);
            Assert.AreEqual(left.Denominator, right.Denominator);
        }
    }
}
