namespace Qtfy.Numerics.LinearAlgebra;

using Memory;

public sealed class ColumnMajorMatrix<TElement, TAlignment> : IDisposable,
    IMatrix<TElement, ColumnMajorMatrixView<TElement, TAlignment>, StrideVectorView<TElement>,
        VectorView<TElement, TAlignment>>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private nuint pointer;

    private NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    private readonly int columnStride;

    public ColumnMajorMatrix(int rows, int columns)
    {
        var elementSize = (nuint)Unsafe.SizeOf<TElement>();
        var bytesPerColumn = elementSize * (nuint)rows;
        var alignedBytesPerColumn = AddressMath.AlignUpTo(bytesPerColumn, TAlignment.ByteAlignment());
        var totalBytes = alignedBytesPerColumn * (nuint)columns;
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        unsafe
        {
            this.pointer = (nuint)this.memory.Pointer();
        }

        this.rows = rows;
        this.columns = columns;
        this.columnStride = (int)(alignedBytesPerColumn / elementSize);
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public ref TElement this[int row, int column]
        => ref Unsafe.Add(ref this.memory.Reference<TElement>(), row + (column * this.columnStride));

    public ColumnMajorMatrixView<TElement, TAlignment> AsView()
    {
        unsafe
        {
            return new (ref *(TElement*)this.pointer, this.rows, this.columns, this.columnStride);
        }
    }

    public void Dispose()
    {
        this.memory.Dispose();
        this.pointer = 0;
    }
}
