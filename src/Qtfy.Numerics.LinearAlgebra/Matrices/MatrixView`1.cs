// <copyright file="MatrixView`1.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct MatrixView<TElement> :
    IStridedMatrixView<TElement, StrideVectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly ref TElement data;
    private readonly int rows;
    private readonly int columns;
    private readonly int rowSpan;
    private readonly int colSpan;

    internal MatrixView(ref TElement data, int rows, int columns, int rowSpan, int colSpan)
    {
        this.data = ref data;
        this.rows = rows;
        this.columns = columns;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public int RowStride => this.rowSpan;

    public int ColumnStride => this.colSpan;

    public static bool IsAlwaysSquare() => false;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public int RowSpan => this.rowSpan;

    public int ColSpan => this.colSpan;

    public ref TElement GetPinnableReference()
        => ref this.data;

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * this.rowSpan + column * this.colSpan;
            return ref Add(ref this.data, offset);
        }
    }

    public StrideVectorView<TElement> Row(int row)
    {
        var offset = row * this.rowSpan;
        return new (
            ref Add(ref this.data, offset),
            this.colSpan,
            this.columns);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        var offset = column * this.colSpan;
        return new (
            ref Add(ref this.data, offset),
            this.rowSpan,
            this.rows);
    }

}
