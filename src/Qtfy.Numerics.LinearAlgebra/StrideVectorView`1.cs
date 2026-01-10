using System.Runtime.CompilerServices;

namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct StrideVectorView<TElement> : IVectorView<TElement>
    where TElement : unmanaged
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

    public ref TElement this[int index]
        => ref Unsafe.Add(ref this.reference, index * this.stride);
}
