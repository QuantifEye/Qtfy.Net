// <copyright file="BigRationalTests.ArithmeticOperators.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System;
    using System.Numerics;
    using NUnit.Framework;

    public partial class BigRationalTests
    {
        [TestCase("1/2", "-1/2")]
        [TestCase("-1/2", "1/2")]
        public void UnaryMinus(string input, string expected)
        {
            var rational = BigRational.Parse(input);
            AssertEqual(
                BigRational.Parse(expected),
                -rational);
        }

        [TestCase("1/2")]
        [TestCase("-1/2")]
        public void UnaryPlus(string input)
        {
            var rational = BigRational.Parse(input);
            AssertEqual(rational, +rational);
        }

        [TestCase("1/2", "3/2")]
        [TestCase("-1/2", "1/2")]
        [TestCase("5/2", "7/2")]
        [TestCase("-5/2", "-3/2")]
        public void IncrementTest(string input, string expected)
        {
            var rational = BigRational.Parse(input);
            AssertEqual(
                BigRational.Parse(expected),
                ++rational);
        }

        [TestCase("1/2", "-1/2")]
        [TestCase("-1/2", "-3/2")]
        [TestCase("5/2", "3/2")]
        [TestCase("-5/2", "-7/2")]
        public void DecrementTest(string input, string expected)
        {
            var rational = BigRational.Parse(input);
            AssertEqual(
                BigRational.Parse(expected),
                --rational);
        }

        [TestCase("1/2", "-1/2", "0")]
        [TestCase("1/2", "1/2", "1")]
        [TestCase("10", "105/10", "205/10")]
        [TestCase("10", "-105/10", "-1/2")]
        public void TestAddition(string left, string right, string expected)
        {
            AssertEqual(
                BigRational.Parse(expected),
                BigRational.Parse(left) + BigRational.Parse(right));
        }

        [TestCase("1/2", "1/2", "0")]
        [TestCase("5/2", "1/2", "4/2")]
        [TestCase("-5/2", "1/2", "-6/2")]
        [TestCase("5/2", "-1/2", "6/2")]
        public void TestSubtraction(string left, string right, string expected)
        {
            AssertEqual(
                BigRational.Parse(expected),
                BigRational.Parse(left) - BigRational.Parse(right));
        }

        [TestCase("1/2", "1/2", "1/4")]
        [TestCase("5/2", "3/2", "15/4")]
        [TestCase("-5/2", "-3/2", "15/4")]
        [TestCase("-5/2", "3/2", "-15/4")]
        [TestCase("5/2", "-3/2", "-15/4")]
        public void TestMultiplication(string left, string right, string expected)
        {
            AssertEqual(
                BigRational.Parse(expected),
                BigRational.Parse(left) * BigRational.Parse(right));
        }

        [TestCase("1/2", "1/2", "1")]
        [TestCase("5/2", "3/2", "10/6")]
        [TestCase("-5/2", "3/2", "-10/6")]
        [TestCase("5/2", "-3/2", "-10/6")]
        public void TestDivision(string left, string right, string expected)
        {
            AssertEqual(
                BigRational.Parse(expected),
                BigRational.Parse(left) / BigRational.Parse(right));
        }

        [Test]
        public void TestDivideError()
        {
            var left = new BigRational(1, 2);
            var right = new BigRational(0);
            BigRational expected = default;
            Assert.Throws<DivideByZeroException>(
                () => expected = left / right);
        }

        [TestCase("3/2", "1", "1/2")]
        public void TestModulus(string left, string right, string expected)
        {
            AssertEqual(
                BigRational.Parse(expected),
                BigRational.Parse(left) % BigRational.Parse(right));
        }

        /// <summary>
        /// Checks that mod works the same as BigInteger mod for integer values rationals.
        /// </summary>
        [TestCase(5, 3)]
        [TestCase(-5, 3)]
        [TestCase(5, -3)]
        [TestCase(-5, -3)]
        [TestCase(3, 5)]
        [TestCase(-3, 5)]
        [TestCase(3, -5)]
        [TestCase(-3, -5)]
        public void CheckModBigIntegerConsistency(int left, int right)
        {
            var leftInt = (BigInteger)left;
            var rightInt = (BigInteger)right;
            var expected = new BigRational(leftInt % rightInt);

            var leftRational = new BigRational(leftInt);
            var rightRational = new BigRational(rightInt);
            var actual = leftRational % rightRational;

            AssertEqual(expected, actual);
        }
    }
}
