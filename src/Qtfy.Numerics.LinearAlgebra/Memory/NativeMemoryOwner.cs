// <copyright file="NativeMemoryOwner.cs" company="QuantifEye">
// Copyright (c) QuantifEye. All rights reserved.
// Licensed under the Apache 2.0 license.
// See LICENSE.txt file in the project root for full license information.
// </copyright>

namespace Qtfy.Numerics.LinearAlgebra.Memory;

/// <summary>
/// Owns aligned native memory.
/// </summary>
public sealed class NativeMemoryOwner : IDisposable
{
    /// <summary>
    /// The default alignment, in bytes.
    /// </summary>
    public const nuint DefaultAlignment = 64;

    private nuint pointer;

    /// <summary>
    /// Initializes a new instance of the <see cref="NativeMemoryOwner"/> class.
    /// </summary>
    /// <param name="byteLength">
    /// The requested number of bytes.
    /// </param>
    /// <param name="alignment">
    /// The alignment, in bytes.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="byteLength"/> is zero.
    /// If <paramref name="alignment"/> is zero, not a power of two, or smaller than the pointer size.
    /// </exception>
    /// <exception cref="OutOfMemoryException">
    /// If the allocation fails.
    /// </exception>
    public NativeMemoryOwner(nuint byteLength, nuint alignment)
    {
        ArgumentOutOfRangeException.ThrowIfZero(byteLength);

        if (!BitOperations.IsPow2((ulong)alignment))
        {
            throw new ArgumentOutOfRangeException(nameof(alignment));
        }

        ArgumentOutOfRangeException.ThrowIfLessThan(alignment, (nuint)IntPtr.Size);

        var alignedByteLength = AddressMath.AlignUpTo(byteLength, alignment);
        unsafe
        {
            void* allocated = NativeMemory.AlignedAlloc(alignedByteLength, alignment);
            if (allocated == null)
            {
                throw new OutOfMemoryException();
            }

            pointer = (nuint)allocated;
        }

        ByteLength = alignedByteLength;
        Alignment = alignment;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="NativeMemoryOwner"/> class.
    /// </summary>
    ~NativeMemoryOwner()
    {
        Dispose(ref pointer);
    }

    /// <summary>
    /// Gets the allocated size, in bytes (rounded up to the alignment).
    /// </summary>
    public nuint ByteLength { get; }

    /// <summary>
    /// Gets the alignment used for this allocation, in bytes.
    /// </summary>
    public nuint Alignment { get; }

    /// <summary>
    /// Gets the native pointer for this allocation.
    /// </summary>
    /// <returns>
    /// A pointer to the allocation.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// If the owner is disposed.
    /// </exception>
    public nuint Pointer()
    {
        var ptr = pointer;
        if (ptr == 0)
        {
            ThrowDisposed();
        }

        return ptr;
    }

    /// <summary>
    /// Gets a reference to the first element in the allocation.
    /// </summary>
    /// <typeparam name="T">
    /// The element type.
    /// </typeparam>
    /// <returns>
    /// A reference to the first element.
    /// </returns>
    /// <exception cref="ObjectDisposedException">
    /// If the owner is disposed.
    /// </exception>
    public unsafe ref T Reference<T>()
        where T : unmanaged
    {
        return ref AsRef<T>((void*)Pointer());
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(ref pointer);
        GC.SuppressFinalize(this);
    }

    [DoesNotReturn]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void ThrowDisposed()
    {
        throw new ObjectDisposedException(nameof(NativeMemoryOwner));
    }

    private static void Dispose(ref nuint pointer)
    {
        if (pointer == 0)
        {
            return;
        }

        unsafe
        {
            NativeMemory.AlignedFree((void*)pointer);
        }

        pointer = 0;
    }
}
