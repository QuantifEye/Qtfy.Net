// <copyright file="ArrayTools.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Net.Numerics
{
    /// <summary>
    /// A collection of array extensions.
    /// </summary>
    public static class ArrayTools
    {
        /// <summary>
        /// Copies the provided array.
        /// </summary>
        /// <param name="self">
        /// The array to copy.
        /// </param>
        /// <typeparam name="T">
        /// The type of the elements in the matrix.
        /// </typeparam>
        /// <returns>
        /// A copy of the provided array.
        /// </returns>
        internal static T[] Copy<T>(this T[] self)
        {
            return (T[])self.Clone();
        }
    }
}
