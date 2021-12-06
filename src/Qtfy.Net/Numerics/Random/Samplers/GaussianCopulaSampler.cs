// <copyright file="GaussianCopulaSampler.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using Qtfy.Net.Numerics.Distributions;

    /// <summary>
    /// A gaussian copula sampler. That is a sampler that uses a correlation matrix
    /// to generate correlated uniform[0, 1] variables.
    /// </summary>
    public sealed partial class GaussianCopulaSampler : ISampler<double[]>
    {
        private readonly double[] choleskyFactor;

        private readonly double[] buffer;

        private readonly StandardNormalSampler standardNormalSampler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianCopulaSampler"/> class.
        /// </summary>
        /// <param name="engine">
        /// The random number engine.
        /// </param>
        /// <param name="choleskyFactor">
        /// The Cholesky factorization of the correlation matrix.
        /// </param>
        /// <param name="order">
        /// The size of rows (or columns of the original correlation matrix).
        /// </param>
        private GaussianCopulaSampler(IRandomNumberEngine engine, double[] choleskyFactor, int order)
        {
            this.buffer = new double[order];
            this.Length = order;
            this.choleskyFactor = choleskyFactor;
            this.standardNormalSampler = new StandardNormalSampler(engine);
        }

        /// <summary>
        /// Gets the length of the array <see cref="GetNext"/> returns.
        /// </summary>
        public int Length { get; }

        /// <inheritdoc />
        public double[] GetNext()
        {
            var resultSize = this.Length;
            var result = new double[resultSize];
            var normals = this.buffer;
            this.standardNormalSampler.Fill(normals);
            unsafe
            {
                fixed (double* cPin = this.choleskyFactor, nPin = normals, rPin = result)
                {
                    var r = rPin;
                    var rEnd = rPin + resultSize;
                    Multiply(cPin, nPin, rPin, rEnd);
                    do
                    {
                        *r = StandardNormalDistribution.CumulativeDistributionFunction(*r);
                    }
                    while (++r != rEnd);
                }
            }

            return result;
        }

        private static unsafe void Multiply(
            double* choleskyFactor,
            double* normals,
            double* result,
            double* resultEnd)
        {
            double* z;
            var zEnd = normals;
            double total;
            do
            {
                z = normals;
                ++zEnd;
                total = *z * *choleskyFactor;
                ++choleskyFactor;
                while (++z != zEnd)
                {
                    total += *z * *choleskyFactor;
                    ++choleskyFactor;
                }

                *result = total;
            }
            while (++result != resultEnd);
        }
    }
}
