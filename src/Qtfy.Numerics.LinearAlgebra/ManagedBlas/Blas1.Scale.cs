namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas1<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void Scale<TVectorViewX>(TNumber alpha, TVectorViewX x)
        where TVectorViewX : IVectorView<TNumber>
    {
        if (TVectorViewX.StrideIsAlwaysOne())
        {
            UnsafeBlas1.Scale(
                n: x.Length,
                alpha: alpha,
                x: ref x.GetPinnableReference());
        }
        else
        {
            UnsafeBlas1.Scale(
                n: x.Length,
                alpha: alpha,
                x: ref x.GetPinnableReference(),
                strideX: x.Stride);
        }
    }
}
