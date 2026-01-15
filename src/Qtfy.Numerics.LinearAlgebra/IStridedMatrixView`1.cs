namespace Qtfy.Numerics.LinearAlgebra;

public interface IStridedMatrixView<TElement, TRowView, TColumnView, TSelf> :
    IMatrixView<TElement, TRowView, TColumnView, TSelf>
    where TRowView : IVectorView<TElement, TRowView>, allows ref struct
    where TColumnView : IVectorView<TElement, TColumnView>, allows ref struct
    where TSelf : IStridedMatrixView<TElement, TRowView, TColumnView, TSelf>, allows ref struct
{
    int RowStride { get; }

    int ColumnStride { get; }

    ref TElement GetPinnableReference();

    static abstract bool RowStrideIsAlwaysOne();

    static abstract bool ColumnStrideIsAlwaysOne();
}
