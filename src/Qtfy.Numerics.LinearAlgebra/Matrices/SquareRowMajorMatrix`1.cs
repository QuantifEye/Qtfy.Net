namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public sealed class SquareRowMajorMatrix<TElement> :
    IMatrix<TElement, SquareRowMajorMatrixView<TElement>, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;

    private readonly int order;

    public SquareRowMajorMatrix(int order)
    {
        array = new TElement[order * order];
        this.order = order;
    }

    public int Order => order;

    public int Rows => order;

    public int Columns => order;

    public ref TElement GetPinnableReference()
        => ref array.Reference();

    public ref TElement this[int row, int column]
        => ref array[row * order + column];

    public SquareRowMajorMatrixView<TElement> AsView()
        => new (ref array.Reference(), order);
}
