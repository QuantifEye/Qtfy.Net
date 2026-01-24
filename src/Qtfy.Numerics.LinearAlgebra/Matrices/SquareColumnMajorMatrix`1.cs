namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public sealed class SquareColumnMajorMatrix<TElement> :
    IMatrix<TElement, SquareColumnMajorMatrixView<TElement>, StrideVectorView<TElement>, VectorView<TElement>>
{
    private readonly TElement[] memory;
    private readonly int order;

    public SquareColumnMajorMatrix(int order)
    {
        this.order = order;
        memory = new TElement[order * order];
    }

    public int Order => order;

    public int Rows => order;

    public int Columns => order;

    public ref TElement GetPinnableReference()
        => ref memory.Reference();

    public ref TElement this[int row, int column]
        => ref memory[row + column * order];

    public SquareColumnMajorMatrixView<TElement> AsView()
        => new (ref memory.Reference(), order);
}
