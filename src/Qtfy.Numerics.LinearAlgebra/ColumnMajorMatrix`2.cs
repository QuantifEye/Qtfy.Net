namespace Qtfy.Numerics.LinearAlgebra;

using Qtfy.Memory;

public sealed unsafe class ColumnMajorMatrix<TElement, TAlignment> : IDisposable
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private readonly NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    public ColumnMajorMatrix(int rows, int columns)
    {
        var totalBytes = (nuint)(rows * columns) * (nuint)sizeof(TElement);
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public TElement* Pointer => this.memory.Pointer<TElement>();

    public ColumnMajorMatrixView<TElement, TAlignment> AsView()
        => new(this.Pointer, this.rows, this.columns);

    public void Dispose()
    {
        this.memory.Dispose();
    }
}
