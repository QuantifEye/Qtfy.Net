// <copyright file="MatrixView.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

/// <summary>
/// A read-only view over a matrix.
/// </summary>
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

    /// <summary>
    /// Gets the number of rows in the view.
    /// </summary>
    public int Rows => this.rows;

    /// <summary>
    /// Gets the number of columns in the view.
    /// </summary>
    public int Columns => this.columns;

    /// <summary>
    /// Gets the element stride between adjacent rows.
    /// </summary>
    public int RowSpan => this.rowSpan;

    /// <summary>
    /// Gets the element stride between adjacent columns.
    /// </summary>
    public int ColSpan => this.colSpan;

    /// <summary>
    /// Gets the element at the specified indices.
    /// </summary>
    /// <param name="row">
    /// The row index.
    /// </param>
    /// <param name="column">
    /// The column index.
    /// </param>
    /// <returns>
    /// The element at the specified indices.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="row"/> or <paramref name="column"/> is out of range.
    /// </exception>
    public ref readonly TNumber this[int row, int column]
    {
        get
        {
            var offset = checked(row * this.rowSpan + column * this.colSpan);
            return ref this.data[offset];
        }
    }
}
