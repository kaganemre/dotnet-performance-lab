using BenchmarkDotNet.Jobs;

namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[DisassemblyDiagnoser(printSource: true)]
[MemoryDiagnoser]
public class ListAnyCountBenchmarks
{
    private List<int> _list = null!;

    [GlobalSetup]
    public void Setup() => _list = Enumerable.Range(1, 10_000).ToList();

    [Benchmark]
    public bool Any() => _list.Any();

    [Benchmark]
    public bool CountCheck() => _list.Count != 0;
}