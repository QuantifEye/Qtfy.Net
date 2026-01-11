// <copyright file="AddressMath.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Memory;

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/// <summary>
/// Provides alignment helpers for native addresses.
/// </summary>
public struct AddressMath
{
    /// <summary>
    /// Tries to clamp an element range to vector-aligned bounds.
    /// </summary>
    /// <typeparam name="T">
    /// The element type.
    /// </typeparam>
    /// <param name="begin">
    /// The start of the element range.
    /// </param>
    /// <param name="end">
    /// The end of the element range.
    /// </param>
    /// <param name="vBegin">
    /// The aligned vector start.
    /// </param>
    /// <param name="vEnd">
    /// The aligned vector end.
    /// </param>
    /// <returns>
    /// <see langword="true" /> when the range can be aligned for vector operations; otherwise
    /// <see langword="false" />.
    /// </returns>
    public static unsafe bool TryGetVectorAlignedRange<T>(
        T* begin,
        T* end,
        out Vector<T> * vBegin,
        out Vector<T> * vEnd)
        where T : unmanaged
    {
        if (Vector<T>.IsSupported && Vector.IsHardwareAccelerated)
        {
            var u = AlignUpTo<Vector<T>>((nuint)begin);
            var d = AlignDownTo<Vector<T>>((nuint)end);
            if (u != 0 && u < d)
            {
                vBegin = (Vector<T> *)u;
                vEnd = (Vector<T> *)d;
                return true;
            }
        }

        vBegin = null;
        vEnd = null;
        return false;
    }

    /// <summary>
    /// Aligns an address up to the size of <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    /// The element type.
    /// </typeparam>
    /// <param name="address">
    /// The address to align.
    /// </param>
    /// <returns>
    /// The aligned address.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint AlignUpTo<T>(nuint address)
        where T : unmanaged
    {
        var size = (nuint)Unsafe.SizeOf<T>();
        if (!BitOperations.IsPow2((ulong)size))
        {
            throw new InvalidOperationException();
        }

        var mask = size - 1;
        var invMask = ~mask;
        return (address + mask) & invMask;
    }

    /// <summary>
    /// Aligns an address down to the size of <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    /// The element type.
    /// </typeparam>
    /// <param name="address">
    /// The address to align.
    /// </param>
    /// <returns>
    /// The aligned address.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint AlignDownTo<T>(nuint address)
        where T : unmanaged
    {
        var size = (nuint)Unsafe.SizeOf<T>();

        if (!BitOperations.IsPow2((ulong)size))
        {
            throw new InvalidOperationException();
        }

        var mask = size - 1;
        var invMask = ~mask;
        return address & invMask;
    }

    /// <summary>
    /// Aligns an address up to the specified alignment.
    /// </summary>
    /// <param name="address">
    /// The address to align.
    /// </param>
    /// <param name="alignment">
    /// The alignment, in bytes.
    /// </param>
    /// <returns>
    /// The aligned address.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint AlignUpTo(nuint address, nuint alignment)
    {
        Debug.Assert(alignment != 0 && BitOperations.IsPow2((ulong)alignment), "Alignment must be a power of two.");
        var mask = alignment - 1;
        var invMask = ~mask;
        return (address + mask) & invMask;
    }

    /// <summary>
    /// Aligns an address down to the specified alignment.
    /// </summary>
    /// <param name="address">
    /// The address to align.
    /// </param>
    /// <param name="alignment">
    /// The alignment, in bytes.
    /// </param>
    /// <returns>
    /// The aligned address.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint AlignDownTo(nuint address, nuint alignment)
    {
        Debug.Assert(alignment != 0 && BitOperations.IsPow2((ulong)alignment), "Alignment must be a power of two.");
        var mask = alignment - 1;
        var invMask = ~mask;
        return address & invMask;
    }

    /// <summary>
    /// Computes the natural alignment for <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    /// The element type.
    /// </typeparam>
    /// <returns>
    /// The alignment, in bytes.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nuint GetAlignment<T>()
        where T : unmanaged
    {
        var probe = default(AlignmentProbe<T>);
        ref byte padding = ref Unsafe.As<AlignmentProbe<T>, byte>(ref probe);
        ref T value = ref probe.Value;
        var offset = Unsafe.ByteOffset(ref padding, ref Unsafe.As<T, byte>(ref value));
        return (nuint)offset;
    }

    /// <summary>
    /// Checks if the address is aligned to the size of <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    /// The element type.
    /// </typeparam>
    /// <param name="address">
    /// The address to check.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the address is aligned; otherwise <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlignedTo<T>(nuint address)
        where T : unmanaged
    {
        var size = (nuint)Unsafe.SizeOf<T>();
        if (!BitOperations.IsPow2((ulong)size))
        {
            throw new InvalidOperationException();
        }

        var mask = size - 1;
        return (address & mask) == 0;
    }

    /// <summary>
    /// Checks if the address is aligned to the specified alignment.
    /// </summary>
    /// <param name="address">
    /// The address to check.
    /// </param>
    /// <param name="alignment">
    /// The alignment, in bytes.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the address is aligned; otherwise <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlignedTo(nuint address, nuint alignment)
    {
        Debug.Assert(alignment != 0 && BitOperations.IsPow2((ulong)alignment), "Alignment must be a power of two.");
        var mask = alignment - 1;
        return (address & mask) == 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct AlignmentProbe<T>
        where T : unmanaged
    {
        public byte Padding;
        public T Value;
    }
}
