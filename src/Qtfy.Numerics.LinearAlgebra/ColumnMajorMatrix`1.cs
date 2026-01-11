namespace Qtfy.Numerics.LinearAlgebra;

public sealed class ColumnMajorMatrix<TElement>
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

    public ColumnMajorMatrixView<TElement> AsView()
        => new(ref this.memory.Reference(), this.rows, this.columns);
}
