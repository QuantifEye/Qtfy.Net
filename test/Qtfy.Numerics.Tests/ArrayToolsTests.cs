// <copyright file="ArrayToolsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests;

using NUnit.Framework;

internal sealed class ArrayToolsTests
{
    [Test]
    public void TestCopy()
    {
        int[] source = [456, 789, 123];
        var copy = source.Copy();
        Assert.That(source, Is.Not.SameAs(copy));
        Assert.That(source, Is.EqualTo(copy));
    }
}
