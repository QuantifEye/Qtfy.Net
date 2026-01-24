using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void TriangularMatrixMultiply<TNumber, TSide, TUpperLower, TDiagonal>(
        int rows,
        int columns,
        TNumber alpha,
        ref TNumber matrix,
        int rowStrideA,
        int colStrideA,
        ref TNumber b,
        int rowStrideB,
        int colStrideB)
        where TNumber : INumber<TNumber>
        where TSide : ISide
        where TUpperLower : IUpperLower
        where TDiagonal : IDiagonal
    {
        DebugAssertNotZero(rows, columns);

        var isUnit = TDiagonal.IsUnitDiagonal();

        if (TSide.IsLeft())
        {
            if (TUpperLower.IsUpper())
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);

                    for (int j = 0; j < columns; j++)
                    {
                        ref var bRef = ref Add(ref bRowRef, j * colStrideB);
                        var sum = bRef;

                        if (!isUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (i * rowStrideA) + (i * colStrideA));
                            sum *= aDiag;
                        }

                        if (i + 1 < rows)
                        {
                            ref var aRef = ref Add(ref matrix, (i * rowStrideA) + ((i + 1) * colStrideA));
                            ref var bKRef = ref Add(ref b, ((i + 1) * rowStrideB) + (j * colStrideB));

                            for (int k = i + 1; k < rows; k++)
                            {
                                sum += aRef * bKRef;

                                if (k + 1 == rows)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, colStrideA);
                                bKRef = ref Add(ref bKRef, rowStrideB);
                            }
                        }

                        bRef = alpha * sum;
                    }
                }
            }
            else
            {
                for (int i = rows - 1; i >= 0; i--)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);

                    for (int j = 0; j < columns; j++)
                    {
                        ref var bRef = ref Add(ref bRowRef, j * colStrideB);
                        var sum = bRef;

                        if (!isUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (i * rowStrideA) + (i * colStrideA));
                            sum *= aDiag;
                        }

                        if (i > 0)
                        {
                            ref var aRef = ref Add(ref matrix, i * rowStrideA);
                            ref var bKRef = ref Add(ref b, j * colStrideB);

                            for (int k = 0; k < i; k++)
                            {
                                sum += aRef * bKRef;

                                if (k + 1 == i)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, colStrideA);
                                bKRef = ref Add(ref bKRef, rowStrideB);
                            }
                        }

                        bRef = alpha * sum;
                    }
                }
            }
        }
        else
        {
            if (TUpperLower.IsUpper())
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);

                    for (int j = columns - 1; j >= 0; j--)
                    {
                        ref var bRef = ref Add(ref bRowRef, j * colStrideB);
                        var sum = bRef;

                        if (!isUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (j * rowStrideA) + (j * colStrideA));
                            sum *= aDiag;
                        }

                        if (j > 0)
                        {
                            ref var aRef = ref Add(ref matrix, j * colStrideA);
                            ref var bKRef = ref bRowRef;

                            for (int k = 0; k < j; k++)
                            {
                                sum += bKRef * aRef;

                                if (k + 1 == j)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bKRef = ref Add(ref bKRef, colStrideB);
                            }
                        }

                        bRef = alpha * sum;
                    }
                }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    ref var bRowRef = ref Add(ref b, i * rowStrideB);

                    for (int j = 0; j < columns; j++)
                    {
                        ref var bRef = ref Add(ref bRowRef, j * colStrideB);
                        var sum = bRef;

                        if (!isUnit)
                        {
                            ref var aDiag = ref Add(ref matrix, (j * rowStrideA) + (j * colStrideA));
                            sum *= aDiag;
                        }

                        if (j + 1 < columns)
                        {
                            ref var aRef = ref Add(ref matrix, ((j + 1) * rowStrideA) + (j * colStrideA));
                            ref var bKRef = ref Add(ref bRowRef, (j + 1) * colStrideB);

                            for (int k = j + 1; k < columns; k++)
                            {
                                sum += bKRef * aRef;

                                if (k + 1 == columns)
                                {
                                    break;
                                }

                                aRef = ref Add(ref aRef, rowStrideA);
                                bKRef = ref Add(ref bKRef, colStrideB);
                            }
                        }

                        bRef = alpha * sum;
                    }
                }
            }
        }
    }
}
