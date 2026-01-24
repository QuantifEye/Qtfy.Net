namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void SymmetricRank2KUpdate<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC, TUpperLower>(
        TNumber alpha,
        TMatrixViewA a,
        TMatrixViewB b,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA : IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : IVectorView<TNumber>, allows ref struct
        where TMatrixViewB : IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB : IVectorView<TNumber>, allows ref struct
        where TMatrixViewC : Matrices.ISymmetricMatrixView<TNumber, TMatrixRowC, TMatrixColumnC, TUpperLower>, allows ref struct
        where TMatrixRowC : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC : IVectorView<TNumber>, allows ref struct
        where TUpperLower : Matrices.Traits.IUpperLower
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

        UnsafeBlas.SymmetricRank2KUpdate<TNumber, TUpperLower>(
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
