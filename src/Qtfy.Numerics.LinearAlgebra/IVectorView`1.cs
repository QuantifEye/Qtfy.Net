namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TElement> :
    IStorage
{
    int Length { get; }

    int Stride { get; }

    ref TElement GetPinnableReference();

    ref TElement this[int index] { get; }

    static abstract bool StrideIsAlwaysOne();
}
