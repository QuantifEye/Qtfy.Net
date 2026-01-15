namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas2<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void TriangularMatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        Uplo uplo,
        Diag diag,
        TMatrixView matrix,
        TVectorViewX x,
        TVectorViewY y)
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");
        Debug.Assert(y.Length == n, "y length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas2.TriangularMatrixVectorMultiply(
            uplo: uplo,
            diag: diag,
            n: n,
            matrix: ref matrix.GetPinnableReference(),
            rowStride: rowStride,
            colStride: colStride,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            y: ref y.GetPinnableReference(),
            strideY: y.Stride);
    }
}
