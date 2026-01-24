namespace Qtfy.Numerics.LinearAlgebra;

public struct DebugAssertions
{
    [Conditional("DEBUG")]
    public static void GreaterThenZero<TNumber>(params ReadOnlySpan<TNumber> args)
        where TNumber : INumberBase<TNumber>
    {
        foreach (var n in args)
        {
            Debug.Assert(!TNumber.IsZero(n), "Value must not be zero");
            Debug.Assert(TNumber.IsPositive(n), "Value must be greater than zero");
        }
    }

    [Conditional("DEBUG")]
    public static void IsNotNull<T1>(in T1 ref1)
    {
        AssertNotNull(ref1);
    }

    [Conditional("DEBUG")]
    public static void IsNotNull<T1, T2>(in T1 ref1, in T2 ref2)
    {
        AssertNotNull(ref1);
        AssertNotNull(ref2);
    }

    [Conditional("DEBUG")]
    public static void IsNotNull<T1, T2, T3>(in T1 ref1, in T2 ref2, in T3 ref3)
    {
        AssertNotNull(ref1);
        AssertNotNull(ref2);
        AssertNotNull(ref3);
    }

    [Conditional("DEBUG")]
    private static void AssertNotNull<T>(in T reference)
    {
        Debug.Assert(!IsNullRef(in reference), "Reference may not be null");
    }
}
