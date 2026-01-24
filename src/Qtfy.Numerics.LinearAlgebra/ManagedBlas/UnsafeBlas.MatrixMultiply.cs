namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
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
        where TNumber : INumber<TNumber>
    {
        if (m <= 0 || n <= 0 || k <= 0)
        {
            return;
        }

        ref var aRowRef = ref a;
        ref var cRowRef = ref c;
        var rowsRemaining = m;

        while (true)
        {
            ref var bColRef = ref b;
            ref var cColRef = ref cRowRef;
            var colsRemaining = n;

            while (true)
            {
                var sum = TNumber.Zero;
                ref var aRef = ref aRowRef;
                ref var bRef = ref bColRef;

                var innerRemaining = k;
                while (--innerRemaining != 0)
                {
                    sum += aRef * bRef;
                    aRef = ref Add(ref aRef, colStrideA);
                    bRef = ref Add(ref bRef, rowStrideB);
                }

                sum += aRef * bRef;

                cColRef = (beta * cColRef) + (alpha * sum);

                if (--colsRemaining == 0) break;
                bColRef = ref Add(ref bColRef, colStrideB);
                cColRef = ref Add(ref cColRef, colStrideC);
            }

            if (--rowsRemaining == 0) return;

            aRowRef = ref Add(ref aRowRef, rowStrideA);
            cRowRef = ref Add(ref cRowRef, rowStrideC);
        }
    }
}
