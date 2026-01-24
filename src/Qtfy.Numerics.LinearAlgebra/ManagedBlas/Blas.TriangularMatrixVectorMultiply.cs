namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void TriangularMatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TUpperLower, TDiagonal, TVectorViewX, TVectorViewY>(
        TMatrixView matrix,
        TVectorViewX x,
        TVectorViewY y)
        where TMatrixView : Matrices.ITriangularMatrixView<TNumber, TMatrixRow, TMatrixColumn, TUpperLower, TDiagonal>, allows ref struct
        where TMatrixRow : IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : IVectorView<TNumber>, allows ref struct
        where TUpperLower : Matrices.Traits.IUpperLower
        where TDiagonal : Matrices.Traits.IDiagonal
        where TVectorViewX : IVectorView<TNumber>, allows ref struct
        where TVectorViewY : IVectorView<TNumber>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");
        Debug.Assert(y.Length == n, "y length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas.TriangularMatrixVectorMultiply<TNumber, TUpperLower, TDiagonal>(
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
