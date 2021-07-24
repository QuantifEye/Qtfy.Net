// <copyright file="ArrayToolsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
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

        [Test]
        public void TestValueEqualsDouble()
        {
            Assert.True(ArrayTools.ValueEquals(
                Array.Empty<double>(),
                Array.Empty<double>()));
            Assert.True(ArrayTools.ValueEquals(
                new[] { 1d, 2d },
                new[] { 1d, 2d }));
            Assert.False(ArrayTools.ValueEquals(
                new[] { 1d },
                new[] { 1d, 2d }));
            Assert.True(ArrayTools.ValueEquals(
                new[] { 1d },
                new[] { 1d }));
            Assert.False(ArrayTools.ValueEquals(
                new[] { double.NaN },
                new[] { double.NaN }));
            Assert.False(ArrayTools.ValueEquals(
                new[] { 1d },
                new[] { double.NaN }));
            Assert.False(ArrayTools.ValueEquals(
                new[] { double.NaN },
                new[] { 1d }));
        }
    }
}
