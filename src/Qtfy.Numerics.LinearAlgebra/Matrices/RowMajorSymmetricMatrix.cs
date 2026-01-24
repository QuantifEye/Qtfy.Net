namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Traits;
using Vectors;

public sealed class RowMajorSymmetricMatrix<TElement, TUpperLower> :
    ISymmetricMatrix<
        TElement,
        RowMajorSymmetricMatrixView<TElement, TUpperLower>,
        VectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower>
    where TUpperLower : IUpperLower, allows ref struct
{
    private readonly TElement[] data;
    private readonly int order;

    public RowMajorSymmetricMatrix(int order)
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
        => ref data[row * order + column];

    public RowMajorSymmetricMatrixView<TElement, TUpperLower> AsView()
        => new (ref data.Reference(), order);
}
