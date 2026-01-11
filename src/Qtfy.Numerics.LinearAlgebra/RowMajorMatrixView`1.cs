namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct RowMajorMatrixView<TElement>
    : IMatrixView<TElement, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    private readonly int rowStride;

    public RowMajorMatrixView(ref TElement reference, int rows, int columns)
    {
        this.reference = reference;
        this.rows = rows;
        this.columns = columns;
        this.rowStride = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public VectorView<TElement> Row(int row)
    {
        return new (ref System.Runtime.CompilerServices.Unsafe.Add(ref this.reference, row * this.rowStride), this.columns);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        return new (ref System.Runtime.CompilerServices.Unsafe.Add(ref this.reference, column), this.rowStride, this.rows);
    }

    public ref TElement this[int row, int column]
        => ref System.Runtime.CompilerServices.Unsafe.Add(ref this.reference, (row * this.rowStride) + column);
}
