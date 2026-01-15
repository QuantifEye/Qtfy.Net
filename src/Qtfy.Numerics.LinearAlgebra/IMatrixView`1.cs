namespace Qtfy.Numerics.LinearAlgebra;

public interface IMatrixView<TElement, TRowView, TColumnView, TSelf> :
    IStorage
    where TRowView : IVectorView<TElement, TRowView>, allows ref struct
    where TColumnView : IVectorView<TElement, TColumnView>, allows ref struct
    where TSelf : IMatrixView<TElement, TRowView, TColumnView, TSelf>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement this[int row, int column] { get; }

    TRowView Row(int row);

    TColumnView Column(int column);
}
