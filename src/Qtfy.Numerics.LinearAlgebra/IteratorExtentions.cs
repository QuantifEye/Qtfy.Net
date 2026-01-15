namespace Qtfy.Numerics.LinearAlgebra;

public static class IteratorExtentions
{
    public static void CreateForwardIterator<TElement, TVectorView>(this TVectorView self)
        where TVectorView : IVectorView<TElement>
    {
        throw new NotImplementedException("This sure would be nice");
        // if (TVectorView.IsAlwaysContigious())
        // {
        //     return new ContiguousIterator<TElement>(ref self.GetPinnableReference());
        // }
        // else
        // {
        //     return new StrideIterator<TElement>(ref self.GetPinnableReference());
        // }
    }
}
