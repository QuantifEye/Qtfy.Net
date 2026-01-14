namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct StrideVectorView<TElement> : IVectorView<StrideVectorView<TElement>, TElement>
{
    private readonly ref TElement reference;
    private readonly int stride;

    public StrideVectorView(ref TElement reference, int stride, int length)
    {
        this.reference = reference;
        this.stride = stride;
        this.Length = length;
    }

    public int Length { get; }

    internal int Stride => this.stride;

    internal ref TElement GetReference() => ref this.reference;

    public ref TElement this[int index]
        => ref Add(ref this.reference, index * this.stride);
}
