namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void Swap<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        while (true)
        {
            var temp = xRef;
            xRef = yRef;
            yRef = temp;

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);
        }
    }

    public static void Swap<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        while (true)
        {
            var temp = xRef;
            xRef = yRef;
            yRef = temp;

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, 1);
        }
    }

    public static void Swap<TNumber>(
        int n,
        ref TNumber x,
        ref TNumber y)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        while (true)
        {
            var temp = xRef;
            xRef = yRef;
            yRef = temp;

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, 1);
        }
    }
}
