namespace Qtfy.Numerics.LinearAlgebra;

public readonly unsafe ref struct VectorView<TElement, TAlignment> : IVectorView<TElement, TAlignment>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private readonly TElement* pointer;

    public VectorView(TElement* pointer, int length)
    {
        this.pointer = pointer;
        this.Length = length;
    }

    public int Length { get; }

    public ref TElement this[int index]
        => ref this.pointer[index];
}
