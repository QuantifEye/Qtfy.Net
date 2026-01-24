namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void SymmetricRankKUpdate<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewC, TMatrixRowC, TMatrixColumnC, TUpperLower>(
        TNumber alpha,
        TMatrixViewA a,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA : IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : IVectorView<TNumber>, allows ref struct
        where TMatrixViewC : Matrices.ISymmetricMatrixView<TNumber, TMatrixRowC, TMatrixColumnC, TUpperLower>, allows ref struct
        where TMatrixRowC : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC : IVectorView<TNumber>, allows ref struct
        where TUpperLower : Matrices.Traits.IUpperLower
    {
        var n = c.Rows;
        var k = a.Columns;

        Debug.Assert(c.Columns == n, "c must be square.");
        Debug.Assert(a.Rows == n, "a rows must match c size.");

        var rowStrideA = a.RowStride;
        var colStrideA = a.ColumnStride;
        var rowStrideC = c.RowStride;
        var colStrideC = c.ColumnStride;

        UnsafeBlas.SymmetricRankKUpdate<TNumber, TUpperLower>(
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
