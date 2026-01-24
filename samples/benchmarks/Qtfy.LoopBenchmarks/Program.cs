using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<LoopBenchmarks>();

public class LoopBenchmarks
{
    [Params(10)]
    public int Count;

    [Params(1, 2, 4)]
    public int Stride;

    private double[] _a = Array.Empty<double>();
    private double[] _b = Array.Empty<double>();

    [GlobalSetup]
    public void Setup()
    {
        var length = ((Count - 1) * Stride) + 1;
        _a = new double[length];
        _b = new double[length];

        for (var i = 0; i < length; i++)
        {
            _a[i] = i + 1;
            _b[i] = (i + 1) * 0.25;
        }
    }

    [Benchmark(Baseline = true)]
    public double CountdownWhile()
    {
        var i = Count;
        if (i <= 0)
        {
            return 0.0;
        }

        var sum = 0.0;
        ref var aRef = ref _a[0];
        ref var bRef = ref _b[0];

        while (--i != 0)
        {
            sum += aRef * bRef;
            aRef = ref Unsafe.Add(ref aRef, Stride);
            bRef = ref Unsafe.Add(ref bRef, Stride);
        }

        sum += aRef * bRef;
        return sum;
    }

    [Benchmark]
    public double WhileTrueBreak()
    {
        var i = Count;
        if (i <= 0)
        {
            return 0.0;
        }

        var sum = 0.0;
        ref var aRef = ref _a[0];
        ref var bRef = ref _b[0];

        while (true)
        {
            sum += aRef * bRef;
            if (--i == 0)
            {
                break;
            }

            aRef = ref Unsafe.Add(ref aRef, Stride);
            bRef = ref Unsafe.Add(ref bRef, Stride);
        }

        return sum;
    }
}
