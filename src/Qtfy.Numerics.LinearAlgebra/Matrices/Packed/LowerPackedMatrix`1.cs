namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public sealed class LowerPackedMatrix<TElement> :
    IMatrix<TElement, LowerPackedMatrixView<TElement>, LowerPackedRowView<TElement>, LowerPackedColumnView<TElement>>
{
    private readonly TElement[] data;
    private readonly int order;

    public LowerPackedMatrix(int order)
    {
        this.order = order;
        this.data = new TElement[(order * (order + 1)) / 2];
    }

    public int Rows => this.order;

    public int Columns => this.order;

    public ref TElement GetPinnableReference()
        => ref this.data.Reference();

    public ref TElement this[int row, int column]
    {
        get
        {
            Debug.Assert(row >= column, "Only the lower triangle is stored.");
            var index = GetIndex(row, column);
            return ref this.data[index];
        }
    }

    public LowerPackedMatrixView<TElement> AsView()
        => new (ref this.data.Reference(), this.order);

    public static bool IsPinned() => false;

    internal static int GetIndex(int row, int column)
        => (row * (row + 1) / 2) + column;
}
