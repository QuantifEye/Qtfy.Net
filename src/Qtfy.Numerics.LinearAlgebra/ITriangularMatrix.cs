namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;

public interface ITriangularMatrix<TElement, TMatrixView, TRowView, TColumnView, TUpperLower, TDiagonal> :
    IMatrix<TElement, TMatrixView, TRowView, TColumnView>
    where TMatrixView : ITriangularMatrixView<TElement, TRowView, TColumnView, TUpperLower, TDiagonal>, allows ref struct
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    static abstract bool IsUpper();

    static abstract bool IsUnitDiagonal();
}
