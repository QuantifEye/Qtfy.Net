// <copyright file="PiecewiseConstantDistribution.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Distributions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A piecewise constant distribution is described distribution which is uniformly distributed within sub intervals.
    /// This can be thought of as being analogous to the distribution implied by a histogram.
    /// </summary>
    /// <para>
    /// A piecewise constant distribution is described by n distinct domain points,
    /// where n is greater than 1, and n - 1 non-negative weight.
    /// let S be the sum of all weights.
    /// let w_i be the weight assigned to interval i.
    /// let i_lower be the lower bound of interval i.
    /// let i_upper be the upper bound of an interval i.
    /// Then the probability that a random variable will fall in interval i is equal to
    /// w_i / (S * (i_upper - i_lower)).
    /// </para>
    public class PiecewiseConstantDistribution : IDistribution<double>
    {
        private readonly double[] boundaries;

        private readonly double[] cumulativeProbabilities;

        private PiecewiseConstantDistribution(double[] boundaries, double[] cumulativeProbabilities)
        {
            this.boundaries = boundaries;
            this.cumulativeProbabilities = cumulativeProbabilities;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PiecewiseConstantDistribution"/> class.
        /// </summary>
        /// <param name="domain">
        /// A sequence of strictly monotonically increasing values.
        /// </param>
        /// <param name="weights">
        /// A sequence of non negative weights.
        /// </param>
        /// <returns>
        /// Returns a new <see cref="PiecewiseConstantDistribution"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="domain"/> is null.
        /// If <paramref name="weights"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the number of elements in <paramref name="domain"/> is less than 2.
        /// If the number of element in <paramref name="weights"/> is not one less than the number of elements in <paramref name="domain"/>.
        /// If <paramref name="domain"/> is not sorted and unique.
        /// If any of the values in <paramref name="weights"/> is less than zero.
        /// </exception>
        public static PiecewiseConstantDistribution Create(IEnumerable<double> domain, IEnumerable<double> weights)
        {
            var b = domain.ToArray();
            var cp = new[] { 0d }.Concat(weights).ToArray();

            if (b.Length < 2 || b.Length != cp.Length)
            {
                throw new ArgumentException("Require at least two boundaries");
            }

            if (!IsStrictlyMonotonic(b))
            {
                throw new ArgumentException("values must be strictly monotonic.");
            }

            if (!AllNonNegative(cp))
            {
                throw new ArgumentException("Weights must be non negative");
            }

            for (var i = 1; i < cp.Length; ++i)
            {
                cp[i] = cp[i - 1] + cp[i];
            }

            var total = cp[^1];
            for (var i = 0; i < cp.Length; ++i)
            {
                cp[i] /= total;
            }

            return new PiecewiseConstantDistribution(b, cp);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">
        /// If <paramref name="probability"/> is not in range [0, 1].
        /// </exception>
        public double Quantile(double probability)
        {
            if (probability >= 0d && probability <= 1d)
            {
                var cumulativeProbabilities = this.cumulativeProbabilities;
                var boundaries = this.boundaries;
                var i = 0;
                for (; i < cumulativeProbabilities.Length; ++i)
                {
                    if (cumulativeProbabilities[i] >= probability)
                    {
                        break;
                    }
                }

                return cumulativeProbabilities[i] == probability
                    ? this.boundaries[i]
                    : LinearInterpolate(cumulativeProbabilities[i - 1], cumulativeProbabilities[i], boundaries[i - 1], boundaries[i], probability);
            }

            throw new ArgumentException("invalid probability");
        }

        /// <inheritdoc/>
        public double CumulativeDistribution(double x)
        {
            if (double.IsNaN(x))
            {
                return double.NaN;
            }

            var boundaries = this.boundaries;
            var cumulativeProbabilities = this.cumulativeProbabilities;
            if (x <= boundaries[0])
            {
                return 0d;
            }

            if (x >= boundaries[^1])
            {
                return 1d;
            }

            var i = Array.BinarySearch(boundaries, 0, boundaries.Length, x);
            if (i < 0)
            {
                i = ~i;
                return LinearInterpolate(boundaries[i - 1], boundaries[i], cumulativeProbabilities[i - 1], cumulativeProbabilities[i], x);
            }
            else
            {
                return cumulativeProbabilities[i];
            }
        }

        /// <summary>
        /// Perform linear interpolation between two points.
        /// </summary>
        /// <returns>
        /// The interpolated value.
        /// </returns>
        /// <param name="x0">
        /// First coordinate of first point.
        /// </param>
        /// <param name="x1">
        /// Second coordinate of first point.
        /// </param>
        /// <param name="y0">
        /// First coordinate of second point.
        /// </param>
        /// <param name="y1">
        /// Second coordinate of second value.
        /// </param>
        /// <param name="x">
        /// First coordinate of new  point.
        /// </param>
        private static double LinearInterpolate(double x0, double x1, double y0, double y1, double x)
        {
            var m = (y1 - y0) / (x1 - x0);
            return y0 + m * (x - x0);
        }

        /// <summary>
        /// Check whether values in array are strictly monotonic.
        /// </summary>
        /// <returns><c>true</c> if the values are strictly monotonic, <c>false</c> otherwise.</returns>
        /// <param name="array">
        /// The array to be checked.
        /// </param>
        private static bool IsStrictlyMonotonic(double[] array)
        {
            for (var i = 1; i < array.Length; ++i)
            {
                if (array[i - 1] >= array[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check whether all entries of an array are non-negative.
        /// </summary>
        /// <returns><c>true</c> if all values are non-negative, <c>false</c> otherwise.</returns>
        /// <param name="array">
        /// The array to be checked.
        /// </param>
        private static bool AllNonNegative(double[] array)
        {
            for (var i = 0; i < array.Length; ++i)
            {
                if (array[i] < 0d)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
