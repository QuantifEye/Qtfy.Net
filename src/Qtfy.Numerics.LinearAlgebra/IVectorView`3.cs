namespace Qtfy.Numerics.LinearAlgebra;

public interface IVectorView<TSelf, TElement, TAlignment> : IVectorView<TSelf, TElement>
    where TAlignment : unmanaged, IAlignmentPolicy<TAlignment>
    where TSelf : IVectorView<TSelf, TElement, TAlignment>
{
}
