namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class SquareMatrix<TElement> :
    IMatrix<TElement, MatrixView<TElement>, StrideVectorView<TElement>, StrideVectorView<TElement>>
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
            this.rowSpan = order;
            this.colSpan = 1;
        }
        else
        {
            this.rowSpan = 1;
            this.colSpan = order;
        }

        this.array = new TElement[order * order];
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public ref TElement GetPinnableReference()
        => ref this.array.Reference();

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * this.rowSpan + column * this.colSpan;
            return ref this.array[offset];
        }
    }

    public MatrixView<TElement> AsView()
        => new (ref this.array.Reference(), this.order, this.order, this.rowSpan, this.colSpan);

    public static bool IsPinned() => false;
}
