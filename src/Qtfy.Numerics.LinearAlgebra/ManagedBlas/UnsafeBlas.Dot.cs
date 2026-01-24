namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static TNumber Dot<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = TNumber.Zero;
        while (true)
        {
            total += xRef * yRef;
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);
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
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = TNumber.Zero;
        while (true)
        {
            total += xRef * yRef;
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);
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
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = TNumber.Zero;
        while (true)
        {
            total += xRef * yRef;
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, 1);
        }

        return total;
    }

    public static TNumber Dot<TNumber>(
        int n,
        ref TNumber x,
        ref TNumber y)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;
        var total = TNumber.Zero;
        while (true)
        {
            total += xRef * yRef;
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, 1);
        }

        return total;
    }
}
