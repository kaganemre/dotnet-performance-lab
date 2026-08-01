using BenchmarkDotNet.Jobs;

namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[DisassemblyDiagnoser(printSource: true)]
[MemoryDiagnoser]
public class ListContainsAnyBenchmarks
{
    private readonly List<int> _list = [];

    [GlobalSetup]
    public void Setup()
    {
        _list.AddRange(Enumerable.Range(1, 10_000));
    }

    [Benchmark]
    public bool ListContains() => _list.Contains(-1);

    [Benchmark]
    public bool LinqAny() => _list.Any(x => x == -1);
}