namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas1
{
    public static void Scale<TNumber>(int n, TNumber alpha, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        xRef *= alpha;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            xRef *= alpha;
        }
    }

    public static void Scale<TNumber>(int n, TNumber alpha, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        xRef *= alpha;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            xRef *= alpha;
        }
    }
}
