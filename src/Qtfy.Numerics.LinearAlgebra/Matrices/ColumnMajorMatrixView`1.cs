namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct ColumnMajorMatrixView<TElement> :
    IMatrixView<TElement, StrideVectorView<TElement>, VectorView<TElement>>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    public ColumnMajorMatrixView(ref TElement reference, int rows, int columns)
    {
        this.reference = reference;
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public StrideVectorView<TElement> Row(int row)
    {
        return new(
            ref Unsafe.Add(ref this.reference, row),
            this.rows,
            this.columns);
    }

    public VectorView<TElement> Column(int column)
    {
        return new(
            ref Unsafe.Add(ref this.reference, column * this.rows),
            this.rows);
    }

    public ref TElement this[int row, int column]
        => ref Unsafe.Add(ref this.reference, row + (column * this.rows));
}
