namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void TriangularMatrixVectorSolve<TMatrixView, TMatrixRow, TMatrixColumn, TUpperLower, TDiagonal, TVectorViewB, TVectorViewX>(
        TMatrixView matrix,
        TVectorViewB b,
        TVectorViewX x)
        where TMatrixView :  Matrices.ITriangularMatrixView<TNumber, TMatrixRow, TMatrixColumn, TUpperLower, TDiagonal>, allows ref struct
        where TMatrixRow :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumn :  IVectorView<TNumber>, allows ref struct
        where TUpperLower : Matrices.Traits.IUpperLower
        where TDiagonal : Matrices.Traits.IDiagonal
        where TVectorViewB :  IVectorView<TNumber>, allows ref struct
        where TVectorViewX :  IVectorView<TNumber>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(b.Length == n, "b length must match matrix size.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas.TriangularMatrixVectorSolve<TNumber, TUpperLower, TDiagonal>(
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
