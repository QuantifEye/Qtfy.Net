namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct LowerPackedColumnView<TElement> : IPackedVectorView<TElement>
{
    private readonly ref TElement data;
    private readonly int column;
    private readonly int rowOffset;
    private readonly int length;

    public LowerPackedColumnView(ref TElement data, int order, int column)
        : this(ref data, column, 0, order - column)
    {
    }

    public LowerPackedColumnView(ref TElement data, int column, int rowOffset, int length)
    {
        this.data = ref data;
        this.column = column;
        this.rowOffset = rowOffset;
        this.length = length;
    }

    public int Length => this.length;

    public ref TElement this[int index]
    {
        get
        {
            var row = this.column + this.rowOffset + index;
            var offset = LowerPackedMatrix<TElement>.GetIndex(row, this.column);
            return ref Add(ref this.data, offset);
        }
    }

}
