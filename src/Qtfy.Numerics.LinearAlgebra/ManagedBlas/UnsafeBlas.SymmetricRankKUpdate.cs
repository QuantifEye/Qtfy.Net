using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void SymmetricRankKUpdate<TNumber, TUpperLower>(
        int n,
        int k,
        TNumber alpha,
        ref TNumber a,
        int rowStrideA,
        int colStrideA,
        TNumber beta,
        ref TNumber c,
        int rowStrideC,
        int colStrideC)
        where TNumber : INumber<TNumber>
        where TUpperLower : IUpperLower
    {
        DebugAssertNotZero(n, k);

        if (TUpperLower.IsUpper())
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    var sum = TNumber.Zero;
                    ref var aRowI = ref Add(ref a, i * rowStrideA);
                    ref var aRowJ = ref Add(ref a, j * rowStrideA);
                    ref var aRefI = ref aRowI;
                    ref var aRefJ = ref aRowJ;

                    for (int p = 0; p < k; p++)
                    {
                        sum += aRefI * aRefJ;

                        if (p + 1 == k)
                        {
                            break;
                        }

                        aRefI = ref Add(ref aRefI, colStrideA);
                        aRefJ = ref Add(ref aRefJ, colStrideA);
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
                    ref var aRefI = ref aRowI;
                    ref var aRefJ = ref aRowJ;

                    for (int p = 0; p < k; p++)
                    {
                        sum += aRefI * aRefJ;

                        if (p + 1 == k)
                        {
                            break;
                        }

                        aRefI = ref Add(ref aRefI, colStrideA);
                        aRefJ = ref Add(ref aRefJ, colStrideA);
                    }

                    ref var cRef = ref Add(ref c, (i * rowStrideC) + (j * colStrideC));
                    cRef = (beta * cRef) + (alpha * sum);
                }
            }
        }
    }
}
