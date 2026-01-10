namespace Qtfy.Numerics.LinearAlgebra;


public interface IAlignmentPolicy<TAlignment>
    where TAlignment : struct, IAlignmentPolicy<TAlignment>
{
    public static abstract int ByteAlignment();
}

public readonly struct Align64 : IAlignmentPolicy<Align64>
{
    public static int ByteAlignment()
    {
        return 64;
    }
}

public readonly struct Align128 : IAlignmentPolicy<Align64>
{
    public static int ByteAlignment()
    {
        return 128;
    }
}
