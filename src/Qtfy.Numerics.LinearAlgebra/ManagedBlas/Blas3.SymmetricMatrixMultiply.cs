namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas3<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void SymmetricMatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
        Side side,
        Uplo uplo,
        TNumber alpha,
        TMatrixViewA matrix,
        TMatrixViewB b,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA : struct, IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixViewB : struct, IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixViewC : struct, IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC>, allows ref struct
        where TMatrixRowC : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC : struct, IVectorView<TNumber>, allows ref struct
    {
        var rows = c.Rows;
        var columns = c.Columns;

        Debug.Assert(b.Rows == rows, "b rows must match c rows.");
        Debug.Assert(b.Columns == columns, "b columns must match c columns.");

        if (side == Side.Left)
        {
            Debug.Assert(matrix.Rows == rows, "matrix rows must match c rows.");
            Debug.Assert(matrix.Columns == rows, "matrix must be square.");
        }
        else
        {
            Debug.Assert(matrix.Rows == columns, "matrix rows must match c columns.");
            Debug.Assert(matrix.Columns == columns, "matrix must be square.");
        }

        var rowStrideA = matrix.RowStride;
        var colStrideA = matrix.ColumnStride;
        var rowStrideB = b.RowStride;
        var colStrideB = b.ColumnStride;
        var rowStrideC = c.RowStride;
        var colStrideC = c.ColumnStride;

        UnsafeBlas3.SymmetricMatrixMultiply(
            side: side,
            uplo: uplo,
            rows: rows,
            columns: columns,
            alpha: alpha,
            matrix: ref matrix.GetPinnableReference(),
            rowStrideA: rowStrideA,
            colStrideA: colStrideA,
            b: ref b.GetPinnableReference(),
            rowStrideB: rowStrideB,
            colStrideB: colStrideB,
            beta: beta,
            c: ref c.GetPinnableReference(),
            rowStrideC: rowStrideC,
            colStrideC: colStrideC);
    }
}
