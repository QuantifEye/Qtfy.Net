using Qtfy.Numerics.LinearAlgebra.Memory;
using Qtfy.Numerics.LinearAlgebra.Vectors;

namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public sealed class RowMajorMatrix<TElement, TAlignment> :
    IMatrix<TElement, RowMajorMatrixView<TElement, TAlignment>, VectorView<TElement, TAlignment>, StrideVectorView<TElement>>,
    IDisposable
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private unsafe TElement* pointer;

    private readonly NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    private readonly int rowStride;

    public RowMajorMatrix(int rows, int columns)
    {
        var elementSize = (nuint)SizeOf<TElement>();
        var bytesPerRow = elementSize * (nuint)columns;
        var alignedBytesPerRow = AddressMath.AlignUpTo(bytesPerRow, TAlignment.ByteAlignment());
        var totalBytes = alignedBytesPerRow * (nuint)rows;
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        unsafe
        {
            this.pointer = (TElement*)this.memory.Pointer();
        }

        this.rows = rows;
        this.columns = columns;
        this.rowStride = (int)(alignedBytesPerRow / elementSize);
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
                return ref this.pointer[row * this.rowStride + column];
            }
        }
    }

    public RowMajorMatrixView<TElement, TAlignment> AsView()
    {
        unsafe
        {
            return new (ref AsRef<TElement>(this.pointer), this.rows, this.columns, this.rowStride);
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
