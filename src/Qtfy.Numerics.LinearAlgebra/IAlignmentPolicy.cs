namespace Qtfy.Numerics.LinearAlgebra;


public interface IAlignmentPolicy<TAlignment>
    where TAlignment : struct, IAlignmentPolicy<TAlignment>
{
    public static abstract nuint ByteAlignment();

    public static virtual bool IsAligned(nuint address)
    {
        return AddressMath.IsAlignedTo(address, TAlignment.ByteAlignment());
    }
}

public readonly struct Align64 : IAlignmentPolicy<Align64>
{
    public static nuint ByteAlignment()
    {
        return 64;
    }
}

public readonly struct Align128 : IAlignmentPolicy<Align64>
{
    public static nuint ByteAlignment()
    {
        return 128;
    }
}
