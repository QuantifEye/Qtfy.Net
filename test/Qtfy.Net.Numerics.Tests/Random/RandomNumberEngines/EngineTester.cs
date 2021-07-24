// <copyright file="EngineTester.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.RandomNumberEngines
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random;

    public abstract class EngineTester<TEngine>
        where TEngine : IRandomNumberEngine
    {
        protected abstract TEngine GetEngine();

        protected void Compare<T>(T[] expected, Func<TEngine, T> func)
        {
            var engine = this.GetEngine();
            var actual = Enumerable.Repeat(engine, expected.Length).Select(func).ToArray();
            Assert.AreEqual(expected, actual);
        }

        private static uint Cast(ulong value)
        {
            return value > uint.MaxValue
                ? throw new AssertionException(null)
                : (uint)value;
        }

        protected void TestUInt(uint[] expected, uint value)
        {
            this.Compare(expected, mt => mt.NextUInt(value));
            this.Compare(expected, mt => Cast(mt.NextULong(value)));
        }

        protected void TestULong(ulong[] expected, ulong value)
        {
            this.Compare(expected, mt => mt.NextULong(value));
            if (value <= uint.MaxValue)
            {
                this.Compare(
                    expected.Select(Cast).ToArray(),
                    mt => Cast(mt.NextULong(value)));
            }
        }

        private void TestUtil<TResult>(Func<TEngine, TResult> left, Func<TEngine, TResult> right)
        {
            const int size = 100;
            var expected = Enumerable.Repeat(this.GetEngine(), size).Select(left);
            var actual = Enumerable.Repeat(this.GetEngine(), size).Select(right);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestNextStandardUniform()
        {
            const ulong two53 = 1UL << 53;
            this.TestUtil(
                e => e.NextStandardUniform(),
                e => Math.ScaleB(e.NextULong(two53), -53));
        }

        [Test]
        public void TestNextCanonical()
        {
            this.TestUtil(
                e => e.NextCanonical(),
                e => RandomFunctions.Canonical(e.NextULong()));
        }

        [Test]
        public void TestNextIncrementedCanonical()
        {
            this.TestUtil(
                e => e.NextIncrementedCanonical(),
                e => RandomFunctions.IncrementedCanonical(e.NextULong()));
        }
    }
}
