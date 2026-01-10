// <copyright file="GenericBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

using System.Numerics;
using System.Runtime.InteropServices;

public readonly struct GenericBlas<TRawBlas> : IGenericBlas
    where TRawBlas : IGenericRawBlas
{
    public static TNumber Dot<TNumber>(ReadOnlySpan<TNumber> x, ReadOnlySpan<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        ref readonly var yRef = ref MemoryMarshal.GetReference(span: y);
        return TRawBlas.Dot(n: x.Length, x: in xRef, strideX: 1, y: in yRef, strideY: 1);
    }

    public static void AddScaled<TNumber>(TNumber alpha, ReadOnlySpan<TNumber> x, Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        ref var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.AddScaled(n: x.Length, alpha: alpha, x: in xRef, strideX: 1, y: ref yRef, strideY: 1);
    }

    public static void Scale<TNumber>(TNumber alpha, Span<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        ref var xRef = ref MemoryMarshal.GetReference(span: x);
        TRawBlas.Scale(n: x.Length, alpha: alpha, x: ref xRef, strideX: 1);
    }

    public static void Copy<T>(ReadOnlySpan<T> x, Span<T> y)
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        ref var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.Copy(n: x.Length, x: in xRef, strideX: 1, y: ref yRef, strideY: 1);
    }

    public static void Swap<T>(Span<T> x, Span<T> y)
    {
        ref var xRef = ref MemoryMarshal.GetReference(span: x);
        ref var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.Swap(n: x.Length, x: ref xRef, strideX: 1, y: ref yRef, strideY: 1);
    }

    public static TNumber AbsSum<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        return TRawBlas.AbsSum(n: x.Length, x: in xRef, strideX: 1);
    }

    public static int ArgMaxAbs<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : INumberBase<TNumber>, IComparisonOperators<TNumber, TNumber, bool>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        return TRawBlas.ArgMaxAbs(n: x.Length, x: in xRef, strideX: 1);
    }

    public static TNumber Norm2<TNumber>(ReadOnlySpan<TNumber> x)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        return TRawBlas.Norm2(n: x.Length, x: in xRef, strideX: 1);
    }

    public static void ComputeGivensRotation<TNumber>(ref TNumber a, ref TNumber b, out TNumber c, out TNumber s)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        TRawBlas.ComputeGivensRotation(a: ref a, b: ref b, c: out c, s: out s);
    }

    public static void ApplyGivensRotation<TNumber>(Span<TNumber> x, Span<TNumber> y, TNumber c, TNumber s)
        where TNumber : INumberBase<TNumber>
    {
        ref var xRef = ref MemoryMarshal.GetReference(span: x);
        ref var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.ApplyGivensRotation(n: x.Length, x: ref xRef, strideX: 1, y: ref yRef, strideY: 1, c: c, s: s);
    }

    public static void MatrixVectorMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        var matrixData = matrix.Data;
        ref readonly var matrixRef = ref MemoryMarshal.GetReference(span: matrixData);
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        ref var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.MatrixVectorMultiply(
            rows: matrix.Rows,
            columns: matrix.Columns,
            alpha: alpha,
            matrix: in matrixRef,
            rowStride: matrix.RowSpan,
            colStride: matrix.ColSpan,
            x: in xRef,
            strideX: 1,
            beta: beta,
            y: ref yRef,
            strideY: 1);
    }

    public static void Rank1Update<TNumber>(
        TNumber alpha,
        ReadOnlySpan<TNumber> x,
        ReadOnlySpan<TNumber> y,
        MatrixView<TNumber> matrix)
        where TNumber : INumberBase<TNumber>
    {
        var matrixData = matrix.Data;
        ref var matrixRef = ref MemoryMarshal.GetReference(span: matrixData);
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        ref readonly var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.Rank1Update(
            rows: matrix.Rows,
            columns: matrix.Columns,
            alpha: alpha,
            x: in xRef,
            strideX: 1,
            y: in yRef,
            strideY: 1,
            matrix: ref matrixRef,
            rowStride: matrix.RowSpan,
            colStride: matrix.ColSpan);
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
        var matrixData = matrix.Data;
        ref readonly var matrixRef = ref MemoryMarshal.GetReference(span: matrixData);
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        ref var yRef = ref MemoryMarshal.GetReference(span: y);
        TRawBlas.SymmetricMatrixVectorMultiply(
            uplo: uplo,
            size: matrix.Rows,
            alpha: alpha,
            matrix: in matrixRef,
            rowStride: matrix.RowSpan,
            colStride: matrix.ColSpan,
            x: in xRef,
            strideX: 1,
            beta: beta,
            y: ref yRef,
            strideY: 1);
    }

    public static void SymmetricRank1Update<TNumber>(
        Uplo uplo,
        TNumber alpha,
        ReadOnlySpan<TNumber> x,
        MatrixView<TNumber> matrix)
        where TNumber : INumberBase<TNumber>
    {
        var matrixData = matrix.Data;
        ref var matrixRef = ref MemoryMarshal.GetReference(span: matrixData);
        ref readonly var xRef = ref MemoryMarshal.GetReference(span: x);
        TRawBlas.SymmetricRank1Update(
            uplo: uplo,
            size: matrix.Rows,
            alpha: alpha,
            x: in xRef,
            strideX: 1,
            matrix: ref matrixRef,
            rowStride: matrix.RowSpan,
            colStride: matrix.ColSpan);
    }

    public static void TriangularMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        Diag diag,
        MatrixView<TNumber> matrix,
        Span<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        var matrixData = matrix.Data;
        ref readonly var matrixRef = ref MemoryMarshal.GetReference(span: matrixData);
        ref var xRef = ref MemoryMarshal.GetReference(span: x);
        TRawBlas.TriangularMatrixVectorMultiply(
            uplo: uplo,
            diag: diag,
            size: matrix.Rows,
            matrix: in matrixRef,
            rowStride: matrix.RowSpan,
            colStride: matrix.ColSpan,
            x: ref xRef,
            strideX: 1);
    }

    public static void TriangularSolve<TNumber>(
        Uplo uplo,
        Diag diag,
        MatrixView<TNumber> matrix,
        Span<TNumber> x)
        where TNumber : INumberBase<TNumber>
    {
        var matrixData = matrix.Data;
        ref readonly var matrixRef = ref MemoryMarshal.GetReference(span: matrixData);
        ref var xRef = ref MemoryMarshal.GetReference(span: x);
        TRawBlas.TriangularSolve(
            uplo: uplo,
            diag: diag,
            size: matrix.Rows,
            matrix: in matrixRef,
            rowStride: matrix.RowSpan,
            colStride: matrix.ColSpan,
            x: ref xRef,
            strideX: 1);
    }

    public static void MatrixMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>
    {
        var aData = a.Data;
        var bData = b.Data;
        var cData = c.Data;
        ref readonly var aRef = ref MemoryMarshal.GetReference(span: aData);
        ref readonly var bRef = ref MemoryMarshal.GetReference(span: bData);
        ref var cRef = ref MemoryMarshal.GetReference(span: cData);
        TRawBlas.MatrixMultiply(
            m: a.Rows,
            n: b.Columns,
            k: a.Columns,
            alpha: alpha,
            a: in aRef,
            rowStrideA: a.RowSpan,
            colStrideA: a.ColSpan,
            b: in bRef,
            rowStrideB: b.RowSpan,
            colStrideB: b.ColSpan,
            beta: beta,
            c: ref cRef,
            rowStrideC: c.RowSpan,
            colStrideC: c.ColSpan);
    }

    public static void SymmetricRankKUpdate<TNumber>(
        Uplo uplo,
        TNumber alpha,
        MatrixView<TNumber> a,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>
    {
        var aData = a.Data;
        var cData = c.Data;
        ref readonly var aRef = ref MemoryMarshal.GetReference(span: aData);
        ref var cRef = ref MemoryMarshal.GetReference(span: cData);
        TRawBlas.SymmetricRankKUpdate(
            uplo: uplo,
            size: a.Rows,
            k: a.Columns,
            alpha: alpha,
            a: in aRef,
            rowStrideA: a.RowSpan,
            colStrideA: a.ColSpan,
            beta: beta,
            c: ref cRef,
            rowStrideC: c.RowSpan,
            colStrideC: c.ColSpan);
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
        var aData = a.Data;
        var bData = b.Data;
        ref readonly var aRef = ref MemoryMarshal.GetReference(span: aData);
        ref var bRef = ref MemoryMarshal.GetReference(span: bData);
        TRawBlas.TriangularSolveMultiple(
            side: side,
            uplo: uplo,
            diag: diag,
            m: b.Rows,
            n: b.Columns,
            alpha: alpha,
            a: in aRef,
            rowStrideA: a.RowSpan,
            colStrideA: a.ColSpan,
            b: ref bRef,
            rowStrideB: b.RowSpan,
            colStrideB: b.ColSpan);
    }

    public static void CopyScaled<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>
    {
        var aData = a.Data;
        var bData = b.Data;
        ref readonly var aRef = ref MemoryMarshal.GetReference(span: aData);
        ref var bRef = ref MemoryMarshal.GetReference(span: bData);
        TRawBlas.CopyScaled(
            rows: a.Rows,
            columns: a.Columns,
            alpha: alpha,
            a: in aRef,
            rowStrideA: a.RowSpan,
            colStrideA: a.ColSpan,
            b: ref bRef,
            rowStrideB: b.RowSpan,
            colStrideB: b.ColSpan);
    }

    public static void MatrixAddScaled<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        TNumber beta,
        MatrixView<TNumber> b)
        where TNumber : INumberBase<TNumber>
    {
        var aData = a.Data;
        var bData = b.Data;
        ref readonly var aRef = ref MemoryMarshal.GetReference(span: aData);
        ref var bRef = ref MemoryMarshal.GetReference(span: bData);
        TRawBlas.MatrixAddScaled(
            rows: a.Rows,
            columns: a.Columns,
            alpha: alpha,
            a: in aRef,
            rowStrideA: a.RowSpan,
            colStrideA: a.ColSpan,
            beta: beta,
            b: ref bRef,
            rowStrideB: b.RowSpan,
            colStrideB: b.ColSpan);
    }
}
