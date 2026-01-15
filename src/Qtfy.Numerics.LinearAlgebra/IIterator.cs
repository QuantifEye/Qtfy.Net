namespace Qtfy.Numerics.LinearAlgebra;

public unsafe interface IIterator<T>
    where T : allows ref struct
{
    ref T Current { get; }

    ref T GetNext();
}
