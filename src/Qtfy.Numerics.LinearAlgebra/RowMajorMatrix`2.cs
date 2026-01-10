using System.Runtime.CompilerServices;
using Qtfy.Memory;

namespace Qtfy.Numerics.LinearAlgebra;

public sealed unsafe class RowMajorMatrix<TElement, TAlignment> : IDisposable
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TElement : unmanaged
{
    private readonly NativeMemoryOwner memory;

    private readonly int rows;

    private readonly int columns;

    public RowMajorMatrix(int rows, int columns)
    {
        var bytesPerRow =
            AddressMath.AlignUpTo((nuint)Unsafe.SizeOf<TElement>() * (nuint)rows, TAlignment.ByteAlignment());
        var totalBytes = bytesPerRow * (nuint)columns;
        this.memory = new NativeMemoryOwner(totalBytes, TAlignment.ByteAlignment());
        this.rows = rows;
        this.columns = columns;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public unsafe TElement* Pointer => this.memory.Pointer<TElement>();

    public RowMajorMatrixView<TElement, TAlignment> AsView()
        => new(this.Pointer, this.rows, this.columns);

    public void Dispose()
    {
        this.memory.Dispose();
    }
}
