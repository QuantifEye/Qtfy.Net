namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas3
{
    public static void SymmetricMatrixMultiply<TNumber>(
        Side side,
        Uplo uplo,
        int rows,
        int columns,
        TNumber alpha,
        ref TNumber matrix,
        int rowStrideA,
        int colStrideA,
        ref TNumber b,
        int rowStrideB,
        int colStrideB,
        TNumber beta,
        ref TNumber c,
        int rowStrideC,
        int colStrideC)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(rows, columns);

        if (side == Side.Left)
        {
            if (uplo == Uplo.Upper)
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var cRowRef = ref Add(ref c, i * rowStrideC);

                    for (int j = 0; j < columns; j++)
                    {
                        var sum = TNumber.Zero;

                        if (i > 0)
                        {
                            ref var aRef = ref Add(ref matrix, i * colStrideA);
                            ref var bRef = ref Add(ref b, j * colStrideB);

                            for (int k = 0; k < i; k++)
                            {
                                sum += aRef * bRef;

                                if (k + 1 == i)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bRef = ref Add(ref bRef, rowStrideB);
                            }
                        }

                        ref var aDiagRef = ref Add(ref matrix, (i * rowStrideA) + (i * colStrideA));
                        ref var bDiagRef = ref Add(ref b, (i * rowStrideB) + (j * colStrideB));

                        for (int k = i; k < rows; k++)
                        {
                            sum += aDiagRef * bDiagRef;

                            if (k + 1 == rows)
                            {
                                break;
                            }

                            aDiagRef = ref Add(ref aDiagRef, colStrideA);
                            bDiagRef = ref Add(ref bDiagRef, rowStrideB);
                        }

                        ref var cRef = ref Add(ref cRowRef, j * colStrideC);
                        cRef = (beta * cRef) + (alpha * sum);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var cRowRef = ref Add(ref c, i * rowStrideC);

                    for (int j = 0; j < columns; j++)
                    {
                        var sum = TNumber.Zero;

                        ref var aRowRef = ref Add(ref matrix, i * rowStrideA);
                        ref var bRef = ref Add(ref b, j * colStrideB);

                        for (int k = 0; k <= i; k++)
                        {
                            sum += aRowRef * bRef;

                            if (k == i)
                            {
                                break;
                            }

                            aRowRef = ref Add(ref aRowRef, colStrideA);
                            bRef = ref Add(ref bRef, rowStrideB);
                        }

                        if (i + 1 < rows)
                        {
                            ref var aRef = ref Add(ref matrix, ((i + 1) * rowStrideA) + (i * colStrideA));
                            ref var bRef2 = ref Add(ref b, ((i + 1) * rowStrideB) + (j * colStrideB));

                            for (int k = i + 1; k < rows; k++)
                            {
                                sum += aRef * bRef2;

                                if (k + 1 == rows)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bRef2 = ref Add(ref bRef2, rowStrideB);
                            }
                        }

                        ref var cRef = ref Add(ref cRowRef, j * colStrideC);
                        cRef = (beta * cRef) + (alpha * sum);
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
                    ref var cRowRef = ref Add(ref c, i * rowStrideC);

                    for (int j = 0; j < columns; j++)
                    {
                        var sum = TNumber.Zero;

                        if (j > 0)
                        {
                            ref var aRef = ref Add(ref matrix, j * colStrideA);
                            ref var bRef = ref bRowRef;

                            for (int k = 0; k < j; k++)
                            {
                                sum += bRef * aRef;

                                if (k + 1 == j)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bRef = ref Add(ref bRef, colStrideB);
                            }
                        }

                        ref var aDiagRef = ref Add(ref matrix, (j * rowStrideA) + (j * colStrideA));
                        ref var bDiagRef = ref Add(ref bRowRef, j * colStrideB);

                        for (int k = j; k < columns; k++)
                        {
                            sum += bDiagRef * aDiagRef;

                            if (k + 1 == columns)
                            {
                                break;
                            }

                            aDiagRef = ref Add(ref aDiagRef, colStrideA);
                            bDiagRef = ref Add(ref bDiagRef, colStrideB);
                        }

                        ref var cRef = ref Add(ref cRowRef, j * colStrideC);
                        cRef = (beta * cRef) + (alpha * sum);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);
                    ref var cRowRef = ref Add(ref c, i * rowStrideC);

                    for (int j = 0; j < columns; j++)
                    {
                        var sum = TNumber.Zero;

                        ref var aRowRef = ref Add(ref matrix, j * rowStrideA);
                        ref var bRef = ref bRowRef;

                        for (int k = 0; k <= j; k++)
                        {
                            sum += bRef * aRowRef;

                            if (k == j)
                            {
                                break;
                            }

                            aRowRef = ref Add(ref aRowRef, colStrideA);
                            bRef = ref Add(ref bRef, colStrideB);
                        }

                        if (j + 1 < columns)
                        {
                            ref var aRef = ref Add(ref matrix, ((j + 1) * rowStrideA) + (j * colStrideA));
                            ref var bRef2 = ref Add(ref bRowRef, (j + 1) * colStrideB);

                            for (int k = j + 1; k < columns; k++)
                            {
                                sum += bRef2 * aRef;

                                if (k + 1 == columns)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bRef2 = ref Add(ref bRef2, colStrideB);
                            }
                        }

                        ref var cRef = ref Add(ref cRowRef, j * colStrideC);
                        cRef = (beta * cRef) + (alpha * sum);
                    }
                }
            }
        }
    }
}
