// <copyright file="BigRationalAiTests.Series.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals.AiTests;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void ExpHandlesEdgeTerms()
    {
        Assert.That(BigRational.Exp(R(1, 2), 0), Is.EqualTo(BigRational.Zero));
        Assert.That(BigRational.Exp(R(1, 2), 1), Is.EqualTo(BigRational.One));
        Assert.Throws<ArgumentException>(() => _ = BigRational.Exp(R(1, 2), -1));
    }

    [Test]
    public void ExpMatchesTaylorSeries()
    {
        var power = R(1, 2);
        var expected = new BigRational(new BigInteger(79), new BigInteger(48));
        Assert.That(BigRational.Exp(power, 4), Is.EqualTo(expected));
    }

    [Test]
    public void LogHandlesTermsAndKnownValues()
    {
        Assert.Throws<ArgumentException>(() => _ = BigRational.Log(R(2, 1), -1));
        Assert.Throws<DivideByZeroException>(() => _ = BigRational.Log(R(1, 1), 1));

        var expectedTerms1 = R(2, 3);
        Assert.That(BigRational.Log(R(2, 1), 1), Is.EqualTo(expectedTerms1));

        var expectedTerms2 = R(56, 81);
        Assert.That(BigRational.Log(R(2, 1), 2), Is.EqualTo(expectedTerms2));
    }
}
