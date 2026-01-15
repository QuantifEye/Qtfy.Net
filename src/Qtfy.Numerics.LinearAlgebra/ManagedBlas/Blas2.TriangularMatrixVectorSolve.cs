namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas2<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void TriangularMatrixVectorSolve<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewB, TVectorViewX>(
        Uplo uplo,
        Diag diag,
        TMatrixView matrix,
        TVectorViewB b,
        TVectorViewX x)
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewB : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(b.Length == n, "b length must match matrix size.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas2.TriangularMatrixVectorSolve(
            uplo: uplo,
            diag: diag,
            n: n,
            matrix: ref matrix.GetPinnableReference(),
            rowStride: rowStride,
            colStride: colStride,
            b: ref b.GetPinnableReference(),
            strideB: b.Stride,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride);
    }
}
