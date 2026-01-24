namespace Qtfy.Numerics.LinearAlgebra;

public interface IVector<TElement, TView>
    where TView : IVectorView<TElement>, allows ref struct

{
    ref TElement this[int index] { get; }

    TView AsView();
}
