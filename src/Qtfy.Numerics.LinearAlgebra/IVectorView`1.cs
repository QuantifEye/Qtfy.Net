namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TElement>
{
    int Length { get; }

    ref TElement this[int index] { get; }
}

