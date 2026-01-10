// <copyright file="IGenericBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.GenericMath;

using Qtfy.Numerics.LinearAlgebra;
using System.Numerics;

public interface IGenericBlas
{
    public static abstract TNumber Dot<TNumber>(ReadOnlySpan<TNumber> x, ReadOnlySpan<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void AddScaled<TNumber>(TNumber alpha, ReadOnlySpan<TNumber> x, Span<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixVectorMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>;
}
