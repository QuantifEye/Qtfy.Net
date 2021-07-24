// <copyright file="Constants.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    /// <summary>
    /// A static class containing various mathematical constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The square root of two pi. That is <c>Math.Sqrt(2d * Math.PI)</c>.
        /// </summary>
        public const double SqrtTwoPi =
            2.506628274631000502415765284811045253006986740609938316629923576342293654607841974946595838378057266;

        /// <summary>
        /// The log of the square root of two pi. That is <c>Math.Log(Math.Sqrt(2d * Math.PI))</c>.
        /// </summary>
        public const double LogSqrtTwoPi =
            0.9189385332046727417803297364056176398613974736377834128171515404827656959272603976947432986359541976;

        /// <summary>
        /// The square root of two. That is <c>Math.Sqrt(2d)</c>.
        /// </summary>
        public const double SqrtTwo =
            1.4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727;

        /// <summary>
        /// The natural logarithm of two multiplied by two.
        /// </summary>
        public const double TwoLnTwo =
            1.386294361119890618834464242916353136151000268720510508241360018986787243939389431211726653992837375;
    }
}
