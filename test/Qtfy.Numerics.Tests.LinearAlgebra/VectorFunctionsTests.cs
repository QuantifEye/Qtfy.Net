// <copyright file="VectorFunctionsTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.Tests.LinearAlgebra;

using Qtfy.Numerics.LinearAlgebra;

internal sealed class VectorFunctionsTests
{
    [Test]
    public void DotReturnsExpectedValue()
    {
        var left = new[] { 1d, -2d, 3d };
        var right = new[] { 4d, 5d, -6d };

        var actual = VectorFunctions.Dot(left, right);

        Assert.That(actual, Is.EqualTo(-24d));
    }

    [Test]
    public void DotThrowsForMismatchedLengths()
    {
        Assert.Throws<ArgumentException>(() => VectorFunctions.Dot([1d, 2d], [1d]));
    }

    [Test]
    public void DotThrowsForNullVectors()
    {
        Assert.Throws<ArgumentNullException>(() => VectorFunctions.Dot(null, [1d]));
        Assert.Throws<ArgumentNullException>(() => VectorFunctions.Dot([1d], null));
    }
}
