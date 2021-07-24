// <copyright file="BigRationalTests.Overloads.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;
    using NUnit.Framework;

    public partial class BigRationalTests
    {
        [TestCase("op_Equality")]
        [TestCase("op_Inequality")]
        [TestCase("op_LessThan")]
        [TestCase("op_LessThanOrEqual")]
        [TestCase("op_GreaterThan")]
        [TestCase("op_GreaterThanOrEqual")]
        public void TestHasComparisonOverload(string name)
        {
            AssertHasOverloads<bool>(name);
        }

        [TestCase("op_Addition")]
        [TestCase("op_Subtraction")]
        [TestCase("op_Multiply")]
        [TestCase("op_Division")]
        [TestCase("op_Modulus")]
        public void TestHasArithmeticOverload(string name)
        {
            AssertHasOverloads<BigRational>(name);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestAdditionOverloads(dynamic left, dynamic right)
        {
            AssertEqual(
                (BigRational)left + (BigRational)right,
                left + right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestSubtractionOverloads(dynamic left, dynamic right)
        {
            AssertEqual(
                (BigRational)left - (BigRational)right,
                left - right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestMultiplicationOverloads(dynamic left, dynamic right)
        {
            AssertEqual(
                (BigRational)left * (BigRational)right,
                left * right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestDivisionOverloads(dynamic left, dynamic right)
        {
            AssertEqual(
                (BigRational)left / (BigRational)right,
                left / right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestModulusOverloads(dynamic left, dynamic right)
        {
            AssertEqual(
                (BigRational)left % (BigRational)right,
                left % right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestEqualityOverloads(dynamic left, dynamic right)
        {
            var expected = (BigRational)left == (BigRational)right;
            Assert.AreEqual(expected, left == right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestInequalityOverloads(dynamic left, dynamic right)
        {
            Assert.AreEqual(
                (BigRational)left != (BigRational)right,
                left != right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestLessThanOverloads(dynamic left, dynamic right)
        {
            Assert.AreEqual(
                (BigRational)left < (BigRational)right,
                left < right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestGreaterThanOverloads(dynamic left, dynamic right)
        {
            Assert.AreEqual(
                (BigRational)left > (BigRational)right,
                left > right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestLessThanOrEqualOverloads(dynamic left, dynamic right)
        {
            Assert.AreEqual(
                (BigRational)left <= (BigRational)right,
                left <= right);
        }

        [TestCaseSource(typeof(OverloadCases))]
        public void TestGreaterThanOrEqualOverloads(dynamic left, dynamic right)
        {
            Assert.AreEqual(
                (BigRational)left >= (BigRational)right,
                left >= right);
        }

        private static bool HasMethod<TLeft, TRight, TReturn>(string name)
        {
            var count = typeof(BigRational)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(m => m.Name == name)
                .Where(m => m.ReturnType == typeof(TReturn))
                .Where(m => m.GetParameters().Length == 2)
                .Where(m => m.GetParameters()[0].ParameterType == typeof(TLeft))
                .Count(m => m.GetParameters()[1].ParameterType == typeof(TRight));

            return count == 1;
        }

        private static void AssertHasOverloads<TReturn>(string name)
        {
            Assert.True(HasMethod<BigRational, BigRational, TReturn>(name));
            Assert.True(HasMethod<BigRational, BigInteger, TReturn>(name));
            Assert.True(HasMethod<BigInteger, BigRational, TReturn>(name));
            Assert.True(HasMethod<BigRational, ulong, TReturn>(name));
            Assert.True(HasMethod<ulong, BigRational, TReturn>(name));
            Assert.True(HasMethod<BigRational, long, TReturn>(name));
            Assert.True(HasMethod<long, BigRational, TReturn>(name));
        }

        [SuppressMessage("Microsoft.Performance", "CA1812", Justification = "class is instantiated by unit testing")]
        private class OverloadCases : IEnumerable
        {
            private static readonly object[][] Cases = MakeCases();

            private static object[][] MakeCases()
            {
                int[] allIntegers = { -2, -1, 1, 2 };
                int[] positiveIntegers = { 1, 2 };
                BigRational[] positiveRationals =
                {
                    new (1, 2),
                    new (1),
                    new (3, 2),
                    new (2),
                };
                var allRationals = positiveRationals
                    .Concat(positiveRationals.Select(r => -r))
                    .OrderBy(x => x)
                    .ToArray();

                static IEnumerable<(BigRational rational, int integer)> MakeProduct(
                    BigRational[] rationals,
                    int[] integers)
                {
                    foreach (var r in rationals)
                    {
                        foreach (var i in integers)
                        {
                            yield return (r, i);
                        }
                    }
                }

                var signedCases = MakeProduct(allRationals, allIntegers).ToArray();
                var unsignedCases = MakeProduct(allRationals, positiveIntegers).ToArray();

                static IEnumerable<object[]> Case(object left, object right)
                {
                    yield return new[] { left, right };
                    yield return new[] { right, left };
                }

                IEnumerable<IEnumerable<object[]>> Cases()
                {
                    foreach (var (r, i) in signedCases)
                    {
                        yield return Case(r, (BigInteger)i);
                        yield return Case(r, (long)i);
                    }

                    foreach (var (r, i) in unsignedCases)
                    {
                        yield return Case(r, (ulong)i);
                    }
                }

                return Cases().SelectMany(x => x).ToArray();
            }

            public IEnumerator GetEnumerator()
            {
                return Cases.GetEnumerator();
            }
        }
    }
}
