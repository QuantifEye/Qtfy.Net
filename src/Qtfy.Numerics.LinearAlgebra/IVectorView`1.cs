namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TElement, TSelf> :
    IStorage
    where TSelf : IVectorView<TElement, TSelf>, allows ref struct
{
    int Length { get; }

    int Stride { get; }

    ref TElement GetPinnableReference();

    ref TElement this[int index] { get; }

    static abstract bool StrideIsAlwaysOne();
}
