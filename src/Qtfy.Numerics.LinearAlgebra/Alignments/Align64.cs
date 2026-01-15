namespace Qtfy.Numerics.LinearAlgebra.Alignments;

public readonly struct Align64 : IAlignmentPolicy<Align64>
{
    public static nuint ByteAlignment()
    {
        return 64;
    }
}
