namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public sealed class ColumnMajorSymmetricMatrix<TElement, TUpperLower> :
    ISymmetricMatrix<
        TElement,
        ColumnMajorSymmetricMatrixView<TElement, TUpperLower>,
        StrideVectorView<TElement>,
        VectorView<TElement>,
        TUpperLower>
    where TUpperLower : IUpperLower, allows ref struct
{
    private readonly TElement[] data;
    private readonly int order;

    public ColumnMajorSymmetricMatrix(int order)
    {
        this.order = order;
        data = new TElement[order * order];
    }

    public int Rows => order;

    public int Columns => order;

    public static bool IsUpper() => TUpperLower.IsUpper();

    public ref TElement GetPinnableReference()
        => ref data.Reference();

    public ref TElement this[int row, int column]
        => ref data[row + (column * order)];

    public ColumnMajorSymmetricMatrixView<TElement, TUpperLower> AsView()
        => new (ref data.Reference(), order);
}
