namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

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
        this.memory = new TElement[rows * columns];
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public ref TElement GetPinnableReference()
        => ref this.memory.Reference();

    public ref TElement this[int row, int column]
        => ref this.memory[row + column * this.rows];

    public ColumnMajorMatrixView<TElement> AsView()
        => new (ref this.memory.Reference(), this.rows, this.columns);

}
