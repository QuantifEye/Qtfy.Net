// <copyright file="Matrix.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

public sealed class Matrix<TNumber>
    where TNumber : unmanaged
{
    private readonly TNumber[] data;
    private readonly int rows;
    private readonly int columns;
    private readonly int rowSpan;
    private readonly int colSpan;

    public Matrix(int rows, int columns, bool isRowMajor = true)
    {
        this.rows = rows;
        this.columns = columns;

        if (isRowMajor)
        {
            this.rowSpan = columns;
            this.colSpan = 1;
        }
        else
        {
            this.rowSpan = 1;
            this.colSpan = rows;
        }

        this.data = new TNumber[rows * columns];
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public int RowSpan => this.rowSpan;

    public int ColSpan => this.colSpan;

    internal TNumber[] Data => this.data;

    public ref TNumber this[int row, int column]
    {
        get
        {
            var offset = (row * this.rowSpan) + (column * this.colSpan);
            return ref this.Data[offset];
        }
    }

    public MatrixView<TNumber> AsView()
    {
        return new MatrixView<TNumber>(this.Data.AsSpan(), this.rows, this.columns, this.rowSpan, this.colSpan);
    }

    public MatrixView<TNumber> AsSpan()
    {
        return new MatrixView<TNumber>(this.Data.AsSpan(), this.rows, this.columns, this.rowSpan, this.colSpan);
    }
}
