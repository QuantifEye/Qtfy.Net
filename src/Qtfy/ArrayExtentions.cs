using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Qtfy;

public static class ArrayExtentions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Reference<T>(this T[] array)
    {
        return ref MemoryMarshal.GetArrayDataReference(array);
    }
}
