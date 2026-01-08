// <copyright file="BigRationalAiTests.Comparisons.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals.AiTests;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void EqualityOperatorsCompareCanonicalValues()
    {
        var left = R(1, 2);
        var right = R(2, 4);

        Assert.That(left == right, Is.True);
        Assert.That(left != right, Is.False);
    }

    [Test]
    public void EqualityWithIntegralTypes()
    {
        var value = new BigRational(5);
        var bigIntegerValue = (BigInteger)5;
        long longValue = 5L;
        ulong ulongValue = 5UL;

        Assert.That(value == (BigInteger)5, Is.True);
        Assert.That(bigIntegerValue == value, Is.True);
        Assert.That(value == 5L, Is.True);
        Assert.That(longValue == value, Is.True);
        Assert.That(value == 5UL, Is.True);
        Assert.That(ulongValue == value, Is.True);

        Assert.That(value != (BigInteger)6, Is.True);
        Assert.That(value != 6L, Is.True);
        Assert.That(value != 6UL, Is.True);
    }

    [Test]
    public void ComparisonOperatorsWorkAcrossTypes()
    {
        var value = R(3, 2);
        Assert.That(value > R(4, 3), Is.True);
        Assert.That(value >= R(3, 2), Is.True);
        Assert.That(value < R(2, 1), Is.True);
        Assert.That(value <= R(3, 2), Is.True);

        Assert.That(value > (BigInteger)1, Is.True);
        Assert.That(value < (BigInteger)2, Is.True);
        Assert.That(value > 1L, Is.True);
        Assert.That(value < 2L, Is.True);
        Assert.That(value > 1UL, Is.True);
        Assert.That(value < 2UL, Is.True);
    }

    [Test]
    public void CompareToNullReturnsPositive()
    {
        var value = R(1, 2);
        Assert.That(value.CompareTo(null), Is.EqualTo(1));
    }

    [Test]
    public void CompareToRejectsUnsupportedType()
    {
        var value = R(1, 2);
        Assert.Throws<ArgumentException>(() => value.CompareTo("not a number"));
    }

    [Test]
    public void CompareToObjectRejectsNonBigRational()
    {
        var value = R(1, 2);
        Assert.Throws<ArgumentException>(() => ((IComparable)value).CompareTo(1));
        Assert.Throws<ArgumentException>(() => ((IComparable)value).CompareTo(1.0));
        Assert.Throws<ArgumentException>(() => ((IComparable)value).CompareTo("not a number"));
    }

    [Test]
    public void CompareToHandlesNumericTypes()
    {
        var value = R(3, 2);
        Assert.That(value.CompareTo(new BigRational(1)), Is.GreaterThan(0));
        Assert.That(value.CompareTo(new BigInteger(2)), Is.LessThan(0));
        Assert.That(value.CompareTo(1.0m), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2.0m), Is.LessThan(0));
        Assert.That(value.CompareTo(1.0d), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2.0d), Is.LessThan(0));
        Assert.That(value.CompareTo(1.0f), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2.0f), Is.LessThan(0));
        Assert.That(value.CompareTo((Half)1.0), Is.GreaterThan(0));
        Assert.That(value.CompareTo((Half)2.0), Is.LessThan(0));
        Assert.That(value.CompareTo((Int128)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((UInt128)2), Is.LessThan(0));
        Assert.That(value.CompareTo((nint)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((nuint)2), Is.LessThan(0));
        Assert.That(value.CompareTo(1UL), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2UL), Is.LessThan(0));
        Assert.That(value.CompareTo(1L), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2L), Is.LessThan(0));
        Assert.That(value.CompareTo(1U), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2U), Is.LessThan(0));
        Assert.That(value.CompareTo(1), Is.GreaterThan(0));
        Assert.That(value.CompareTo(2), Is.LessThan(0));
        Assert.That(value.CompareTo((ushort)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((ushort)2), Is.LessThan(0));
        Assert.That(value.CompareTo((char)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((char)2), Is.LessThan(0));
        Assert.That(value.CompareTo((short)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((short)2), Is.LessThan(0));
        Assert.That(value.CompareTo((byte)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((byte)2), Is.LessThan(0));
        Assert.That(value.CompareTo((sbyte)1), Is.GreaterThan(0));
        Assert.That(value.CompareTo((sbyte)2), Is.LessThan(0));
    }

    [Test]
    public void CompareToWithNonFiniteFloatingPointThrows()
    {
        var value = R(1, 2);
        Assert.That(value.CompareTo(double.NaN), Is.EqualTo(1));
        Assert.That(value.CompareTo(double.PositiveInfinity), Is.EqualTo(-1));
        Assert.That(value.CompareTo(double.NegativeInfinity), Is.EqualTo(1));

        Assert.That(value.CompareTo(float.NaN), Is.EqualTo(1));
        Assert.That(value.CompareTo(float.PositiveInfinity), Is.EqualTo(-1));
        Assert.That(value.CompareTo(float.NegativeInfinity), Is.EqualTo(1));

        Assert.That(value.CompareTo(Half.NaN), Is.EqualTo(1));
        Assert.That(value.CompareTo(Half.PositiveInfinity), Is.EqualTo(-1));
        Assert.That(value.CompareTo(Half.NegativeInfinity), Is.EqualTo(1));
    }

    [Test]
    public void EqualsHandlesNumericTypesAndNonFiniteValues()
    {
        var value = R(3, 2);
        Assert.That(value.Equals(new BigRational(3, 2)), Is.True);
        Assert.That(value.Equals(new BigInteger(2)), Is.False);
        Assert.That(value.Equals(1.5m), Is.True);
        Assert.That(value.Equals(1.5d), Is.True);
        Assert.That(value.Equals(1.5f), Is.True);
        Assert.That(value.Equals((Half)1.5), Is.True);
        Assert.That(value.Equals((Int128)1), Is.False);
        Assert.That(value.Equals((UInt128)1), Is.False);
        Assert.That(value.Equals((nint)1), Is.False);
        Assert.That(value.Equals((nuint)1), Is.False);
        Assert.That(value.Equals(1UL), Is.False);
        Assert.That(value.Equals(1L), Is.False);
        Assert.That(value.Equals(1U), Is.False);
        Assert.That(value.Equals(1), Is.False);
        Assert.That(value.Equals((ushort)1), Is.False);
        Assert.That(value.Equals((char)1), Is.False);
        Assert.That(value.Equals((short)1), Is.False);
        Assert.That(value.Equals((byte)1), Is.False);
        Assert.That(value.Equals((sbyte)1), Is.False);

        Assert.That(value.Equals(double.NaN), Is.False);
        Assert.That(value.Equals(float.PositiveInfinity), Is.False);
        Assert.That(value.Equals((Half)Half.NegativeInfinity), Is.False);
    }

    [Test]
    public void GetHashCodeMatchesCanonicalEquivalent()
    {
        var a = R(1, 2);
        var b = R(2, 4);
        Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
    }
}
