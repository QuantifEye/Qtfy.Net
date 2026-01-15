namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas1<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static GivensRotation<TNumber> SetupGivensRotation(TNumber a, TNumber b)
        => UnsafeBlas1.SetupGivensRotation(
            a: a,
            b: b);
}
