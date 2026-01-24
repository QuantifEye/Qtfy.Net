namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static TNumber Norm2<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        var scale = TNumber.Zero;
        var sumsq = TNumber.Zero;

        while (true)
        {
            var absx = TNumber.Abs(xRef);
            if (!TNumber.IsZero(absx))
            {
                if (TNumber.IsZero(scale))
                {
                    scale = absx;
                    sumsq = TNumber.One;
                }
                else if (absx > scale)
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

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
        }

        if (TNumber.IsZero(scale))
        {
            return TNumber.Zero;
        }

        return scale * TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sumsq)));
    }

    public static TNumber Norm2<TNumber>(int n, ref TNumber x)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        var scale = TNumber.Zero;
        var sumsq = TNumber.Zero;

        while (true)
        {
            var absx = TNumber.Abs(xRef);
            if (!TNumber.IsZero(absx))
            {
                if (TNumber.IsZero(scale))
                {
                    scale = absx;
                    sumsq = TNumber.One;
                }
                else if (absx > scale)
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

            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, 1);
        }

        if (TNumber.IsZero(scale))
        {
            return TNumber.Zero;
        }

        return scale * TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sumsq)));
    }
}
