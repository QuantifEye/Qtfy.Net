namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

public readonly ref struct SymmetricMatrixView<TElement, TUpperLower> :
    ISymmetricMatrixView<TElement, SymmetricRowView<TElement>, SymmetricColumnView<TElement>, TUpperLower, SymmetricMatrixView<TElement, TUpperLower>>
    where TUpperLower : IUpperLower
{
    private readonly ref TElement data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public SymmetricMatrixView(ref TElement data, int order, int rowSpan, int colSpan)
    {
        this.data = data;
        this.order = order;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => this.order;

    public int Columns => this.order;

    public static bool IsUpper => TUpperLower.IsUpper();

    public static Uplo Uplo => IsUpper ? Uplo.Upper : Uplo.Lower;

    public int RowStride => this.rowSpan;

    public int ColumnStride => this.colSpan;

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

    public SymmetricRowView<TElement> Row(int row)
        => new (ref this.data, this.order, this.rowSpan, this.colSpan, row);

    public SymmetricColumnView<TElement> Column(int column)
        => new (ref this.data, this.order, this.rowSpan, this.colSpan, column);

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public static bool IsPinned() => false;
}
