using System.ComponentModel.DataAnnotations;
using Qtfy.Numerics.LinearAlgebra.Memory;

namespace Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class Vector<TElement, TAlignment> : IDisposable
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private unsafe TElement* pointer;

    private readonly NativeMemoryOwner memory;

    private readonly int length;

    public Vector(int length)
    {
        unsafe
        {
            this.memory = new NativeMemoryOwner((nuint)length, TAlignment.ByteAlignment());
            this.pointer = (TElement*)this.memory.Pointer();
            this.length = this.length;
        }
    }

    public int Length => this.length;

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
            return new VectorView<TElement, TAlignment>(ref AsRef<TElement>(this.pointer), this.length);
        }
    }

    public void Dispose()
    {
        this.memory.Dispose();
        unsafe
        {
            this.pointer = null;
        }
    }
}
