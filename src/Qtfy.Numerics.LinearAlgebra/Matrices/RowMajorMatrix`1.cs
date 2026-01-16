namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class RowMajorMatrix<TElement> :
    IMatrix<TElement, RowMajorMatrixView<TElement>, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;

    private readonly int rows;

    private readonly int columns;

    public RowMajorMatrix(int rows, int columns)
    {
        this.array = new TElement[rows * columns];
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public ref TElement GetPinnableReference()
        => ref this.array.Reference();

    public ref TElement this[int row, int column]
        => ref this.array[row * this.columns + column];

    public RowMajorMatrixView<TElement> AsView()
        => new (ref this.array.Reference(), this.rows, this.columns);

}
