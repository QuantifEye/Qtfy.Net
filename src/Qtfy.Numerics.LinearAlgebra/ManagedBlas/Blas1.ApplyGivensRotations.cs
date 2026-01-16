namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas1<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void ApplyGivensRotation<TVectorViewX, TVectorViewY>(
        TVectorViewX x, TVectorViewY y, TNumber c, TNumber s)
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber>, allows ref struct
    {
        Debug.Assert(x.Length == y.Length, "x and y must have the same length.");

        int n = x.Length;
        if (TVectorViewX.StrideIsAlwaysOne())
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                UnsafeBlas1.ApplyGivensRotation(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference(),
                    c: c,
                    s: s);
            }
            else
            {
                UnsafeBlas1.ApplyGivensRotation(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    y: ref y.GetPinnableReference(),
                    strideY: y.Stride,
                    c: c,
                    s: s);
            }
        }
        else
        {
            if (TVectorViewY.StrideIsAlwaysOne())
            {
                UnsafeBlas1.ApplyGivensRotation(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: x.Stride,
                    y: ref y.GetPinnableReference(),
                    c: c,
                    s: s);
            }
            else
            {
                UnsafeBlas1.ApplyGivensRotation(
                    n: n,
                    x: ref x.GetPinnableReference(),
                    strideX: x.Stride,
                    y: ref y.GetPinnableReference(),
                    strideY: y.Stride,
                    c: c,
                    s: s);
            }
        }
    }
}
