using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Memory;

public sealed class ColumnMajorMatrix<TElement, TAlignment> :
    IMatrix<
        TElement,
        ColumnMajorMatrixView<TElement, TAlignment>,
        StrideVectorView<TElement>,
        VectorView<TElement, TAlignment>>,
    IDisposable
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private unsafe TElement* pointer;

    private NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    private readonly int columnStride;

    public ColumnMajorMatrix(int rows, int columns)
    {
        var elementSize = (nuint)SizeOf<TElement>();
        var bytesPerColumn = elementSize * (nuint)rows;
        var alignedBytesPerColumn = AddressMath.AlignUpTo(bytesPerColumn, TAlignment.ByteAlignment());
        var totalBytes = alignedBytesPerColumn * (nuint)columns;
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        unsafe
        {
            this.pointer = (TElement*)this.memory.Pointer();
        }

        this.rows = rows;
        this.columns = columns;
        this.columnStride = (int)(alignedBytesPerColumn / elementSize);
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public ref TElement GetPinnableReference()
    {
        unsafe
        {
            return ref AsRef<TElement>(this.pointer);
        }
    }

    public ref TElement this[int row, int column]
    {
        get
        {
            unsafe
            {
                return ref this.pointer[row + column * this.columnStride];
            }
        }
    }

    public ColumnMajorMatrixView<TElement, TAlignment> AsView()
    {
        unsafe
        {
            return new (ref AsRef<TElement>(this.pointer), this.rows, this.columns, this.columnStride);
        }
    }

    public void Dispose()
    {
        this.memory.Dispose();
        unsafe
        {
            this.pointer = null;
        }
    }

    public static bool IsPinned() => true;
}
