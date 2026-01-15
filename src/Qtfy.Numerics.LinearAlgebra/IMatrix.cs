namespace Qtfy.Numerics.LinearAlgebra;

public interface IMatrix<TElement, TMatrixView, TRowView, TColumnView> : IStorage
    where TMatrixView : IMatrixView<TElement, TRowView, TColumnView, TMatrixView>, allows ref struct
    where TRowView : IVectorView<TElement, TRowView>, allows ref struct
    where TColumnView : IVectorView<TElement, TColumnView>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement GetPinnableReference();

    ref TElement this[int row, int column] { get; }

    TMatrixView AsView();
}
