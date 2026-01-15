namespace Qtfy.Numerics.LinearAlgebra.Matrices;

public readonly struct Indexing
{
    public static int Strided(int row, int column, int rowStride, int columnStride)
        => (row * rowStride) + (column * columnStride);

    public static int RowMajor(int row, int column, int rowStride)
        => (row * rowStride) + column;

    public static int ColumnMajor(int row, int column, int columnStride)
        => row + (column * columnStride);

    public static int Square(int row, int column, int order)
        => (row * order) + column;

    public static int LowerTriangularPacked(int row, int column)
        => (row * (row + 1) / 2) + column;
}
