namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public sealed class ColumnMajorTriangularMatrix<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrix<
        TElement,
        ColumnMajorTriangularMatrixView<TElement, TUpperLower, TDiagonal>,
        StrideVectorView<TElement>,
        VectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    private readonly TElement[] data;
    private readonly int order;

    public ColumnMajorTriangularMatrix(int order)
    {
        this.order = order;
        data = new TElement[order * order];
    }

    public int Rows => order;

    public int Columns => order;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public static bool IsUnitDiagonal() => TDiagonal.IsUnitDiagonal();

    public ref TElement GetPinnableReference()
        => ref data.Reference();

    public ref TElement this[int row, int column]
        => ref data[row + (column * order)];

    public ColumnMajorTriangularMatrixView<TElement, TUpperLower, TDiagonal> AsView()
        => new (ref data.Reference(), order);
}
