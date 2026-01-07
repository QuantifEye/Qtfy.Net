// <copyright file="ConstantsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests;

using static System.Math;

internal sealed class ConstantsTests
{
    [Test]
    public void TestSqrtTwoPi()
    {
        Assert.That(Sqrt(2d * PI), Is.EqualTo(Constants.SqrtTwoPi).Within(ScaleB(4, -53)));
    }

    [Test]
    public void LogSqrtTwoPi()
    {
        Assert.That(Log(Constants.SqrtTwoPi), Is.EqualTo(Constants.LogSqrtTwoPi));
    }

    [Test]
    public void TestSqrtTwo()
    {
        Assert.That(Sqrt(2d), Is.EqualTo(Constants.SqrtTwo));
    }

    [Test]
    public void TestTwoLnTwo()
    {
        Assert.That(2d * Log(2d), Is.EqualTo(Constants.TwoLnTwo));
    }
}
