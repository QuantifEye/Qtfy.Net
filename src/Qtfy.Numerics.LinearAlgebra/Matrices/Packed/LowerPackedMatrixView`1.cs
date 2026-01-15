namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct LowerPackedMatrixView<TElement> :
    IMatrixView<TElement, LowerPackedRowView<TElement>, LowerPackedColumnView<TElement>, LowerPackedMatrixView<TElement>>
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowOffset;
    private readonly int colOffset;

    public LowerPackedMatrixView(ref TElement data, int order)
        : this(ref data, order, 0, 0)
    {
    }

    public LowerPackedMatrixView(ref TElement data, int order, int rowOffset, int colOffset)
    {
        this.data = data;
        this.order = order;
        this.rowOffset = rowOffset;
        this.colOffset = colOffset;
    }

    public int Rows => this.order;

    public int Columns => this.order;

    public ref TElement GetPinnableReference()
        => ref this.data;

    public ref TElement this[int row, int column]
    {
        get
        {
            var adjustedRow = this.rowOffset + row;
            var adjustedColumn = this.colOffset + column;
            Debug.Assert(adjustedRow >= adjustedColumn, "Only the lower triangle is stored.");
            var index = LowerPackedMatrix<TElement>.GetIndex(adjustedRow, adjustedColumn);
            return ref Add(ref this.data, index);
        }
    }

    public LowerPackedRowView<TElement> Row(int row)
    {
        var adjustedRow = this.rowOffset + row;
        var index = LowerPackedMatrix<TElement>.GetIndex(adjustedRow, this.colOffset);
        var length = Math.Min(this.order, adjustedRow - this.colOffset + 1);
        ref var reference = ref Add(ref this.data, index);
        return new (ref reference, length);
    }

    public LowerPackedColumnView<TElement> Column(int column)
    {
        var columnIndex = this.colOffset + column;
        var rowStart = this.rowOffset >= columnIndex ? this.rowOffset : columnIndex;
        var rowOffset = rowStart - columnIndex;
        var length = this.order - (rowStart - this.rowOffset);
        return new (ref this.data, this.order, columnIndex, rowOffset, length);
    }

    public static bool IsPinned() => false;
}
