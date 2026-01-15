namespace Qtfy.Numerics.LinearAlgebra.Iterators;

public ref struct ContiguousIterator<T> : IIterator<T>
{
    private ref T current;

    public ContiguousIterator(ref T current)
    {
        this.current = ref current;
    }

    public ref T Current => ref this.current;

    public ref T GetNext()
    {
        this.current = ref Add(ref this.current, 1);
        return ref this.current;
    }
}
