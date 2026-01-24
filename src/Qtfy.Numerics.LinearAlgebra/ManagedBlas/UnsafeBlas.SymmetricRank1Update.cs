namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void SymmetricRank1Update<TNumber>(
        Uplo uplo,
        int n,
        TNumber alpha,
        ref TNumber x,
        int strideX,
        ref TNumber matrix,
        int rowStride,
        int colStride)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);

        if (uplo == Uplo.Upper)
        {
            ref var xIRef = ref x;
            for (int i = 0; i < n; i++)
            {
                var xVal = xIRef;
                ref var aRef = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                ref var xJRef = ref xIRef;

                for (int j = i; j < n; j++)
                {
                    aRef += alpha * xVal * xJRef;

                    if (j + 1 == n)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                }

                if (i + 1 == n)
                {
                    break;
                }

                xIRef = ref Add(ref xIRef, strideX);
            }
        }
        else
        {
            ref var xIRef = ref x;
            for (int i = 0; i < n; i++)
            {
                var xVal = xIRef;
                ref var aRef = ref Add(ref matrix, i * rowStride);
                ref var xJRef = ref x;

                for (int j = 0; j <= i; j++)
                {
                    aRef += alpha * xVal * xJRef;

                    if (j == i)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                }

                if (i + 1 == n)
                {
                    break;
                }

                xIRef = ref Add(ref xIRef, strideX);
            }
        }
    }
}
