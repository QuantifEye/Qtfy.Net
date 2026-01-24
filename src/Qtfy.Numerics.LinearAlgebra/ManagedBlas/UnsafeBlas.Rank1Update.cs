namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void Rank1Update<TNumber>(
        int rows,
        int columns,
        TNumber alpha,
        ref TNumber x,
        int strideX,
        ref TNumber y,
        int strideY,
        ref TNumber matrix,
        int rowStride,
        int colStride)
        where TNumber : INumberBase<TNumber>
    {
        DebugAssertNotZero(rows, columns);

        ref var xRef = ref x;
        for (int i = 0; i < rows; i++)
        {
            var xVal = xRef;
            ref var aRef = ref Add(ref matrix, i * rowStride);
            ref var yRef = ref y;

            for (int j = 0; j < columns; j++)
            {
                aRef += alpha * xVal * yRef;

                if (j + 1 == columns)
                {
                    break;
                }

                aRef = ref Add(ref aRef, colStride);
                yRef = ref Add(ref yRef, strideY);
            }

            if (i + 1 == rows)
            {
                break;
            }

            xRef = ref Add(ref xRef, strideX);
        }
    }
}
