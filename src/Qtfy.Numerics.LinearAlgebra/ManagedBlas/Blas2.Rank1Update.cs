namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas2<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void Rank1Update<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TVectorViewX x,
        TVectorViewY y,
        TMatrixView matrix)
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn, TMatrixView>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber, TMatrixRow>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber, TMatrixColumn>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber, TVectorViewX>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber, TVectorViewY>, allows ref struct
    {
        var rows = matrix.Rows;
        var columns = matrix.Columns;

        Debug.Assert(x.Length == rows, "x length must match matrix rows.");
        Debug.Assert(y.Length == columns, "y length must match matrix columns.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas2.Rank1Update(
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
