namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public readonly ref struct ColumnMajorTriangularMatrixView<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrixView<
        TElement,
        StrideVectorView<TElement>,
        VectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    private readonly ref TElement data;
    private readonly int order;

    public ColumnMajorTriangularMatrixView(ref TElement data, int order)
    {
        this.data = ref data;
        this.order = order;
    }

    public int Rows => order;

    public int Columns => order;

    public static bool IsAlwaysSquare() => true;

    public int RowStride => 1;

    public int ColumnStride => order;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public static bool IsUnitDiagonal() => TDiagonal.IsUnitDiagonal();

    public ref TElement GetPinnableReference()
        => ref data;

    public ref TElement this[int row, int column]
        => ref Add(ref data, row + (column * order));

    public StrideVectorView<TElement> Row(int row)
        => new (ref Add(ref data, row), order, order);

    public VectorView<TElement> Column(int column)
        => new (ref Add(ref data, column * order), order);

    public static bool RowStrideIsAlwaysOne() => true;

    public static bool ColumnStrideIsAlwaysOne() => false;
}
