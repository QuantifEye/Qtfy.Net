namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct SquareMatrixView<TElement> :
    IStridedMatrixView<TElement, StrideVectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public SquareMatrixView(ref TElement data, int order, int rowSpan, int colSpan)
    {
        this.data = ref data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public int RowStride => this.rowSpan;

    public int ColumnStride => this.colSpan;

    public static bool IsAlwaysSquare() => true;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public ref TElement GetPinnableReference()
        => ref this.data;

    public ref TElement this[int row, int column]
        => ref Add(ref this.data, row * this.rowSpan + column * this.colSpan);

    public StrideVectorView<TElement> Row(int row)
    {
        var offset = row * this.rowSpan;
        return new (ref Add(ref this.data, offset), this.colSpan, this.order);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        var offset = column * this.colSpan;
        return new (ref Add(ref this.data, offset), this.rowSpan, this.order);
    }
}
