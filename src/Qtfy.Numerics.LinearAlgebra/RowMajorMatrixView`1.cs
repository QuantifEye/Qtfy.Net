using System.Runtime.CompilerServices;

namespace Qtfy.Numerics.LinearAlgebra;

public readonly ref struct RowMajorMatrixView<TElement>
    where TElement : unmanaged
{
    private readonly ref TElement reference;

    private readonly int rows;

    private readonly int columns;

    private readonly int rowStride;

    public RowMajorMatrixView(ref TElement reference, int rows, int columns)
    {
        this.reference = reference;
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public VectorView<TElement> Row(int row)
    {
        return new (ref Unsafe.Add(ref this.reference, row * this.rowStride), this.columns);
    }

    public StrideVectorView<TElement> Column(int column)
    {
        return new (ref Unsafe.Add(ref this.reference, column), this.rowStride, this.rows);
    }
}
