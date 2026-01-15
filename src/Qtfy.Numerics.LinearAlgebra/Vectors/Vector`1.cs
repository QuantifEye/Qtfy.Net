// <copyright file="Vector`1.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class Vector<TNumber> : IVectorView<TNumber>
    where TNumber : INumber<TNumber>
{
    private readonly TNumber[] data;

    public Vector(int length)
    {
        this.data = new TNumber[length];
    }

    public int Length => this.data.Length;

    public int Stride
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => 1;
    }

    public ref TNumber GetPinnableReference()
        => ref this.data.Reference();

    public ref TNumber this[int index] => ref this.data[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StrideIsAlwaysOne() => true;

    public VectorView<TNumber> AsView()
    {
        return new VectorView<TNumber>(ref this.data.Reference(), this.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPinned() => false;
}
