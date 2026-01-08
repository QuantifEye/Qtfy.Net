// <copyright file="BigRationalAiTests.NumericTraits.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals.AiTests;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void NumericClassificationFlagsAreStable()
    {
        var zero = BigRational.Zero;
        var value = R(3, 2);

        Assert.That(BigRational.IsCanonical(zero), Is.True);
        Assert.That(BigRational.IsCanonical(value), Is.True);
        Assert.That(BigRational.IsFinite(value), Is.True);
        Assert.That(BigRational.IsInfinity(value), Is.False);
        Assert.That(BigRational.IsNaN(value), Is.False);
        Assert.That(BigRational.IsPositiveInfinity(value), Is.False);
        Assert.That(BigRational.IsNegativeInfinity(value), Is.False);
        Assert.That(BigRational.IsComplexNumber(value), Is.False);
        Assert.That(BigRational.IsImaginaryNumber(value), Is.False);
        Assert.That(BigRational.IsRealNumber(value), Is.True);
        Assert.That(BigRational.IsSubnormal(value), Is.False);
        Assert.That(BigRational.IsNormal(zero), Is.False);
        Assert.That(BigRational.IsNormal(value), Is.True);
    }

    [Test]
    public void StaticInterfacePredicatesUseINumberSemantics()
    {
        var zero = BigRational.Zero;
        var positive = R(1, 2);
        var negative = R(-1, 2);

        Assert.That(StaticIsPositive(zero), Is.False);
        Assert.That(StaticIsNegative(zero), Is.False);
        Assert.That(StaticIsZero(zero), Is.True);
        Assert.That(StaticIsInteger(zero), Is.True);
        Assert.That(StaticSign(zero), Is.EqualTo(0));

        Assert.That(StaticIsPositive(positive), Is.True);
        Assert.That(StaticIsNegative(positive), Is.False);
        Assert.That(StaticIsInteger(positive), Is.False);
        Assert.That(StaticSign(positive), Is.EqualTo(1));

        Assert.That(StaticIsPositive(negative), Is.False);
        Assert.That(StaticIsNegative(negative), Is.True);
        Assert.That(StaticIsInteger(negative), Is.False);
        Assert.That(StaticSign(negative), Is.EqualTo(-1));
    }

    [Test]
    public void EvenAndOddIntegerPredicates()
    {
        Assert.That(BigRational.IsEvenInteger(new BigRational(0)), Is.True);
        Assert.That(BigRational.IsEvenInteger(new BigRational(4)), Is.True);
        Assert.That(BigRational.IsOddInteger(new BigRational(3)), Is.True);
        Assert.That(BigRational.IsEvenInteger(R(1, 2)), Is.False);
        Assert.That(BigRational.IsOddInteger(R(1, 2)), Is.False);
    }

    [Test]
    public void PowerOfTwoPredicateMatchesIntegerPowers()
    {
        Assert.That(BigRational.IsPow2(new BigRational(1)), Is.True);
        Assert.That(BigRational.IsPow2(new BigRational(2)), Is.True);
        Assert.That(BigRational.IsPow2(new BigRational(4)), Is.True);
        Assert.That(BigRational.IsPow2(new BigRational(0)), Is.False);
        Assert.That(BigRational.IsPow2(new BigRational(-2)), Is.False);
        Assert.That(BigRational.IsPow2(R(1, 2)), Is.False);
    }

    [Test]
    public void MinMaxAndClampBehave()
    {
        var low = R(1, 2);
        var high = R(3, 2);

        Assert.That(BigRational.Max(low, high), Is.EqualTo(high));
        Assert.That(BigRational.Min(low, high), Is.EqualTo(low));
        Assert.That(BigRational.MaxNumber(low, high), Is.EqualTo(high));
        Assert.That(BigRational.MinNumber(low, high), Is.EqualTo(low));
        Assert.That(BigRational.MaxNative(low, high), Is.EqualTo(high));
        Assert.That(BigRational.MinNative(low, high), Is.EqualTo(low));

        Assert.That(BigRational.Clamp(low, low, high), Is.EqualTo(low));
        Assert.That(BigRational.Clamp(high, low, high), Is.EqualTo(high));
        Assert.That(BigRational.Clamp(R(2, 1), low, high), Is.EqualTo(high));
        Assert.That(BigRational.Clamp(R(0, 1), low, high), Is.EqualTo(low));
        Assert.That(BigRational.ClampNative(R(2, 1), low, high), Is.EqualTo(high));

        Assert.Throws<ArgumentException>(() => _ = BigRational.Clamp(low, high, low));
    }

    [Test]
    public void MagnitudeSelectionUsesAbsoluteValues()
    {
        var left = R(-3, 2);
        var right = R(1, 1);

        Assert.That(BigRational.MaxMagnitude(left, right), Is.EqualTo(left));
        Assert.That(BigRational.MaxMagnitudeNumber(left, right), Is.EqualTo(left));
        Assert.That(BigRational.MinMagnitude(left, right), Is.EqualTo(right));
        Assert.That(BigRational.MinMagnitudeNumber(left, right), Is.EqualTo(right));

        var tieLeft = R(-2, 1);
        var tieRight = R(2, 1);
        Assert.That(BigRational.MaxMagnitude(tieLeft, tieRight), Is.EqualTo(tieLeft));
        Assert.That(BigRational.MinMagnitude(tieLeft, tieRight), Is.EqualTo(tieLeft));
    }
}
