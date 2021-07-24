// <copyright file="ReducedThreeFry4X64Tests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.RandomNumberEngines
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;

    public class ReducedThreeFry4X64Tests
    {
        [Test]
        public void TestEquivalence()
        {
            for (ulong i = 0; i < 10; ++i)
            {
                Assert.AreEqual(
                    GetValues(new ThreeFry4X64(i)),
                    GetValues(new ReducedThreeFry4X64(i)));

                static IEnumerable<ulong> GetValues(IRandomNumberEngine engine)
                    => Enumerable.Repeat(engine, 100).Select(e => e.NextULong());
            }
        }
    }
}
