// <copyright file="Matrix.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public sealed class Matrix<TElement> :
    IMatrix<TElement, MatrixView<TElement>, StrideVectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;
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

        this.array = new TElement[rows * columns];
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public ref TElement GetPinnableReference()
        => ref this.array.Reference();

    internal TElement[] Data => this.array;

    internal int RowSpan => this.rowSpan;

    internal int ColSpan => this.colSpan;

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * this.rowSpan + column * this.colSpan;
            return ref this.array[offset];
        }
    }

    public MatrixView<TElement> AsView()
        => new (ref this.array.Reference(), this.rows, this.columns, this.rowSpan, this.colSpan);

    public static bool IsPinned() => false;
}
