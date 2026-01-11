using System.Runtime.InteropServices;

namespace Qtfy;

public static class ArrayExtentions
{
    public static ref T Reference<T>(this T[] array)
    {
        return ref MemoryMarshal.GetArrayDataReference(array);
    }
}
