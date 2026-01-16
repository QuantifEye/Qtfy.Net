namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct LowerPackedRowView<TElement> : IVectorView<TElement>
{
    private readonly ref TElement reference;
    private readonly int length;

    public LowerPackedRowView(ref TElement data, int row)
        : this(ref data, row, 0, row + 1)
    {
    }

    public LowerPackedRowView(ref TElement data, int row, int columnOffset, int length)
    {
        var offset = LowerPackedMatrix<TElement>.GetIndex(row, columnOffset);
        this.reference = ref Add(ref data, offset);
        this.length = length;
    }

    public int Length => this.length;

    public int Stride => 1;

    public ref TElement GetPinnableReference()
        => ref this.reference;

    public ref TElement this[int index]
        => ref Add(ref this.reference, index);

    public static bool StrideIsAlwaysOne() => true;

}
