using Qtfy.Numerics.LinearAlgebra.BLAS;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static GivensRotation<TNumber> SetupGivensRotation<TNumber>(TNumber a, TNumber b)
        where TNumber : INumber<TNumber>
    {
        if (TNumber.IsZero(b))
        {
            return new GivensRotation<TNumber>(TNumber.One, TNumber.Zero, a);
        }

        if (TNumber.IsZero(a))
        {
            return new GivensRotation<TNumber>(TNumber.Zero, TNumber.One, b);
        }

        var absA = TNumber.Abs(a);
        var absB = TNumber.Abs(b);
        var scale = absA + absB;

        var scaledA = a / scale;
        var scaledB = b / scale;
        var sum = (scaledA * scaledA) + (scaledB * scaledB);

        var r = scale * TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(sum)));

        var roe = absA > absB ? a : b;
        if (TNumber.IsNegative(roe))
        {
            r = -r;
        }

        var c = a / r;
        var s = b / r;

        return new GivensRotation<TNumber>(c, s, r);
    }
}
