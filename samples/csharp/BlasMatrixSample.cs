using Qtfy.Numerics.LinearAlgebra;
using Qtfy.Numerics.LinearAlgebra.ManagedBlas;
using Qtfy.Numerics.LinearAlgebra.Matrices;
using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;
using Qtfy.Numerics.LinearAlgebra.Vectors;

public static class BlasMatrixSample
{
    public static void Run()
    {
        var matrix = new RowMajorMatrix<double>(rows: 2, columns: 3);
        matrix[0, 0] = 1;
        matrix[0, 1] = 2;
        matrix[0, 2] = 3;
        matrix[1, 0] = 4;
        matrix[1, 1] = 5;
        matrix[1, 2] = 6;

        var x = new Vector<double>(length: 3);
        x[0] = 1;
        x[1] = 2;
        x[2] = 3;

        var y = new Vector<double>(length: 2);

        var matrixView = matrix.AsView();
        var xView = x.AsView();
        var yView = y.AsView();

        Blas<double>.MatrixVectorMultiply(
            alpha: 1.0,
            matrix: matrixView,
            x: xView,
            beta: 0.0,
            y: yView);

        UnsafeBlas.MatrixVectorMultiply(
            rows: matrix.Rows,
            columns: matrix.Columns,
            alpha: 1.0,
            matrix: ref matrixView.GetPinnableReference(),
            rowStride: matrixView.RowStride,
            colStride: matrixView.ColumnStride,
            x: ref xView.GetPinnableReference(),
            strideX: xView.Stride,
            beta: 0.0,
            y: ref yView.GetPinnableReference(),
            strideY: yView.Stride);

        var triangular = new TriangularMatrix<double, Upper, NonUnitDiagonal>(order: 3, isRowMajor: true);
        for (int i = 0; i < triangular.Rows; i++)
        {
            for (int j = i; j < triangular.Columns; j++)
            {
                triangular[i, j] = 1 + i + j;
            }
        }

        var tx = new Vector<double>(length: 3);
        tx[0] = 1;
        tx[1] = 2;
        tx[2] = 3;

        var ty = new Vector<double>(length: 3);

        var triangularView = triangular.AsView();
        var txView = tx.AsView();
        var tyView = ty.AsView();

        Blas<double>.TriangularMatrixVectorMultiply(triangularView, txView, tyView);

        UnsafeBlas.TriangularMatrixVectorMultiply<double, Upper, NonUnitDiagonal>(
            n: triangularView.Rows,
            matrix: ref triangularView.GetPinnableReference(),
            rowStride: triangularView.RowStride,
            colStride: triangularView.ColumnStride,
            x: ref txView.GetPinnableReference(),
            strideX: txView.Stride,
            y: ref tyView.GetPinnableReference(),
            strideY: tyView.Stride);
    }
}
