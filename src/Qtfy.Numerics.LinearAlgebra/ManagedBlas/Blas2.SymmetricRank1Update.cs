namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas2<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void SymmetricRank1Update<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX>(
        Uplo uplo,
        TNumber alpha,
        TVectorViewX x,
        TMatrixView matrix)
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn, TMatrixView>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber, TMatrixRow>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber, TMatrixColumn>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber, TVectorViewX>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas2.SymmetricRank1Update(
            uplo: uplo,
            n: n,
            alpha: alpha,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            matrix: ref matrix.GetPinnableReference(),
            rowStride: rowStride,
            colStride: colStride);
    }
}
