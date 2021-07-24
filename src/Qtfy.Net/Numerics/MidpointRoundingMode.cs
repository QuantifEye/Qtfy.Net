// <copyright file="MidpointRoundingMode.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    /// <summary>
    /// An enumeration that determines how a number is rounded.
    /// </summary>
    public enum MidpointRoundingMode
    {
        /// <summary>
        /// Indicates that if a number is not a whole number of ticks away from zero, it should be rounded
        /// to the nearest multiple of the specified tick size. If the number is exactly half way
        /// between two such numbers, it is rounded down to the nearest even-multiple of the specified tick
        /// size.
        /// </summary>
        ToEven,

        /// <summary>
        /// Indicates that if a number is not a whole number of ticks away from zero, it should be rounded
        /// to the nearest multiple of the specified tick size. If the number is exactly half way
        /// between two such numbers, it is rounded up to the nearest multiple of the specified tick size.
        /// </summary>
        Up,

        /// <summary>
        /// Indicates that if a number is not a whole number of ticks away from zero, it should be rounded
        /// to the nearest multiple of the specified tick size. If the number is exactly half way
        /// between two such numbers, it is rounded down to the nearest multiple of the specified tick size.
        /// </summary>
        Down,

        /// <summary>
        /// Indicates that if a number is not a whole number of ticks away from zero, it should be rounded
        /// to the nearest multiple of the specified tick size. If the number is exactly half way
        /// between two such numbers, it is rounded away from zero to the nearest multiple of the specified
        /// tick size.
        /// </summary>
        AwayFromZero,

        /// <summary>
        /// Indicates that if a number is not a whole number of ticks away from zero, it should be rounded
        /// to the nearest multiple of the specified tick size. If the number is exactly half way
        /// between two such numbers, it is rounded towards zero to the nearest multiple of the specified
        /// tick size.
        /// </summary>
        TowardZero,
    }
}
