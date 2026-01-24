namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void Scale<TNumber>(int n, TNumber alpha, ref TNumber x, int strideX)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        while (true)
        {
            xRef *= alpha;
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
        }
    }

    public static void Scale<TNumber>(int n, TNumber alpha, ref TNumber x)
        where TNumber : INumberBase<TNumber>
    {
        if (n <= 0)
        {
            return;
        }

        ref var xRef = ref x;
        while (true)
        {
            xRef *= alpha;
            if (--n == 0)
            {
                break;
            }

            xRef = ref Add(ref xRef, 1);
        }
    }
}
