namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;
using Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct SymmetricMatrixView<TElement, TUpperLower> :
    ISymmetricMatrixView<TElement, StrideVectorView<TElement>, StrideVectorView<TElement>, TUpperLower>
    where TUpperLower : IUpperLower
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

    public int Rows => this.order;

    public int Columns => this.order;

    public static bool IsAlwaysSquare() => true;

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

    public StrideVectorView<TElement> Row(int row)
        => new (ref Add(ref this.data, row * this.rowSpan), this.colSpan, this.order);

    public StrideVectorView<TElement> Column(int column)
        => new (ref Add(ref this.data, column * this.colSpan), this.rowSpan, this.order);

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => false;

}
