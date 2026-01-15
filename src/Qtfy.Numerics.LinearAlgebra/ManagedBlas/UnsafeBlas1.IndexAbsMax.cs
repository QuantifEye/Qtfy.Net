namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas1
{
    public static int IndexAbsMax<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var maxAbs = TNumber.Abs(xRef);
        var maxIndex = 0;
        var index = 0;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            index++;

            var absx = TNumber.Abs(xRef);
            if (TNumber.IsPositive(absx - maxAbs))
            {
                maxAbs = absx;
                maxIndex = index;
            }
        }

        return maxIndex;
    }

    public static int IndexAbsMax<TNumber>(int n, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var maxAbs = TNumber.Abs(xRef);
        var maxIndex = 0;
        var index = 0;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            index++;

            var absx = TNumber.Abs(xRef);
            if (TNumber.IsPositive(absx - maxAbs))
            {
                maxAbs = absx;
                maxIndex = index;
            }
        }

        return maxIndex;
    }
}
