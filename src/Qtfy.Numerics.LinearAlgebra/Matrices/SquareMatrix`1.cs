namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public sealed class SquareMatrix<TElement> :
    IMatrix<TElement, SquareMatrixView<TElement>, StrideVectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly TElement[] array;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public SquareMatrix(int order, bool isRowMajor = true)
    {
        this.order = order;

        if (isRowMajor)
        {
            rowSpan = order;
            colSpan = 1;
        }
        else
        {
            rowSpan = 1;
            colSpan = order;
        }

        array = new TElement[order * order];
    }

    public int Order => order;

    public int Rows => order;

    public int Columns => order;

    public ref TElement GetPinnableReference()
        => ref array.Reference();

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * rowSpan + column * colSpan;
            return ref array[offset];
        }
    }

    public SquareMatrixView<TElement> AsView()
        => new (ref array.Reference(), order, rowSpan, colSpan);
}
