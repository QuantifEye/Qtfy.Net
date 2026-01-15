namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public interface ITriangularMatrix<TElement, TMatrixView, TRowView, TColumnView, TUpperLower, TDiagonal> :
    IMatrix<TElement, TMatrixView, TRowView, TColumnView>
    where TMatrixView : ITriangularMatrixView<TElement, TRowView, TColumnView, TUpperLower, TDiagonal, TMatrixView>, allows ref struct
    where TRowView : IVectorView<TElement, TRowView>, allows ref struct
    where TColumnView : IVectorView<TElement, TColumnView>, allows ref struct
    where TUpperLower : IUpperLower
    where TDiagonal : IDiagonal
{
    static abstract bool IsUpper { get; }

    static abstract bool IsUnitDiagonal { get; }

    static abstract Uplo Uplo { get; }

    static abstract Diag Diag { get; }
}
