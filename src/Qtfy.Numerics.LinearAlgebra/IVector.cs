namespace Qtfy.Numerics.LinearAlgebra;

public interface IVector<TElement, TView> : IStorage
{
    ref TElement this[int row, int column] { get; }

    TView AsView();
}
