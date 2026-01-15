namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas2
{
    public static void SymmetricMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        int n,
        TNumber alpha,
        ref TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX,
        TNumber beta,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);

        ref var yScaleRef = ref y;
        for (int i = 0; i < n; i++)
        {
            yScaleRef *= beta;

            if (i + 1 == n)
            {
                break;
            }

            yScaleRef = ref Add(ref yScaleRef, strideY);
        }

        if (uplo == Uplo.Upper)
        {
            for (int i = 0; i < n; i++)
            {
                ref var yIRef = ref Add(ref y, i * strideY);
                ref var xIRef = ref Add(ref x, i * strideX);

                ref var aRef = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                ref var xJRef = ref xIRef;
                ref var yJRef = ref yIRef;

                for (int j = i; j < n; j++)
                {
                    var aVal = aRef;
                    yIRef += alpha * aVal * xJRef;
                    if (j != i)
                    {
                        yJRef += alpha * aVal * xIRef;
                    }

                    if (j + 1 == n)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                    yJRef = ref Add(ref yJRef, strideY);
                }
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                ref var yIRef = ref Add(ref y, i * strideY);
                ref var xIRef = ref Add(ref x, i * strideX);

                ref var aRef = ref Add(ref matrix, i * rowStride);
                ref var xJRef = ref x;
                ref var yJRef = ref y;

                for (int j = 0; j <= i; j++)
                {
                    var aVal = aRef;
                    yIRef += alpha * aVal * xJRef;
                    if (j != i)
                    {
                        yJRef += alpha * aVal * xIRef;
                    }

                    if (j == i)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                    yJRef = ref Add(ref yJRef, strideY);
                }
            }
        }
    }
}
