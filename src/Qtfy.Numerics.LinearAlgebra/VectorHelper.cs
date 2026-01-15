namespace Qtfy.Numerics.LinearAlgebra;

public static class VectorHelper
{
    public static int WidestVectorBitWidth()
    {
        if (Vector512.IsHardwareAccelerated)
        {
            return 512;
        }

        if (Vector256.IsHardwareAccelerated)
        {
            return 256;
        }

        if (Vector128.IsHardwareAccelerated)
        {
            return 128;
        }

        return 0;
    }
}
