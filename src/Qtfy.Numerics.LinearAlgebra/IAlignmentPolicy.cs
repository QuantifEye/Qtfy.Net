namespace LinAlg;

public interface IAlignmentPolicy<TAlignment>
    where TAlignment : struct, IAlignmentPolicy<TAlignment>
{
    public static abstract int IndexAlignment();

    public static virtual int ByteAlignment()
    {
        return TAlignment.IndexAlignment() * sizeof(double);
    }

    public static virtual int AlignUpIndex(int majorLength)
    {
        return (int)AlignmentHelper.AlignUp((nuint)majorLength, (nuint)TAlignment.IndexAlignment());
    }
}

public readonly struct ElementAlignment : IAlignmentPolicy<VectorAlignment>
{
    public static int IndexAlignment()
    {
        return 1;
    }
}

public readonly struct VectorAlignment : IAlignmentPolicy<VectorAlignment>
{
    public static int IndexAlignment()
    {
        if (System.Numerics.Vector.IsHardwareAccelerated && System.Numerics.Vector<double>.IsSupported)
        {
            return System.Numerics.Vector<double>.Count;
        }

        return 1;
    }
}

public readonly struct SharingAlignment : IAlignmentPolicy<VectorAlignment>
{
    public static int IndexAlignment()
    {
        return 64 / sizeof(double);
    }
}