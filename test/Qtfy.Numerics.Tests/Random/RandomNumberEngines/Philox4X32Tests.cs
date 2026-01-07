// <copyright file="Philox4X32Tests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.Random.RandomNumberEngines;

using Qtfy.Numerics.Random.RandomNumberEngines;

internal sealed class Philox4X32Tests
{
    [Test]
    public void TestFirstBuffer()
    {
        var engine = new Philox4X32(0);
        var actual = new uint[4];
        for (var i = 0; i < actual.Length; ++i)
        {
            actual[i] = engine.NextUInt();
        }

        var expected = new[]
        {
            0x6627e8d5U,
            0xe169c58dU,
            0xbc57ac4cU,
            0x9b00dbd8U,
        };

        Assert.That(actual, Is.EqualTo(expected));
    }
}
