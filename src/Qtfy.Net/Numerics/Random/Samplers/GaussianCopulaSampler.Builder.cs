// <copyright file="GaussianCopulaSampler.Builder.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using System;

    public sealed partial class GaussianCopulaSampler
    {
        /// <summary>
        /// An object that is able to create <see cref="GaussianCopulaSampler"/>s with the
        /// same covariance matrix, but with different <see cref="IRandomNumberEngine"/>s.
        /// </summary>
        public sealed class Builder
        {
            private readonly double[] choleskyFactor;

            private readonly int order;

            /// <summary>
            /// Initializes a new instance of the <see cref="Builder"/> class.
            /// </summary>
            /// <param name="correlationMatrix">
            /// The correlation matrix.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// If <paramref name="correlationMatrix"/> is null.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// If <paramref name="correlationMatrix"/> is not zero indexed,
            /// if <paramref name="correlationMatrix"/> is not symmetric,
            /// if <paramref name="correlationMatrix"/> is empty,
            /// if <paramref name="correlationMatrix"/> is not positive definite.
            /// if any value in <paramref name="correlationMatrix"/> is not a valid correlation.
            /// if any value on the diagonal if <paramref name="correlationMatrix"/> is not equal to one.
            /// </exception>
            public Builder(double[,] correlationMatrix)
            {
                if (correlationMatrix is null)
                {
                    throw new ArgumentNullException(nameof(correlationMatrix));
                }

                this.choleskyFactor = Impl.PackedCholeskyFactorCorrelationMatrix(correlationMatrix);
                this.order = correlationMatrix.GetLength(0);
            }

            /// <summary>
            /// Builds a new instance of a gaussian copula sampler.
            /// </summary>
            /// <param name="engine">
            /// The random number engine to use as a ransom source.
            /// </param>
            /// <returns>
            /// A new instance of a gaussian copula sampler.
            /// </returns>
            public GaussianCopulaSampler Build(IRandomNumberEngine engine)
            {
                return new (engine, this.choleskyFactor, this.order);
            }
        }
    }
}
