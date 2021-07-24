// <copyright file="MultivariateNormalSampler.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    /// <summary>
    /// A multivariate normal (gaussian) sampler.
    /// </summary>
    public sealed partial class MultivariateNormalSampler : ISampler<double[]>
    {
        private readonly double[] mean;

        private readonly double[] buffer;

        private readonly double[] choleskyFactor;

        private readonly StandardNormalSampler standardNormalSampler;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultivariateNormalSampler"/> class.
        /// </summary>
        /// <param name="engine">
        /// The simulation engine to be used for sampling.
        /// </param>
        /// <param name="mean">
        /// The mean values of the variables to be generated simultaneously.
        /// </param>
        /// <param name="choleskyFactor">
        /// The Cholesky factorization of a matrix defining the desired joint distribution.
        /// </param>
        private MultivariateNormalSampler(IRandomNumberEngine engine, double[] mean, double[] choleskyFactor)
        {
            this.Length = mean.Length;
            this.mean = mean;
            this.choleskyFactor = choleskyFactor;
            this.standardNormalSampler = new StandardNormalSampler(engine);
            this.buffer = new double[this.mean.Length];
        }

        /// <summary>
        /// Gets the length of the arrays <see cref="GetNext"/> returns.
        /// </summary>
        public int Length { get; }

        /// <inheritdoc />
        public double[] GetNext()
        {
            var size = this.Length;
            var result = new double[size];
            var normals = this.buffer;
            this.standardNormalSampler.Fill(normals);
            unsafe
            {
                fixed (double* fPin = this.choleskyFactor, nPin = normals, mPin = this.mean, rPin = result)
                {
                    MultiplyAdd(fPin, nPin, mPin, rPin, rPin + size);
                }
            }

            return result;
        }

        private static unsafe void MultiplyAdd(
            double* choleskyFactor,
            double* normals,
            double* mean,
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

                *result = total + *mean;
                ++mean;
            }
            while (++result != resultEnd);
        }
    }
}
