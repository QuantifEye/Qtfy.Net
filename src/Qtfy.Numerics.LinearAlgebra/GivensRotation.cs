namespace Qtfy.Numerics.LinearAlgebra.BLAS;

/// <summary>
/// Result of generating a Givens / plane rotation.
/// This follows the shape of the C++ std::linalg result type.
/// </summary>
public readonly record struct GivensRotation<TNumber>(TNumber C, TNumber S, TNumber R)
    where TNumber : INumberBase<TNumber>;
