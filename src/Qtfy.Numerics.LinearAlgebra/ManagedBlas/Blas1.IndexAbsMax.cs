namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas1<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static int IndexAbsMax<TVectorViewX>(TVectorViewX x)
        where TVectorViewX : struct, IVectorView<TNumber, TVectorViewX>, allows ref struct
    {
        var n = x.Length;

        if (TVectorViewX.StrideIsAlwaysOne())
        {
            return UnsafeBlas1.IndexAbsMax(
                n: n,
                x: ref x.GetPinnableReference());
        }

        return UnsafeBlas1.IndexAbsMax(
            n: n,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride);
    }
}
