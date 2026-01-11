namespace Qtfy.Numerics.LinearAlgebra;

public interface IMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TRowView, TElement>, allows ref struct
    where TColumnView : IVectorView<TColumnView, TElement>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement this[int row, int column] { get; }

    TRowView Row(int row);

    TColumnView Column(int column);
}
