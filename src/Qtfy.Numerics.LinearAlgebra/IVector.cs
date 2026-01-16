namespace Qtfy.Numerics.LinearAlgebra;

public interface IVector<TElement, TView>
{
    ref TElement this[int index] { get; }

    TView AsView();
}
