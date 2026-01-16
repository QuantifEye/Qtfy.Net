namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public interface ISymmetricMatrixView<TElement, TRowView, TColumnView, TUpperLower> :
    IStridedMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower
{
    static abstract bool IsUpper { get; }

    static abstract Uplo Uplo { get; }
}
