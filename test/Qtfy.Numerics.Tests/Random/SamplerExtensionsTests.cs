// <copyright file="SamplerExtensionsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random;

using System;
using NUnit.Framework;
using Qtfy.Net.Numerics.Random;

internal sealed class SamplerExtensionsTests
{
    [Test]
    public void TestGetNextArray()
    {
        Assert.That(new MockSampler().GetNext(3), Is.EqualTo([1d, 2d, 3d]));
    }

    [Test]
    public void TestGetNegativeNumber()
    {
        Assert.Throws<ArgumentException>(
            () => _ = new MockSampler().GetNext(-1));
    }

    [TestCase(null)]
    public void TestGetNextArrayNull(ISampler<double> nullSampler)
    {
        Assert.Throws<ArgumentNullException>(() => _ = nullSampler.GetNext(1));
    }

    private sealed class MockSampler : ISampler<double>
    {
        private double current;

        public double GetNext()
        {
            return ++this.current;
        }
    }
}
