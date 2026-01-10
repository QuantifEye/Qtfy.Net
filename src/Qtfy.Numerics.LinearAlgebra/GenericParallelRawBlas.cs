// <copyright file="GenericParallelRawBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.GenericMath;

using System.Numerics;

public readonly struct GenericParallelRawBlas : IGenericRawBlas
{
    public static TNumber Dot<TNumber>(
        int n,
        ref readonly TNumber x,
        int strideX,
        ref readonly TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void AddScaled<TNumber>(
        int n,
        TNumber alpha,
        ref readonly TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void MatrixVectorMultiply<TNumber>(
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
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }

    public static void MatrixMultiply<TNumber>(
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
        where TNumber : INumberBase<TNumber>
    {
        throw new NotImplementedException();
    }
}
