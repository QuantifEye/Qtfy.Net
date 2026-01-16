namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;
using Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct TriangularMatrixView<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrixView<
        TElement,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower
    where TDiagonal : IDiagonal
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

    public int Rows => this.order;

    public int Columns => this.order;

    public static bool IsAlwaysSquare() => true;

    public int RowStride => this.rowSpan;

    public int ColumnStride => this.colSpan;

    public static bool IsUpper => TUpperLower.IsUpper();

    public static bool IsUnitDiagonal => TDiagonal.IsUnitDiagonal();

    public static Uplo Uplo => IsUpper ? Uplo.Upper : Uplo.Lower;

    public static Diag Diag => IsUnitDiagonal ? Diag.Unit : Diag.NonUnit;

    public ref TElement GetPinnableReference()
        => ref this.data;

    public ref TElement this[int row, int column]
    {
        get => ref Add(ref this.data, row * this.rowSpan + column * this.colSpan);
    }

    public StrideVectorView<TElement> Row(int row)
    {
        return new (ref Add(ref this.data, row * this.rowSpan), this.colSpan, this.order);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        return new (ref Add(ref this.data, column * this.colSpan), this.rowSpan, this.order);
    }

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

}
