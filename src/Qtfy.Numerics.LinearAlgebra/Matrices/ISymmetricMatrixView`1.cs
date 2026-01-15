namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public interface ISymmetricMatrixView<TElement, TRowView, TColumnView, TUpperLower, TSelf> :
    IStridedMatrixView<TElement, TRowView, TColumnView, TSelf>
    where TRowView : IVectorView<TElement, TRowView>, allows ref struct
    where TColumnView : IVectorView<TElement, TColumnView>, allows ref struct
    where TUpperLower : IUpperLower
    where TSelf : ISymmetricMatrixView<TElement, TRowView, TColumnView, TUpperLower, TSelf>, allows ref struct
{
    static abstract bool IsUpper { get; }

    static abstract Uplo Uplo { get; }
}
