namespace Qtfy.Numerics.LinearAlgebra.Alignments;

public readonly struct Align128 : IAlignmentPolicy<Align64>
{
    public static nuint ByteAlignment()
    {
        return 128;
    }
}
