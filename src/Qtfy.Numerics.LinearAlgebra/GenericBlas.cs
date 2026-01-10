// <copyright file="GenericBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.GenericMath;

using Qtfy.Numerics.LinearAlgebra;
using System.Numerics;

public readonly struct GenericBlas : IGenericBlas
{
    public static TNumber Dot<TNumber>(ReadOnlySpan<TNumber> x, ReadOnlySpan<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void AddScaled<TNumber>(TNumber alpha, ReadOnlySpan<TNumber> x, Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void Scale<TNumber>(TNumber alpha, Span<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void Copy<T>(ReadOnlySpan<T> x, Span<T> y)
    {
        throw new NotImplementedException();
    }

    public static void Swap<T>(Span<T> x, Span<T> y)
    {
        throw new NotImplementedException();
    }

    public static TNumber AbsSum<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static int ArgMaxAbs<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : INumberBase<TNumber>, IComparisonOperators<TNumber, TNumber, bool>
    {
        throw new NotImplementedException();
    }

    public static TNumber Norm2<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void ComputeGivensRotation<TNumber>(ref TNumber a, ref TNumber b, out TNumber c, out TNumber s)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void ApplyGivensRotation<TNumber>(Span<TNumber> x, Span<TNumber> y, TNumber c, TNumber s)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void MatrixVectorMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void Rank1Update<TNumber>(
        TNumber alpha,
        ReadOnlySpan<TNumber> x,
        ReadOnlySpan<TNumber> y,
        MatrixView<TNumber> matrix)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void SymmetricMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void SymmetricRank1Update<TNumber>(
        Uplo uplo,
        TNumber alpha,
        ReadOnlySpan<TNumber> x,
        MatrixView<TNumber> matrix)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void TriangularMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        Diag diag,
        MatrixView<TNumber> matrix,
        Span<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void TriangularSolve<TNumber>(
        Uplo uplo,
        Diag diag,
        MatrixView<TNumber> matrix,
        Span<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void MatrixMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void SymmetricRankKUpdate<TNumber>(
        Uplo uplo,
        TNumber alpha,
        MatrixView<TNumber> a,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void TriangularSolveMultiple<TNumber>(
        Side side,
        Uplo uplo,
        Diag diag,
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void CopyScaled<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void MatrixAddScaled<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        TNumber beta,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }
}
