namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas3
{
    public static void MatrixMultiply<TNumber>(
        int m,
        int n,
        int k,
        TNumber alpha,
        ref TNumber a,
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
        DebugAssertNotZero(m, n, k);

        for (int row = 0; row < m; row++)
        {
            ref var aRowRef = ref Add(ref a, row * rowStrideA);
            ref var cRowRef = ref Add(ref c, row * rowStrideC);

            for (int col = 0; col < n; col++)
            {
                var sum = TNumber.Zero;
                ref var aRef = ref aRowRef;
                ref var bRef = ref Add(ref b, col * colStrideB);

                for (int inner = 0; inner < k; inner++)
                {
                    sum += aRef * bRef;

                    if (inner + 1 == k)
                    {
                        break;
                    }

                    aRef = ref Add(ref aRef, colStrideA);
                    bRef = ref Add(ref bRef, rowStrideB);
                }

                ref var cRef = ref Add(ref cRowRef, col * colStrideC);
                cRef = (beta * cRef) + (alpha * sum);
            }
        }
    }
}
