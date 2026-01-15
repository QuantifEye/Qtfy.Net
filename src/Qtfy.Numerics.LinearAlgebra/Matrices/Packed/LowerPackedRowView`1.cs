namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct LowerPackedRowView<TElement> : IVectorView<TElement, LowerPackedRowView<TElement>>
{
    private readonly ref TElement reference;
    private readonly int length;

    public LowerPackedRowView(ref TElement data, int row)
    {
        var offset = LowerPackedMatrix<TElement>.GetIndex(row, 0);
        this.reference = ref Add(ref data, offset);
        this.length = row + 1;
    }

    public int Length => this.length;

    public int Stride => 1;

    public ref TElement GetPinnableReference()
        => ref this.reference;

    public ref TElement this[int index]
        => ref Add(ref this.reference, index);

    public static bool StrideIsAlwaysOne() => true;

    public static bool IsPinned() => false;
}
