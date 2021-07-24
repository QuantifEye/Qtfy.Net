// <copyright file="ConstantsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
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
        public void LogSqrtTwoPi()
        {
            Assert.AreEqual(Log(Constants.SqrtTwoPi), Constants.LogSqrtTwoPi);
        }

        [Test]
        public void TestSqrtTwo()
        {
            Assert.AreEqual(Sqrt(2d), Constants.SqrtTwo);
        }

        [Test]
        public void TestTwoLnTwo()
        {
            Assert.AreEqual(2d * Log(2d), Constants.TwoLnTwo);
        }
    }
}
