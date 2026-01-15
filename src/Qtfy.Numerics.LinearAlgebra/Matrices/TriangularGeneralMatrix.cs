namespace Qtfy.Numerics.LinearAlgebra.Matrices;

using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;
using Qtfy.Numerics.LinearAlgebra.Vectors;

public sealed class TriangularGeneralMatrix<TElement, TUpperLower, TDiagonal> :
    ITriangularMatrix<
        TElement,
        TriangularGeneralMatrixView<TElement, TUpperLower, TDiagonal>,
        StrideVectorView<TElement>,
        StrideVectorView<TElement>,
        TUpperLower,
        TDiagonal>
    where TUpperLower : IUpperLower
    where TDiagonal : IDiagonal
{
    private readonly TElement[] data;
    private readonly int order;
    private readonly int rowSpan;
    private readonly int colSpan;

    public TriangularGeneralMatrix(int order, bool isRowMajor = true)
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

    public static bool IsUnitDiagonal => TDiagonal.IsUnitDiagonal();

    public static Uplo Uplo => IsUpper ? Uplo.Upper : Uplo.Lower;

    public static Diag Diag => IsUnitDiagonal ? Diag.Unit : Diag.NonUnit;

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

    public TriangularGeneralMatrixView<TElement, TUpperLower, TDiagonal> AsView()
        => new (ref this.data.Reference(), this.order, this.rowSpan, this.colSpan);

    public static bool IsPinned() => false;
}
