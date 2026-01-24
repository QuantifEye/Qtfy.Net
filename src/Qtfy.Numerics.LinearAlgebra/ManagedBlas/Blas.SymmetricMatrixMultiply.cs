namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void SymmetricMatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC, TSide, TUpperLower>(
        TNumber alpha,
        TMatrixViewA matrix,
        TMatrixViewB b,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA : Matrices.ISymmetricMatrixView<TNumber, TMatrixRowA, TMatrixColumnA, TUpperLower>, allows ref struct
        where TMatrixRowA : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : IVectorView<TNumber>, allows ref struct
        where TMatrixViewB : IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB : IVectorView<TNumber>, allows ref struct
        where TMatrixViewC : IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC>, allows ref struct
        where TMatrixRowC : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC : IVectorView<TNumber>, allows ref struct
        where TSide : Matrices.Traits.ISide
        where TUpperLower : Matrices.Traits.IUpperLower
    {
        var rows = c.Rows;
        var columns = c.Columns;

        Debug.Assert(b.Rows == rows, "b rows must match c rows.");
        Debug.Assert(b.Columns == columns, "b columns must match c columns.");

        if (TSide.IsLeft())
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

        UnsafeBlas.SymmetricMatrixMultiply<TNumber, TSide, TUpperLower>(
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
