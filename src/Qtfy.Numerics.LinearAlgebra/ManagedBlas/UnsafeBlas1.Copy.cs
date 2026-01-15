namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas1
{
    public static void Copy<TNumber>(int n, ref TNumber x, int strideX, ref TNumber y, int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
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
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
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

    public static void Copy<TNumber>(int n, ref TNumber x, ref TNumber y, int strideY)
    where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        ref var yRef = ref y;
        yRef = xRef;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, strideY);
            yRef = xRef;
        }
    }

    public static void Copy<TNumber>(int n, ref TNumber x, ref TNumber y)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
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
