namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class SquareRowMajorMatrix<TElement> :
    IMatrix<TElement, RowMajorMatrixView<TElement>, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;
    private readonly int order;

    public SquareRowMajorMatrix(int order)
    {
        this.order = order;
        this.array = new TElement[order * order];
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public ref TElement GetPinnableReference()
        => ref this.array.Reference();

    public ref TElement this[int row, int column]
        => ref this.array[row * this.order + column];

    public RowMajorMatrixView<TElement> AsView()
        => new (ref this.array.Reference(), this.order, this.order);

    public static bool IsPinned() => false;
}
