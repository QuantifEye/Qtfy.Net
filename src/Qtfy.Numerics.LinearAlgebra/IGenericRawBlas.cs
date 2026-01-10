// <copyright file="IGenericRawBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.GenericMath;

using Qtfy.Numerics.LinearAlgebra;
using System.Numerics;

public interface IGenericRawBlas
{
    public static abstract TNumber Dot<TNumber>(
        int n,
        ref readonly TNumber x,
        int strideX,
        ref readonly TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>;

    public static abstract void AddScaled<TNumber>(
        int n,
        TNumber alpha,
        ref readonly TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>;

    public static abstract void Scale<TNumber>(
        int n,
        TNumber alpha,
        ref TNumber x,
        int strideX)
        where TNumber : INumberBase<TNumber>;

    public static abstract void Copy<T>(
        int n,
        ref readonly T x,
        int strideX,
        ref T y,
        int strideY);

    public static abstract void Swap<T>(
        int n,
        ref T x,
        int strideX,
        ref T y,
        int strideY);

    public static abstract TNumber AbsSum<TNumber>(
        int n,
        ref readonly TNumber x,
        int strideX)
        where TNumber : INumberBase<TNumber>;

    public static abstract int ArgMaxAbs<TNumber>(
        int n,
        ref readonly TNumber x,
        int strideX)
        where TNumber : INumberBase<TNumber>, IComparisonOperators<TNumber, TNumber, bool>;

    public static abstract TNumber Norm2<TNumber>(
        int n,
        ref readonly TNumber x,
        int strideX)
        where TNumber : IFloatingPointIeee754<TNumber>;

    public static abstract void ComputeGivensRotation<TNumber>(
        ref TNumber a,
        ref TNumber b,
        out TNumber c,
        out TNumber s)
        where TNumber : IFloatingPointIeee754<TNumber>;

    public static abstract void ApplyGivensRotation<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY,
        TNumber c,
        TNumber s)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixVectorMultiply<TNumber>(
        int rows,
        int columns,
        TNumber alpha,
        ref readonly TNumber matrix,
        int rowStride,
        int colStride,
        ref readonly TNumber x,
        int strideX,
        TNumber beta,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>;

    public static abstract void Rank1Update<TNumber>(
        int rows,
        int columns,
        TNumber alpha,
        ref readonly TNumber x,
        int strideX,
        ref readonly TNumber y,
        int strideY,
        ref TNumber matrix,
        int rowStride,
        int colStride)
        where TNumber : INumberBase<TNumber>;

    public static abstract void SymmetricMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        int size,
        TNumber alpha,
        ref readonly TNumber matrix,
        int rowStride,
        int colStride,
        ref readonly TNumber x,
        int strideX,
        TNumber beta,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>;

    public static abstract void SymmetricRank1Update<TNumber>(
        Uplo uplo,
        int size,
        TNumber alpha,
        ref readonly TNumber x,
        int strideX,
        ref TNumber matrix,
        int rowStride,
        int colStride)
        where TNumber : INumberBase<TNumber>;

    public static abstract void TriangularMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        Diag diag,
        int size,
        ref readonly TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX)
        where TNumber : INumberBase<TNumber>;

    public static abstract void TriangularSolve<TNumber>(
        Uplo uplo,
        Diag diag,
        int size,
        ref readonly TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixMultiply<TNumber>(
        int m,
        int n,
        int k,
        TNumber alpha,
        ref readonly TNumber a,
        int rowStrideA,
        int colStrideA,
        ref readonly TNumber b,
        int rowStrideB,
        int colStrideB,
        TNumber beta,
        ref TNumber c,
        int rowStrideC,
        int colStrideC)
        where TNumber : INumberBase<TNumber>;

    public static abstract void SymmetricRankKUpdate<TNumber>(
        Uplo uplo,
        int size,
        int k,
        TNumber alpha,
        ref readonly TNumber a,
        int rowStrideA,
        int colStrideA,
        TNumber beta,
        ref TNumber c,
        int rowStrideC,
        int colStrideC)
        where TNumber : INumberBase<TNumber>;

    public static abstract void TriangularSolveMultiple<TNumber>(
        Side side,
        Uplo uplo,
        Diag diag,
        int m,
        int n,
        TNumber alpha,
        ref readonly TNumber a,
        int rowStrideA,
        int colStrideA,
        ref TNumber b,
        int rowStrideB,
        int colStrideB)
        where TNumber : INumberBase<TNumber>;

    public static abstract void CopyScaled<TNumber>(
        int rows,
        int columns,
        TNumber alpha,
        ref readonly TNumber a,
        int rowStrideA,
        int colStrideA,
        ref TNumber b,
        int rowStrideB,
        int colStrideB)
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixAddScaled<TNumber>(
        int rows,
        int columns,
        TNumber alpha,
        ref readonly TNumber a,
        int rowStrideA,
        int colStrideA,
        TNumber beta,
        ref TNumber b,
        int rowStrideB,
        int colStrideB)
        where TNumber : INumberBase<TNumber>;
}
