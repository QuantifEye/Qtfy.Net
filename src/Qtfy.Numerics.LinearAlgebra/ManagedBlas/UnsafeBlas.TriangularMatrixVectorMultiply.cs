using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void TriangularMatrixVectorMultiply<TNumber, TUpperLower, TDiagonal>(
        int n,
        ref TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
        where TUpperLower : IUpperLower
        where TDiagonal : IDiagonal
    {
        if (n <= 0)
        {
            return;
        }

        var isUnit = TDiagonal.IsUnitDiagonal();

        if (TUpperLower.IsUpper())
        {
            ref var aDiagRef = ref matrix;
            ref var xRowRef = ref x;
            ref var yRowRef = ref y;

            for (var row = 0; row < n; row++)
            {
                var sum = isUnit ? xRowRef : aDiagRef * xRowRef;

                var upperCount = n - row - 1;
                ref var aRef = ref aDiagRef;
                ref var xRef = ref xRowRef;

                for (var col = 0; col < upperCount; col++)
                {
                    aRef = ref Add(ref aRef, colStride);
                    xRef = ref Add(ref xRef, strideX);
                    sum += aRef * xRef;
                }

                yRowRef = sum;

                aDiagRef = ref Add(ref aDiagRef, rowStride + colStride);
                xRowRef = ref Add(ref xRowRef, strideX);
                yRowRef = ref Add(ref yRowRef, strideY);
            }
        }
        else
        {
            ref var rowRef = ref matrix;
            ref var yRowRef = ref y;

            for (var row = 0; row < n; row++)
            {
                ref var aRef = ref rowRef;
                ref var xRef = ref x;
                var sum = TNumber.Zero;

                for (var col = 0; col < row; col++)
                {
                    sum += aRef * xRef;
                    aRef = ref Add(ref aRef, colStride);
                    xRef = ref Add(ref xRef, strideX);
                }

                sum += isUnit ? xRef : aRef * xRef;
                yRowRef = sum;

                rowRef = ref Add(ref rowRef, rowStride);
                yRowRef = ref Add(ref yRowRef, strideY);
            }
        }
    }
}
