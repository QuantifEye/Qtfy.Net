namespace Qtfy.Numerics.LinearAlgebra.Iterators;

public ref struct StrideIterator<T> : IIterator<T>
{
    private ref T current;

    private readonly int stride;

    public StrideIterator(ref T current, int stride)
    {
        this.current = ref current;
        this.stride = stride;
    }

    public ref T Current => ref this.current;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T GetNext()
    {
        this.current = ref Add(ref this.current, this.stride);
        return ref this.current;
    }
}
