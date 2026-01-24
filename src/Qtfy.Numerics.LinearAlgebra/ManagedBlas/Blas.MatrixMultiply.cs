namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct Blas<TNumber>
    where TNumber : INumber<TNumber>
{
    public static void MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
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
        where TMatrixViewC : IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC>, allows ref struct
        where TMatrixRowC : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC : IVectorView<TNumber>, allows ref struct
    {
        Debug.Assert(b.Rows == a.Columns, "b rows must match a columns.");
        Debug.Assert(c.Rows == a.Rows, "c rows must match a rows.");
        Debug.Assert(c.Columns == b.Columns, "c columns must match b columns.");

        UnsafeBlas.MatrixMultiply(
            m: a.Rows,
            n: b.Columns,
            k: a.Columns,
            alpha: alpha,
            a: ref a.GetPinnableReference(),
            rowStrideA: a.RowStride,
            colStrideA: a.ColumnStride,
            b: ref b.GetPinnableReference(),
            rowStrideB: b.RowStride,
            colStrideB: b.ColumnStride,
            beta: beta,
            c: ref c.GetPinnableReference(),
            rowStrideC: c.RowStride,
            colStrideC: c.ColumnStride);
    }

    public static void MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
        TMatrixViewA a,
        TMatrixViewB b,
        TMatrixViewC c)
        where TMatrixViewA : IStridedMatrixView<TNumber, TMatrixRowA, TMatrixColumnA>, allows ref struct
        where TMatrixRowA : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnA : IVectorView<TNumber>, allows ref struct
        where TMatrixViewB : IStridedMatrixView<TNumber, TMatrixRowB, TMatrixColumnB>, allows ref struct
        where TMatrixRowB : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnB : IVectorView<TNumber>, allows ref struct
        where TMatrixViewC : IStridedMatrixView<TNumber, TMatrixRowC, TMatrixColumnC>, allows ref struct
        where TMatrixRowC : IVectorView<TNumber>, allows ref struct
        where TMatrixColumnC : IVectorView<TNumber>, allows ref struct
    {
        MatrixMultiply<TMatrixViewA, TMatrixRowA, TMatrixColumnA, TMatrixViewB, TMatrixRowB, TMatrixColumnB, TMatrixViewC, TMatrixRowC, TMatrixColumnC>(
            alpha: TNumber.One,
            a: a,
            b: b,
            beta: TNumber.Zero,
            c: c);
    }
}
