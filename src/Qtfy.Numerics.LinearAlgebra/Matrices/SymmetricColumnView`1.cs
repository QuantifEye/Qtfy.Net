namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct SymmetricColumnView<TElement> : IVectorView<TElement, SymmetricColumnView<TElement>>
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;
    private readonly int column;
    private readonly int rowOffset;
    private readonly int length;

    public SymmetricColumnView(ref TElement data, int order, int rowSpan, int colSpan, int column)
        : this(ref data, order, rowSpan, colSpan, column, 0, order)
    {
    }

    public SymmetricColumnView(ref TElement data, int order, int rowSpan, int colSpan, int column, int rowOffset, int length)
    {
        this.data = data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
        this.column = column;
        this.rowOffset = rowOffset;
        this.length = length;
    }

    public int Length => this.length;

    public int Stride => this.rowSpan;

    public ref TElement GetPinnableReference()
        => ref Add(ref this.data, (this.rowOffset * this.rowSpan) + (this.column * this.colSpan));

    public ref TElement this[int index]
    {
        get
        {
            var offset = (this.rowOffset + index) * this.rowSpan + this.column * this.colSpan;
            return ref Add(ref this.data, offset);
        }
    }

    public static bool StrideIsAlwaysOne() => false;

    public static bool IsPinned() => false;
}
