namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Vectors;

public readonly ref struct SquareColumnMajorMatrixView<TElement> :
    IStridedMatrixView<TElement, StrideVectorView<TElement>, VectorView<TElement>>
{
    private readonly ref TElement reference;
    private readonly int order;

    public SquareColumnMajorMatrixView(ref TElement reference, int order)
    {
        this.reference = ref reference;
        this.order = order;
    }

    public int Order => order;

    public int Rows => order;

    public int Columns => order;

    public int RowStride => 1;

    public int ColumnStride => order;

    public static bool IsAlwaysSquare() => true;

    public static bool RowStrideIsAlwaysOne() => true;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public ref TElement GetPinnableReference()
        => ref reference;

    public StrideVectorView<TElement> Row(int row)
        => new (ref Add(ref reference, row), order, order);

    public VectorView<TElement> Column(int column)
        => new (ref Add(ref reference, column * order), order);

    public ref TElement this[int row, int column]
        => ref Add(ref reference, row + column * order);
}
