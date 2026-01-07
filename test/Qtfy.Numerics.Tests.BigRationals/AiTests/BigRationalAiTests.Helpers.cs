// <copyright file="BigRationalAiTests.Helpers.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals;

using System.Numerics;
using NUnit.Framework;

internal sealed partial class BigRationalAiTests
{
    private static BigRational R(long numerator, long denominator)
    {
        return new BigRational(numerator, denominator);
    }

    private static void AssertRational(BigRational actual, BigInteger numerator, BigInteger denominator)
    {
        Assert.That(actual.Numerator, Is.EqualTo(numerator));
        Assert.That(actual.Denominator, Is.EqualTo(denominator));
    }

    private static void AssertCanonical(BigRational value)
    {
        Assert.That(value.Denominator > BigInteger.Zero, Is.True);

        if (value.Numerator.IsZero)
        {
            Assert.That(value.Denominator, Is.EqualTo(BigInteger.One));
            return;
        }

        var gcd = BigInteger.GreatestCommonDivisor(BigInteger.Abs(value.Numerator), value.Denominator);
        Assert.That(gcd, Is.EqualTo(BigInteger.One));
    }

    private static BigInteger WrapToUnsigned(BigInteger value, int bits)
    {
        var modulus = BigInteger.One << bits;
        var result = value % modulus;
        if (result.Sign < 0)
        {
            result += modulus;
        }

        return result;
    }

    private static BigInteger WrapToSigned(BigInteger value, int bits)
    {
        var modulus = BigInteger.One << bits;
        var result = value % modulus;
        if (result.Sign < 0)
        {
            result += modulus;
        }

        var signBit = modulus >> 1;
        if (result >= signBit)
        {
            result -= modulus;
        }

        return result;
    }

    private static bool StaticIsPositive<T>(T value)
        where T : INumberBase<T>
    {
        return T.IsPositive(value);
    }

    private static bool StaticIsNegative<T>(T value)
        where T : INumberBase<T>
    {
        return T.IsNegative(value);
    }

    private static bool StaticIsZero<T>(T value)
        where T : INumberBase<T>
    {
        return T.IsZero(value);
    }

    private static bool StaticIsInteger<T>(T value)
        where T : INumberBase<T>
    {
        return T.IsInteger(value);
    }

    private static int StaticSign<T>(T value)
        where T : INumber<T>
    {
        return T.Sign(value);
    }
}
