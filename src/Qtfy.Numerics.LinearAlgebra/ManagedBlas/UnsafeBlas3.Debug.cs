namespace Qtfy.Numerics.LinearAlgebra.BLAS;

public partial struct UnsafeBlas3
{
    [Conditional("DEBUG")]
    private static void DebugAssertNotZero<T>(params T[] values)
        where T : INumber<T>
    {
        var zero = T.AdditiveIdentity;
        for (int i = 0; i < values.Length; i++)
        {
            Debug.Assert(values[i] > zero, "Value must be greater than zero.");
        }
    }
}
