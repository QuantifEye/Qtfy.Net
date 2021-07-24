// <copyright file="BigRationalTests.Rounding.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
    using System.Numerics;
    using System.Reflection;
    using NUnit.Framework;

    public partial class BigRationalTests
    {
        [TestCase("1/2", "1")]
        [TestCase("-1/2", "0")]
        [TestCase("1", "1")]
        [TestCase("-1", "-1")]
        public void Ceiling(string input, string expected)
        {
            AssertEqual(
                BigRational.Ceiling(BigRational.Parse(input)),
                BigRational.Parse(expected));
        }

        [TestCase("1/6", "1/3", "1/3")]
        [TestCase("3/6", "1/3", "2/3")]
        [TestCase("2/6", "1/3", "1/3")]
        public void CeilingWithTick(string input, string tick, string expected)
        {
            AssertEqual(
                BigRational.Ceiling(BigRational.Parse(input), BigRational.Parse(tick)),
                BigRational.Parse(expected));
        }

        [TestCase("3/2", 1)]
        [TestCase("-3/2", -2)]
        [TestCase("1", 1)]
        [TestCase("-1", -1)]
        public void Floor(string rational, int rounded)
        {
            AssertEqual(
                BigRational.Floor(BigRational.Parse(rational)),
                new BigInteger(rounded));
        }

        [TestCase("1/6", "1/3", "0")]
        [TestCase("3/6", "1/3", "1/3")]
        [TestCase("2/6", "1/3", "1/3")]
        [TestCase("-1/6", "1/3", "-1/3")]
        [TestCase("-3/6", "1/3", "-2/3")]
        [TestCase("-2/6", "1/3", "-1/3")]
        [TestCase("-2", "1/3", "-2")]
        [TestCase("2", "1", "2")]
        public void FloorWithTick(string input, string tick, string expected)
        {
            AssertEqual(
                BigRational.Floor(BigRational.Parse(input), BigRational.Parse(tick)),
                BigRational.Parse(expected));
        }

        [TestCase("1/6", "1/3", MidpointRoundingMode.Down, "0/3")]
        [TestCase("3/6", "1/3", MidpointRoundingMode.Down, "1/3")]
        [TestCase("1/6", "1/3", MidpointRoundingMode.Up, "1/3")]
        [TestCase("3/6", "1/3", MidpointRoundingMode.Up, "2/3")]
        [TestCase("1/6", "1/3", MidpointRoundingMode.TowardZero, "0/3")]
        [TestCase("3/6", "1/3", MidpointRoundingMode.TowardZero, "1/3")]
        [TestCase("1/6", "1/3", MidpointRoundingMode.AwayFromZero, "1/3")]
        [TestCase("3/6", "1/3", MidpointRoundingMode.AwayFromZero, "2/3")]
        [TestCase("1/6", "1/3", MidpointRoundingMode.ToEven, "0/3")]
        [TestCase("3/6", "1/3", MidpointRoundingMode.ToEven, "2/3")]
        [TestCase("-1/6", "1/3", MidpointRoundingMode.Down, "-1/3")]
        [TestCase("-3/6", "1/3", MidpointRoundingMode.Down, "-2/3")]
        [TestCase("-1/6", "1/3", MidpointRoundingMode.Up, "0/3")]
        [TestCase("-3/6", "1/3", MidpointRoundingMode.Up, "-1/3")]
        [TestCase("-1/6", "1/3", MidpointRoundingMode.TowardZero, "0/3")]
        [TestCase("-3/6", "1/3", MidpointRoundingMode.TowardZero, "-1/3")]
        [TestCase("-1/6", "1/3", MidpointRoundingMode.AwayFromZero, "-1/3")]
        [TestCase("-3/6", "1/3", MidpointRoundingMode.AwayFromZero, "-2/3")]
        [TestCase("-1/6", "1/3", MidpointRoundingMode.ToEven, "0/3")]
        [TestCase("-3/6", "1/3", MidpointRoundingMode.ToEven, "-2/3")]
        public void TestRoundToTickAtMidPoint(string unrounded, string tick, MidpointRoundingMode mode, string expected)
        {
            AssertEqual(
                BigRational.RoundToTick(BigRational.Parse(unrounded), BigRational.Parse(tick), mode),
                BigRational.Parse(expected));
        }

        [TestCase(MidpointRoundingMode.Down)]
        [TestCase(MidpointRoundingMode.Up)]
        [TestCase(MidpointRoundingMode.ToEven)]
        [TestCase(MidpointRoundingMode.TowardZero)]
        [TestCase(MidpointRoundingMode.AwayFromZero)]
        public void TestRoundToTickNotAtMidPoint(MidpointRoundingMode mode)
        {
            const int range = 6;
            var epsilon = new BigRational(1, 12);
            var tick = new BigRational(1, 3);
            var max = range * tick;
            var min = -range * tick;
            for (BigRational rational = min; rational <= max; rational += tick)
            {
                AssertEqual(rational, BigRational.RoundToTick(rational + epsilon, tick, mode));
                AssertEqual(rational, BigRational.RoundToTick(rational - epsilon, tick, mode));
                AssertEqual(rational, BigRational.RoundToTick(rational, tick, mode));
            }
        }

        [Test]
        public void RoundToTickWithModeError()
        {
            Assert.Throws<ArgumentException>(() =>
                BigRational.RoundToTick(1, new BigRational(1, 2), (MidpointRoundingMode)100));
        }

        [Test]
        public void RoundToTickWithTickError()
        {
            Assert.Throws<ArgumentException>(() => BigRational.RoundToTick(1, new BigRational(-1, 2), default));
        }

        [TestCase("1/2", MidpointRoundingMode.Down, 0)]
        [TestCase("-1/2", MidpointRoundingMode.Down, -1)]
        [TestCase("1/2", MidpointRoundingMode.Up, 1)]
        [TestCase("-1/2", MidpointRoundingMode.Up, 0)]
        [TestCase("1/2", MidpointRoundingMode.TowardZero, 0)]
        [TestCase("-1/2", MidpointRoundingMode.TowardZero, 0)]
        [TestCase("1/2", MidpointRoundingMode.AwayFromZero, 1)]
        [TestCase("-1/2", MidpointRoundingMode.AwayFromZero, -1)]
        [TestCase("1/2", MidpointRoundingMode.ToEven, 0)]
        [TestCase("-1/2", MidpointRoundingMode.ToEven, 0)]
        public void RoundToIntAtMidPoint(string rational, MidpointRoundingMode mode, int rounded)
        {
            Assert.AreEqual(
                BigRational.RoundToInt(BigRational.Parse(rational), mode),
                new BigInteger(rounded));
        }

        [TestCase(MidpointRoundingMode.Down)]
        [TestCase(MidpointRoundingMode.Up)]
        [TestCase(MidpointRoundingMode.ToEven)]
        [TestCase(MidpointRoundingMode.TowardZero)]
        [TestCase(MidpointRoundingMode.AwayFromZero)]
        public void RoundToIntNotAtMidPoint(MidpointRoundingMode mode)
        {
            const int range = 3;
            var epsilon = new BigRational(1, 3);
            for (BigRational rational = -range; rational <= range; ++rational)
            {
                AssertEqual(rational, BigRational.RoundToInt(rational + epsilon, mode));
                AssertEqual(rational, BigRational.RoundToInt(rational - epsilon, mode));
            }
        }

        [Test]
        public void RoundToIntModeError()
        {
            Assert.Throws<ArgumentException>(() => BigRational.RoundToInt(1, (MidpointRoundingMode)100));
        }

        [Test]
        public void TestRoundImplError()
        {
            static void Helper()
            {
                try
                {
                    typeof(BigRational)
                        .GetMethod("RoundImpl", BindingFlags.NonPublic | BindingFlags.Static)
                        .Invoke(null, new object[] { default(BigRational), (MidpointRoundingMode)int.MaxValue });
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }

            Assert.Throws<ArgumentException>(Helper);
        }
    }
}
