using System.Numerics;
using Qtfy.Memory;

namespace Qtfy.Numerics.LinearAlgebra;

public sealed class Vector<TNumber, TAlignment>
    where TNumber : unmanaged, INumber<TNumber>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly NativeMemoryOwner memory;

    public Vector(int length)
    {
        this.memory = new NativeMemoryOwner((nuint)length, TAlignment.ByteAlignment());
        this.Length = this.Length;
    }

    public int Length { get; }

    public ref TNumber this[int index]
    {
        get
        {
            unsafe
            {
                return ref this.memory.Pointer<TNumber>()[index];
            }
        }
    }

    public Span<TNumber> AsView()
    {
        // TODO: Implement me
        throw new NotImplementedException();
    }
}
