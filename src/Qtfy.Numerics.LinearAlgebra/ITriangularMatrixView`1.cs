namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;

public interface ITriangularMatrixView<TElement, TRowView, TColumnView, TUpperLower, TDiagonal> :
    IStridedMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    static abstract bool IsUpper();

    static abstract bool IsUnitDiagonal();
}
