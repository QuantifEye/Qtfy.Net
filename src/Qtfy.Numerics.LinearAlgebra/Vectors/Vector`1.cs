// <copyright file="Vector`1.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class Vector<TNumber>
    : IVector<TNumber, VectorView<TNumber>>
    where TNumber : INumber<TNumber>
{
    private readonly TNumber[] data;

    public Vector(int length)
    {
        data = new TNumber[length];
    }

    public int Length => data.Length;

    public ref TNumber GetPinnableReference()
        => ref data.Reference();

    public ref TNumber this[int index] => ref data[index];

    public VectorView<TNumber> AsView()
        => new (ref data.Reference(), data.Length);
}
