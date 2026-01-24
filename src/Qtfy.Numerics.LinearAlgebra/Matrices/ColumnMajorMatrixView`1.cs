using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly ref struct ColumnMajorMatrixView<TElement> :
    IStridedMatrixView<TElement, StrideVectorView<TElement>, VectorView<TElement>>
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    public ColumnMajorMatrixView(ref TElement reference, int rows, int columns)
    {
        this.reference = ref reference;
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => rows;

    public int Columns => columns;

    public int RowStride => 1;

    public int ColumnStride => rows;

    public ref TElement GetPinnableReference()
        => ref reference;

    public static bool IsAlwaysSquare() => false;

    public static bool RowStrideIsAlwaysOne() => true;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public StrideVectorView<TElement> Row(int row)
    {
        return new (ref Add(ref reference, row), rows, columns);
    }

    public VectorView<TElement> Column(int column)
    {
        return new (
            ref Add(ref reference, column * rows),
            rows);
    }

    public ref TElement this[int row, int column]
        => ref Add(ref reference, row + column * rows);

}
