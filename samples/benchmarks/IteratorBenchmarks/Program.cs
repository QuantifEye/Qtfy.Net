using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<IteratorBenchmarks>();

public interface IIterator<T>
{
    ref T GetNext();

    ref T Current();
}

public ref struct StridedIterator<T> : IIterator<T>
{
    private ref T current;
    private int stride;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref T GetNext()
    {
        this.current = ref Unsafe.Add(ref this.current, this.stride);
        return ref this.current;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref T Current() => ref this.current;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static StridedIterator<T> Create(ref T current, int stride)
    {
        Unsafe.SkipInit(out StridedIterator<T> iterator);
        iterator.current = current;
        iterator.stride = stride;
        return iterator;
    }
}

public ref struct ContigiousIterator<T> : IIterator<T>
{
    private ref T current;

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref T GetNext()
    {
        this.current = ref Unsafe.Add(ref this.current, 1);
        return ref this.current;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public ref T Current() => ref this.current;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ContigiousIterator<T> Create(ref T current)
    {
        Unsafe.SkipInit(out ContigiousIterator<T> iterator);
        iterator.current = current;
        return iterator;
    }
}

public class IteratorBenchmarks
{
    [Params(10)] public int Count;

    [Params(1, 2, 4)] public int Stride;

    private double[] _a = Array.Empty<double>();
    private double[] _b = Array.Empty<double>();

    [GlobalSetup]
    public void Setup()
    {
        var length = ((this.Count - 1) * this.Stride) + 1;
        this._a = new double[length];
        this._b = new double[length];

        for (var i = 0; i < length; i++)
        {
            this._a[i] = i + 1;
            this._b[i] = (i + 1) * 0.25;
        }
    }

    [Benchmark(Baseline = true)]
    public double CountdownWhile()
    {
        var i = this.Count;
        if (i <= 0)
        {
            return 0.0;
        }

        var sum = 0.0;
        ref var aRef = ref this._a[0];
        ref var bRef = ref this._b[0];

        while (--i != 0)
        {
            sum += aRef * bRef;
            aRef = ref Unsafe.Add(ref aRef, this.Stride);
            bRef = ref Unsafe.Add(ref bRef, this.Stride);
        }

        sum += aRef * bRef;
        return sum;
    }

    [Benchmark]
    public double WhileTrueBreak()
    {
        var i = this.Count;
        if (i <= 0)
        {
            return 0.0;
        }

        var sum = 0.0;
        ref var aRef = ref this._a[0];
        ref var bRef = ref this._b[0];

        while (true)
        {
            sum += aRef * bRef;
            if (--i == 0)
            {
                break;
            }

            aRef = ref Unsafe.Add(ref aRef, this.Stride);
            bRef = ref Unsafe.Add(ref bRef, this.Stride);
        }

        return sum;
    }
}
