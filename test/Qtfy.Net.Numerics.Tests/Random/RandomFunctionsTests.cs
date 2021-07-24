// <copyright file="RandomFunctionsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random
{
    using System;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random;

    public class RandomFunctionsTests
    {
        [Test]
        public void TestCanonicalMin()
        {
            Assert.AreEqual(
                0d,
                RandomFunctions.Canonical(0UL));
        }

        [Test]
        public void TestCanonicalMax()
        {
            Assert.AreEqual(
                Math.BitDecrement(1d),
                RandomFunctions.Canonical(ulong.MaxValue));
        }

        [Test]
        public void TestIncrementedCanonicalCanonicalMin()
        {
            Assert.AreEqual(
                1d - Math.BitDecrement(1d),
                RandomFunctions.IncrementedCanonical(0UL));
        }

        [Test]
        public void TestIncrementedCanonicalCanonicalMax()
        {
            Assert.AreEqual(
                1d,
                RandomFunctions.IncrementedCanonical(ulong.MaxValue));
        }
    }
}
