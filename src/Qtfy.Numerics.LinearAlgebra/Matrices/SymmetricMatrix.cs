namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public sealed class SymmetricMatrix<TElement, TUpperLower> :
    ISymmetricMatrix<
        TElement,
        SymmetricMatrixView<TElement, TUpperLower>,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower>
    where TUpperLower : IUpperLower, allows ref struct
{
    private readonly TElement[] data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public SymmetricMatrix(int order, bool isRowMajor = true)
    {
        this.order = order;
        if (isRowMajor)
        {
            rowSpan = order;
            colSpan = 1;
        }
        else
        {
            rowSpan = 1;
            colSpan = order;
        }

        data = new TElement[order * order];
    }

    public int Rows => order;

    public int Columns => order;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public ref TElement GetPinnableReference()
        => ref data.Reference();

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * rowSpan + column * colSpan;
            return ref data[offset];
        }
    }

    public SymmetricMatrixView<TElement, TUpperLower> AsView()
        => new (ref data.Reference(), order, rowSpan, colSpan);
}
