using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct RowMajorMatrixView<TElement>
    : IStridedMatrixView<TElement, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    public RowMajorMatrixView(ref TElement reference, int rows, int columns)
    {
        this.reference = ref reference;
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => rows;

    public int Columns => columns;

    public int RowStride => columns;

    public int ColumnStride => 1;

    public ref TElement GetPinnableReference()
        => ref reference;

    public static bool IsAlwaysSquare() => false;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => true;

    public VectorView<TElement> Row(int row)
    {
        return new (ref Add(ref reference, row * columns), columns);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        return new (ref Add(ref reference, column), columns, rows);
    }

    public ref TElement this[int row, int column]
        => ref Add(ref reference, row * columns + column);
}
