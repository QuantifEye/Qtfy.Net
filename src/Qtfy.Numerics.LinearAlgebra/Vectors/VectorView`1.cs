namespace Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct VectorView<TElement> : IVectorView<TElement>
{
    private readonly ref TElement reference;

    public VectorView(ref TElement reference, int length)
    {
        this.reference = ref reference;
        this.Length = length;
    }

    public int Length { get; }

    public int Stride
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref TElement GetPinnableReference() => ref this.reference;

    public ref TElement this[int index]
        => ref Add(ref this.reference, index);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StrideIsAlwaysOne() => true;
}
