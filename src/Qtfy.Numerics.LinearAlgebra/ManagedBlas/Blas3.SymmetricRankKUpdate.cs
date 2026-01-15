namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas3<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void SymmetricRankKUpdate<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
        Uplo uplo,
        TNumber alpha,
        TMatrixViewA a,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA : struct, IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA, TMatrixViewA>, allows ref struct
        where TMatrixRowA : struct, IVectorView<TNumber, TMatrixRowA>, allows ref struct
        where TMatrixColumnA : struct, IVectorView<TNumber, TMatrixColumnA>, allows ref struct
        where TMatrixViewC : struct, IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC, TMatrixViewC>, allows ref struct
        where TMatrixRowC : struct, IVectorView<TNumber, TMatrixRowC>, allows ref struct
        where TMatrixColumnC : struct, IVectorView<TNumber, TMatrixColumnC>, allows ref struct
    {
        var n = c.Rows;
        var k = a.Columns;

        Debug.Assert(c.Columns == n, "c must be square.");
        Debug.Assert(a.Rows == n, "a rows must match c size.");

        var rowStrideA = a.RowStride;
        var colStrideA = a.ColumnStride;
        var rowStrideC = c.RowStride;
        var colStrideC = c.ColumnStride;

        UnsafeBlas3.SymmetricRankKUpdate(
            uplo: uplo,
            n: n,
            k: k,
            alpha: alpha,
            a: ref a.GetPinnableReference(),
            rowStrideA: rowStrideA,
            colStrideA: colStrideA,
            beta: beta,
            c: ref c.GetPinnableReference(),
            rowStrideC: rowStrideC,
            colStrideC: colStrideC);
    }
}
