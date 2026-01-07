// <copyright file="BigRationalAiTests.Conversions.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals;

using System;
using System.Numerics;
using NUnit.Framework;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void ImplicitIntegralConversionsAreExact()
    {
        Assert.That((BigRational)(sbyte)(-5), Is.EqualTo(new BigRational(-5)));
        Assert.That((BigRational)(byte)5, Is.EqualTo(new BigRational(5)));
        Assert.That((BigRational)(short)(-6), Is.EqualTo(new BigRational(-6)));
        Assert.That((BigRational)(ushort)6, Is.EqualTo(new BigRational(6)));
        Assert.That((BigRational)-7, Is.EqualTo(new BigRational(-7)));
        Assert.That((BigRational)7U, Is.EqualTo(new BigRational(7)));
        Assert.That((BigRational)-8L, Is.EqualTo(new BigRational(-8)));
        Assert.That((BigRational)8UL, Is.EqualTo(new BigRational(8)));
        Assert.That((BigRational)(nint)(-9), Is.EqualTo(new BigRational(-9)));
        Assert.That((BigRational)(nuint)9, Is.EqualTo(new BigRational(9)));
        Assert.That((BigRational)(Int128)(-10), Is.EqualTo(new BigRational(-10)));
        Assert.That((BigRational)(UInt128)10, Is.EqualTo(new BigRational(10)));
        Assert.That((BigRational)(char)65, Is.EqualTo(new BigRational(65)));
        Assert.That((BigRational)new BigInteger(11), Is.EqualTo(new BigRational(11)));
    }

    [Test]
    public void FloatingConversionsToBigRationalRejectNonFinite()
    {
        Assert.Throws<ArgumentException>(() => _ = (BigRational)double.NaN);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)double.PositiveInfinity);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)double.NegativeInfinity);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)float.NaN);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)float.PositiveInfinity);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)float.NegativeInfinity);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)Half.NaN);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)Half.PositiveInfinity);
        Assert.Throws<ArgumentException>(() => _ = (BigRational)Half.NegativeInfinity);
    }

    [Test]
    public void ExplicitFloatingConversionsMatchExpectedValues()
    {
        var value = R(1, 8);
        Assert.That((double)value, Is.EqualTo(0.125d));
        Assert.That((float)value, Is.EqualTo(0.125f));
        Assert.That((Half)value, Is.EqualTo((Half)0.125));
        Assert.That((decimal)value, Is.EqualTo(0.125m));
    }

    [Test]
    public void ExplicitBigIntegerConversionTruncatesTowardZero()
    {
        Assert.That((BigInteger)R(7, 2), Is.EqualTo(new BigInteger(3)));
        Assert.That((BigInteger)R(-7, 2), Is.EqualTo(new BigInteger(-3)));
    }

    [Test]
    public void ExplicitSignedIntegralConversionsSaturate()
    {
        var hugePositive = new BigRational(BigInteger.One << 200, BigInteger.One);
        var hugeNegative = new BigRational(-(BigInteger.One << 200), BigInteger.One);

        Assert.That((int)hugePositive, Is.EqualTo(int.MaxValue));
        Assert.That((int)hugeNegative, Is.EqualTo(int.MinValue));
        Assert.That((long)hugePositive, Is.EqualTo(long.MaxValue));
        Assert.That((long)hugeNegative, Is.EqualTo(long.MinValue));
        Assert.That((short)hugePositive, Is.EqualTo(short.MaxValue));
        Assert.That((short)hugeNegative, Is.EqualTo(short.MinValue));
        Assert.That((sbyte)hugePositive, Is.EqualTo(sbyte.MaxValue));
        Assert.That((sbyte)hugeNegative, Is.EqualTo(sbyte.MinValue));
        Assert.That((nint)hugePositive, Is.EqualTo(nint.MaxValue));
        Assert.That((nint)hugeNegative, Is.EqualTo(nint.MinValue));
        Assert.That((Int128)hugePositive, Is.EqualTo(Int128.MaxValue));
        Assert.That((Int128)hugeNegative, Is.EqualTo(Int128.MinValue));
    }

    [Test]
    public void ExplicitUnsignedIntegralConversionsClampToRange()
    {
        var hugePositive = new BigRational(BigInteger.One << 200, BigInteger.One);
        var negative = R(-3, 2);

        Assert.That((uint)hugePositive, Is.EqualTo(uint.MaxValue));
        Assert.That((uint)negative, Is.EqualTo(0u));
        Assert.That((ulong)hugePositive, Is.EqualTo(ulong.MaxValue));
        Assert.That((ulong)negative, Is.EqualTo(0UL));
        Assert.That((ushort)hugePositive, Is.EqualTo(ushort.MaxValue));
        Assert.That((ushort)negative, Is.EqualTo((ushort)0));
        Assert.That((byte)hugePositive, Is.EqualTo(byte.MaxValue));
        Assert.That((byte)negative, Is.EqualTo((byte)0));
        Assert.That((nuint)hugePositive, Is.EqualTo(nuint.MaxValue));
        Assert.That((nuint)negative, Is.EqualTo((nuint)0));
        Assert.That((UInt128)hugePositive, Is.EqualTo(UInt128.MaxValue));
        Assert.That((UInt128)negative, Is.EqualTo(UInt128.MinValue));
        Assert.That((char)hugePositive, Is.EqualTo(char.MaxValue));
        Assert.That((char)negative, Is.EqualTo('\0'));
    }

    [Test]
    public void ExplicitIntegralConversionsTruncateFractionalPart()
    {
        Assert.That((int)R(7, 2), Is.EqualTo(3));
        Assert.That((int)R(-7, 2), Is.EqualTo(-3));
        Assert.That((uint)R(7, 2), Is.EqualTo(3u));
    }

    [Test]
    public void TryConvertFromCheckedRejectsNonFinite()
    {
        Assert.Throws<OverflowException>(() => BigRational.TryConvertFromChecked(double.NaN, out _));
        Assert.Throws<OverflowException>(() => BigRational.TryConvertFromChecked(float.PositiveInfinity, out _));
        Assert.Throws<OverflowException>(() => BigRational.TryConvertFromChecked(Half.NegativeInfinity, out _));
    }

    [Test]
    public void TryConvertFromSaturatingRejectsNonFinite()
    {
        Assert.That(BigRational.TryConvertFromSaturating(double.NaN, out var result), Is.False);
        Assert.That(result, Is.EqualTo(default(BigRational)));
        Assert.That(BigRational.TryConvertFromTruncating(float.PositiveInfinity, out result), Is.False);
        Assert.That(result, Is.EqualTo(default(BigRational)));
    }

    [Test]
    public void CreateCheckedUsesCheckedConversions()
    {
        var value = BigRational.CreateChecked(123);
        Assert.That(value, Is.EqualTo(new BigRational(123)));
        Assert.Throws<OverflowException>(() => _ = BigRational.CreateChecked(double.NaN));
    }

    [Test]
    public void CreateSaturatingRejectsUnsupportedValues()
    {
        Assert.Throws<NotSupportedException>(() => _ = BigRational.CreateSaturating(double.NaN));
        Assert.Throws<NotSupportedException>(() => _ = BigRational.CreateTruncating(float.NaN));
    }

    [Test]
    public void TryConvertToCheckedEnforcesIntegralRequirement()
    {
        var value = R(1, 2);
        Assert.Throws<OverflowException>(() => BigRational.TryConvertToChecked<int>(value, out _));

        var integer = new BigRational(12);
        Assert.That(BigRational.TryConvertToChecked<int>(integer, out var result), Is.True);
        Assert.That(result, Is.EqualTo(12));
    }

    [Test]
    public void TryConvertToSaturatingClampsIntegers()
    {
        var huge = new BigRational(BigInteger.One << 100, BigInteger.One);
        Assert.That(BigRational.TryConvertToSaturating<int>(huge, out var result), Is.True);
        Assert.That(result, Is.EqualTo(int.MaxValue));

        var negative = new BigRational(-(BigInteger.One << 100), BigInteger.One);
        Assert.That(BigRational.TryConvertToSaturating<int>(negative, out result), Is.True);
        Assert.That(result, Is.EqualTo(int.MinValue));

        Assert.That(BigRational.TryConvertToSaturating<uint>(negative, out var unsignedResult), Is.True);
        Assert.That(unsignedResult, Is.EqualTo(uint.MinValue));
    }

    [Test]
    public void TryConvertToTruncatingWrapsIntegers()
    {
        var value = new BigRational((BigInteger.One << 40) + 123, BigInteger.One);

        Assert.That(BigRational.TryConvertToTruncating<int>(value, out var intResult), Is.True);
        Assert.That(intResult, Is.EqualTo((int)WrapToSigned((BigInteger.One << 40) + 123, 32)));

        Assert.That(BigRational.TryConvertToTruncating<uint>(value, out var uintResult), Is.True);
        Assert.That(uintResult, Is.EqualTo((uint)WrapToUnsigned((BigInteger.One << 40) + 123, 32)));
    }
}
