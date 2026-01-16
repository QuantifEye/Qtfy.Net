namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class SquareColumnMajorMatrix<TElement> :
    IMatrix<TElement, SquareColumnMajorMatrixView<TElement>, StrideVectorView<TElement>, VectorView<TElement>>
{
    private readonly TElement[] memory;
    private readonly int order;

    public SquareColumnMajorMatrix(int order)
    {
        this.order = order;
        this.memory = new TElement[order * order];
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public ref TElement GetPinnableReference()
        => ref this.memory.Reference();

    public ref TElement this[int row, int column]
        => ref this.memory[row + column * this.order];

    public SquareColumnMajorMatrixView<TElement> AsView()
        => new (ref this.memory.Reference(), this.order);
}
