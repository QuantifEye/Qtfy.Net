namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas2
{
    public static void TriangularMatrixVectorMultiply<TNumber>(
        Uplo uplo,
        Diag diag,
        int n,
        ref TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(n);

        if (uplo == Uplo.Upper)
        {
            for (int i = 0; i < n; i++)
            {
                ref var yIRef = ref Add(ref y, i * strideY);
                ref var aRef = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                ref var xIRef = ref Add(ref x, i * strideX);

                var sum = diag == Diag.Unit ? xIRef : aRef * xIRef;

                if (i + 1 < n)
                {
                    ref var aUpperRef = ref Add(ref aRef, colStride);
                    ref var xUpperRef = ref Add(ref xIRef, strideX);

                    for (int j = i + 1; j < n; j++)
                    {
                        sum += aUpperRef * xUpperRef;

                        if (j + 1 == n)
                        {
                            break;
                        }

                        aUpperRef = ref Add(ref aUpperRef, colStride);
                        xUpperRef = ref Add(ref xUpperRef, strideX);
                    }
                }

                yIRef = sum;
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                ref var yIRef = ref Add(ref y, i * strideY);
                ref var aRef = ref Add(ref matrix, i * rowStride);
                ref var xRef = ref x;
                var sum = TNumber.Zero;

                for (int j = 0; j < i; j++)
                {
                    sum += aRef * xRef;
                    aRef = ref Add(ref aRef, colStride);
                    xRef = ref Add(ref xRef, strideX);
                }

                if (diag == Diag.Unit)
                {
                    sum += xRef;
                }
                else
                {
                    sum += aRef * xRef;
                }

                yIRef = sum;
            }
        }
    }
}
