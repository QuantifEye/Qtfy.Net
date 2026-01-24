using Qtfy.Numerics.LinearAlgebra.Matrices.Traits;

namespace Qtfy.Numerics.LinearAlgebra.ManagedBlas;

public partial struct UnsafeBlas
{
    public static void SymmetricMatrixVectorMultiply<TNumber, TUpperLower>(
        int n,
        TNumber alpha,
        ref TNumber matrix,
        int rowStride,
        int colStride,
        ref TNumber x,
        int strideX,
        TNumber beta,
        ref TNumber y,
        int strideY)
        where TNumber : INumberBase<TNumber>
        where TUpperLower : IUpperLower
    {
        if (n <= 0)
        {
            return;
        }

        ref var yScaleRef = ref y;
        var scaleRemaining = n;
        while (true)
        {
            if (--scaleRemaining != 0)
            {
                yScaleRef *= beta;
                yScaleRef = ref Add(ref yScaleRef, strideY);
            }
            else
            {
                break;
            }
        }

        yScaleRef *= beta;

        if (TUpperLower.IsUpper())
        {
            ref var aDiagRef = ref matrix;
            ref var xIRef = ref x;
            ref var yIRef = ref y;

            if (n > 1)
            {
                var i = 0;
                var rowsRemaining = n;
                while (true)
                {
                    if (--rowsRemaining != 0)
                    {
                        var xVal = xIRef;
                        yIRef += alpha * aDiagRef * xVal;
                        
                        ref var aRef = ref Add(ref aDiagRef, colStride);
                        ref var xJRef = ref Add(ref xIRef, strideX);
                        ref var yJRef = ref Add(ref yIRef, strideY);
                        
                        var innerRemaining = n - i - 1;
                                            while (true)
                        {
                            if (--innerRemaining != 0)
                            {
                        
                                var aVal = aRef;
                                yIRef += alpha * aVal * xJRef;
                                yJRef += alpha * aVal * xVal;
                        
                                aRef = ref Add(ref aRef, colStride);
                                xJRef = ref Add(ref xJRef, strideX);
                                yJRef = ref Add(ref yJRef, strideY);
                                                }
                            else
                            {
                                break;
                            }
                        }
                        
                        var aLast = aRef;
                        yIRef += alpha * aLast * xJRef;
                        yJRef += alpha * aLast * xVal;
                        
                        aDiagRef = ref Add(ref aDiagRef, rowStride + colStride);
                        xIRef = ref Add(ref xIRef, strideX);
                        yIRef = ref Add(ref yIRef, strideY);
                        ++i;
                    }
                    else
                    {
                        break;
                    }
                }    }

            yIRef += alpha * aDiagRef * xIRef;
        }
        else
        {
            ref var rowRef = ref matrix;
            ref var xIRef = ref x;
            ref var yIRef = ref y;

            yIRef += alpha * rowRef * xIRef;
            rowRef = ref Add(ref rowRef, rowStride);
            xIRef = ref Add(ref xIRef, strideX);
            yIRef = ref Add(ref yIRef, strideY);

            if (n > 1)
            {
                var i = 1;
                var rowsRemaining = n - 1;
                while (true)
                {
                    if (--rowsRemaining != 0)
                    {
                        var xVal = xIRef;
                        ref var aRef = ref rowRef;
                        ref var xJRef = ref x;
                        ref var yJRef = ref y;
                        
                        var innerRemaining = i;
                                            while (true)
                        {
                            if (--innerRemaining != 0)
                            {
                        
                                var aVal = aRef;
                                yIRef += alpha * aVal * xJRef;
                                yJRef += alpha * aVal * xVal;
                        
                                aRef = ref Add(ref aRef, colStride);
                                xJRef = ref Add(ref xJRef, strideX);
                                yJRef = ref Add(ref yJRef, strideY);
                                                }
                            else
                            {
                                break;
                            }
                        }
                        
                        var aLast = aRef;
                        yIRef += alpha * aLast * xJRef;
                        yJRef += alpha * aLast * xVal;
                        
                        aRef = ref Add(ref aRef, colStride);
                        xJRef = ref Add(ref xJRef, strideX);
                        yIRef += alpha * aRef * xJRef;
                        
                        rowRef = ref Add(ref rowRef, rowStride);
                        xIRef = ref Add(ref xIRef, strideX);
                        yIRef = ref Add(ref yIRef, strideY);
                        ++i;
                    }
                    else
                    {
                        break;
                    }
                }         {
                    var xVal = xIRef;
                    ref var aRef = ref rowRef;
                    ref var xJRef = ref x;
                    ref var yJRef = ref y;

                    var innerRemaining = i;
                    while (true)
                    {
                        if (--innerRemaining != 0)
                        {
                            var aVal = aRef;
                            yIRef += alpha * aVal * xJRef;
                            yJRef += alpha * aVal * xVal;
                            
                            aRef = ref Add(ref aRef, colStride);
                            xJRef = ref Add(ref xJRef, strideX);
                            yJRef = ref Add(ref yJRef, strideY);
                        }
                        else
                        {
                            break;
                        }
                    }

                    var aLast = aRef;
                    yIRef += alpha * aLast * xJRef;
                    yJRef += alpha * aLast * xVal;

                    aRef = ref Add(ref aRef, colStride);
                    xJRef = ref Add(ref xJRef, strideX);
                    yIRef += alpha * aRef * xJRef;
                }
            }
        }
    }
}
