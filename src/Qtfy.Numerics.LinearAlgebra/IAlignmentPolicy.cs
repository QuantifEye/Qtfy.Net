using Qtfy.Numerics.LinearAlgebra.Memory;

namespace Qtfy.Numerics.LinearAlgebra;


public interface IAlignmentPolicy<TAlignment>
    where TAlignment : IAlignmentPolicy<TAlignment>
{
    public static abstract nuint ByteAlignment();

    public static virtual bool IsAligned(nuint address)
    {
        return AddressMath.IsAlignedTo(address, TAlignment.ByteAlignment());
    }
}
