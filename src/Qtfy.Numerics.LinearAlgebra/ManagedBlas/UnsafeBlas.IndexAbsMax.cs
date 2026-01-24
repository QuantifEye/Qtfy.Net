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

        var abs = TNumber.Abs(xRef);
        var maxAbs = abs;
        var maxIndex = 0;

        var index = 0;
        while (++index != n)
        {
            xRef = ref Add(ref xRef, strideX);

            abs = TNumber.Abs(xRef);
            if (abs > maxAbs)
            {
                maxAbs = abs;
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

        var abs = TNumber.Abs(xRef);
        var maxAbs = abs;
        var maxIndex = 0;
        var index = 0;

        while (++index != n)
        {
            xRef = ref Add(ref xRef, 1);

            abs = TNumber.Abs(xRef);
            if (abs > maxAbs)
            {
                maxAbs = abs;
                maxIndex = index;
            }
        }

        return maxIndex;
    }
}
