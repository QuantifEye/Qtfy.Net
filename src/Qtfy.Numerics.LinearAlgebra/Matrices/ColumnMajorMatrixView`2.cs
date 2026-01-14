namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct ColumnMajorMatrixView<TElement, TAlignment> :
    IMatrixView<TElement, StrideVectorView<TElement>, VectorView<TElement, TAlignment>>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    private readonly int columnStride;

    public ColumnMajorMatrixView(ref TElement reference, int rows, int columns, int columnStride)
    {
        this.reference = reference;
        this.rows = rows;
        this.columns = columns;
        this.columnStride = columnStride;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public StrideVectorView<TElement> Row(int row)
        => new (ref Unsafe.Add(ref this.reference, row), this.columnStride, this.columns);

    public VectorView<TElement, TAlignment> Column(int column)
        => new (ref Unsafe.Add(ref this.reference, column * this.columnStride), this.rows);

    public ref TElement this[int row, int column]
        => ref Unsafe.Add(ref this.reference, row + column * this.columnStride);
}
