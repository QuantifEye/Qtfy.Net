namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void TriangularMatrixSolve<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TSide, TUpperLower, TDiagonal>(
        TNumber alpha,
        TMatrixViewA matrix,
        TMatrixViewB b)
        where TMatrixViewA : Matrices.ITriangularMatrixView<TNumber, TMatrixRowA, TMatrixColumnA, TUpperLower, TDiagonal>, allows ref struct
        where TMatrixRowA : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : IVectorView<TNumber>, allows ref struct
        where TMatrixViewB : IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB : IVectorView<TNumber>, allows ref struct
        where TSide : Matrices.Traits.ISide
        where TUpperLower : Matrices.Traits.IUpperLower
        where TDiagonal : Matrices.Traits.IDiagonal
    {
        var rows = b.Rows;
        var columns = b.Columns;

        if (TSide.IsLeft())
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

        UnsafeBlas.TriangularMatrixSolve<TNumber, TSide, TUpperLower, TDiagonal>(
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
