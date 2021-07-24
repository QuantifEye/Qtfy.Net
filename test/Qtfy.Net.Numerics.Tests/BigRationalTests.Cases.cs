// <copyright file="BigRationalTests.Cases.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public partial class BigRationalTests
    {
        private static readonly IEnumerable<object[]> LessThanCaseValues = new[]
        {
            new object[] { new BigRational(3, 2), new BigRational(5, 2) },
            new object[] { new BigRational(-3, 2), new BigRational(5, 2) },
            new object[] { new BigRational(-5, 2), new BigRational(-3, 2) },
        };

        private static readonly IEnumerable<object[]> UnequalCaseValues = LessThanCaseValues
            .Concat(LessThanCaseValues.Select(arr => new[] { arr[1], arr[0] }))
            .ToArray();

        private static readonly IEnumerable<object[]> EqualCaseValues = new[]
        {
            new object[] { new BigRational(5, 2), new BigRational(5, 2) },
            new object[] { new BigRational(-5, 2), new BigRational(-5, 2) },
        };

        [SuppressMessage("Microsoft.Performance", "CA1812", Justification = "class is instantiated by unit testing")]
        private class LessThanCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
                => LessThanCaseValues.GetEnumerator();
        }

        [SuppressMessage("Microsoft.Performance", "CA1812", Justification = "class is instantiated by unit testing")]
        private class UnequalCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
                => UnequalCaseValues.GetEnumerator();
        }

        [SuppressMessage("Microsoft.Performance", "CA1812", Justification = "class is instantiated by unit testing")]
        private class EqualCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
                => EqualCaseValues.GetEnumerator();
        }
    }
}
