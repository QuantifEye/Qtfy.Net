// <copyright file="ThreeFry4X64Tests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.RandomNumberEngines
{
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;

    public class ThreeFry4X64Tests
    {
        [Test]
        public void TestFirstBuffer()
        {
            var engine = new ThreeFry4X64(0);
            var actual = new ulong[4];
            for (var i = 0; i < actual.Length; ++i)
            {
                actual[i] = engine.NextULong();
            }

            var expected = new[]
            {
                0x09218ebde6c85537UL,
                0x55941f5266d86105UL,
                0x4bd25e16282434dcUL,
                0xee29ec846bd2e40bUL,
            };

            Assert.AreEqual(expected, actual);
        }
    }
}
