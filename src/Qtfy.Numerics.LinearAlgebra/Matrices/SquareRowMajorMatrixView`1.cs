namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public readonly ref struct SquareRowMajorMatrixView<TElement> :
    IStridedMatrixView<TElement, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly ref TElement reference;
    private readonly int order;

    public SquareRowMajorMatrixView(ref TElement reference, int order)
    {
        this.reference = ref reference;
        this.order = order;
    }

    public int Order => order;

    public int Rows => order;

    public int Columns => order;

    public int RowStride => order;

    public int ColumnStride => 1;

    public static bool IsAlwaysSquare() => true;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => true;

    public ref TElement GetPinnableReference()
        => ref reference;

    public VectorView<TElement> Row(int row)
        => new (ref Add(ref reference, row * order), order);

    public StrideVectorView<TElement> Column(int column)
        => new (ref Add(ref reference, column), order, order);

    public ref TElement this[int row, int column]
        => ref Add(ref reference, row * order + column);
}
