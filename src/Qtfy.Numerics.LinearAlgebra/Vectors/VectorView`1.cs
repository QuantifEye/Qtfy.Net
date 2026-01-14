namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct VectorView<TElement> : IVectorView<VectorView<TElement>, TElement>
{
    private readonly ref TElement reference;

    public VectorView(ref TElement reference, int length)
    {
        this.reference = reference;
        this.Length = length;
    }

    public int Length { get; }

    internal ref TElement GetReference() => ref this.reference;

    public ref TElement this[int index]
        => ref Add(ref this.reference, index);
}
