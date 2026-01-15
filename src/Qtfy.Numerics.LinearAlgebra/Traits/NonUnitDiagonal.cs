namespace Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public readonly struct NonUnitDiagonal : IDiagonal
{
    public static bool IsUnitDiagonal() => false;
}
