// <copyright file="ConstantsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random
{
    using NUnit.Framework;
    using static System.Math;

    public class ConstantsTests
    {
        [Test]
        public void TestSqrtTwoPi()
        {
            Assert.AreEqual(Sqrt(2d * PI), Constants.SqrtTwoPi, ScaleB(4, -53));
        }

        [Test]
        public void TestLogSqrtTwoPi()
        {
            Assert.AreEqual(Log(Sqrt(2d * PI)), Constants.LogSqrtTwoPi, ScaleB(1, -53));
        }

        [Test]
        public void TestSqrtTwo()
        {
            Assert.AreEqual(Sqrt(2d), Constants.SqrtTwo, ScaleB(1, -53));
        }

        [Test]
        public void TestTwoLnTwo()
        {
            Assert.AreEqual(2d * Log(2d), Constants.TwoLnTwo, ScaleB(1, -53));
        }
    }
}
