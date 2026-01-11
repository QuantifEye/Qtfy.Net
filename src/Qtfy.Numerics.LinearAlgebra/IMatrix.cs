namespace Qtfy.Numerics.LinearAlgebra;

public interface IMatrix<TElement>
{
    ref TElement this[int row, int column] { get; }
}
