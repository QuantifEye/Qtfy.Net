namespace Qtfy.Numerics.LinearAlgebra;

public readonly unsafe ref struct RowMajorMatrixView<TElement, TAlignment>
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly TElement* pointer;

    private readonly int rows;

    private readonly int columns;

    private readonly int rowStride;

    public RowMajorMatrixView(TElement* pointer, int rows, int columns)
    {
        this.pointer = pointer;
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public VectorView<TElement, TAlignment> Row(int row)
    {
        return new(this.pointer + row * this.rowStride, this.columns);
    }

    public StrideVectorView<TElement, TAlignment> Column(int column)
    {
        return new(this.pointer + column, this.rowStride, this.rows);
    }

    public ref TElement this[int row, int column]
        => ref this.pointer[row * this.rowStride + column];
}
