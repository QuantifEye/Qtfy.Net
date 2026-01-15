namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct SymmetricRowView<TElement> : IVectorView<TElement, SymmetricRowView<TElement>>
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;
    private readonly int row;
    private readonly int columnOffset;
    private readonly int length;

    public SymmetricRowView(ref TElement data, int order, int rowSpan, int colSpan, int row)
        : this(ref data, order, rowSpan, colSpan, row, 0, order)
    {
    }

    public SymmetricRowView(ref TElement data, int order, int rowSpan, int colSpan, int row, int columnOffset, int length)
    {
        this.data = data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
        this.row = row;
        this.columnOffset = columnOffset;
        this.length = length;
    }

    public int Length => this.length;

    public int Stride => this.colSpan;

    public ref TElement GetPinnableReference()
        => ref Add(ref this.data, (this.row * this.rowSpan) + (this.columnOffset * this.colSpan));

    public ref TElement this[int index]
    {
        get
        {
            var offset = this.row * this.rowSpan + (this.columnOffset + index) * this.colSpan;
            return ref Add(ref this.data, offset);
        }
    }

    public static bool StrideIsAlwaysOne() => false;

    public static bool IsPinned() => false;
}
