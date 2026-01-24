namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void Rank1Update<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TVectorViewX x,
        TVectorViewY y,
        TMatrixView matrix)
        where TMatrixView : IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn>, allows ref struct
        where TMatrixRow : IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : IVectorView<TNumber>, allows ref struct
        where TVectorViewX : IVectorView<TNumber>, allows ref struct
        where TVectorViewY : IVectorView<TNumber>, allows ref struct
    {
        var rows = matrix.Rows;
        var columns = matrix.Columns;

        Debug.Assert(x.Length == rows, "x length must match matrix rows.");
        Debug.Assert(y.Length == columns, "y length must match matrix columns.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas.Rank1Update(
            rows: rows,
            columns: columns,
            alpha: alpha,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            y: ref y.GetPinnableReference(),
            strideY: y.Stride,
            matrix: ref matrix.GetPinnableReference(),
            rowStride: rowStride,
            colStride: colStride);
    }
}
