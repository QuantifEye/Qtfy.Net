namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public sealed class ColumnMajorMatrix<TElement> :
    IMatrix<
        TElement,
        ColumnMajorMatrixView<TElement>,
        StrideVectorView<TElement>,
        VectorView<TElement>>
{
    private readonly TElement[] memory;

    private readonly int rows;

    private readonly int columns;

    public ColumnMajorMatrix(int rows, int columns)
    {
        memory = new TElement[rows * columns];
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => rows;

    public int Columns => columns;

    public ref TElement GetPinnableReference()
        => ref memory.Reference();

    public ref TElement this[int row, int column]
        => ref memory[row + column * rows];

    public ColumnMajorMatrixView<TElement> AsView()
        => new (ref memory.Reference(), rows, columns);

}
