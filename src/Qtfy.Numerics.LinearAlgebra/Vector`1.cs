// <copyright file="Vector`1.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

using System.Numerics;

public sealed class Vector<TNumber>
    where TNumber : unmanaged, INumber<TNumber>
{
    private readonly TNumber[] data;

    public Vector(int length)
    {
        this.data = new TNumber[length];
    }

    public int Length => this.data.Length;

    public ref TNumber this[int index] => ref this.data[index];

    public Span<TNumber> AsView()
    {
        return this.data.AsSpan();
    }
}
