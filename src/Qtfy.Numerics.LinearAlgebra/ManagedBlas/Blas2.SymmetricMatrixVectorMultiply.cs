namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas2<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void SymmetricMatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
        Uplo uplo,
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
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");
        Debug.Assert(y.Length == n, "y length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas2.SymmetricMatrixVectorMultiply(
            uplo: uplo,
            n: n,
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

    public static void SymmetricMatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TUpperLower, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TMatrixView matrix,
        TVectorViewX x,
        TNumber beta,
        TVectorViewY y)
        where TMatrixView : struct, Qtfy.Numerics.LinearAlgebra.Matrices.ISymmetricMatrixView<TNumber, TMatrixRow, TMatrixColumn, TUpperLower>, allows ref struct
        where TMatrixRow : struct, IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : struct, IVectorView<TNumber>, allows ref struct
        where TUpperLower : Qtfy.Numerics.LinearAlgebra.Matrices.Traits.IUpperLower
        where TVectorViewX : struct, IVectorView<TNumber>, allows ref struct
        where TVectorViewY : struct, IVectorView<TNumber>, allows ref struct
    {
        SymmetricMatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TVectorViewX, TVectorViewY>(
            uplo: TMatrixView.Uplo,
            alpha: alpha,
            matrix: matrix,
            x: x,
            beta: beta,
            y: y);
    }
}
