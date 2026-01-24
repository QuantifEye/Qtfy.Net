namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void Copy<TNumber>(int n, ref TNumber x, int strideX, ref TNumber y, int strideY)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        yRef = xRef;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);

            yRef = xRef;
        }
    }

    public static void Copy<TNumber>(int n, ref TNumber x, int strideX, ref TNumber y)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        yRef = xRef;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, 1);

            yRef = xRef;
        }
    }

    public static void Copy<TNumber>(int n, ref TNumber x, ref TNumber y)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        yRef = xRef;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, 1);

            yRef = xRef;
        }
    }
}
