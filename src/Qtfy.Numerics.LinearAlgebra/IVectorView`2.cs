namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TElement, TAlignment> :
    IVectorView<TElement>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
}
