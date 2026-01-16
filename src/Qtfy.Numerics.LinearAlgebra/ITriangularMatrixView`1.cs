namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public interface ITriangularMatrixView<TElement, TRowView, TColumnView, TUpperLower, TDiagonal> :
    IStridedMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower
    where TDiagonal : IDiagonal
{
    static abstract bool IsUpper { get; }

    static abstract bool IsUnitDiagonal { get; }

    static abstract Uplo Uplo { get; }

    static abstract Diag Diag { get; }
}
