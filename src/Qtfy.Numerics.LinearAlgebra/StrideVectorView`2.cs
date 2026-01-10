namespace Qtfy.Numerics.LinearAlgebra;

public ref struct StrideVectorView<TElement, TAlignment> : IVectorView<TElement, TAlignment>
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private unsafe TElement* pointer;

    private readonly int stride;

    public unsafe StrideVectorView(TElement* pointer, int stride, int length)
    {
        this.pointer = pointer;
        this.stride = stride;
        this.Length = length;
    }

    public int Length { get; }

    public ref TElement this[int index]
    {
        get
        {
            unsafe
            {
                return ref this.pointer[index * this.stride];
            }
        }
    }
}
