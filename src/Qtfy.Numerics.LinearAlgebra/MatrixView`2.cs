using System.Diagnostics;
using Qtfy.Memory;

namespace Qtfy.Numerics.LinearAlgebra;

using System.Runtime.CompilerServices;

public readonly ref struct MatrixView<TElement, TAlignment>
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
    private readonly unsafe TElement* memory;
    private readonly int rows;
    private readonly int columns;
    private readonly int rowSpan;
    private readonly int colSpan;

    internal unsafe MatrixView(TElement* memory, int rows, int columns, int rowSpan, int colSpan)
    {
        Debug.Assert(TAlignment.IsAligned((nuint)memory));
        this.memory = memory;
        this.rows = rows;
        this.columns = columns;
        this.rowSpan = rowSpan;
        this.colSpan = colSpan;
    }

    public int Rows => this.rows;

    public int Columns => this.columns;

    public int RowSpan => this.rowSpan;

    public int ColSpan => this.colSpan;

    public ref TElement this[int row, int column]
    {
        get
        {
            unsafe
            {
                var offset = (row * this.rowSpan) + (column * this.colSpan);
                return ref this.memory[offset];
            }
        }
    }
}
