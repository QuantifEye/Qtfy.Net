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
        public void TestPackedCholeskyFactorCorrelationMatrix()
        {
            var correlation = new[,]
            {
                { 1.0, 0.5, 0.5 },
                { 0.5, 1.0, 0.5 },
                { 0.5, 0.5, 1.0 },
            };

            var actual = PackedCholeskyFactorCorrelationMatrix(correlation);
            var expected = new[]
            {
                1.0,
                0.5, 0.8660254037844386,
                0.5, 0.28867513459481292, 0.81649658092772603,
            };

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void TestInvalidCorrelationMatrix()
        {
            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
                {
                    { 1.0, 0.8 },
                    { 0.5, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
                {
                    { 1.0, -1.1 },
                    { -1.1, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
                {
                    { 1.0, double.NaN },
                    { double.NaN, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCorrelationMatrix(new[,]
                {
                    { 1.1, 0.5 },
                    { 0.5, 1.0 },
                }));
        }

        [Test]
        public void TestInvalidCovarianceMatrix()
        {
            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.0, 0.8 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new double[0, 0] { }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.0, 0.8 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(
                    (double[,])Array.CreateInstance(typeof(double), new[] { 1, 1 }, new[] { 1, 1 })));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.0, 0.8 },
                    { 0.5, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.0, -1.1 },
                    { -1.1, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { -1.0, 0.5 },
                    { 0.5, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { 1.0, double.NaN },
                    { double.NaN, 1.0 },
                }));

            Assert.Throws<ArgumentException>(
                () => _ = PackedCholeskyFactorCovarianceMatrix(new[,]
                {
                    { double.NaN, 0.5 },
                    { 0.5, 1.0 },
                }));
        }
    }
}
