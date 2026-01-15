namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas1
{
    public static TNumber AbsSum<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var sum = TNumber.Abs(xRef);

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            sum += TNumber.Abs(xRef);
        }

        return sum;
    }

    public static TNumber AbsSum<TNumber>(int n, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var sum = TNumber.Abs(xRef);

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            sum += TNumber.Abs(xRef);
        }

        return sum;
    }
}
