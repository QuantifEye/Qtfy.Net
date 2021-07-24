// <copyright file="StandardNormalSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;
    using Qtfy.Net.Numerics.Random.Samplers;

    public class StandardNormalSamplerTests
    {
        [Test]
        public void TestConstructInvalid()
        {
            Assert.Throws<ArgumentNullException>(
                () => _ = new StandardNormalSampler(null));
        }

        [Test]
        public void TestFill()
        {
            var sampler1 = new StandardNormalSampler(new ReducedThreeFry4X64(1));
            var actual = Enumerable.Repeat(sampler1, 10).Select(x => x.GetNext()).ToArray();

            var sampler2 = new StandardNormalSampler(new ReducedThreeFry4X64(1));
            var expected = new double[10];
            sampler2.Fill(expected);

            Assert.AreEqual(expected, actual);
        }
    }
}
