namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas1
{
    public static TNumber Dot<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = xRef * yRef;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);
            total += xRef * yRef;
        }

        return total;
    }

    public static TNumber DotAlternative<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = xRef * yRef;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);
            total += xRef * yRef;
        }

        return total;
    }

    public static TNumber Dot<TNumber>(
        int n,
        ref TNumber x,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = xRef * yRef;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, strideY);
            total += xRef * yRef;
        }

        return total;
    }

    public static TNumber Dot<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = xRef * yRef;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, 1);
            total += xRef * yRef;
        }

        return total;
    }

    public static TNumber Dot<TNumber>(
        int n,
        ref TNumber x,
        ref TNumber y)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = xRef * yRef;
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, 1);
            total += xRef * yRef;
        }

        return total;
    }
}
