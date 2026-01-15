namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct Blas3<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
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
        var m = a.Rows;
        var k = a.Columns;

        Debug.Assert(b.Rows == k, "b rows must match a columns.");

        var n = b.Columns;

        Debug.Assert(c.Rows == m, "c rows must match a rows.");
        Debug.Assert(c.Columns == n, "c columns must match b columns.");

        var rowStrideA = a.RowStride;
        var colStrideA = a.ColumnStride;
        var rowStrideB = b.RowStride;
        var colStrideB = b.ColumnStride;
        var rowStrideC = c.RowStride;
        var colStrideC = c.ColumnStride;

        UnsafeBlas3.MatrixMultiply(
            m: m,
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

    public static void MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
        TMatrixViewA a,
        TMatrixViewB b,
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
        MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
            alpha: TNumber.One,
            a: a,
            b: b,
            beta: TNumber.Zero,
            c: c);
    }
}
