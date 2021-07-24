// <copyright file="Impl.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Random.Samplers
{
    using System;
    using MathNet.Numerics.LinearAlgebra;

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
        /// if <paramref name="covarianceMatrix"/> is empty,
        /// if <paramref name="covarianceMatrix"/> is not positive definite.
        /// if <paramref name="covarianceMatrix"/> contain any non-finite values.
        /// </exception>
        internal static double[] PackedCholeskyFactorCovarianceMatrix(double[,] covarianceMatrix)
        {
            CheckDimensions(covarianceMatrix);
            CheckCovarianceValues(covarianceMatrix);
            return FactorMatrix(covarianceMatrix);
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
        internal static double[] PackedCholeskyFactorCorrelationMatrix(double[,] correlationMatrix)
        {
            CheckDimensions(correlationMatrix);
            CheckCorrelationValues(correlationMatrix);
            return FactorMatrix(correlationMatrix);
        }

        /// <summary>
        /// Performs a Cholesky decomposition.
        /// </summary>
        /// <returns>
        /// The result of the Cholesky decomposition stored in a packed triangular array.
        /// </returns>
        /// <param name="matrix">
        /// The matrix to be factored.
        /// </param>
        /// <exception cref="ArgumentNullException">If <paramref name="matrix"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="matrix"/> is not a square matrix.</exception>
        /// <exception cref="ArgumentException">If <paramref name="matrix"/> is not positive definite.</exception>
        private static double[] FactorMatrix(double[,] matrix)
        {
            var cov = Matrix<double>.Build.DenseOfArray(matrix);
            var rows = cov.RowCount;
            var factor = cov.Cholesky().Factor;
            var result = new double[(rows * (rows + 1)) / 2];
            for (int r = 0, d = 0; r < rows; ++r)
            {
                for (var c = 0; c <= r; ++c, ++d)
                {
                    result[d] = factor[r, c];
                }
            }

            return result;
        }

        /// <summary>
        /// Checks the dimensions of a matrix and its indexation (it should start to count entries at zero).
        /// </summary>
        /// <param name="matrix">
        /// The matrix to be checked.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="matrix"/> is not square,
        /// of if <paramref name="matrix"/> is empty,
        /// or if <paramref name="matrix"/> is not zero indexed.
        /// </exception>
        private static void CheckDimensions(double[,] matrix)
        {
            if (matrix.GetLowerBound(0) != 0 || matrix.GetLowerBound(1) != 0)
            {
                throw new ArgumentException("matrix must be zero indexed");
            }

            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            if (rows != columns)
            {
                throw new ArgumentException("matrix must be square");
            }

            if (rows < 1)
            {
                throw new ArgumentException("matrix must not be empty.");
            }
        }

        /// <summary>
        /// Checks the properties of a correlation matrix, i.e. whether it is a symmetric matrix and contains diagonal values equal to one.
        /// </summary>
        /// <param name="matrix">
        /// The matrix to be checked.
        /// </param>
        /// <exception cref="ArgumentException">
        /// if <paramref name="matrix"/> is not symmetric,
        /// or if <paramref name="matrix"/> has diagonal elements that are not equal to one.
        /// of if <paramref name="matrix"/> any non-diagonal elements are greater than one or less than minus one.
        /// </exception>
        private static void CheckCorrelationValues(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            for (var r = 0; r < rows; ++r)
            {
                for (var c = 0; c < r; ++c)
                {
                    var corr = matrix[r, c];
                    if (corr < -1d || corr > 1d || corr != matrix[c, r])
                    {
                        throw new ArgumentException("Matrix must be symmetric and values must be in range [-1.0, 1.0].");
                    }
                }

                if (matrix[r, r] != 1d)
                {
                    throw new ArgumentException("Diagonal values must equal to 1.0.");
                }
            }
        }

        /// <summary>
        /// Checks the properties of a covariance matrix, i.e. whether
        /// it is a symmetric matrix and contains positive diagonal values.
        /// </summary>
        /// <param name="matrix">
        /// The matrix to be checked.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="matrix"/> is not symmetric.
        /// or if any values in <paramref name="matrix"/> are less than zero,
        /// or if any values in <paramref name="matrix"/> are double.NaN,
        /// of if any values in <paramref name="matrix"/> infinite.
        /// </exception>
        private static void CheckCovarianceValues(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            for (var r = 0; r < rows; ++r)
            {
                for (var c = 0; c < r; ++c)
                {
                    if (matrix[r, c] != matrix[c, r])
                    {
                        throw new ArgumentException("Matrix must be symmetric.");
                    }
                }

                var variance = matrix[r, r];
                if (!double.IsFinite(variance) || variance <= 0d)
                {
                    throw new ArgumentException("Diagonal values must be positive.");
                }
            }
        }
    }
}
