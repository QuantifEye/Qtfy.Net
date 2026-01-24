namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public readonly ref struct TriangularMatrixView<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrixView<
        TElement,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public TriangularMatrixView(ref TElement data, int order, int rowSpan, int colSpan)
    {
        this.data = ref data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => order;

    public int Columns => order;

    public int RowStride => rowSpan;

    public int ColumnStride => colSpan;


    public ref TElement GetPinnableReference()
        => ref data;

    public ref TElement this[int row, int column]
    {
        get => ref Add(ref data, row * rowSpan + column * colSpan);
    }

    public StrideVectorView<TElement> Row(int row)
    {
        return new(ref Add(ref data, row * rowSpan), colSpan, order);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        return new(ref Add(ref data, column * colSpan), rowSpan, order);
    }

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public static bool IsAlwaysSquare() => true;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public static bool IsUnitDiagonal() => TDiagonal.IsUnitDiagonal();

}
