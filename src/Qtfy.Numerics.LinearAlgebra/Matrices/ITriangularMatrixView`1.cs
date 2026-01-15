namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public interface ITriangularMatrixView<TElement, TRowView, TColumnView, TUpperLower, TDiagonal, TSelf> :
    IStridedMatrixView<TElement, TRowView, TColumnView, TSelf>
    where TRowView : IVectorView<TElement, TRowView>, allows ref struct
    where TColumnView : IVectorView<TElement, TColumnView>, allows ref struct
    where TUpperLower : IUpperLower
    where TDiagonal : IDiagonal
    where TSelf : ITriangularMatrixView<TElement, TRowView, TColumnView, TUpperLower, TDiagonal, TSelf>, allows ref struct
{
    static abstract bool IsUpper { get; }

    static abstract bool IsUnitDiagonal { get; }

    static abstract Uplo Uplo { get; }

    static abstract Diag Diag { get; }
}
