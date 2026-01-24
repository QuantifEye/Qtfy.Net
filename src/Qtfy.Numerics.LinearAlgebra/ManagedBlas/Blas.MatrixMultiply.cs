namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumberBase<TNumber>
{
    public static void MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
        TNumber alpha,
        TMatrixViewA a,
        TMatrixViewB b,
        TNumber beta,
        TMatrixViewC c)
        where TMatrixViewA :  IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA :  IVectorView<TNumber>, allows ref struct
        where TMatrixViewB :  IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB :  IVectorView<TNumber>, allows ref struct
        where TMatrixViewC :  IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC>, allows ref struct
        where TMatrixRowC :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC :  IVectorView<TNumber>, allows ref struct
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

        UnsafeBlas.MatrixMultiply(
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
        where TMatrixViewA :  IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA :  IVectorView<TNumber>, allows ref struct
        where TMatrixViewB :  IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB :  IVectorView<TNumber>, allows ref struct
        where TMatrixViewC :  IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC>, allows ref struct
        where TMatrixRowC :  IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC :  IVectorView<TNumber>, allows ref struct
    {
        MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
            alpha: TNumber.One,
            a: a,
            b: b,
            beta: TNumber.Zero,
            c: c);
    }
}
