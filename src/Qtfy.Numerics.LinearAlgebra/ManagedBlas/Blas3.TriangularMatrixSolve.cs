namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas3<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void TriangularMatrixSolve<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB>(
        Side side,
        Uplo uplo,
        Diag diag,
        TNumber alpha,
        TMatrixViewA matrix,
        TMatrixViewB b)
        where TMatrixViewA : struct, IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixViewB : struct, IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB : struct, IVectorView<TNumber>, allows ref struct
    {
        var rows = b.Rows;
        var columns = b.Columns;

        if (side == Side.Left)
        {
            Debug.Assert(matrix.Rows == rows, "matrix rows must match b rows.");
            Debug.Assert(matrix.Columns == rows, "matrix must be square.");
        }
        else
        {
            Debug.Assert(matrix.Rows == columns, "matrix rows must match b columns.");
            Debug.Assert(matrix.Columns == columns, "matrix must be square.");
        }

        var rowStrideA = matrix.RowStride;
        var colStrideA = matrix.ColumnStride;
        var rowStrideB = b.RowStride;
        var colStrideB = b.ColumnStride;

        UnsafeBlas3.TriangularMatrixSolve(
            side: side,
            uplo: uplo,
            diag: diag,
            rows: rows,
            columns: columns,
            alpha: alpha,
            matrix: ref matrix.GetPinnableReference(),
            rowStrideA: rowStrideA,
            colStrideA: colStrideA,
            b: ref b.GetPinnableReference(),
            rowStrideB: rowStrideB,
            colStrideB: colStrideB);
    }
}
