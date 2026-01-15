namespace Qtfy.Numerics.LinearAlgebra;

public interface IStridedMatrixView<TElement, TRowView, TColumnView> :
    IMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
{
    int RowStride { get; }

    int ColumnStride { get; }

    ref TElement GetPinnableReference();

    static abstract bool RowStrideIsAlwaysOne();

    static abstract bool ColumnStrideIsAlwaysOne();
}
