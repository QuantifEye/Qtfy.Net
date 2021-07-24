// <copyright file="MultivariateNormalSampler.Builder.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using System;

    public sealed partial class MultivariateNormalSampler
    {
        /// <summary>
        /// An object that is able to create <see cref="MultivariateNormalSampler"/>s with the
        /// same mean vector and covariance matrix, but with different <see cref="IRandomNumberEngine"/>s.
        /// </summary>
        public sealed class Builder
        {
            private readonly double[] mean;

            private readonly double[] choleskyFactor;

            /// <summary>
            /// Initializes a new instance of the <see cref="Builder"/> class.
            /// </summary>
            /// <param name="mean">
            /// The mean vector of the reference multivariate normal distribution.
            /// </param>
            /// <param name="covarianceMatrix">
            /// The covariance matrix of the reference multivariate normal distribution.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// If <paramref name="mean"/> is null.
            /// If <paramref name="covarianceMatrix"/> is null.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// If any values in <paramref name="mean"/> are nan or infinite.
            /// If the number of entries in <paramref name="mean"/> is not equal to the number of rows (or columns) of <paramref name="covarianceMatrix"/>.
            /// If <paramref name="covarianceMatrix"/> is not a valid covariance matrix.
            /// If <paramref name="covarianceMatrix"/> is not zero indexed.
            /// </exception>
            public Builder(double[] mean, double[,] covarianceMatrix)
            {
                if (mean is null)
                {
                    throw new ArgumentNullException(nameof(mean));
                }

                if (covarianceMatrix is null)
                {
                    throw new ArgumentNullException(nameof(covarianceMatrix));
                }

                var factor = Impl.PackedCholeskyFactorCovarianceMatrix(covarianceMatrix);

                foreach (var t in mean)
                {
                    if (!double.IsFinite(t))
                    {
                        throw new ArgumentException("Mean values must be finite and not NaN");
                    }
                }

                if (mean.Length != covarianceMatrix.GetLength(0))
                {
                    throw new ArgumentException(
                        "mean must have length equal to number of rows of square covariance matrix");
                }

                this.choleskyFactor = factor;
                this.mean = mean.Copy();
            }

            /// <summary>
            /// Builds a new instance of a multivariate normal sampler.
            /// </summary>
            /// <param name="engine">
            /// The random number engine to use as a ransom source.
            /// </param>
            /// <returns>
            /// A new instance of a multivariate normal sampler.
            /// </returns>
            public MultivariateNormalSampler Build(IRandomNumberEngine engine)
            {
                return new (engine, this.mean, this.choleskyFactor);
            }
        }
    }
}
