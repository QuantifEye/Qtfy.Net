namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TSelf, TElement>
    where TSelf : IVectorView<TSelf, TElement>, allows ref struct
{
    int Length { get; }

    ref TElement this[int index] { get; }
}

