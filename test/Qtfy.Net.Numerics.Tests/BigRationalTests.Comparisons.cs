// <copyright file="BigRationalTests.Comparisons.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System.Numerics;
    using NUnit.Framework;

    public partial class BigRationalTests
    {
        [TestCaseSource(typeof(EqualCases))]
        public void TestIEquatableTrue(BigRational left, BigRational right)
        {
            Assert.True(left.Equals(right));
        }

        [TestCaseSource(typeof(UnequalCases))]
        public void TestIEquatableFalse(BigRational left, BigRational right)
        {
            Assert.False(left.Equals(right));
        }

        [TestCaseSource(typeof(EqualCases))]
        public void TestIComparableZero(BigRational left, BigRational right)
        {
            Assert.AreEqual(0, left.CompareTo(right));
        }

        [TestCaseSource(typeof(LessThanCases))]
        public void TestIComparableOne(BigRational smaller, BigRational greater)
        {
            Assert.AreEqual(-1, smaller.CompareTo(greater));
        }

        [TestCaseSource(typeof(LessThanCases))]
        public void TestIComparableMinusOne(BigRational smaller, BigRational greater)
        {
            Assert.AreEqual(1, greater.CompareTo(smaller));
        }

        [Test]
        public void EqualsObject()
        {
            BigRational rational = new BigRational(7);
            object obj = new BigRational(7);
            Assert.False(rational.Equals(new object()));
            Assert.True(rational.Equals(obj));
        }

        [TestCaseSource(typeof(EqualCases))]
        public void EqualityOperatorTrue(BigRational left, BigRational right)
        {
            Assert.True(left == right);
        }

        [TestCaseSource(typeof(UnequalCases))]
        public void EqualityOperatorFalse(BigRational left, BigRational right)
        {
            Assert.False(left == right);
        }

        [TestCaseSource(typeof(UnequalCases))]
        public void InequalityOperatorTrue(BigRational left, BigRational right)
        {
            Assert.True(left != right);
        }

        [TestCaseSource(typeof(EqualCases))]
        public void InequalityOperatorFalse(BigRational left, BigRational right)
        {
            Assert.False(left != right);
        }

        [TestCaseSource(typeof(LessThanCases))]
        public void LessThanOperatorTrue(BigRational smaller, BigRational greater)
        {
            Assert.True(smaller < greater);
        }

        [TestCaseSource(typeof(LessThanCases))]
        [TestCaseSource(typeof(EqualCases))]
        public void LessThanOperatorFalse(BigRational smallerOrSame, BigRational greaterOrSame)
        {
            Assert.False(greaterOrSame < smallerOrSame);
        }

        [TestCaseSource(typeof(LessThanCases))]
        public void GreaterThanOperatorTrue(BigRational smaller, BigRational greater)
        {
            Assert.True(greater > smaller);
        }

        [TestCaseSource(typeof(LessThanCases))]
        [TestCaseSource(typeof(EqualCases))]
        public void GreaterThanOperatorFalse(BigRational smallerOrSame, BigRational greaterOrSame)
        {
            Assert.False(smallerOrSame > greaterOrSame);
        }

        [TestCaseSource(typeof(LessThanCases))]
        [TestCaseSource(typeof(EqualCases))]
        public void GreaterThanOrEqualOperatorTrue(BigRational smallerOrSame, BigRational greaterOrSame)
        {
            Assert.True(greaterOrSame >= smallerOrSame);
        }

        [TestCaseSource(typeof(LessThanCases))]
        public void GreaterThanOrEqualOperatorFalse(BigRational smaller, BigRational greater)
        {
            Assert.False(smaller >= greater);
        }

        [TestCaseSource(typeof(LessThanCases))]
        [TestCaseSource(typeof(EqualCases))]
        public void LessThanOrEqualOperatorTrue(BigRational smallerOrSame, BigRational greaterOrSame)
        {
            Assert.True(smallerOrSame <= greaterOrSame);
        }

        [TestCaseSource(typeof(LessThanCases))]
        public void LessThanOrEqualOperatorFalse(BigRational smaller, BigRational greater)
        {
            Assert.False(greater <= smaller);
        }

        [TestCase("4", 4, true)]
        [TestCase("1/2", 2, false)]
        [TestCase("-4", -4, true)]
        [TestCase("-1/2", 2, false)]
        public void EqualsInteger(string left, int right, bool expected)
        {
            var rational = BigRational.Parse(left);
            Assert.AreEqual(expected, rational.Equals((BigInteger)right));
            Assert.AreEqual(expected, rational.Equals((long)right));
            Assert.AreEqual(expected, rational.Equals((int)right));
            Assert.AreEqual(expected, rational.Equals((short)right));
            Assert.AreEqual(expected, rational.Equals((sbyte)right));

            if (right >= 0)
            {
                Assert.AreEqual(expected, rational.Equals((ulong)right));
                Assert.AreEqual(expected, rational.Equals((uint)right));
                Assert.AreEqual(expected, rational.Equals((ushort)right));
                Assert.AreEqual(expected, rational.Equals((byte)right));
            }
        }

        [TestCase("1", 1, 0)]
        [TestCase("5/2", 2, 1)]
        [TestCase("3/2", 2, -1)]
        [TestCase("-5/2", 2, -1)]
        [TestCase("-3/2", 2, -1)]
        [TestCase("-5/2", -2, -1)]
        [TestCase("-3/2", -2, 1)]
        public void CompareToInteger(string left, int right, int expected)
        {
            var rational = BigRational.Parse(left);
            Assert.AreEqual(expected, rational.CompareTo((BigInteger)right));
            Assert.AreEqual(expected, rational.CompareTo((long)right));
            Assert.AreEqual(expected, rational.CompareTo((int)right));
            Assert.AreEqual(expected, rational.CompareTo((short)right));
            Assert.AreEqual(expected, rational.CompareTo((sbyte)right));

            if (right >= 0)
            {
                Assert.AreEqual(expected, rational.CompareTo((ulong)right));
                Assert.AreEqual(expected, rational.CompareTo((uint)right));
                Assert.AreEqual(expected, rational.CompareTo((ushort)right));
                Assert.AreEqual(expected, rational.CompareTo((byte)right));
            }
        }
    }
}
