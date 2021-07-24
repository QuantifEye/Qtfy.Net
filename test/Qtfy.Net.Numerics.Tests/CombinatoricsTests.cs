// <copyright file="CombinatoricsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    public class CombinatoricsTests
    {
        private static readonly (int[] left, int[] right)[] ExpectedPowerSetAndCompliment =
        {
            (Array.Empty<int>(), new[] { 1, 2, 3 }),
            (new[] { 1 }, new[] { 2, 3 }),
            (new[] { 2 }, new[] { 1, 3 }),
            (new[] { 1, 2 }, new[] { 3 }),
            (new[] { 3 }, new[] { 1, 2 }),
            (new[] { 1, 3 }, new[] { 2 }),
            (new[] { 2, 3 }, new[] { 1 }),
            (new[] { 1, 2, 3 }, Array.Empty<int>()),
        };

        private static readonly int[][] ExpectedPowerSet = ExpectedPowerSetAndCompliment
            .Select(x => x.left)
            .ToArray();

        private static void TestPowerSetHelper(int[][] actual)
        {
            var expected = ExpectedPowerSet;
            Assert.AreEqual(actual.Length, expected.Length);
            for (var i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        private static void TestPowerSetWithComplimentHelper((int[] left, int[] right)[] actual)
        {
            var expected = ExpectedPowerSetAndCompliment;
            Assert.AreEqual(actual.Length, ExpectedPowerSetAndCompliment.Length);
            for (var i = 0; i < actual.Length; i++)
            {
                Assert.AreEqual(expected[i].left, actual[i].left);
                Assert.AreEqual(expected[i].right, actual[i].right);
            }
        }

        [Test]
        public void TestPowerSet()
        {
            var actual = Combinatorics.PowerSet(new[] { 1, 2, 3 });
            TestPowerSetHelper(actual.ToArray());
        }

        [Test]
        public void TestPowerSetNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = Combinatorics.PowerSet((IEnumerable<int>)null));

            Assert.Throws<ArgumentNullException>(
                () => _ = Combinatorics.PowerSet(new[] { 1, 2 }, null));

            Assert.Throws<ArgumentNullException>(
                () => _ = Combinatorics.PowerSet(null, EqualityComparer<int>.Default));
        }

        [Test]
        public void TestPowerSetWithComplimentNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = Combinatorics.PowerSetWithCompliment((IEnumerable<int>)null));

            Assert.Throws<ArgumentNullException>(
                () => _ = Combinatorics.PowerSetWithCompliment(new[] { 1, 2 }, null));

            Assert.Throws<ArgumentNullException>(
                () => _ = Combinatorics.PowerSetWithCompliment(null, EqualityComparer<int>.Default));
        }

        [Test]
        public void TestPowerSetWithEqualityComparer()
        {
            var actual = Combinatorics.PowerSet(
                new[] { 1, 2, 3 },
                EqualityComparer<int>.Default);
            TestPowerSetHelper(actual.ToArray());
        }

        [Test]
        public void TestPowerSetWithCompliment()
        {
            var actual = Combinatorics.PowerSetWithCompliment(new[] { 1, 2, 3 });
            TestPowerSetWithComplimentHelper(actual.ToArray());
        }

        [Test]
        public void TestPowerSetWithComplimentAndEqualityComparer()
        {
            var actual = Combinatorics.PowerSetWithCompliment(
                new[] { 1, 2, 3 },
                EqualityComparer<int>.Default);
            TestPowerSetWithComplimentHelper(actual.ToArray());
        }

        [Test]
        public void PowerSetEmptyTest()
        {
            var source = Array.Empty<int>();
            var actual = Combinatorics.PowerSet(source).ToArray();
            Assert.AreEqual(1, actual.Length);
            var empty = actual[0];
            Assert.AreEqual(0, empty.Length);
        }

        [Test]
        public void TestPowerSetTooLargeError()
        {
            Assert.Throws<ArgumentException>(
                () => Combinatorics.PowerSet(Enumerable.Range(1, 64)));
        }
    }
}
