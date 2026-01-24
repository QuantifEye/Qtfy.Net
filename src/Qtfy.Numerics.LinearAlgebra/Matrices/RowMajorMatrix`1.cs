namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public sealed class RowMajorMatrix<TElement> :
    IMatrix<TElement, RowMajorMatrixView<TElement>, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;

    private readonly int rows;

    private readonly int columns;

    public RowMajorMatrix(int rows, int columns)
    {
        array = new TElement[rows * columns];
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => rows;

    public int Columns => columns;

    public ref TElement GetPinnableReference()
        => ref array.Reference();

    public ref TElement this[int row, int column]
        => ref array[row * columns + column];

    public RowMajorMatrixView<TElement> AsView()
        => new (ref array.Reference(), rows, columns);

}
