namespace Qtfy.Numerics.LinearAlgebra;

public interface IPackedVectorView<TElement>
{
    int Length { get; }

    ref TElement this[int index] { get; }
}
