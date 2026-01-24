namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static TNumber AbsSum<TVectorViewX>(TVectorViewX x)
        where TVectorViewX : IVectorView<TNumber>, allows ref struct
    {
        var n = x.Length;

        if (TVectorViewX.StrideIsAlwaysOne())
        {
            return UnsafeBlas.AbsSum(
                n: n,
                x: ref x.GetPinnableReference());
        }

        return UnsafeBlas.AbsSum(
            n: n,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride);
    }
}
