namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void SymmetricRank2KUpdate<TNumber>(
        Uplo uplo,
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
        DebugAssertNotZero(n, k);

        if (uplo == Uplo.Upper)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    var sum = TNumber.Zero;
                    ref var aRowI = ref Add(ref a, i * rowStrideA);
                    ref var aRowJ = ref Add(ref a, j * rowStrideA);
                    ref var bRowI = ref Add(ref b, i * rowStrideB);
                    ref var bRowJ = ref Add(ref b, j * rowStrideB);
                    ref var aRefI = ref aRowI;
                    ref var aRefJ = ref aRowJ;
                    ref var bRefI = ref bRowI;
                    ref var bRefJ = ref bRowJ;

                    for (int p = 0; p < k; p++)
                    {
                        sum += (aRefI * bRefJ) + (bRefI * aRefJ);

                        if (p + 1 == k)
                        {
                            break;
                        }

                        aRefI = ref Add(ref aRefI, colStrideA);
                        aRefJ = ref Add(ref aRefJ, colStrideA);
                        bRefI = ref Add(ref bRefI, colStrideB);
                        bRefJ = ref Add(ref bRefJ, colStrideB);
                    }

                    ref var cRef = ref Add(ref c, (i * rowStrideC) + (j * colStrideC));
                    cRef = (beta * cRef) + (alpha * sum);
                }
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    var sum = TNumber.Zero;
                    ref var aRowI = ref Add(ref a, i * rowStrideA);
                    ref var aRowJ = ref Add(ref a, j * rowStrideA);
                    ref var bRowI = ref Add(ref b, i * rowStrideB);
                    ref var bRowJ = ref Add(ref b, j * rowStrideB);
                    ref var aRefI = ref aRowI;
                    ref var aRefJ = ref aRowJ;
                    ref var bRefI = ref bRowI;
                    ref var bRefJ = ref bRowJ;

                    for (int p = 0; p < k; p++)
                    {
                        sum += (aRefI * bRefJ) + (bRefI * aRefJ);

                        if (p + 1 == k)
                        {
                            break;
                        }

                        aRefI = ref Add(ref aRefI, colStrideA);
                        aRefJ = ref Add(ref aRefJ, colStrideA);
                        bRefI = ref Add(ref bRefI, colStrideB);
                        bRefJ = ref Add(ref bRefJ, colStrideB);
                    }

                    ref var cRef = ref Add(ref c, (i * rowStrideC) + (j * colStrideC));
                    cRef = (beta * cRef) + (alpha * sum);
                }
            }
        }
    }
}
