namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static TNumber Dot<TVectorViewX, TVectorViewY>(TVectorViewX x, TVectorViewY y)
        where TVectorViewX :  IVectorView<TNumber>, allows ref struct
        where TVectorViewY :  IVectorView<TNumber>, allows ref struct
    {
        Debug.Assert(x.Length == y.Length, "x and y must have the same length.");

        if (TVectorViewX.StrideIsAlwaysOne())
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                return UnsafeBlas.Dot(
                    n: x.Length,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference());
            }

            return UnsafeBlas.Dot(
                n: x.Length,
                x: ref x.GetPinnableReference(),
                strideX: 1,
                y: ref y.GetPinnableReference(),
                strideY: y.Stride);
        }

        if (TVectorViewY.StrideIsAlwaysOne())
        {
            return UnsafeBlas.Dot(
                n: x.Length,
                x: ref x.GetPinnableReference(),
                strideX: x.Stride,
                y: ref y.GetPinnableReference());
        }

        return UnsafeBlas.Dot(
            n: x.Length,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            y: ref y.GetPinnableReference(),
            strideY: y.Stride);
    }
}
