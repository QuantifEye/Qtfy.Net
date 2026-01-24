using Qtfy.Numerics.LinearAlgebra.BLAS;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static GivensRotation<TNumber> SetupGivensRotation(TNumber a, TNumber b)
        => UnsafeBlas.SetupGivensRotation(
            a: a,
            b: b);
}
