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
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        var xValue = xRef;
        var yValue = yRef;
        yRef = (c * yValue) - (s * xValue);
        xRef = (c * xValue) + (s * yValue);

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, strideY);

            xValue = xRef;
            yValue = yRef;
            yRef = (c * yValue) - (s * xValue);
            xRef = (c * xValue) + (s * yValue);
        }
    }

    public static void ApplyGivensRotation<TNumber>(
        int n,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        TNumber c,
        TNumber s)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        var xValue = xRef;
        var yValue = yRef;
        yRef = (c * yValue) - (s * xValue);
        xRef = (c * xValue) + (s * yValue);

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            yRef = ref Add(ref yRef, 1);

            xValue = xRef;
            yValue = yRef;
            yRef = (c * yValue) - (s * xValue);
            xRef = (c * xValue) + (s * yValue);
        }
    }

    public static void ApplyGivensRotation<TNumber>(
        int n,
        ref TNumber x,
        ref TNumber y,
        TNumber c,
        TNumber s)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        ref var yRef = ref y;

        var xValue = xRef;
        var yValue = yRef;
        yRef = (c * yValue) - (s * xValue);
        xRef = (c * xValue) + (s * yValue);

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            yRef = ref Add(ref yRef, 1);

            xValue = xRef;
            yValue = yRef;
            yRef = (c * yValue) - (s * xValue);
            xRef = (c * xValue) + (s * yValue);
        }
    }
}
