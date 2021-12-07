// <copyright file="Impl.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using System;

    /// <summary>
    /// Implementation functions for samplers.
    /// </summary>
    internal static class Impl
    {
        /// <summary>
        /// Performs the cholesky decomposition of the provided matrix,
        /// and returns it in row major packed form.
        /// </summary>
        /// <param name="covarianceMatrix">
        /// The covariance matrix to factor.
        /// </param>
        /// <returns>
        /// The factored correlation matrix.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="covarianceMatrix"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="covarianceMatrix"/> is not zero indexed,
        /// if <paramref name="covarianceMatrix"/> is not symmetric,
        /// if <paramref name="covarianceMatrix"/> is not positive definite,
        /// if <paramref name="covarianceMatrix"/> is not a valid covariance matrix.
        /// </exception>
        internal static double[] PackedCholeskyFactorCovarianceMatrix(double[,] covarianceMatrix)
        {
            AssertValidCovarianceMatrix(covarianceMatrix);
            return PackedCholeskyDecomposition(covarianceMatrix);
        }

        /// <summary>
        /// Performs the cholesky decomposition of the provided matrix,
        /// and returns it in row major packed form.
        /// </summary>
        /// <param name="correlationMatrix">
        /// The correlation matrix to factor.
        /// </param>
        /// <returns>
        /// The factored correlation matrix.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="correlationMatrix"/> is not zero indexed,
        /// if <paramref name="correlationMatrix"/> is not symmetric,
        /// if <paramref name="correlationMatrix"/> is not positive definite,
        /// if <paramref name="correlationMatrix"/> is not a valid covariance matrix.
        /// </exception>
        internal static double[] PackedCholeskyFactorCorrelationMatrix(double[,] correlationMatrix)
        {
            AssertValidCorrelationMatrix(correlationMatrix);
            return PackedCholeskyDecomposition(correlationMatrix);
        }

        private static double[] PackedCholeskyDecomposition(double[,] matrix)
        {
            unsafe
            {
                var rows = matrix.GetLength(0);
                var result = new double[rows * (rows + 1) / 2];
                fixed (double* m = matrix, r = result)
                {
                    PackedCholeskyDecompositionImpl(m, r, rows);
                }

                return result;
            }
        }

        private static unsafe void PackedCholeskyDecompositionImpl(double* matrix, double* result, nint order)
        {
            double* outputRowJ, inputRow, outputRowI;
            double sum;
            nint i, j;

            outputRowI = result;
            for (i = 0; i < order; i++)
            {
                inputRow = matrix + order * i;
                outputRowI += i;
                outputRowJ = result;
                for (j = 0; j < i; j++)
                {
                    outputRowJ += j;
                    outputRowI[j] = 1.0 / outputRowJ[j] * (*inputRow - Dot(outputRowI, outputRowI + j, outputRowJ));
                    ++inputRow;
                }

                outputRowJ += i;
                sum = *inputRow - Dot(outputRowI, outputRowI + i, outputRowJ);
                if (sum <= 0d)
                {
                    throw new ArgumentException("expected positive definite matrix.");
                }

                outputRowI[i] = Math.Sqrt(sum);
            }

            static double Dot(double* l, double* lend, double* r)
            {
                var sum = 0d;
                while (l != lend)
                {
                    sum += *l * *r;
                    ++l;
                    ++r;
                }

                return sum;
            }
        }

        private static void CheckDimensions(double[,] matrix)
        {
            if (matrix.GetLowerBound(0) != 0 || matrix.GetLowerBound(1) != 0)
            {
                throw new ArgumentException("matrix must be zero indexed");
            }

            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("matrix must be square");
            }
        }

        private static void AssertValidCorrelationMatrix(double[,] matrix)
        {
            CheckDimensions(matrix);
            var rows = matrix.GetLength(0);
            for (var r = 0; r < rows; ++r)
            {
                for (var c = 0; c < r; ++c)
                {
                    var corr = matrix[r, c];
                    if (corr < -1d || corr > 1d || corr != matrix[c, r])
                    {
                        throw new ArgumentException("matrix must be symmetric and only contain values in range [-1, 1]");
                    }
                }

                if (matrix[r, r] != 1d)
                {
                    throw new ArgumentException("diagonal entries must equal one");
                }
            }
        }

        private static void AssertValidCovarianceMatrix(double[,] matrix)
        {
            CheckDimensions(matrix);
            var rows = matrix.GetLength(0);
            for (var r = 0; r < rows; ++r)
            {
                for (var c = 0; c < r; ++c)
                {
                    var cov = matrix[r, c];
                    if (!double.IsFinite(cov) || cov != matrix[c, r])
                    {
                        throw new ArgumentException("matrix must be symmetric and have finite values.");
                    }
                }

                var variance = matrix[r, r];
                if (!double.IsFinite(variance) || variance <= 0d)
                {
                    throw new ArgumentException("diagonal entries must be positive and finite.");
                }
            }
        }
    }
}
