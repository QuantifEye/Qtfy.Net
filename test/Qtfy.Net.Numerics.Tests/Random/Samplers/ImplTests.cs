// <copyright file="ImplTests.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests.Random.Samplers
{
    using System;
    using NUnit.Framework;
    using static Qtfy.Net.Numerics.Random.Samplers.Impl;

    public class ImplTests
    {
        [Test]
        public void TestShapeAndSymmetry()
        {
            static void CheckThrows(string message, double[,] input)
            {
                Assert.Throws<ArgumentException>(() => _ = PackedCholeskyFactorCorrelationMatrix(input), message);
                Assert.Throws<ArgumentException>(() => _ = PackedCholeskyFactorCovarianceMatrix(input), message);
            }

            CheckThrows(
                "not square",
                new[,]
                {
                    { 0.5, 0.5 },
                });

            CheckThrows(
                "not square",
                new[,]
                {
                    { 0.5 },
                    { 0.5 },
                });

            CheckThrows(
                "zero indexing",
                (double[,])Array.CreateInstance(typeof(double), new[] { 1, 1 }, new[] { 0, 1 }));

            CheckThrows(
                "zero indexing",
                (double[,])Array.CreateInstance(typeof(double), new[] { 1, 1 }, new[] { 1, 0 }));

            CheckThrows(
                "not symmetric",
                new[,]
                {
                    { 1.0, 0.8 },
                    { 0.5, 1.0 },
                });

            CheckThrows(
                "not symmetric due to nan",
                new[,]
                {
                    { 1.0, double.NaN },
                    { 0.5, 1.0 },
                });

            CheckThrows(
                "symmetric nans",
                new[,]
                {
                    { 1.0, double.NaN },
                    { double.NaN, 1.0 },
                });

            CheckThrows(
                "nan on diagonal",
                new[,]
                {
                    { double.NaN, 0.5 },
                    { 0.5, 1.0 },
                });
        }

        [Test]
        public void TestNonDefinite()
        {
            var negativeDefinite = new[,]
            {
                { 1.0, -1.1 },
                { -1.1, 1.0 },
            };

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(negativeDefinite), "non definite");
        }

        [Test]
        public void TestPositiveSemiDefinite()
        {
            var semiPositiveDefinite = new[,]
            {
                { 1.0, 1.0 },
                { 1.0, 1.0 },
            };

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(semiPositiveDefinite), "semi positive definite");
        }

        [Test]
        public void TestPackedCholeskyFactorMatrix()
        {
            var input = new[,]
            {
                { 1.0, 0.5, 0.5 },
                { 0.5, 1.0, 0.5 },
                { 0.5, 0.5, 1.0 },
            };

            var expected = new[]
            {
                1.0,
                0.5, 0.8660254037844386,
                0.5, 0.28867513459481292, 0.81649658092772603,
            };

            Assert.AreEqual(PackedCholeskyFactorCorrelationMatrix(input), expected);
            Assert.AreEqual(PackedCholeskyFactorCovarianceMatrix(input), expected);

            input = new[,]
            {
                { 1.0, 0.5 },
                { 0.5, 1.0 },
            };

            expected = new[]
            {
                1.0,
                0.5, 0.8660254037844386,
            };

            Assert.AreEqual(PackedCholeskyFactorCorrelationMatrix(input), expected);
            Assert.AreEqual(PackedCholeskyFactorCovarianceMatrix(input), expected);

            input = new[,]
            {
                { 1.0 },
            };

            expected = new[]
            {
                1.0,
            };

            Assert.AreEqual(PackedCholeskyFactorCorrelationMatrix(input), expected);
            Assert.AreEqual(PackedCholeskyFactorCovarianceMatrix(input), expected);
        }

        [TestCase(1.1)]
        [TestCase(-1.1)]
        [TestCase(0.9)]
        [TestCase(-0.9)]
        [TestCase(0d)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        public void TestInvalidCorrelationValuesOnDiagonal(double value)
        {
            Assert.Throws<ArgumentException>(() => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
            {
                { value, 0.5 },
                { 0.5, 1.0 },
            }));

            Assert.Throws<ArgumentException>(() => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
            {
                { 1.0, 0.5 },
                { 0.5, value },
            }));
        }

        [TestCase(1.1)]
        [TestCase(-1.1)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.NaN)]
        public void TestInvalidCorrelationValues(double value)
        {
            Assert.Throws<ArgumentException>(() => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
            {
                { 1.0, value },
                { value, 1.0 },
            }));
        }

        [TestCase(-1.0)]
        [TestCase(0.0)]
        [TestCase(double.NegativeInfinity)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NaN)]
        public void TestInvalidCovarianceOnDiagonal(double value)
        {
            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.1, 2.0 },
                    { 2.0, value },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { value, 2.0 },
                    { 2.0, 1.1 },
                }));
        }

        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        public void TestInvalidCovarianceValues(double value)
        {
            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.1, value },
                    { value, 1.1 },
                }));
        }
    }
}
