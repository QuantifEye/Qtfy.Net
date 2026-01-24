namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static int IndexAbsMax<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return 0;
        }

        ref var xRef = ref x;
        var maxAbs = TNumber.Abs(xRef);
        var maxIndex = 0;
        var index = 0;
        while (true)
        {
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            ++index;
            var absx = TNumber.Abs(xRef);
            if (absx > maxAbs)
            {
                maxAbs = absx;
                maxIndex = index;
            }
        }

        return maxIndex;
    }

    public static int IndexAbsMax<TNumber>(int n, ref TNumber x)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return 0;
        }

        ref var xRef = ref x;
        var maxAbs = TNumber.Abs(xRef);
        var maxIndex = 0;
        var index = 0;
        while (true)
        {
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, 1);
            ++index;
            var absx = TNumber.Abs(xRef);
            if (absx > maxAbs)
            {
                maxAbs = absx;
                maxIndex = index;
            }
        }

        return maxIndex;
    }
}
