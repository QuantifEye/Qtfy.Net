namespace Qtfy;

public static class ArrayExtentions
{
    public static ref T GetReferenceToFirstElement<T>(this T[] array)
    {
        return ref array[0];
    }
}
