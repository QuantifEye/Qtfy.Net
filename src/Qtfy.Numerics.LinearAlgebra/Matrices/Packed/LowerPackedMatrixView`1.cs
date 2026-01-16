namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct LowerPackedMatrixView<TElement> :
    IPackedMatrixView<TElement, LowerPackedRowView<TElement>, LowerPackedColumnView<TElement>>
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
        this.data = ref data;
        this.order = order;
        this.rowOffset = rowOffset;
        this.colOffset = colOffset;
    }

    public int Rows => this.order - this.rowOffset;

    public int Columns => this.order - this.colOffset;

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
        var length = Math.Max(0, adjustedRow - this.colOffset + 1);
        return new (ref this.data, adjustedRow, this.colOffset, length);
    }

    public LowerPackedColumnView<TElement> Column(int column)
    {
        var columnIndex = this.colOffset + column;
        var rowStart = this.rowOffset >= columnIndex ? this.rowOffset : columnIndex;
        var rowOffset = rowStart - columnIndex;
        var length = this.order - rowStart;
        return new (ref this.data, columnIndex, rowOffset, length);
    }

}
