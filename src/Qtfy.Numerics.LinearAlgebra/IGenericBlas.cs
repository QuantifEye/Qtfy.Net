// <copyright file="IGenericBlas.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

using Qtfy.Numerics.LinearAlgebra.Matrices;

namespace Qtfy.Numerics.LinearAlgebra;

public interface IGenericBlas
{
    public static abstract TNumber Dot<TNumber, TVectorViewX, TVectorViewY>(TVectorViewX x, TVectorViewY y)
        where TVectorViewX : IVectorView<TNumber>
        where TVectorViewY : IVectorView<TNumber>
        where TNumber : INumberBase<TNumber>;

    public static abstract void AddScaled<TNumber, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TVectorViewX x,
        TVectorViewY y)
        where TVectorViewX : IVectorView<TNumber>
        where TVectorViewY : IVectorView<TNumber>
        where TNumber : INumberBase<TNumber>;

    public static abstract void MatrixVectorMultiply<TNumber, TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TMatrixView matrix,
        TVectorViewX x,
        TNumber beta,
        TVectorViewY y)
        where TNumber : INumberBase<TNumber>
        where TMatrixRow : IVectorView<TNumber>
        where TMatrixColumn : IVectorView<TNumber>
        where TMatrixView : IMatrixView<TNumber, TMatrixRow, TMatrixColumn>
        where TVectorViewY : IVectorView<TNumber>;

    public static abstract void MatrixMultiply<TNumber>(
        TNumber alpha,
        MatrixView<TNumber> a,
        MatrixView<TNumber> b,
        TNumber beta,
        MatrixView<TNumber> c)
        where TNumber : INumberBase<TNumber>;
}
