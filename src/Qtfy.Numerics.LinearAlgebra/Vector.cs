// <copyright file="Vector.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

using System.Numerics;

namespace Qtfy.Numerics.LinearAlgebra;

/// <summary>
/// A dense vector backed by aligned native memory.
/// </summary>
public sealed class Vector<TNumber>
    where TNumber : INumber<TNumber>
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
