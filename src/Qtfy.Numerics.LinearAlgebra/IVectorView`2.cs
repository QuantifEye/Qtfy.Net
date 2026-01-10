namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TElement, TAlignment> : IVectorView<TElement>
    where TElement : unmanaged
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
{
}
