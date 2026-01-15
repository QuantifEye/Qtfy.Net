namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas2
{
    public static void MatrixVectorMultiply<TNumber>(
        int rows,
        int columns,
        TNumber alpha,
        ref TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX,
        TNumber beta,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(rows, columns);

        ref var yRef = ref y;
        for (int row = 0; row < rows; row++)
        {
            ref var aRef = ref Add(ref matrix, row * rowStride);
            ref var xRef = ref x;
            var sum = TNumber.Zero;

            for (int col = 0; col < columns; col++)
            {
                sum += aRef * xRef;

                if (col + 1 == columns)
                {
                    break;
                }

                aRef = ref Add(ref aRef, colStride);
                xRef = ref Add(ref xRef, strideX);
            }

            yRef = (beta * yRef) + (alpha * sum);

            if (row + 1 == rows)
            {
                break;
            }

            yRef = ref Add(ref yRef, strideY);
        }
    }
}
