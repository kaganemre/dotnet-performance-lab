using BenchmarkDotNet.Jobs;

namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net10_0)]
[DisassemblyDiagnoser(printSource: true)]
[MemoryDiagnoser]
public class ListExistsAnyBenchmarks
{
    private List<int> _numbers = [];

    [GlobalSetup]
    public void Setup() => _numbers = Enumerable.Range(1, 10_000).ToList();

    [Benchmark]
    public bool Exists() => _numbers.Exists(x => x == -1);

    [Benchmark]
    public bool Any() => _numbers.Any(x => x == -1);
}