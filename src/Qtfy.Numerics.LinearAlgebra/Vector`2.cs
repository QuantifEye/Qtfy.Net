using System.Numerics;
using Qtfy.Memory;

namespace Qtfy.Numerics.LinearAlgebra;

public sealed class Vector<TElement, TAlignment>
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly NativeMemoryOwner memory;

    public Vector(int length)
    {
        this.memory = new NativeMemoryOwner((nuint)length, TAlignment.ByteAlignment());
        this.Length = this.Length;
    }

    public int Length { get; }

    public ref TElement this[int index]
    {
        get
        {
            unsafe
            {
                return ref this.memory.Pointer<TElement>()[index];
            }
        }
    }

    public Span<TElement> AsView()
    {
        // TODO: Implement me
        throw new NotImplementedException();
    }
}
