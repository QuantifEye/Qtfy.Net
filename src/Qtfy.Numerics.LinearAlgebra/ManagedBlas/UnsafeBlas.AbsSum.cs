namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static TNumber AbsSum<TNumber>(int n, ref TNumber x, int strideX)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        var sum = TNumber.Abs(xRef);
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, strideX);
            sum += TNumber.Abs(xRef);
        }

        return sum;
    }

    public static TNumber AbsSum<TNumber>(int n, ref TNumber x)
        where TNumber : INumber<TNumber>
    {
        if (n <= 0)
        {
            return TNumber.Zero;
        }

        ref var xRef = ref x;
        var sum = TNumber.Abs(xRef);
        while (--n != 0)
        {
            xRef = ref Add(ref xRef, 1);
            sum += TNumber.Abs(xRef);
        }

        return sum;
    }
}
