// <copyright file="Matrix.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

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
            rowSpan = columns;
            colSpan = 1;
        }
        else
        {
            rowSpan = 1;
            colSpan = rows;
        }

        array = new TElement[rows * columns];
    }

    public int Rows => rows;

    public int Columns => columns;

    public ref TElement GetPinnableReference()
        => ref array.Reference();

    internal TElement[] Data => array;

    internal int RowSpan => rowSpan;

    internal int ColSpan => colSpan;

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * rowSpan + column * colSpan;
            return ref array[offset];
        }
    }

    public MatrixView<TElement> AsView()
        => new (ref array.Reference(), rows, columns, rowSpan, colSpan);

}
