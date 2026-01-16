namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Vectors;

public readonly ref struct SquareColumnMajorMatrixView<TElement> :
    IStridedMatrixView<TElement, StrideVectorView<TElement>, VectorView<TElement>>
{
    private readonly ref TElement reference;
    private readonly int order;

    public SquareColumnMajorMatrixView(ref TElement reference, int order)
    {
        this.reference = ref reference;
        this.order = order;
    }

    public int Order => this.order;

    public int Rows => this.order;

    public int Columns => this.order;

    public int RowStride => 1;

    public int ColumnStride => this.order;

    public static bool IsAlwaysSquare() => true;

    public static bool RowStrideIsAlwaysOne() => true;

    public static bool ColumnStrideIsAlwaysOne() => false;

    public ref TElement GetPinnableReference()
        => ref this.reference;

    public StrideVectorView<TElement> Row(int row)
        => new (ref Add(ref this.reference, row), this.order, this.order);

    public VectorView<TElement> Column(int column)
        => new (ref Add(ref this.reference, column * this.order), this.order);

    public ref TElement this[int row, int column]
        => ref Add(ref this.reference, row + column * this.order);
}
