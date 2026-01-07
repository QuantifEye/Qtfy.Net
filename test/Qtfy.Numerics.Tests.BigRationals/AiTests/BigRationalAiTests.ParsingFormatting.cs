// <copyright file="BigRationalAiTests.ParsingFormatting.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.BigRationals.AiTests;

internal sealed partial class BigRationalAiTests
{
    [Test]
    public void ParseHandlesIntegersAndFractions()
    {
        Assert.That(BigRational.Parse("5", CultureInfo.InvariantCulture), Is.EqualTo(new BigRational(5)));
        Assert.That(BigRational.Parse("3/4", CultureInfo.InvariantCulture), Is.EqualTo(R(3, 4)));
    }

    [Test]
    public void ParseThrowsForNullOrInvalidInput()
    {
        Assert.Throws<ArgumentNullException>(() => BigRational.Parse((string)null!, CultureInfo.InvariantCulture));
        Assert.Throws<FormatException>(() => BigRational.Parse("not a number", CultureInfo.InvariantCulture));
        Assert.Throws<FormatException>(() => BigRational.Parse("1/0", CultureInfo.InvariantCulture));
    }

    [Test]
    public void TryParseHandlesInvalidInput()
    {
        Assert.That(BigRational.TryParse(string.Empty, out var emptyResult), Is.False);
        Assert.That(emptyResult, Is.EqualTo(default(BigRational)));

        Assert.That(BigRational.TryParse("1/0", out var zeroResult), Is.False);
        Assert.That(zeroResult, Is.EqualTo(default(BigRational)));
    }

    [Test]
    public void TryParseHonorsNumberStylesAndProvider()
    {
        var style = NumberStyles.Integer | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;
        Assert.That(BigRational.TryParse("  +42 ", style, CultureInfo.InvariantCulture, out var result), Is.True);
        AssertRational(result, new BigInteger(42), BigInteger.One);
    }

    [Test]
    public void ParseAndTryParseSupportUtf8Spans()
    {
        var utf8 = Encoding.UTF8.GetBytes("7/8");
        Assert.That(BigRational.Parse(utf8, CultureInfo.InvariantCulture), Is.EqualTo(R(7, 8)));

        Assert.That(BigRational.TryParse(utf8, CultureInfo.InvariantCulture, out var parsed), Is.True);
        Assert.That(parsed, Is.EqualTo(R(7, 8)));

        Assert.That(BigRational.TryParse(ReadOnlySpan<byte>.Empty, CultureInfo.InvariantCulture, out var empty), Is.False);
        Assert.That(empty, Is.EqualTo(default(BigRational)));
    }

    [Test]
    public void SpanOverloadsParseAndTryParse()
    {
        ReadOnlySpan<char> span = "9/10".AsSpan();
        Assert.That(BigRational.Parse(span, CultureInfo.InvariantCulture), Is.EqualTo(R(9, 10)));

        Assert.That(BigRational.TryParse(span, CultureInfo.InvariantCulture, out var parsed), Is.True);
        Assert.That(parsed, Is.EqualTo(R(9, 10)));

        Assert.That(BigRational.TryParse(span, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed), Is.True);
        Assert.That(parsed, Is.EqualTo(R(9, 10)));
    }

    [Test]
    public void ToStringAlwaysUsesFractionFormat()
    {
        var value = R(3, 4);
        Assert.That(value.ToString(), Is.EqualTo("3/4"));
        Assert.That(value.ToString("X", CultureInfo.InvariantCulture), Is.EqualTo("3/4"));

        var hexValue = new BigRational(255, 16);
        Assert.That(hexValue.ToString("X", CultureInfo.InvariantCulture), Is.EqualTo("0FF/10"));
    }

    [Test]
    public void TryFormatWritesToCharBuffer()
    {
        var value = R(3, 4);
        Span<char> buffer = stackalloc char[8];
        Assert.That(value.TryFormat(buffer, out var written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture), Is.True);
        Assert.That(new string(buffer[..written]), Is.EqualTo("3/4"));
    }

    [Test]
    public void TryFormatFailsForSmallCharBuffer()
    {
        var value = R(123, 4567);
        Span<char> buffer = stackalloc char[4];
        Assert.That(value.TryFormat(buffer, out var written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture), Is.False);
        Assert.That(written, Is.EqualTo(0));
    }

    [Test]
    public void TryFormatWritesToUtf8Buffer()
    {
        var value = R(3, 4);
        Span<byte> buffer = stackalloc byte[8];
        Assert.That(value.TryFormat(buffer, out var written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture), Is.True);
        var text = Encoding.UTF8.GetString(buffer[..written]);
        Assert.That(text, Is.EqualTo("3/4"));
    }

    [Test]
    public void TryFormatFailsForSmallUtf8Buffer()
    {
        var value = R(123, 4567);
        Span<byte> buffer = stackalloc byte[4];
        Assert.That(value.TryFormat(buffer, out var written, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture), Is.False);
        Assert.That(written, Is.EqualTo(0));
    }
}
