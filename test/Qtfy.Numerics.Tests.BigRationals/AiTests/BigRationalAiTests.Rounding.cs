// <copyright file="BigRationalAiTests.Rounding.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals;

using System;
using System.Numerics;
using NUnit.Framework;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void CeilingAndFloorHandleSigns()
    {
        Assert.That(BigRational.Ceiling(R(1, 2)), Is.EqualTo(new BigRational(1)));
        Assert.That(BigRational.Floor(R(1, 2)), Is.EqualTo(new BigRational(0)));
        Assert.That(BigRational.Ceiling(R(-1, 2)), Is.EqualTo(new BigRational(0)));
        Assert.That(BigRational.Floor(R(-1, 2)), Is.EqualTo(new BigRational(-1)));
    }

    [Test]
    public void CeilingAndFloorWithTickSize()
    {
        var tick = R(1, 2);
        var value = R(3, 4);

        Assert.That(BigRational.Ceiling(value, tick), Is.EqualTo(new BigRational(1)));
        Assert.That(BigRational.Floor(value, tick), Is.EqualTo(R(1, 2)));
    }

    [Test]
    public void RoundToTickSupportsMidpointModes()
    {
        var tick = R(1, 2);
        var value = R(3, 4);

        Assert.That(BigRational.RoundToTick(value, tick, MidpointRoundingMode.ToEven), Is.EqualTo(new BigRational(1)));
        Assert.That(BigRational.RoundToTick(value, tick, MidpointRoundingMode.Up), Is.EqualTo(new BigRational(1)));
        Assert.That(BigRational.RoundToTick(value, tick, MidpointRoundingMode.Down), Is.EqualTo(R(1, 2)));
        Assert.That(BigRational.RoundToTick(value, tick, MidpointRoundingMode.AwayFromZero), Is.EqualTo(new BigRational(1)));
        Assert.That(BigRational.RoundToTick(value, tick, MidpointRoundingMode.TowardZero), Is.EqualTo(R(1, 2)));

        var negative = R(-3, 4);
        Assert.That(BigRational.RoundToTick(negative, tick, MidpointRoundingMode.AwayFromZero), Is.EqualTo(new BigRational(-1)));
        Assert.That(BigRational.RoundToTick(negative, tick, MidpointRoundingMode.TowardZero), Is.EqualTo(R(-1, 2)));
    }

    [Test]
    public void RoundToTickRejectsInvalidInputs()
    {
        Assert.Throws<ArgumentException>(() => BigRational.RoundToTick(R(1, 2), R(0, 1), MidpointRoundingMode.ToEven));
        Assert.Throws<ArgumentException>(() => BigRational.RoundToTick(R(1, 2), R(-1, 2), MidpointRoundingMode.ToEven));
        Assert.Throws<ArgumentException>(() => BigRational.RoundToTick(R(1, 2), R(1, 2), (MidpointRoundingMode)42));
    }

    [Test]
    public void RoundToIntUsesMidpointModes()
    {
        var value = R(3, 2);
        Assert.That(BigRational.RoundToInt(value, MidpointRoundingMode.ToEven), Is.EqualTo(new BigInteger(2)));
        Assert.That(BigRational.RoundToInt(value, MidpointRoundingMode.Up), Is.EqualTo(new BigInteger(2)));
        Assert.That(BigRational.RoundToInt(value, MidpointRoundingMode.Down), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigRational.RoundToInt(value, MidpointRoundingMode.AwayFromZero), Is.EqualTo(new BigInteger(2)));
        Assert.That(BigRational.RoundToInt(value, MidpointRoundingMode.TowardZero), Is.EqualTo(new BigInteger(1)));

        var negative = R(-3, 2);
        Assert.That(BigRational.RoundToInt(negative, MidpointRoundingMode.ToEven), Is.EqualTo(new BigInteger(-2)));
        Assert.That(BigRational.RoundToInt(negative, MidpointRoundingMode.Up), Is.EqualTo(new BigInteger(-1)));
        Assert.That(BigRational.RoundToInt(negative, MidpointRoundingMode.Down), Is.EqualTo(new BigInteger(-2)));
        Assert.That(BigRational.RoundToInt(negative, MidpointRoundingMode.AwayFromZero), Is.EqualTo(new BigInteger(-2)));
        Assert.That(BigRational.RoundToInt(negative, MidpointRoundingMode.TowardZero), Is.EqualTo(new BigInteger(-1)));
    }

    [Test]
    public void RoundToIntRejectsInvalidMode()
    {
        Assert.Throws<ArgumentException>(() => BigRational.RoundToInt(R(1, 2), (MidpointRoundingMode)99));
    }
}
