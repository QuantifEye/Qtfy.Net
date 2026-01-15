namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas2<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void MatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TMatrixView matrix,
        TVectorViewX x,
        TNumber beta,
        TVectorViewY y)
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber>, allows ref struct
    {
        var rows = matrix.Rows;
        var columns = matrix.Columns;

        Debug.Assert(x.Length == columns, "x length must match matrix columns.");
        Debug.Assert(y.Length == rows, "y length must match matrix rows.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas2.MatrixVectorMultiply(
            rows: rows,
            columns: columns,
            alpha: alpha,
            matrix: ref matrix.GetPinnableReference(),
            rowStride: rowStride,
            colStride: colStride,
            x: ref x.GetPinnableReference(),
            strideX: x.Stride,
            beta: beta,
            y: ref y.GetPinnableReference(),
            strideY: y.Stride);
    }

    public static void MatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        TMatrixView matrix,
        TVectorViewX x,
        TVectorViewY y)
        where TMatrixView : struct, IStridedMatrixView<TNumber, TMatrixRow, TMatrixColumn>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber>, allows ref struct
    {
        MatrixVectorMultiply(
            alpha: TNumber.One,
            matrix: matrix,
            x: x,
            beta: TNumber.Zero,
            y: y);
    }
}
