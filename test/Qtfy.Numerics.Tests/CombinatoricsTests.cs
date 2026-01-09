// <copyright file="CombinatoricsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests;

internal sealed class CombinatoricsTests
{
    private static readonly (int[] left, int[] right)[] ExpectedPowerSetAndComplement =
    [
        ([], [1, 2, 3]),
        ([1], [2, 3]),
        ([2], [1, 3]),
        ([1, 2], [3]),
        ([3], [1, 2]),
        ([1, 3], [2]),
        ([2, 3], [1]),
        ([1, 2, 3], [])
    ];

    private static readonly int[][] ExpectedPowerSet = ExpectedPowerSetAndComplement
        .Select(x => x.left)
        .ToArray();

    private static void TestPowerSetHelper(int[][] actual)
    {
        var expected = ExpectedPowerSet;
        Assert.That(actual.Length, Is.EqualTo(expected.Length));
        for (var i = 0; i < actual.Length; i++)
        {
            Assert.That(actual[i], Is.EqualTo(expected[i]));
        }
    }

    private static void TestPowerSetWithComplementHelper((int[] left, int[] right)[] actual)
    {
        var expected = ExpectedPowerSetAndComplement;
        Assert.That(actual.Length, Is.EqualTo(expected.Length));
        for (var i = 0; i < actual.Length; i++)
        {
            Assert.That(actual[i].left, Is.EqualTo(expected[i].left));
            Assert.That(actual[i].right, Is.EqualTo(expected[i].right));
        }
    }

    [Test]
    public void TestPowerSet()
    {
        var actual = Combinatorics.PowerSet([1, 2, 3]);
        TestPowerSetHelper(actual.ToArray());
    }

    [Test]
    public void TestPowerSetNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => _ = Combinatorics.PowerSet<int>(null!));

        Assert.Throws<ArgumentNullException>(
            () => _ = Combinatorics.PowerSet([1, 2], null!));

        Assert.Throws<ArgumentNullException>(
            () => _ = Combinatorics.PowerSet(null!, EqualityComparer<int>.Default));
    }

    [Test]
    public void TestPowerSetWithComplementNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => _ = Combinatorics.PowerSetWithComplement<int>(null!));

        Assert.Throws<ArgumentNullException>(
            () => _ = Combinatorics.PowerSetWithComplement([1, 2], null!));

        Assert.Throws<ArgumentNullException>(
            () => _ = Combinatorics.PowerSetWithComplement(null!, EqualityComparer<int>.Default));
    }

    [Test]
    public void TestPowerSetWithEqualityComparer()
    {
        var actual = Combinatorics.PowerSet(
            [1, 2, 3],
            EqualityComparer<int>.Default);
        TestPowerSetHelper(actual.ToArray());
    }

    [Test]
    public void TestPowerSetWithComplement()
    {
        var actual = Combinatorics.PowerSetWithComplement([1, 2, 3]);
        TestPowerSetWithComplementHelper(actual.ToArray());
    }

    [Test]
    public void TestPowerSetWithComplementAndEqualityComparer()
    {
        var actual = Combinatorics.PowerSetWithComplement(
            [1, 2, 3],
            EqualityComparer<int>.Default);
        TestPowerSetWithComplementHelper(actual.ToArray());
    }

    [Test]
    public void PowerSetEmptyTest()
    {
        var source = Array.Empty<int>();
        var actual = Combinatorics.PowerSet(source).ToArray();
        Assert.That(actual.Length, Is.EqualTo(1));
        var empty = actual[0];
        Assert.That(empty.Length, Is.Zero);
    }

    [Test]
    public void TestPowerSetTooLargeError()
    {
        Assert.Throws<ArgumentException>(
            () => Combinatorics.PowerSet(Enumerable.Range(1, 64)));
    }
}
