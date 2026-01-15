namespace Qtfy.Numerics.LinearAlgebra.BLAS;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

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
        if (uplo == Uplo.Upper)
        {
            if (diag == Diag.Unit)
            {
                TriangularMatrixVectorMultiply<TNumber, Upper, UnitDiagonal>(
                    n: n,
                    matrix: ref matrix,
                    rowStride: rowStride,
                    colStride: colStride,
                    x: ref x,
                    strideX: strideX,
                    y: ref y,
                    strideY: strideY);
            }
            else
            {
                TriangularMatrixVectorMultiply<TNumber, Upper, NonUnitDiagonal>(
                    n: n,
                    matrix: ref matrix,
                    rowStride: rowStride,
                    colStride: colStride,
                    x: ref x,
                    strideX: strideX,
                    y: ref y,
                    strideY: strideY);
            }
        }
        else
        {
            if (diag == Diag.Unit)
            {
                TriangularMatrixVectorMultiply<TNumber, Lower, UnitDiagonal>(
                    n: n,
                    matrix: ref matrix,
                    rowStride: rowStride,
                    colStride: colStride,
                    x: ref x,
                    strideX: strideX,
                    y: ref y,
                    strideY: strideY);
            }
            else
            {
                TriangularMatrixVectorMultiply<TNumber, Lower, NonUnitDiagonal>(
                    n: n,
                    matrix: ref matrix,
                    rowStride: rowStride,
                    colStride: colStride,
                    x: ref x,
                    strideX: strideX,
                    y: ref y,
                    strideY: strideY);
            }
        }
    }

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
        DebugAssertNotZero(n);

        if (TUpperLower.IsUpper())
        {
            for (int i = 0; i < n; i++)
            {
                ref var yIRef = ref Add(ref y, i * strideY);
                ref var aRef = ref Add(ref matrix, (i * rowStride) + (i * colStride));
                ref var xIRef = ref Add(ref x, i * strideX);

                var sum = TDiagonal.IsUnitDiagonal() ? xIRef : aRef * xIRef;

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

                if (TDiagonal.IsUnitDiagonal())
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
