// <copyright file="GenericBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.GenericMath;

using LinearAlgebra;

public readonly struct GenericBlas<TRawBlas> : IGenericBlas
    where TRawBlas : IGenericRawBlas
{
    public static TNumber Dot<TNumber>(ReadOnlySpan<TNumber> x, ReadOnlySpan<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(x);
        ref readonly var yRef = ref MemoryMarshal.GetReference(y);
        return TRawBlas.Dot(
            n: x.Length,
            x: in xRef,
            strideX: 1,
            y: in yRef,
            strideY: 1);
    }

    public static void AddScaled<TNumber>(TNumber alpha, ReadOnlySpan<TNumber> x, Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
        TRawBlas.AddScaled(
            n: x.Length,
            alpha: alpha,
            x: in xRef,
            strideX: 1,
            y: ref yRef,
            strideY: 1);
    }

    public static void MatrixVectorMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> matrix,
        ReadOnlySpan<TNumber> x,
        TNumber beta,
        Span<TNumber> y)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var matrixRef = ref matrix.GetReference();
        ref readonly var xRef = ref MemoryMarshal.GetReference(x);
        ref var yRef = ref MemoryMarshal.GetReference(y);
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

    public static void MatrixMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>
    {
        ref readonly var aRef = ref a.GetReference();
        ref readonly var bRef = ref b.GetReference();
        ref var cRef = ref c.GetReference();
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
}
