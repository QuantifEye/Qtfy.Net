namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas1
{
    public static TNumber Norm2<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        return Norm2Scaled(n, ref x, strideX);
    }

    public static TNumber Norm2<TNumber>(int n, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        return Norm2Scaled(n, ref x);
    }

    public static TNumber Norm2Scaled<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var scale = TNumber.Abs(xRef);
        var sumsq = TNumber.One;

        if (TNumber.IsZero(scale))
        {
            sumsq = TNumber.Zero;
        }

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            var absx = TNumber.Abs(xRef);
            if (TNumber.IsZero(absx))
            {
                continue;
            }

            if (TNumber.IsZero(scale))
            {
                scale = absx;
                sumsq = TNumber.One;
                continue;
            }

            if (TNumber.IsPositive(absx - scale))
            {
                var t = scale / absx;
                sumsq = TNumber.One + (sumsq * t * t);
                scale = absx;
            }
            else
            {
                var t = absx / scale;
                sumsq += t * t;
            }
        }

        if (TNumber.IsZero(scale))
        {
            return TNumber.Zero;
        }

        return scale * TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sumsq)));
    }

    public static TNumber Norm2Scaled<TNumber>(int n, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var scale = TNumber.Abs(xRef);
        var sumsq = TNumber.One;

        if (TNumber.IsZero(scale))
        {
            sumsq = TNumber.Zero;
        }

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            var absx = TNumber.Abs(xRef);
            if (TNumber.IsZero(absx))
            {
                continue;
            }

            if (TNumber.IsZero(scale))
            {
                scale = absx;
                sumsq = TNumber.One;
                continue;
            }

            if (TNumber.IsPositive(absx - scale))
            {
                var t = scale / absx;
                sumsq = TNumber.One + (sumsq * t * t);
                scale = absx;
            }
            else
            {
                var t = absx / scale;
                sumsq += t * t;
            }
        }

        if (TNumber.IsZero(scale))
        {
            return TNumber.Zero;
        }

        return scale * TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sumsq)));
    }

    public static TNumber Norm2SumSquares<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var sum = xRef * xRef;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            sum += xRef * xRef;
        }

        return TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sum)));
    }

    public static TNumber Norm2SumSquares<TNumber>(int n, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);
        ref var xRef = ref x;
        var sum = xRef * xRef;

        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            sum += xRef * xRef;
        }

        return TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sum)));
    }
}
