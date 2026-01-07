// <copyright file="BigRationalAiTests.Operators.cs" company="QuantifEye">
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
    public void UnaryOperatorsBehaveAsExpected()
    {
        var value = R(3, 2);
        Assert.That(+value, Is.EqualTo(value));
        Assert.That(-value, Is.EqualTo(R(-3, 2)));

        var incremented = value;
        incremented++;
        Assert.That(incremented, Is.EqualTo(R(5, 2)));

        var decremented = value;
        decremented--;
        Assert.That(decremented, Is.EqualTo(R(1, 2)));
    }

    [Test]
    public void ArithmeticWithBigRationalWorks()
    {
        var left = R(1, 2);
        var right = R(1, 3);

        Assert.That(left + right, Is.EqualTo(R(5, 6)));
        Assert.That(left - right, Is.EqualTo(R(1, 6)));
        Assert.That(left * right, Is.EqualTo(R(1, 6)));
        Assert.That(left / right, Is.EqualTo(R(3, 2)));
    }

    [Test]
    public void ModuloUsesTruncatedQuotient()
    {
        var dividend = R(5, 2);
        var divisor = BigRational.One;
        Assert.That(dividend % divisor, Is.EqualTo(R(1, 2)));

        var negativeDividend = R(-5, 2);
        Assert.That(negativeDividend % divisor, Is.EqualTo(R(-1, 2)));
    }

    [Test]
    public void DivisionByZeroThrows()
    {
        Assert.Throws<DivideByZeroException>(() => _ = BigRational.One / BigRational.Zero);
    }

    [Test]
    public void OperationsWithBigIntegerWork()
    {
        var value = R(3, 2);
        var integer = new BigInteger(2);

        Assert.That(value + integer, Is.EqualTo(R(7, 2)));
        Assert.That(integer + value, Is.EqualTo(R(7, 2)));
        Assert.That(value - integer, Is.EqualTo(R(-1, 2)));
        Assert.That(integer - value, Is.EqualTo(R(1, 2)));
        Assert.That(value * integer, Is.EqualTo(R(3, 1)));
        Assert.That(integer * value, Is.EqualTo(R(3, 1)));
        Assert.That(value / integer, Is.EqualTo(R(3, 4)));
        Assert.That(integer / value, Is.EqualTo(R(4, 3)));
        Assert.That(value % integer, Is.EqualTo(R(3, 2)));
        Assert.That(integer % value, Is.EqualTo(R(1, 2)));
    }

    [Test]
    public void OperationsWithLongWork()
    {
        var value = R(3, 2);
        long integer = 2;

        Assert.That(value + integer, Is.EqualTo(R(7, 2)));
        Assert.That(integer + value, Is.EqualTo(R(7, 2)));
        Assert.That(value - integer, Is.EqualTo(R(-1, 2)));
        Assert.That(integer - value, Is.EqualTo(R(1, 2)));
        Assert.That(value * integer, Is.EqualTo(R(3, 1)));
        Assert.That(integer * value, Is.EqualTo(R(3, 1)));
        Assert.That(value / integer, Is.EqualTo(R(3, 4)));
        Assert.That(integer / value, Is.EqualTo(R(4, 3)));
        Assert.That(value % integer, Is.EqualTo(R(3, 2)));
        Assert.That(integer % value, Is.EqualTo(R(1, 2)));
    }

    [Test]
    public void OperationsWithUlongWork()
    {
        var value = R(3, 2);
        ulong integer = 2;

        Assert.That(value + integer, Is.EqualTo(R(7, 2)));
        Assert.That(integer + value, Is.EqualTo(R(7, 2)));
        Assert.That(value - integer, Is.EqualTo(R(-1, 2)));
        Assert.That(integer - value, Is.EqualTo(R(1, 2)));
        Assert.That(value * integer, Is.EqualTo(R(3, 1)));
        Assert.That(integer * value, Is.EqualTo(R(3, 1)));
        Assert.That(value / integer, Is.EqualTo(R(3, 4)));
        Assert.That(integer / value, Is.EqualTo(R(4, 3)));
        Assert.That(value % integer, Is.EqualTo(R(3, 2)));
        Assert.That(integer % value, Is.EqualTo(R(1, 2)));
    }

    [Test]
    public void AbsAndCopySignBehave()
    {
        var value = R(-3, 2);
        Assert.That(BigRational.Abs(value), Is.EqualTo(R(3, 2)));
        Assert.That(BigRational.CopySign(value, R(1, 2)), Is.EqualTo(R(3, 2)));
        Assert.That(BigRational.CopySign(R(3, 2), R(-1, 2)), Is.EqualTo(R(-3, 2)));
    }

    [Test]
    public void MultiplyAddEstimateMatchesExpression()
    {
        var left = R(3, 2);
        var right = R(4, 3);
        var addend = R(1, 4);
        Assert.That(BigRational.MultiplyAddEstimate(left, right, addend), Is.EqualTo((left * right) + addend));
    }

    [Test]
    public void PowHandlesZeroAndNegativeExponents()
    {
        var value = R(2, 3);
        Assert.That(BigRational.Pow(value, 0), Is.EqualTo(BigRational.One));
        Assert.That(BigRational.Pow(value, 2), Is.EqualTo(R(4, 9)));
        Assert.That(BigRational.Pow(value, -2), Is.EqualTo(R(9, 4)));
        Assert.That(BigRational.Pow(BigRational.Zero, 3), Is.EqualTo(BigRational.Zero));
        Assert.Throws<DivideByZeroException>(() => _ = BigRational.Pow(BigRational.Zero, -1));
    }
}
