namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class SquareRowMajorMatrix<TElement> :
    IMatrix<TElement, SquareRowMajorMatrixView<TElement>, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;

    private readonly int order;

    public SquareRowMajorMatrix(int order)
    {
        this.array = new TElement[order * order];
        this.order = order;
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public ref TElement GetPinnableReference()
        => ref this.array.Reference();

    public ref TElement this[int row, int column]
        => ref this.array[row * this.order + column];

    public SquareRowMajorMatrixView<TElement> AsView()
        => new (ref this.array.Reference(), this.order);
}
