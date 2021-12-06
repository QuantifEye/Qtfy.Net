// <copyright file="ArrayToolsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using NUnit.Framework;

    public class ArrayToolsTests
    {
        [Test]
        public void TestCopy()
        {
            var source = new[] { 456, 789, 123 };
            var copy = source.Copy();
            Assert.AreNotSame(source, copy);
            Assert.AreEqual(source, copy);
        }
    }
}
