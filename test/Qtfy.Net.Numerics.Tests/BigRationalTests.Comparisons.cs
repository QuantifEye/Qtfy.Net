// <copyright file="BigRationalTests.Comparisons.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests;

using System.Numerics;
using NUnit.Framework;

internal partial class BigRationalTests
{
    [TestCaseSource(typeof(EqualCases))]
    public void TestIEquatableTrue(BigRational left, BigRational right)
    {
        Assert.That(left.Equals(right), Is.True);
    }

    [TestCaseSource(typeof(UnequalCases))]
    public void TestIEquatableFalse(BigRational left, BigRational right)
    {
        Assert.That(left.Equals(right), Is.False);
    }

    [TestCaseSource(typeof(EqualCases))]
    public void TestIComparableZero(BigRational left, BigRational right)
    {
        Assert.That(left.CompareTo(right), Is.EqualTo(0));
    }

    [TestCaseSource(typeof(LessThanCases))]
    public void TestIComparableOne(BigRational smaller, BigRational greater)
    {
        Assert.That(smaller.CompareTo(greater), Is.EqualTo(-1));
    }

    [TestCaseSource(typeof(LessThanCases))]
    public void TestIComparableMinusOne(BigRational smaller, BigRational greater)
    {
        Assert.That(greater.CompareTo(smaller), Is.EqualTo(1));
    }

    [Test]
    public void EqualsObject()
    {
        BigRational rational = new BigRational(7);
        object obj = new BigRational(7);
        Assert.That(rational.Equals(new object()), Is.False);
        Assert.That(rational.Equals(obj), Is.True);
    }

    [TestCaseSource(typeof(EqualCases))]
    public void EqualityOperatorTrue(BigRational left, BigRational right)
    {
        Assert.That(left == right, Is.True);
    }

    [TestCaseSource(typeof(UnequalCases))]
    public void EqualityOperatorFalse(BigRational left, BigRational right)
    {
        Assert.That(left == right, Is.False);
    }

    [TestCaseSource(typeof(UnequalCases))]
    public void InequalityOperatorTrue(BigRational left, BigRational right)
    {
        Assert.That(left != right, Is.True);
    }

    [TestCaseSource(typeof(EqualCases))]
    public void InequalityOperatorFalse(BigRational left, BigRational right)
    {
        Assert.That(left != right, Is.False);
    }

    [TestCaseSource(typeof(LessThanCases))]
    public void LessThanOperatorTrue(BigRational smaller, BigRational greater)
    {
        Assert.That(smaller < greater, Is.True);
    }

    [TestCaseSource(typeof(LessThanCases))]
    [TestCaseSource(typeof(EqualCases))]
    public void LessThanOperatorFalse(BigRational smallerOrSame, BigRational greaterOrSame)
    {
        Assert.That(greaterOrSame < smallerOrSame, Is.False);
    }

    [TestCaseSource(typeof(LessThanCases))]
    public void GreaterThanOperatorTrue(BigRational smaller, BigRational greater)
    {
        Assert.That(greater > smaller, Is.True);
    }

    [TestCaseSource(typeof(LessThanCases))]
    [TestCaseSource(typeof(EqualCases))]
    public void GreaterThanOperatorFalse(BigRational smallerOrSame, BigRational greaterOrSame)
    {
        Assert.That(smallerOrSame > greaterOrSame, Is.False);
    }

    [TestCaseSource(typeof(LessThanCases))]
    [TestCaseSource(typeof(EqualCases))]
    public void GreaterThanOrEqualOperatorTrue(BigRational smallerOrSame, BigRational greaterOrSame)
    {
        Assert.That(greaterOrSame >= smallerOrSame, Is.True);
    }

    [TestCaseSource(typeof(LessThanCases))]
    public void GreaterThanOrEqualOperatorFalse(BigRational smaller, BigRational greater)
    {
        Assert.That(smaller >= greater, Is.False);
    }

    [TestCaseSource(typeof(LessThanCases))]
    [TestCaseSource(typeof(EqualCases))]
    public void LessThanOrEqualOperatorTrue(BigRational smallerOrSame, BigRational greaterOrSame)
    {
        Assert.That(smallerOrSame <= greaterOrSame, Is.True);
    }

    [TestCaseSource(typeof(LessThanCases))]
    public void LessThanOrEqualOperatorFalse(BigRational smaller, BigRational greater)
    {
        Assert.That(greater <= smaller, Is.False);
    }

    [TestCase(4, 1, 4, true)]
    [TestCase(1, 2, 2, false)]
    [TestCase(-4, 1, -4, true)]
    [TestCase(-1, 2, 2, false)]
    public void EqualsInteger(int numerator, int denominator, int right, bool expected)
    {
        var left = new BigRational(numerator, denominator);

        Assert.That(left.Equals((BigInteger)right), Is.EqualTo(expected));
        Assert.That(left.Equals((long)right), Is.EqualTo(expected));
        Assert.That(left.Equals((int)right), Is.EqualTo(expected));
        Assert.That(left.Equals((short)right), Is.EqualTo(expected));
        Assert.That(left.Equals((sbyte)right), Is.EqualTo(expected));

        if (right >= 0)
        {
            Assert.That(left.Equals((ulong)right), Is.EqualTo(expected));
            Assert.That(left.Equals((uint)right), Is.EqualTo(expected));
            Assert.That(left.Equals((ushort)right), Is.EqualTo(expected));
            Assert.That(left.Equals((byte)right), Is.EqualTo(expected));
        }
    }

    [TestCase(1, 1, 1, 0)]
    [TestCase(5, 2, 2, 1)]
    [TestCase(3, 2, 2, -1)]
    [TestCase(-5, 2, 2, -1)]
    [TestCase(-3, 2, 2, -1)]
    [TestCase(-5, 2, -2, -1)]
    [TestCase(-3, 2, -2, 1)]
    public void CompareToInteger(int n, int d, int right, int expected)
    {
        var left = new BigRational(n, d);
        Assert.That(left.CompareTo((BigInteger)right), Is.EqualTo(expected));
        Assert.That(left.CompareTo((long)right), Is.EqualTo(expected));
        Assert.That(left.CompareTo((int)right), Is.EqualTo(expected));
        Assert.That(left.CompareTo((short)right), Is.EqualTo(expected));
        Assert.That(left.CompareTo((sbyte)right), Is.EqualTo(expected));

        if (right >= 0)
        {
            Assert.That(left.CompareTo((ulong)right), Is.EqualTo(expected));
            Assert.That(left.CompareTo((uint)right), Is.EqualTo(expected));
            Assert.That(left.CompareTo((ushort)right), Is.EqualTo(expected));
            Assert.That(left.CompareTo((byte)right), Is.EqualTo(expected));
        }
    }
}
