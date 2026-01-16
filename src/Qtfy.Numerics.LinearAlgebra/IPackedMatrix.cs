namespace Qtfy.Numerics.LinearAlgebra;

public interface IPackedMatrix<TElement, TMatrixView, TRowView, TColumnView>
    where TMatrixView : IPackedMatrixView<TElement, TRowView, TColumnView>, allows ref struct
    where TRowView : IVectorView<TElement>, allows ref struct
    where TColumnView : IPackedVectorView<TElement>, allows ref struct
{
    int Rows { get; }

    int Columns { get; }

    ref TElement GetPinnableReference();

    ref TElement this[int row, int column] { get; }

    TMatrixView AsView();
}
