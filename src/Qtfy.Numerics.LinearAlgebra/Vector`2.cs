namespace Qtfy.Numerics.LinearAlgebra;

using static Unsafe;

public sealed class Vector<TElement, TAlignment> : IDisposable
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private unsafe TElement* pointer;

    private readonly NativeMemoryOwner memory;

    public Vector(int length)
    {
        unsafe
        {
            this.memory = new NativeMemoryOwner((nuint)length, TAlignment.ByteAlignment());
            this.pointer = (TElement*)this.memory.Pointer();
            this.Length = this.Length;
        }
    }

    public int Length { get; }

    public ref TElement this[int index]
    {
        get
        {
            unsafe
            {
                return ref this.pointer[index];
            }
        }
    }

    public VectorView<TElement, TAlignment> AsView()
    {
        unsafe
        {
            return new VectorView<TElement, TAlignment>(ref AsRef<TElement>(this.pointer), this.Length);
        }
    }

    public void Dispose()
    {
        this.memory.Dispose();
        unsafe
        {
            pointer = null;
        }
    }
}
