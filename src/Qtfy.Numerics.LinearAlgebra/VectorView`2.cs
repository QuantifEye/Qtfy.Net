namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct VectorView<TElement, TAlignment>
    : IVectorView<VectorView<TElement, TAlignment>, TElement>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly ref TElement reference;

    public VectorView(ref TElement reference, int length)
    {
        this.reference = reference;
        this.Length = length;
    }

    public int Length { get; }

    public ref TElement this[int index]
        => ref Unsafe.Add(ref this.reference, index);
}
