using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct RowMajorMatrixView<TElement, TAlignment> :
    IStridedMatrixView<TElement, VectorView<TElement, TAlignment>, StrideVectorView<TElement>>
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

    public int RowStride => this.rowStride;

    public int ColumnStride => 1;

    public ref TElement GetPinnableReference()
        => ref this.reference;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => true;

    public VectorView<TElement, TAlignment> Row(int row)
        => new (ref Add(ref this.reference, row * this.rowStride), this.columns);

    public StrideVectorView<TElement> Column(int column)
        => new (ref Add(ref this.reference, column), this.rowStride, this.rows);

    public ref TElement this[int row, int column]
        => ref Add(ref this.reference, row * this.rowStride + column);

    public static bool IsPinned()
    {
        throw new NotImplementedException();
    }
}
