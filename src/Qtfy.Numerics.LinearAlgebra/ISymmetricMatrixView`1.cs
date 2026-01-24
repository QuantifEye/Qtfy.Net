namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;

public interface ISymmetricMatrixView<TElement, TRowView, TColumnView, TUpperLower> :
    IStridedMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower, allows ref struct
{
    static abstract bool IsUpper();
}
