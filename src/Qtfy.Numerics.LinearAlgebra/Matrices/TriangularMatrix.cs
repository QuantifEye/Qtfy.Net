namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public sealed class TriangularMatrix<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrix<
        TElement,
        TriangularMatrixView<TElement, TUpperLower, TDiagonal>,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    private readonly TElement[] data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public TriangularMatrix(int order, bool isRowMajor = true)
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

    public static bool IsUnitDiagonal() => TDiagonal.IsUnitDiagonal();

    public ref TElement GetPinnableReference()
        => ref data.Reference();

    public ref TElement this[int row, int column]
    {
        get
        {
            return ref data[row * rowSpan + column * colSpan];
        }
    }

    public TriangularMatrixView<TElement, TUpperLower, TDiagonal> AsView()
        => new (ref data.Reference(), order, rowSpan, colSpan);
}
