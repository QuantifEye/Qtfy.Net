namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;
using Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct TriangularGeneralMatrixView<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrixView<
        TElement,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower,
        TDiagonal,
        TriangularGeneralMatrixView<TElement, TUpperLower, TDiagonal>>
    where TUpperLower : IUpperLower
    where TDiagonal : IDiagonal
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public TriangularGeneralMatrixView(ref TElement data, int order, int rowSpan, int colSpan)
    {
        this.data = data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => this.order;

    public int Columns => this.order;

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
        get
        {
            var offset = row * this.rowSpan + column * this.colSpan;
            return ref Add(ref this.data, offset);
        }
    }

    public StrideVectorView<TElement> Row(int row)
    {
        var offset = row * this.rowSpan;
        return new (ref Add(ref this.data, offset), this.colSpan, this.order);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        var offset = column * this.colSpan;
        return new (ref Add(ref this.data, offset), this.rowSpan, this.order);
    }

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public static bool IsPinned() => false;
}
