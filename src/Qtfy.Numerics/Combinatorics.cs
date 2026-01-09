// <copyright file="Combinatorics.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics;

/// <summary>
/// A collection of methods that provide combinatorial tools.
/// </summary>
public static class Combinatorics
{
    /// <summary>
    /// Returns the power set of the distinct elements from a sequence by using the default
    /// equality comparer to compare values.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements of the source sequence.
    /// </typeparam>
    /// <param name="sourceElements">
    /// The sequence whose power set must be enumerated.
    /// </param>
    /// <returns>
    /// A sequence of arrays where each array is one of the items in the power set of the provided sequence.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="sourceElements"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="sourceElements"/> has more than 63 distinct elements.
    /// </exception>
    /// <example>
    /// <code>
    /// int[] sourceElements = [1, 2, 3];
    /// var result = PowerSet(sourceElements).ToArray();
    /// int[][] expected =
    /// [
    ///    [],
    ///    [1],
    ///    [2],
    ///    [1, 2],
    ///    [3],
    ///    [1, 3],
    ///    [2, 3],
    ///    [1, 2, 3],
    /// ];
    /// </code>
    /// </example>
    public static IEnumerable<T[]> PowerSet<T>(IEnumerable<T> sourceElements)
    {
        ArgumentNullException.ThrowIfNull(sourceElements);

        var elements = sourceElements.Distinct().ToArray();
        if (elements.Length == 0)
        {
            return [[]];
        }

        var size = elements.Length;
        return PowerSetIterator(elements, PowerSetSize(size), new T[size]);
    }

    /// <summary>
    /// Returns the power set of the distinct elements from a sequence by using
    /// a specified <see cref="IEqualityComparer{T}"/> to compare values.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements of the source sequence.
    /// </typeparam>
    /// <param name="sourceElements">
    /// The sequence whose power set must be enumerated.
    /// </param>
    /// <param name="equalityComparer">
    /// The <see cref="IEqualityComparer{T}"/> used to compare values.
    /// </param>
    /// <returns>
    /// A sequence of arrays where each array is one of the items in the power set of the provided sequence.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="sourceElements"/> is null.
    /// If <paramref name="equalityComparer"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="sourceElements"/> has more than 63 distinct elements.
    /// </exception>
    public static IEnumerable<T[]> PowerSet<T>(IEnumerable<T> sourceElements, IEqualityComparer<T> equalityComparer)
    {
        ArgumentNullException.ThrowIfNull(sourceElements);
        ArgumentNullException.ThrowIfNull(equalityComparer);

        var elements = sourceElements.Distinct(equalityComparer).ToArray();
        var size = elements.Length;
        return PowerSetIterator(elements, PowerSetSize(size), new T[size]);
    }

    /// <summary>
    /// Iterates all possible ways to split a set into two groups (left and right).
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the set.
    /// </typeparam>
    /// <param name="sourceElements">
    /// A collection of elements.
    /// </param>
    /// <returns>
    /// A sequence that iterates all possible ways to split a set into two groups (left and right).
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="sourceElements"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="sourceElements"/> has more than 63 distinct elements.
    /// </exception>
    /// <example>
    /// <code>
    /// int[] sourceElements = [1, 2, 3];
    /// var result = PowerSetWithComplement(sourceElements).ToArray();
    /// (int[] left, int[] right)[] expected =
    /// [
    ///    ([], [1, 2, 3]),
    ///    ([1], [2, 3]),
    ///    ([2], [1, 3]),
    ///    ([1, 2], [3]),
    ///    ([3], [1, 2]),
    ///    ([1, 3], [2]),
    ///    ([2, 3], [1]),
    ///    ([1, 2, 3], []),
    /// ];
    /// </code>
    /// </example>
    public static IEnumerable<(T[] left, T[] right)> PowerSetWithComplement<T>(IEnumerable<T> sourceElements)
    {
        ArgumentNullException.ThrowIfNull(sourceElements);

        var elements = sourceElements.Distinct().ToArray();
        var size = elements.Length;
        return PowerSetIterator(elements, PowerSetSize(size), new T[size], new T[size]);
    }

    /// <summary>
    /// Iterates all possible ways to split a set into two groups (left and right), using the provided <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the set.
    /// </typeparam>
    /// <param name="sourceElements">
    /// A collection of elements.
    /// </param>
    /// <param name="equalityComparer">
    /// The <see cref="IEqualityComparer{T}"/> used to compare values.
    /// </param>
    /// <returns>
    /// A sequence that iterates all possible ways to
    /// split a set into two groups (left and right), using the provided <see cref="IEqualityComparer{T}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="sourceElements"/> is null.
    /// If <paramref name="equalityComparer"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="sourceElements"/> has more than 63 distinct elements.
    /// </exception>
    public static IEnumerable<(T[] left, T[] right)> PowerSetWithComplement<T>(
        IEnumerable<T> sourceElements,
        IEqualityComparer<T> equalityComparer)
    {
        ArgumentNullException.ThrowIfNull(sourceElements);
        ArgumentNullException.ThrowIfNull(equalityComparer);

        var elements = sourceElements.Distinct(equalityComparer).ToArray();
        var size = elements.Length;
        return PowerSetIterator(elements, PowerSetSize(size), new T[size], new T[size]);
    }

    private static ulong PowerSetSize(int setSize)
    {
        if (setSize > 63)
        {
            throw new ArgumentException($"sourceElements must not have more than 63 distinct elements.");
        }

        return (ulong)BigInteger.Pow(2, setSize);
    }

    private static IEnumerable<T[]> PowerSetIterator<T>(T[] elements, ulong powerSetSize, T[] powerSetBuffer)
    {
        for (var bits = 0UL; bits < powerSetSize; bits++)
        {
            yield return Current(elements, bits, powerSetBuffer);
        }
    }

    private static IEnumerable<(T[], T[])> PowerSetIterator<T>(
        T[] elements,
        ulong powerSetSize,
        T[] powerSetBuffer,
        T[] complementBuffer)
    {
        for (var bits = 0UL; bits < powerSetSize; bits++)
        {
            yield return Current(elements, bits, powerSetBuffer, complementBuffer);
        }
    }

    private static T[] Current<T>(T[] elements, ulong bits, T[] powerSetBuffer)
    {
        var powerSetCount = 0;
        for (var i = 0; i < elements.Length; i++)
        {
            if ((bits & (1UL << i)) != 0)
            {
                powerSetBuffer[powerSetCount] = elements[i];
                ++powerSetCount;
            }
        }

        return GetFirst(powerSetBuffer, powerSetCount);
    }

    private static (T[], T[]) Current<T>(T[] elements, ulong bits, T[] powerSetBuffer, T[] complementBuffer)
    {
        var powerSetCount = 0;
        var complementCount = 0;
        for (var i = 0; i < elements.Length; i++)
        {
            if ((bits & (1UL << i)) != 0)
            {
                powerSetBuffer[powerSetCount] = elements[i];
                ++powerSetCount;
            }
            else
            {
                complementBuffer[complementCount] = elements[i];
                ++complementCount;
            }
        }

        return (GetFirst(powerSetBuffer, powerSetCount), GetFirst(complementBuffer, complementCount));
    }

    private static T[] GetFirst<T>(T[] buffer, int count)
    {
        if (count == 0)
        {
            return [];
        }
        else
        {
            var result = new T[count];
            Array.Copy(buffer, result, count);
            return result;
        }
    }
}
