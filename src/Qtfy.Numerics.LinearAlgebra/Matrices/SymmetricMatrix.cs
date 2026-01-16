namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;
using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class SymmetricMatrix<TElement, TUpperLower> :
    ISymmetricMatrix<
        TElement,
        SymmetricMatrixView<TElement, TUpperLower>,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower>
    where TUpperLower : IUpperLower
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
            this.rowSpan = order;
            this.colSpan = 1;
        }
        else
        {
            this.rowSpan = 1;
            this.colSpan = order;
        }

        this.data = new TElement[order * order];
    }

    public int Rows => this.order;

    public int Columns => this.order;

    public static bool IsUpper => TUpperLower.IsUpper();

    public static Uplo Uplo => IsUpper ? Uplo.Upper : Uplo.Lower;

    public ref TElement GetPinnableReference()
        => ref this.data.Reference();

    public ref TElement this[int row, int column]
    {
        get
        {
            var offset = row * this.rowSpan + column * this.colSpan;
            return ref this.data[offset];
        }
    }

    public SymmetricMatrixView<TElement, TUpperLower> AsView()
        => new (ref this.data.Reference(), this.order, this.rowSpan, this.colSpan);
}
