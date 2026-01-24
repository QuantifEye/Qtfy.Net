namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;

public interface ISymmetricMatrix<TElement, TMatrixView, TRowView, TColumnView, TUpperLower> :
    IMatrix<TElement, TMatrixView, TRowView, TColumnView>
    where TMatrixView : ISymmetricMatrixView<TElement, TRowView, TColumnView, TUpperLower>, allows ref struct
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower, allows ref struct
{
    static abstract bool IsUpper();
}
