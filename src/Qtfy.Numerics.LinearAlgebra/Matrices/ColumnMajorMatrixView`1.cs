using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct ColumnMajorMatrixView<TElement> :
    IStridedMatrixView<TElement, StrideVectorView<TElement>, VectorView<TElement>, ColumnMajorMatrixView<TElement>>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    private readonly int columnStride;

    public ColumnMajorMatrixView(ref TElement reference, int rows, int columns)
        : this(ref reference, rows, columns, rows)
    {
    }

    public ColumnMajorMatrixView(ref TElement reference, int rows, int columns, int columnStride)
    {
        this.reference = reference;
        this.rows = rows;
        this.columns = columns;
        this.columnStride = columnStride;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public int RowStride => 1;

    public int ColumnStride => this.columnStride;

    public ref TElement GetPinnableReference()
        => ref this.reference;

    public static bool RowStrideIsAlwaysOne() => true;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public StrideVectorView<TElement> Row(int row)
    {
        return new (
            ref Add(ref this.reference, row),
            this.columnStride,
            this.columns);
    }

    public VectorView<TElement> Column(int column)
    {
        return new (
            ref Add(ref this.reference, column * this.columnStride),
            this.rows);
    }

    public ref TElement this[int row, int column]
        => ref Add(ref this.reference, row + column * this.columnStride);

    public static bool IsPinned()
    {
        throw new NotImplementedException();
    }
}
