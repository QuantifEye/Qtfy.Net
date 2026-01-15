namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas1<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void Swap<TVectorViewX, TVectorViewY>(TVectorViewX x, TVectorViewY y)
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber>, allows ref struct
    {
        int n = x.Length;
        if (TVectorViewX.StrideIsAlwaysOne())
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                UnsafeBlas1.Swap(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference());
            }
            else
            {
                UnsafeBlas1.Swap(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference(),
                    strideY: y.Stride);
            }
        }
        else
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                UnsafeBlas1.Swap(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: x.Stride,
                    y: ref y.GetPinnableReference());
            }
            else
            {
                UnsafeBlas1.Swap(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: x.Stride,
                    y: ref y.GetPinnableReference(),
                    strideY: y.Stride);
            }
        }
    }
}
