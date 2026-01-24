namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void SymmetricMatrixVectorMultiply<TMatrixView, TMatrixRow, TMatrixColumn, TUpperLower, TVectorViewX, TVectorViewY>(
        TNumber alpha,
        TMatrixView matrix,
        TVectorViewX x,
        TNumber beta,
        TVectorViewY y)
        where TMatrixView : Matrices.ISymmetricMatrixView<TNumber, TMatrixRow, TMatrixColumn, TUpperLower>, allows ref struct
        where TMatrixRow : IVectorView<TNumber>, allows ref struct
        where TMatrixColumn : IVectorView<TNumber>, allows ref struct
        where TUpperLower : Matrices.Traits.IUpperLower
        where TVectorViewX : IVectorView<TNumber>, allows ref struct
        where TVectorViewY : IVectorView<TNumber>, allows ref struct
    {
        var n = matrix.Rows;

        Debug.Assert(matrix.Columns == n, "matrix must be square.");
        Debug.Assert(x.Length == n, "x length must match matrix size.");
        Debug.Assert(y.Length == n, "y length must match matrix size.");

        var rowStride = matrix.RowStride;
        var colStride = matrix.ColumnStride;

        UnsafeBlas.SymmetricMatrixVectorMultiply<TNumber, TUpperLower>(
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
}
