namespace Qtfy.Numerics.LinearAlgebra;

public sealed class RowMajorMatrix<TElement>
    : IMatrix<TElement, RowMajorMatrixView<TElement>, VectorView<TElement>, StrideVectorView<TElement>>
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

    public ref TElement this[int row, int column]
        => ref this.memory[(row * this.columns) + column];

    public VectorView<TElement> Row(int row)
    {
        return new(
            ref Unsafe.Add(ref this.memory.Reference(), row * this.columns),
            this.columns);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        return new(
            ref Unsafe.Add(ref this.memory.Reference(), column),
            this.columns,
            this.rows);
    }

    public RowMajorMatrixView<TElement> AsView()
        => new(ref this.memory.Reference(), this.rows, this.columns);
}
