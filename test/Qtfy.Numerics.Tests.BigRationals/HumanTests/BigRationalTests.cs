// <copyright file="BigRationalTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals;

using System;
using System.Numerics;
using System.Collections.Generic;
using NUnit.Framework;

internal partial class BigRationalTests
{
    public static IEnumerable<object[]> ConstantTests()
    {
        yield return [BigRational.One, BigInteger.One, BigInteger.One];
        yield return [BigRational.NegativeOne, BigInteger.MinusOne, BigInteger.One];
        yield return [BigRational.Zero, BigInteger.Zero, BigInteger.One];
    }

    [TestCaseSource(nameof(ConstantTests))]
    public void TestConstant(BigRational x, BigInteger numerator, BigInteger denominator)
    {
        Assert.That(BigRational.One.Numerator, Is.EqualTo(BigInteger.One));
        Assert.That(BigRational.One.Denominator, Is.EqualTo(BigInteger.One));
    }

    /// <summary>
    /// Test that the default initialized <see cref="BigRational"/> is equal to (0/1).
    /// </summary>
    [Test]
    public void DefaultInitialize()
    {
        BigRational rational = default;
        Assert.That(rational.Numerator, Is.EqualTo(BigInteger.Zero));
        Assert.That(rational.Denominator, Is.EqualTo(BigInteger.One));
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
        Assert.That(rational.Numerator, Is.EqualTo((BigInteger)numerator));
        Assert.That(rational.Denominator, Is.EqualTo(BigInteger.One));
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
        Assert.That(rational.Numerator, Is.EqualTo((BigInteger)n2));
        Assert.That(rational.Denominator, Is.EqualTo((BigInteger)d2));
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
        Assert.That(new BigRational(n, d).Sign, Is.EqualTo(expected));
    }

    [TestCase(1, 2, true)]
    [TestCase(-1, 2, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 1, true)]
    [TestCase(-1, 1, false)]
    public void IsPositive(int n, int d, bool expected)
    {
        Assert.That(new BigRational(n, d).IsPositive, Is.EqualTo(expected));
    }

    [TestCase(1, 2, false)]
    [TestCase(-1, 2, true)]
    [TestCase(0, 1, false)]
    [TestCase(1, 1, false)]
    [TestCase(-1, 1, true)]
    public void IsNegative(int n, int d, bool expected)
    {
        Assert.That(new BigRational(n, d).IsNegative, Is.EqualTo(expected));
    }

    [TestCase(1, 2, false)]
    [TestCase(-1, 2, false)]
    [TestCase(0, 1, true)]
    [TestCase(1, 1, false)]
    [TestCase(-1, 1, false)]
    public void IsZero(int n, int d, bool expected)
    {
        Assert.That(new BigRational(n, d).IsZero, Is.EqualTo(expected));
    }

    [TestCase(1, 2, false)]
    [TestCase(-1, 2, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 1, true)]
    [TestCase(-1, 1, false)]
    public void IsOne(int n, int d, bool expected)
    {
        Assert.That(new BigRational(n, d).IsOne, Is.EqualTo(expected));
    }

    [TestCase(1, 2, false)]
    [TestCase(-1, 2, false)]
    [TestCase(0, 1, false)]
    [TestCase(1, 1, false)]
    [TestCase(-1, 1, true)]
    public void IsMinusOne(int n, int d, bool expected)
    {
        Assert.That(new BigRational(n, d).IsMinusOne, Is.EqualTo(expected));
    }

    [TestCase(1, 2, false)]
    [TestCase(-1, 2, false)]
    [TestCase(0, 1, true)]
    [TestCase(1, 1, true)]
    [TestCase(-1, 1, true)]
    public void IsInteger(int n, int d, bool expected)
    {
        Assert.That(new BigRational(n, d).IsInteger, Is.EqualTo(expected));
    }

    [Test]
    public void Deconstruct()
    {
        var (n, d) = new BigRational(7, 3);

        Assert.That(n, Is.EqualTo((BigInteger)7));
        Assert.That(d, Is.EqualTo((BigInteger)3));
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
    [TestCase(0, 1, 0, 1, 1)]
    [TestCase(0, 1, 1, 0, 1)]
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
        Assert.Throws<DivideByZeroException>(
            () => BigRational.Pow(0, -1));
    }

    [TestCase(1, 2, "1/2")]
    [TestCase(-1, 2, "-1/2")]
    [TestCase(1, -2, "-1/2")]
    public void TestToString(int numerator, int denominator, string expected)
    {
        Assert.That(new BigRational(numerator, denominator).ToString(), Is.EqualTo(expected));
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
    [TestCase("123/0", 0, 0, false)]
    [TestCase("xyz", 0, 0, false)]
    public void TestTryParse(string input, int numerator, int denominator, bool expectedSuccess)
    {
        var expectedRational = expectedSuccess
            ? new BigRational(numerator, denominator)
            : default;

        var actualSuccess = BigRational.TryParse(input, out var actualRational);

        Assert.That(actualSuccess, Is.EqualTo(expectedSuccess));
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
        Assert.That(new BigRational(1, 2).GetHashCode(), Is.EqualTo(new BigRational(2, 4).GetHashCode()));
        Assert.That(default(BigRational).GetHashCode(), Is.EqualTo(new BigRational(0).GetHashCode()));
        Assert.That(default(BigRational).GetHashCode(),  Is.Not.EqualTo(new BigRational(1).GetHashCode()));
    }

    private static void AssertCanonical(BigRational rational)
    {
        Assert.That(rational.Denominator > BigRational.Zero, Is.True);
        var n = BigInteger.Abs(rational.Numerator);
        var d = BigInteger.Abs(rational.Denominator);
        var gcd = BigInteger.GreatestCommonDivisor(n, d);
        Assert.That(n / gcd, Is.EqualTo(n));
        Assert.That(d / gcd, Is.EqualTo(d));
    }

    private static void AssertEqual(BigRational left, BigRational right)
    {
        AssertCanonical(left);
        AssertCanonical(right);
        Assert.That(left.Denominator, Is.EqualTo(right.Denominator));
        Assert.That(left.Numerator, Is.EqualTo(right.Numerator));
    }
}
