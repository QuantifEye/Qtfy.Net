using Qtfy.Memory;

namespace Qtfy.Numerics.LinearAlgebra;

public sealed unsafe class RowMajorMatrix<TElement, TAlignment> : IDisposable
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private readonly NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    private readonly int rowStride;

    public RowMajorMatrix(int rows, int columns)
    {
        var elementSize = (nuint)Unsafe.SizeOf<TElement>();
        var bytesPerRow = elementSize * (nuint)columns;
        var alignedBytesPerRow = AddressMath.AlignUpTo(bytesPerRow, TAlignment.ByteAlignment());
        var totalBytes = alignedBytesPerRow * (nuint)rows;
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        this.rows = rows;
        this.columns = columns;
        this.rowStride = (int)(alignedBytesPerRow / elementSize);
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public unsafe TElement* Pointer => this.memory.Pointer<TElement>();

    public RowMajorMatrixView<TElement, TAlignment> AsView()
        => new(ref this.memory.Reference<TElement>(), this.rows, this.columns, this.rowStride);

    public void Dispose()
    {
        this.memory.Dispose();
    }
}
