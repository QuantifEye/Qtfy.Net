namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct LowerPackedColumnView<TElement> : IVectorView<TElement, LowerPackedColumnView<TElement>>
{
    private readonly ref TElement data;
    private readonly int column;
    private readonly int order;
    private readonly int rowOffset;
    private readonly int length;

    public LowerPackedColumnView(ref TElement data, int order, int column)
        : this(ref data, order, column, 0, order - column)
    {
    }

    public LowerPackedColumnView(ref TElement data, int order, int column, int rowOffset, int length)
    {
        this.data = data;
        this.order = order;
        this.column = column;
        this.rowOffset = rowOffset;
        this.length = length;
    }

    public int Length => this.length;

    public int Stride => 0;

    public ref TElement GetPinnableReference()
        => ref Add(ref this.data, LowerPackedMatrix<TElement>.GetIndex(this.column + this.rowOffset, this.column));

    public ref TElement this[int index]
    {
        get
        {
            var row = this.column + this.rowOffset + index;
            var offset = LowerPackedMatrix<TElement>.GetIndex(row, this.column);
            return ref Add(ref this.data, offset);
        }
    }

    public static bool StrideIsAlwaysOne() => false;

    public static bool IsPinned() => false;
}
