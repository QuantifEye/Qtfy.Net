// <copyright file="MatrixView.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct MatrixView<TNumber>
{
    private readonly Span<TNumber> data;
    private readonly int rows;
    private readonly int columns;
    private readonly int rowSpan;
    private readonly int colSpan;

    internal MatrixView(Span<TNumber> data, int rows, int columns, int rowSpan, int colSpan)
    {
        this.data = data;
        this.rows = rows;
        this.columns = columns;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public int RowSpan => this.rowSpan;

    public int ColSpan => this.colSpan;

    internal Span<TNumber> Data => this.data;

    public ref readonly TNumber this[int row, int column]
    {
        get
        {
            var offset = (row * this.rowSpan) + (column * this.colSpan);
            return ref this.data[offset];
        }
    }
}
