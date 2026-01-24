namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public readonly ref struct SymmetricMatrixView<TElement, TUpperLower> :
    ISymmetricMatrixView<TElement, StrideVectorView<TElement>, StrideVectorView<TElement>, TUpperLower>
    where TUpperLower : IUpperLower, allows ref struct
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public SymmetricMatrixView(ref TElement data, int order, int rowSpan, int colSpan)
    {
        this.data = ref data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => order;

    public int Columns => order;

    public static bool IsAlwaysSquare() => true;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public int RowStride => rowSpan;

    public int ColumnStride => colSpan;

    public ref TElement GetPinnableReference()
        => ref data;

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * rowSpan + column * colSpan;
            return ref Add(ref data, offset);
        }
    }

    public StrideVectorView<TElement> Row(int row)
        => new (ref Add(ref data, row * rowSpan), colSpan, order);

    public StrideVectorView<TElement> Column(int column)
        => new (ref Add(ref data, column * colSpan), rowSpan, order);

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

}
