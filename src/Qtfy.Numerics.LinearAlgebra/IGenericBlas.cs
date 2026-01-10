// <copyright file="IGenericBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

using System.Numerics;

namespace Qtfy.Numerics.LinearAlgebra;

public interface IGenericBlas
{
    public static abstract TNumber Dot<TNumber>(ReadOnlySpan<TNumber> x, ReadOnlySpan<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void AddScaled<TNumber>(TNumber alpha, ReadOnlySpan<TNumber> x, Span<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void Scale<TNumber>(TNumber alpha, Span<TNumber> x)
        where TNumber : INumberBase<TNumber>;

    public static abstract void Copy<T>(ReadOnlySpan<T> x, Span<T> y);

    public static abstract void Swap<T>(Span<T> x, Span<T> y);

    public static abstract TNumber AbsSum<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : INumberBase<TNumber>;

    public static abstract int ArgMaxAbs<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : INumberBase<TNumber>, IComparisonOperators<TNumber, TNumber, bool>;

    public static abstract TNumber Norm2<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : IFloatingPointIeee754<TNumber>;

    public static abstract void ComputeGivensRotation<TNumber>(ref TNumber a, ref TNumber b, out TNumber c, out TNumber s)
        where TNumber : IFloatingPointIeee754<TNumber>;

    public static abstract void ApplyGivensRotation<TNumber>(Span<TNumber> x, Span<TNumber> y, TNumber c, TNumber s)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixVectorMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void Rank1Update<TNumber>(
        TNumber alpha,
        ReadOnlySpan<TNumber> x,
        ReadOnlySpan<TNumber> y,
        MatrixView<TNumber> matrix)
        where TNumber : INumberBase<TNumber>;

    public static abstract void SymmetricMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>;

    public static abstract void SymmetricRank1Update<TNumber>(
        Uplo uplo,
        TNumber alpha,
        ReadOnlySpan<TNumber> x,
        MatrixView<TNumber> matrix)
        where TNumber : INumberBase<TNumber>;

    public static abstract void TriangularMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        Diag diag,
        MatrixView<TNumber> matrix,
        Span<TNumber> x)
        where TNumber : INumberBase<TNumber>;

    public static abstract void TriangularSolve<TNumber>(
        Uplo uplo,
        Diag diag,
        MatrixView<TNumber> matrix,
        Span<TNumber> x)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>;

    public static abstract void SymmetricRankKUpdate<TNumber>(
        Uplo uplo,
        TNumber alpha,
        MatrixView<TNumber> a,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>;

    public static abstract void TriangularSolveMultiple<TNumber>(
        Side side,
        Uplo uplo,
        Diag diag,
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>;

    public static abstract void CopyScaled<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixAddScaled<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        TNumber beta,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>;
}
