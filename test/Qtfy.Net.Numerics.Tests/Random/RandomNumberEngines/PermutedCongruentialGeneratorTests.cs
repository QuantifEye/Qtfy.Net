// <copyright file="PermutedCongruentialGeneratorTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.RandomNumberEngines
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using Qtfy.Net.Numerics.Random.RandomNumberEngines;

    public class PermutedCongruentialGeneratorTests
    {
        private static void TestGenerator(PermutedCongruentialGenerator generator, uint[] expected)
        {
            var actual = new uint[expected.Length];
            for (var i = 0; i < actual.Length; ++i)
            {
                actual[i] = generator.NextUInt();
            }

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(Cases))]
        public void TestPcg(ulong initState, ulong initSeq, uint[] expected)
        {
            var generator = new PermutedCongruentialGenerator(initState, initSeq);
            var actual = new uint[expected.Length];
            for (var i = 0; i < actual.Length; ++i)
            {
                actual[i] = generator.NextUInt();
            }

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestConstructInvalid()
        {
            Assert.Throws<ArgumentException>(
                () => _ = new PermutedCongruentialGenerator(1, ulong.MaxValue));
        }

        [Test]
        public void TestDefaultPcg()
        {
            uint[] expected =
            {
                355248013u,
                41705475u,
                3406281715u,
                4186697710u,
                483882979u,
                2766312848u,
                1713261421u,
                154902030u,
                3085534493u,
                3877580365u,
            };

            TestGenerator(new PermutedCongruentialGenerator(), expected);
        }

        [SuppressMessage("Microsoft.Performance", "CA1812", Justification = "class is instantiated by unit testing")]
        private class Cases : IEnumerable
        {
            private static object[] Case(ulong initState, ulong initSeq, uint[] expected)
                => new object[] { initState, initSeq, expected };

            public IEnumerator GetEnumerator()
            {
                yield return Case(
                    1,
                    1,
                    new[]
                    {
                        3380776849u,
                        361947764u,
                        3223725655u,
                        2781001427u,
                        2944471638u,
                        1813784697u,
                        806554406u,
                        2629966899u,
                        3510103874u,
                        2424464043u,
                    });
                yield return Case(
                    10,
                    50,
                    new[]
                    {
                        1614318804u,
                        119671231u,
                        1739985999u,
                        2477405497u,
                        2628116835u,
                        2780395528u,
                        2896065061u,
                        3053079070u,
                        3317316435u,
                        338390484u,
                    });
            }
        }
    }
}
