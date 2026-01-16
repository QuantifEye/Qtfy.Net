namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public interface ISymmetricMatrix<TElement, TMatrixView, TRowView, TColumnView, TUpperLower> :
    IMatrix<TElement, TMatrixView, TRowView, TColumnView>
    where TMatrixView : ISymmetricMatrixView<TElement, TRowView, TColumnView, TUpperLower>, allows ref struct
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower
{
    static abstract bool IsUpper { get; }

    static abstract Uplo Uplo { get; }
}
