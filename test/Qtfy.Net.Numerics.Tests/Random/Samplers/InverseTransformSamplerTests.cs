// <copyright file="InverseTransformSamplerTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Distributions;
    using Qtfy.Net.Numerics.Random;
    using Qtfy.Net.Numerics.Random.Samplers;

    public class InverseTransformSamplerTests
    {
        [Test]
        public void TestConstructInvalid()
        {
            var engine = new MockEngine();
            var distribution = new NormalDistribution(1, 1);
            Assert.Throws<ArgumentNullException>(
                () => _ = new InverseTransformSampler<double>(engine, null));
            Assert.Throws<ArgumentNullException>(
                () => _ = new InverseTransformSampler<double>(null, distribution));
        }

        [Test]
        public void TestGetNext()
        {
            var engine = new MockEngine();
            var dist = StandardUniformDistribution.Instance;
            var sampler = new InverseTransformSampler<double>(engine, dist);
            var actual = sampler.GetNext(100);
            var expected = Enumerable.Range(0, 100).Select(x => x / 100d).ToArray();
            Assert.AreEqual(actual, expected);
        }

        private class MockEngine : IRandomNumberEngine
        {
            private int current;

            public uint NextUInt() => throw new NotImplementedException();

            public uint NextUInt(uint max) => throw new NotImplementedException();

            public ulong NextULong() => throw new NotImplementedException();

            public ulong NextULong(ulong max) => throw new NotImplementedException();

            public double NextCanonical() => throw new NotImplementedException();

            public double NextIncrementedCanonical() => throw new NotImplementedException();

            public double NextSignedCanonical() => throw new NotImplementedException();

            public double NextStandardUniform()
            {
                var c = this.current++;
                return c / 100d;
            }
        }
    }
}
