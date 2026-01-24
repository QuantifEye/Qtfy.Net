namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public readonly ref struct RowMajorSymmetricMatrixView<TElement, TUpperLower> :
    ISymmetricMatrixView<TElement, VectorView<TElement>, StrideVectorView<TElement>, TUpperLower>
    where TUpperLower : IUpperLower, allows ref struct
{
    private readonly ref TElement data;
    private readonly int order;

    public RowMajorSymmetricMatrixView(ref TElement data, int order)
    {
        this.data = ref data;
        this.order = order;
    }

    public int Rows => order;

    public int Columns => order;

    public static bool IsAlwaysSquare() => true;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public int RowStride => order;

    public int ColumnStride => 1;

    public ref TElement GetPinnableReference()
        => ref data;

    public ref TElement this[int row, int column]
        => ref Add(ref data, row * order + column);

    public VectorView<TElement> Row(int row)
        => new (ref Add(ref data, row * order), order);

    public StrideVectorView<TElement> Column(int column)
        => new (ref Add(ref data, column), order, order);

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => true;
}
