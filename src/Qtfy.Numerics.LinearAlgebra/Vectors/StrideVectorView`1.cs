namespace Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct StrideVectorView<TElement> : IVectorView<TElement>
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPinned() => false;
}
