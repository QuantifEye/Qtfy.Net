namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void SymmetricRank1Update<TMatrixView, TMatrixRow, TMatrixColumn, TUpperLower, TVectorViewX>(
        TNumber alpha,
        TVectorViewX x,
        TMatrixView matrix)
        where TMatrixView : Matrices.ISymmetricMatrixView<TNumber, TMatrixRow, TMatrixColumn, TUpperLower>, allows ref struct
        where TMatrixRow : IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : IVectorView<TNumber>, allows ref struct
        where TUpperLower : Matrices.Traits.IUpperLower
        where TVectorViewX : IVectorView<TNumber>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas.SymmetricRank1Update<TNumber, TUpperLower>(
            n: n,
            alpha: alpha,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            matrix: ref matrix.GetPinnableReference(),
            rowStride: rowStride,
            colStride: colStride);
    }
}
