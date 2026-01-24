namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void Scale<TVectorViewX>(TNumber alpha, TVectorViewX x)
        where TVectorViewX : IVectorView<TNumber>
    {
        if (TVectorViewX.StrideIsAlwaysOne())
        {
            UnsafeBlas.Scale(
                n: x.Length,
                alpha: alpha,
                x: ref x.GetPinnableReference());
        }
        else
        {
            UnsafeBlas.Scale(
                n: x.Length,
                alpha: alpha,
                x: ref x.GetPinnableReference(),
                strideX: x.Stride);
        }
    }
}
