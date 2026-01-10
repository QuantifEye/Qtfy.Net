// <copyright file="IMath.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra;

/// <summary>
/// Defines the math operations required by linear algebra algorithms.
/// </summary>
/// <typeparam name="T">
/// The element type.
/// </typeparam>
public interface IMath<T>
{
    /// <summary>
    /// Gets the additive identity.
    /// </summary>
    static abstract T Zero { get; }

    /// <summary>
    /// Gets the multiplicative identity.
    /// </summary>
    static abstract T One { get; }

    /// <summary>
    /// Adds two values.
    /// </summary>
    /// <param name="left">
    /// The left operand.
    /// </param>
    /// <param name="right">
    /// The right operand.
    /// </param>
    /// <returns>
    /// The sum of the operands.
    /// </returns>
    static abstract T Add(T left, T right);

    /// <summary>
    /// Subtracts two values.
    /// </summary>
    /// <param name="left">
    /// The left operand.
    /// </param>
    /// <param name="right">
    /// The right operand.
    /// </param>
    /// <returns>
    /// The difference of the operands.
    /// </returns>
    static abstract T Subtract(T left, T right);

    /// <summary>
    /// Multiplies two values.
    /// </summary>
    /// <param name="left">
    /// The left operand.
    /// </param>
    /// <param name="right">
    /// The right operand.
    /// </param>
    /// <returns>
    /// The product of the operands.
    /// </returns>
    static abstract T Multiply(T left, T right);

    /// <summary>
    /// Divides two values.
    /// </summary>
    /// <param name="left">
    /// The left operand.
    /// </param>
    /// <param name="right">
    /// The right operand.
    /// </param>
    /// <returns>
    /// The quotient of the operands.
    /// </returns>
    static abstract T Divide(T left, T right);

    /// <summary>
    /// Negates a value.
    /// </summary>
    /// <param name="value">
    /// The input value.
    /// </param>
    /// <returns>
    /// The negated value.
    /// </returns>
    static abstract T Negate(T value);
}
