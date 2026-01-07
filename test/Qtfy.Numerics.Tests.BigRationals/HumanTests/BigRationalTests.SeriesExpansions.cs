// <copyright file="BigRationalTests.SeriesExpansions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals;

internal partial class BigRationalTests
{
    [TestCase(7, 14)]
    public void ExpTaylorExpansion(int numerator, int denominator)
    {
        var power = new BigRational(numerator, denominator);
        Assert.That(BigRational.Exp(power, 1), Is.EqualTo(BigRational.One));
        Assert.That(BigRational.Exp(power, 2), Is.EqualTo(1 + power));
        Assert.That(BigRational.Exp(power, 3), Is.EqualTo(1 + power + ((power * power) / 2)));
        Assert.That(BigRational.Exp(power, 4), Is.EqualTo(1 + power + ((power * power) / 2) + ((power * power * power) / 6)));
    }

    [TestCase(2)]
    [TestCase(100.5)]
    public void DoublePrecisionExp(double x)
    {
        var exp = Math.Exp(x);
        BigRational lower = Math.BitDecrement(exp);
        BigRational upper = Math.BitIncrement(exp);
        BigRational actual = BigRational.Exp(x, 500);
        Assert.That(actual < upper, Is.True);
        Assert.That(actual > lower, Is.True);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void NonPositiveTermsExpThrows(int terms)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => BigRational.Exp(1, terms));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void NonPositiveTermsLogThrows(int terms)
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => BigRational.Log(1, terms));
    }

    [TestCase(0, 0)]
    [TestCase(0, 5)]
    [TestCase(-1, 0)]
    [TestCase(-1, 5)]
    public void LogRejectsNonPositiveInput(int value, int terms)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => BigRational.Log(value, terms));
    }

    [Test]
    public void LogOfOneReturnsZero()
    {
        Assert.That(BigRational.Log(BigRational.One, 10), Is.EqualTo(BigRational.Zero));
    }

    [TestCase(2)]
    [TestCase(100.5)]
    public void DoublePrecisionLog(double x)
    {
        var log = Math.Log(x);
        BigRational upper = Math.BitIncrement(log);
        BigRational lower = Math.BitDecrement(log);
        BigRational actual = BigRational.Log(x, 1000);
        Assert.That(actual < upper, Is.True);
        Assert.That(actual > lower, Is.True);
    }
}
