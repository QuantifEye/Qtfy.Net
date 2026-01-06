// <copyright file="RandomFunctionsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random;

using System;
using NUnit.Framework;
using Qtfy.Net.Numerics.Random;

internal sealed class RandomFunctionsTests
{
    [Test]
    public void TestCanonicalMin()
    {
        Assert.That(RandomFunctions.Canonical(0UL), Is.EqualTo(0d));
    }

    [Test]
    public void TestCanonicalMax()
    {
        Assert.That(RandomFunctions.Canonical(ulong.MaxValue), Is.EqualTo(Math.BitDecrement(1d)));
    }

    [Test]
    public void TestIncrementedCanonicalCanonicalMin()
    {
        Assert.That(RandomFunctions.IncrementedCanonical(0UL), Is.EqualTo(1d - Math.BitDecrement(1d)));
    }

    [Test]
    public void TestIncrementedCanonicalCanonicalMax()
    {
        Assert.That(RandomFunctions.IncrementedCanonical(ulong.MaxValue), Is.EqualTo(1d));
    }
}
