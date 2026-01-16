namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct SquareRowMajorMatrixView<TElement> :
    IStridedMatrixView<TElement, VectorView<TElement>, StrideVectorView<TElement>>
{
    private readonly ref TElement reference;
    private readonly int order;

    public SquareRowMajorMatrixView(ref TElement reference, int order)
    {
        this.reference = ref reference;
        this.order = order;
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public int RowStride => this.order;

    public int ColumnStride => 1;

    public static bool IsAlwaysSquare() => true;

    public static bool RowStrideIsAlwaysOne() => false;

    public static bool ColumnStrideIsAlwaysOne() => true;

    public ref TElement GetPinnableReference()
        => ref this.reference;

    public VectorView<TElement> Row(int row)
        => new (ref Add(ref this.reference, row * this.order), this.order);

    public StrideVectorView<TElement> Column(int column)
        => new (ref Add(ref this.reference, column), this.order, this.order);

    public ref TElement this[int row, int column]
        => ref Add(ref this.reference, row * this.order + column);
}
