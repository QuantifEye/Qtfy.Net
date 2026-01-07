// <copyright file="BigRationalAiTests.Construction.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals.AiTests;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void DefaultIsZeroOverOne()
    {
        BigRational value = default;
        AssertRational(value, BigInteger.Zero, BigInteger.One);
        Assert.That(value.IsZero, Is.True);
        Assert.That(value.IsInteger, Is.True);
        Assert.That(value.Sign, Is.EqualTo(0));
        Assert.That(value.IsPositive, Is.False);
        Assert.That(value.IsNegative, Is.False);
        Assert.That(value.IsOne, Is.False);
        Assert.That(value.IsMinusOne, Is.False);
    }

    [Test]
    public void ConstructorWithZeroDenominatorThrows()
    {
        Assert.Throws<DivideByZeroException>(() => _ = new BigRational(1, 0));
    }

    [Test]
    public void ConstructorNormalizesZeroNumerator()
    {
        var value = new BigRational(0, 17);
        AssertRational(value, BigInteger.Zero, BigInteger.One);
        AssertCanonical(value);
    }

    [TestCase(6, 8, 3, 4)]
    [TestCase(-6, 8, -3, 4)]
    [TestCase(6, -8, -3, 4)]
    [TestCase(-6, -8, 3, 4)]
    public void ConstructorReducesAndNormalizesSign(long n, long d, long expectedN, long expectedD)
    {
        var value = new BigRational(n, d);
        AssertRational(value, expectedN, expectedD);
        AssertCanonical(value);
    }

    [Test]
    public void ConstantsAreStable()
    {
        AssertRational(BigRational.Zero, BigInteger.Zero, BigInteger.One);
        AssertRational(BigRational.One, BigInteger.One, BigInteger.One);
        AssertRational(BigRational.NegativeOne, BigInteger.MinusOne, BigInteger.One);
        Assert.That(BigRational.AdditiveIdentity, Is.EqualTo(BigRational.Zero));
        Assert.That(BigRational.MultiplicativeIdentity, Is.EqualTo(BigRational.One));
        Assert.That(BigRational.Radix, Is.EqualTo(2));
    }

    [Test]
    public void PropertyFlagsReflectSignAndIntegralStatus()
    {
        var positive = R(3, 2);
        var negative = R(-3, 2);
        var integer = new BigRational(5);

        Assert.That(positive.IsPositive, Is.True);
        Assert.That(positive.IsNegative, Is.False);
        Assert.That(positive.IsInteger, Is.False);

        Assert.That(negative.IsPositive, Is.False);
        Assert.That(negative.IsNegative, Is.True);
        Assert.That(negative.IsInteger, Is.False);

        Assert.That(integer.IsInteger, Is.True);
        Assert.That(integer.IsOne, Is.False);
    }

    [Test]
    public void DeconstructReturnsNumeratorAndDenominator()
    {
        var value = R(7, 3);
        value.Deconstruct(out var numerator, out var denominator);
        Assert.That(numerator, Is.EqualTo(new BigInteger(7)));
        Assert.That(denominator, Is.EqualTo(new BigInteger(3)));
    }

    [Test]
    public void ReciprocalSwapsNumeratorAndDenominator()
    {
        var value = R(2, 3);
        var reciprocal = value.Reciprocal();
        AssertRational(reciprocal, 3, 2);
    }

    [Test]
    public void ReciprocalOfZeroThrows()
    {
        Assert.Throws<DivideByZeroException>(() => _ = BigRational.Zero.Reciprocal());
    }
}
