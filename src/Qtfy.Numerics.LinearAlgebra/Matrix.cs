// <copyright file="Matrix.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace Qtfy.Numerics.LinearAlgebra;

using Qtfy.Memory;

/// <summary>
/// A dense matrix backed by aligned native memory.
/// </summary>
public sealed class Matrix<TNumber>
    where TNumber : unmanaged
{
    private readonly TNumber[] data;
    private readonly int rows;
    private readonly int columns;
    private readonly int rowSpan;
    private readonly int colSpan;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class.
    /// </summary>
    /// <param name="rows">
    /// The number of rows.
    /// </param>
    /// <param name="columns">
    /// The number of columns.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="rows"/> or <paramref name="columns"/> is less than or equal to zero.
    /// </exception>
    public Matrix(int rows, int columns, bool isRowMajor = true)
    {
        if (rows <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rows), "Rows must be greater than zero.");
        }

        if (columns <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(columns), "Columns must be greater than zero.");
        }

        // TODO: Reimplement this based on new Constructor
    }

    /// <summary>
    /// Gets the number of rows in the matrix.
    /// </summary>
    public int Rows => this.rows;

    /// <summary>
    /// Gets the number of columns in the matrix.
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


    internal TNumber[] Data { get; }

    /// <summary>
    /// Gets or sets an element in the matrix.
    /// </summary>
    /// <param name="row">
    /// The row index.
    /// </param>
    /// <param name="column">
    /// The column index.
    /// </param>
    /// <returns>
    /// A reference to the element at the specified indices.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// If the matrix is disposed.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="row"/> or <paramref name="column"/> is out of range.
    /// </exception>
    public ref TNumber this[int row, int column]
    {
        get
        {
            var offset = checked(row * this.rowSpan + column * this.colSpan);
            return ref this.Data[offset];
        }
    }

    /// <summary>
    /// Returns a read-only view over the matrix.
    /// </summary>
    /// <remarks>
    /// The returned view is only valid while the matrix remains undisposed.
    /// </remarks>
    /// <returns>
    /// A <see cref="MatrixView"/> representing the matrix contents.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// If the matrix is disposed.
    /// </exception>
    public MatrixView<TNumber> AsView()
    {
        return new MatrixView<TNumber>(this.Data.AsSpan(), this.rows, this.columns, this.rowSpan, this.colSpan);
    }
}
