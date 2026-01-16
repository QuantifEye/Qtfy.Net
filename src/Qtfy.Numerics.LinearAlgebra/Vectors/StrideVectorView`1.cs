namespace Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct StrideVectorView<TElement> : IVectorView<TElement>
{
    private readonly ref TElement reference;
    private readonly int stride;
    private readonly int length;

    public StrideVectorView(ref TElement reference, int stride, int length)
    {
        this.reference = ref reference;
        this.stride = stride;
        this.length = length;
    }

    public int Length  => length;

    public int Stride
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this.stride;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TElement GetPinnableReference() => ref this.reference;

    public ref TElement this[int index]
        => ref Add(ref this.reference, index * this.stride);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StrideIsAlwaysOne() => false;
}
