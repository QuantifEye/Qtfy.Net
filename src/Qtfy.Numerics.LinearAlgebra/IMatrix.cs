namespace Qtfy.Numerics.LinearAlgebra;

public interface IMatrix<TElement, TMatrixView, TRowView, TColumnView>
    where TMatrixView : IMatrixView<TElement, TRowView, TColumnView>, allows ref struct
    where TRowView : IVectorView<TRowView, TElement>, allows ref struct
    where TColumnView : IVectorView<TColumnView, TElement>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement this[int row, int column] { get; }

    TMatrixView AsView();
}
