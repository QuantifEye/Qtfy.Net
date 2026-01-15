namespace Qtfy.Numerics.LinearAlgebra;

public interface IMatrixView<TElement, TRowView, TColumnView> :
    IStorage
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IVectorView<TElement>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement this[int row, int column] { get; }

    TRowView Row(int row);

    TColumnView Column(int column);


}
