namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void ApplyGivensRotation<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY,
        TNumber c,
        TNumber s)
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
            var xValue = xRef;
            var yValue = yRef;
            var temp = (c * xValue) + (s * yValue);
            yRef = (c * yValue) - (s * xValue);
            xRef = temp;

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);
        }
    }

    public static void ApplyGivensRotation<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        TNumber c,
        TNumber s)
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
            var xValue = xRef;
            var yValue = yRef;
            var temp = (c * xValue) + (s * yValue);
            yRef = (c * yValue) - (s * xValue);
            xRef = temp;

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, 1);
        }
    }

    public static void ApplyGivensRotation<TNumber>(
        int n,
        ref TNumber x,
        ref TNumber y,
        TNumber c,
        TNumber s)
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
            var xValue = xRef;
            var yValue = yRef;
            var temp = (c * xValue) + (s * yValue);
            yRef = (c * yValue) - (s * xValue);
            xRef = temp;

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, 1);
        }
    }
}
