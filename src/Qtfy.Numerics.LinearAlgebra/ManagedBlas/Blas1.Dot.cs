namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas1<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static TNumber Dot<TVectorViewX, TVectorViewY>(TVectorViewX x, TVectorViewY y)
        where TVectorViewX : struct, IVectorView<TNumber, TVectorViewX>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber, TVectorViewY>, allows ref struct
    {
        Debug.Assert(x.Length == y.Length, "x and y must have the same length.");

        if (TVectorViewX.StrideIsAlwaysOne())
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                return UnsafeBlas1.Dot(
                    n: x.Length,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference());
            }

            return UnsafeBlas1.Dot(
                n: x.Length,
                x: ref x.GetPinnableReference(),
                y: ref y.GetPinnableReference(),
                strideY: y.Stride);
        }

        if (TVectorViewY.StrideIsAlwaysOne())
        {
            return UnsafeBlas1.Dot(
                n: x.Length,
                x: ref x.GetPinnableReference(),
                strideX: x.Stride,
                y: ref y.GetPinnableReference());
        }

        return UnsafeBlas1.Dot(
            n: x.Length,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            y: ref y.GetPinnableReference(),
            strideY: y.Stride);
    }
}
