using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void SymmetricRank2Update<TNumber, TUpperLower>(
        int n,
        TNumber alpha,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY,
        ref TNumber matrix,
        int rowStride,
        int colStride)
        where TNumber : INumber<TNumber>
        where TUpperLower : IUpperLower
    {
        DebugAssertNotZero(n);

        if (TUpperLower.IsUpper())
        {
            ref var xIRef = ref x;
            ref var yIRef = ref y;
            for (int i = 0; i < n; i++)
            {
                var xVal = xIRef;
                var yVal = yIRef;
                ref var aRef = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                ref var xJRef = ref xIRef;
                ref var yJRef = ref yIRef;

                for (int j = i; j < n; j++)
                {
                    aRef += alpha * ((xVal * yJRef) + (yVal * xJRef));

                    if (j + 1 == n)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                    yJRef = ref Add(ref yJRef, strideY);
                }

                if (i + 1 == n)
                {
                    break;
                }

                xIRef = ref Add(ref xIRef, strideX);
                yIRef = ref Add(ref yIRef, strideY);
            }
        }
        else
        {
            ref var xIRef = ref x;
            ref var yIRef = ref y;
            for (int i = 0; i < n; i++)
            {
                var xVal = xIRef;
                var yVal = yIRef;
                ref var aRef = ref Add(ref matrix, i * rowStride);
                ref var xJRef = ref x;
                ref var yJRef = ref y;

                for (int j = 0; j <= i; j++)
                {
                    aRef += alpha * ((xVal * yJRef) + (yVal * xJRef));

                    if (j == i)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                    yJRef = ref Add(ref yJRef, strideY);
                }

                if (i + 1 == n)
                {
                    break;
                }

                xIRef = ref Add(ref xIRef, strideX);
                yIRef = ref Add(ref yIRef, strideY);
            }
        }
    }
}
