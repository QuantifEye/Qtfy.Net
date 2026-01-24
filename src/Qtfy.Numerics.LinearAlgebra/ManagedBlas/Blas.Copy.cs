namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void Copy<TVectorViewX, TVectorViewY>(TVectorViewX x, TVectorViewY y)
        where TVectorViewX :  IVectorView<TNumber>, allows ref struct
        where TVectorViewY :  IVectorView<TNumber>, allows ref struct
    {
        int n = x.Length;
        if (TVectorViewX.StrideIsAlwaysOne())
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                UnsafeBlas.Copy(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference());
            }
            else
            {
                UnsafeBlas.Copy(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: 1,
                    y: ref y.GetPinnableReference(),
                    strideY: y.Stride);
            }
        }
        else
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                UnsafeBlas.Copy(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: x.Stride,
                    y: ref y.GetPinnableReference());
            }
            else
            {
                UnsafeBlas.Copy(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: x.Stride,
                    y: ref y.GetPinnableReference(),
                    strideY: y.Stride);
            }
        }
    }
}
