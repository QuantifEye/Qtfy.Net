namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas3
{
    public static void TriangularMatrixSolve<TNumber>(
        Side side,
        Uplo uplo,
        Diag diag,
        int rows,
        int columns,
        TNumber alpha,
        ref TNumber matrix,
        int rowStrideA,
        int colStrideA,
        ref TNumber b,
        int rowStrideB,
        int colStrideB)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(rows, columns);

        if (side == Side.Left)
        {
            if (uplo == Uplo.Upper)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int i = rows - 1; i >= 0; i--)
                    {
                        ref var bRef = ref Add(ref b, (i * rowStrideB) + (j * colStrideB));
                        var sum = alpha * bRef;

                        if (i + 1 < rows)
                        {
                            ref var aRef = ref Add(ref matrix, (i * rowStrideA) + ((i + 1) * colStrideA));
                            ref var bKRef = ref Add(ref b, ((i + 1) * rowStrideB) + (j * colStrideB));

                            for (int k = i + 1; k < rows; k++)
                            {
                                sum -= aRef * bKRef;

                                if (k + 1 == rows)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, colStrideA);
                                bKRef = ref Add(ref bKRef, rowStrideB);
                            }
                        }

                        if (diag == Diag.NonUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (i * rowStrideA) + (i * colStrideA));
                            sum /= aDiag;
                        }

                        bRef = sum;
                    }
                }
            }
            else
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        ref var bRef = ref Add(ref b, (i * rowStrideB) + (j * colStrideB));
                        var sum = alpha * bRef;

                        if (i > 0)
                        {
                            ref var aRef = ref Add(ref matrix, i * rowStrideA);
                            ref var bKRef = ref Add(ref b, j * colStrideB);

                            for (int k = 0; k < i; k++)
                            {
                                sum -= aRef * bKRef;

                                if (k + 1 == i)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, colStrideA);
                                bKRef = ref Add(ref bKRef, rowStrideB);
                            }
                        }

                        if (diag == Diag.NonUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (i * rowStrideA) + (i * colStrideA));
                            sum /= aDiag;
                        }

                        bRef = sum;
                    }
                }
            }
        }
        else
        {
            if (uplo == Uplo.Upper)
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);

                    for (int j = 0; j < columns; j++)
                    {
                        ref var bRef = ref Add(ref bRowRef, j * colStrideB);
                        var sum = alpha * bRef;

                        if (j > 0)
                        {
                            ref var aRef = ref Add(ref matrix, j * colStrideA);
                            ref var bKRef = ref bRowRef;

                            for (int k = 0; k < j; k++)
                            {
                                sum -= bKRef * aRef;

                                if (k + 1 == j)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bKRef = ref Add(ref bKRef, colStrideB);
                            }
                        }

                        if (diag == Diag.NonUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (j * rowStrideA) + (j * colStrideA));
                            sum /= aDiag;
                        }

                        bRef = sum;
                    }
                }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);

                    for (int j = columns - 1; j >= 0; j--)
                    {
                        ref var bRef = ref Add(ref bRowRef, j * colStrideB);
                        var sum = alpha * bRef;

                        if (j + 1 < columns)
                        {
                            ref var aRef = ref Add(ref matrix, ((j + 1) * rowStrideA) + (j * colStrideA));
                            ref var bKRef = ref Add(ref bRowRef, (j + 1) * colStrideB);

                            for (int k = j + 1; k < columns; k++)
                            {
                                sum -= bKRef * aRef;

                                if (k + 1 == columns)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bKRef = ref Add(ref bKRef, colStrideB);
                            }
                        }

                        if (diag == Diag.NonUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (j * rowStrideA) + (j * colStrideA));
                            sum /= aDiag;
                        }

                        bRef = sum;
                    }
                }
            }
        }
    }
}
