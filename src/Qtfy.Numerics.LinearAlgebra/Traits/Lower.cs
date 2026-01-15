namespace Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public readonly struct Lower : IUpperLower
{
    public static bool IsUpper() => false;
}
