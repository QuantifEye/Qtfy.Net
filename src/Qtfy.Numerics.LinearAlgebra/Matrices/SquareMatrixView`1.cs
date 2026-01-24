namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

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

    public int Order => order;

    public int Rows => order;

    public int Columns => order;

    public int RowStride => rowSpan;

    public int ColumnStride => colSpan;

    public static bool IsAlwaysSquare() => true;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public ref TElement GetPinnableReference()
        => ref data;

    public ref TElement this[int row, int column]
        => ref Add(ref data, row * rowSpan + column * colSpan);

    public StrideVectorView<TElement> Row(int row)
    {
        var offset = row * rowSpan;
        return new (ref Add(ref data, offset), colSpan, order);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        var offset = column * colSpan;
        return new (ref Add(ref data, offset), rowSpan, order);
    }
}
