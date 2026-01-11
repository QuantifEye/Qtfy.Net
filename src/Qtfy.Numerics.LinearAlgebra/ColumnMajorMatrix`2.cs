namespace Qtfy.Numerics.LinearAlgebra;

using Qtfy.Memory;

public sealed unsafe class ColumnMajorMatrix<TElement, TAlignment> : IDisposable
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private readonly NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    private readonly int columnStride;

    public ColumnMajorMatrix(int rows, int columns)
    {
        var elementSize = (nuint)System.Runtime.CompilerServices.Unsafe.SizeOf<TElement>();
        var bytesPerColumn = elementSize * (nuint)rows;
        var alignedBytesPerColumn = AddressMath.AlignUpTo(bytesPerColumn, TAlignment.ByteAlignment());
        var totalBytes = alignedBytesPerColumn * (nuint)columns;
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        this.rows = rows;
        this.columns = columns;
        this.columnStride = (int)(alignedBytesPerColumn / elementSize);
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public TElement* Pointer => this.memory.Pointer<TElement>();

    public ColumnMajorMatrixView<TElement, TAlignment> AsView()
        => new(ref this.memory.Reference<TElement>(), this.rows, this.columns, this.columnStride);

    public void Dispose()
    {
        this.memory.Dispose();
    }
}
