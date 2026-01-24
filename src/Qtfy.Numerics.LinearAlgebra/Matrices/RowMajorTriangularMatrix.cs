namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public sealed class RowMajorTriangularMatrix<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrix<
        TElement,
        RowMajorTriangularMatrixView<TElement, TUpperLower, TDiagonal>,
        VectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower, allows ref struct
    where TDiagonal : IDiagonal, allows ref struct
{
    private readonly TElement[] data;
    private readonly int order;

    public RowMajorTriangularMatrix(int order)
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
        => ref data[row * order + column];

    public RowMajorTriangularMatrixView<TElement, TUpperLower, TDiagonal> AsView()
        => new (ref data.Reference(), order);
}
