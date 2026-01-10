namespace Qtfy.Numerics.LinearAlgebra;

public sealed class RowMajorMatrix<TElement>
    where TElement : unmanaged
{
    private readonly TElement[] memory;

    private readonly int rows;

    private readonly int columns;

    public RowMajorMatrix(int rows, int columns)
    {
        this.memory = new TElement[rows * columns];
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public RowMajorMatrixView<TElement> AsView()
        => new(ref this.memory.GetReferenceToFirstElement(), this.rows, this.columns);
}
