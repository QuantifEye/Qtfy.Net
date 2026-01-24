using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void TriangularMatrixVectorSolve<TNumber, TUpperLower, TDiagonal>(
        int n,
        ref TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber b,
        int strideB,
        ref TNumber x,
        int strideX)
        where TNumber : INumber<TNumber>
        where TUpperLower : IUpperLower
        where TDiagonal : IDiagonal
    {
        DebugAssertNotZero(n);

        var isUnit = TDiagonal.IsUnitDiagonal();

        if (TUpperLower.IsUpper())
        {
            for (int i = n - 1; i >= 0; i--)
            {
                ref var bRef = ref Add(ref b, i * strideB);
                var sum = bRef;

                if (i + 1 < n)
                {
                    ref var aRef = ref Add(ref matrix, (i * rowStride) + ((i + 1) * colStride));
                    ref var xRef = ref Add(ref x, (i + 1) * strideX);

                    for (int j = i + 1; j < n; j++)
                    {
                        sum -= aRef * xRef;

                        if (j + 1 == n)
                        {
                            break;
                        }

                        aRef = ref Add(ref aRef, colStride);
                        xRef = ref Add(ref xRef, strideX);
                    }
                }

                if (!isUnit)
                {
                    ref var aDiag = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                    sum /= aDiag;
                }

                ref var xIRef = ref Add(ref x, i * strideX);
                xIRef = sum;
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                ref var bRef = ref Add(ref b, i * strideB);
                var sum = bRef;

                if (i > 0)
                {
                    ref var aRef = ref Add(ref matrix, i * rowStride);
                    ref var xRef = ref x;

                    for (int j = 0; j < i; j++)
                    {
                        sum -= aRef * xRef;
                        aRef = ref Add(ref aRef, colStride);
                        xRef = ref Add(ref xRef, strideX);
                    }
                }

                if (!isUnit)
                {
                    ref var aDiag = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                    sum /= aDiag;
                }

                ref var xIRef = ref Add(ref x, i * strideX);
                xIRef = sum;
            }
        }
    }
}
