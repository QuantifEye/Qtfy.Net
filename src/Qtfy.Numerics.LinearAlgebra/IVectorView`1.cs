namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TElement>
    where TElement : unmanaged
{
    int Length { get; }

    ref TElement this[int index] { get; }
}

