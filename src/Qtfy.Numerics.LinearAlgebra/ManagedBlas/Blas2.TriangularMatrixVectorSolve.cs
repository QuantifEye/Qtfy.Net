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
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn, TMatrixView>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber, TMatrixRow>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber, TMatrixColumn>, allows ref struct
        where TVectorViewB : struct, IVectorView<TNumber, TVectorViewB>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber, TVectorViewX>, allows ref struct
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

    public static void TriangularMatrixVectorSolve<TMatrixView, TMatrixRow, TMatrixColumn, TUpperLower, TDiagonal, TVectorViewB, TVectorViewX>(
        TMatrixView matrix,
        TVectorViewB b,
        TVectorViewX x)
        where TMatrixView : struct, Qtfy.Numerics.LinearAlgebra.Matrices.ITriangularMatrixView<TNumber, TMatrixRow, TMatrixColumn, TUpperLower, TDiagonal, TMatrixView>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber, TMatrixRow>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber, TMatrixColumn>, allows ref struct
        where TUpperLower : Qtfy.Numerics.LinearAlgebra.Matrices.Traits.IUpperLower
        where TDiagonal : Qtfy.Numerics.LinearAlgebra.Matrices.Traits.IDiagonal
        where TVectorViewB : struct, IVectorView<TNumber, TVectorViewB>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber, TVectorViewX>, allows ref struct
    {
        TriangularMatrixVectorSolve<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewB, TVectorViewX>(
            uplo: TMatrixView.Uplo,
            diag: TMatrixView.Diag,
            matrix: matrix,
            b: b,
            x: x);
    }
}
