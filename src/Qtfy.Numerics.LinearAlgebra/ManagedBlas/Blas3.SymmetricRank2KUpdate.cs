namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas3<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void SymmetricRank2KUpdate<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
        Uplo uplo,
        TNumber alpha,
        TMatrixViewA a,
        TMatrixViewB b,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA : struct, IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA, TMatrixViewA>, allows ref struct
        where TMatrixRowA : struct, IVectorView<TNumber, TMatrixRowA>, allows ref struct
        where TMatrixColumnA : struct, IVectorView<TNumber, TMatrixColumnA>, allows ref struct
        where TMatrixViewB : struct, IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB, TMatrixViewB>, allows ref struct
        where TMatrixRowB : struct, IVectorView<TNumber, TMatrixRowB>, allows ref struct
        where TMatrixColumnB : struct, IVectorView<TNumber, TMatrixColumnB>, allows ref struct
        where TMatrixViewC : struct, IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC, TMatrixViewC>, allows ref struct
        where TMatrixRowC : struct, IVectorView<TNumber, TMatrixRowC>, allows ref struct
        where TMatrixColumnC : struct, IVectorView<TNumber, TMatrixColumnC>, allows ref struct
    {
        var n = c.Rows;
        var k = a.Columns;

        Debug.Assert(c.Columns == n, "c must be square.");
        Debug.Assert(a.Rows == n, "a rows must match c size.");
        Debug.Assert(b.Rows == n, "b rows must match c size.");
        Debug.Assert(b.Columns == k, "b columns must match a columns.");

        var rowStrideA = a.RowStride;
        var colStrideA = a.ColumnStride;
        var rowStrideB = b.RowStride;
        var colStrideB = b.ColumnStride;
        var rowStrideC = c.RowStride;
        var colStrideC = c.ColumnStride;

        UnsafeBlas3.SymmetricRank2KUpdate(
            uplo: uplo,
            n: n,
            k: k,
            alpha: alpha,
            a: ref a.GetPinnableReference(),
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
