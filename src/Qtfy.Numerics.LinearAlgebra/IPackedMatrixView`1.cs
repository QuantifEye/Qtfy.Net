namespace Qtfy.Numerics.LinearAlgebra;

public interface IPackedMatrixView<TElement, TRowView, TColumnView>
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IPackedVectorView<TElement>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement this[int row, int column] { get; }

    TRowView Row(int row);

    TColumnView Column(int column);
}
