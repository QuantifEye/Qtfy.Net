namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct RowMajorMatrixView<TElement, TAlignment>
    : IMatrixView<TElement, VectorView<TElement, TAlignment>, StrideVectorView<TElement>>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    private readonly int rowStride;

    public RowMajorMatrixView(ref TElement reference, int rows, int columns)
        : this(ref reference, rows, columns, columns)
    {
    }

    public RowMajorMatrixView(ref TElement reference, int rows, int columns, int rowStride)
    {
        this.reference = reference;
        this.rows = rows;
        this.columns = columns;
        this.rowStride = rowStride;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public VectorView<TElement, TAlignment> Row(int row)
        => new(ref Unsafe.Add(ref this.reference, row * this.rowStride), this.columns);

    public StrideVectorView<TElement> Column(int column)
        => new(ref Unsafe.Add(ref this.reference, column), this.rowStride, this.rows);

    public ref TElement this[int row, int column]
        => ref Unsafe.Add(ref this.reference, row * this.rowStride + column);
}
