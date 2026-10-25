using BenchmarkDotNet.Configs;

namespace DotNetPerformanceLab.CollectionBenchmarks.Benchmarks;

[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
[DisassemblyDiagnoser(printSource: true)]
public class ListHashSetContainsBenchmarks
{
    [Params(64, 128, 256)]
    public int N;

    private List<string> _list = default!;
    private HashSet<string> _set = default!;
    private string _miss = default!;

    [GlobalSetup]
    public void Setup()
    {
        var data = Enumerable.Range(0, N)
            .Select(i => $"user-{i:D6}@example.com")
            .ToArray();

        _list = new List<string>(data);
        _set = new HashSet<string>(data);
        _miss = "user-999999@example.com";
    }

    [Benchmark]
    public bool ListContains() => _list.Contains(_miss);

    [Benchmark]
    public bool HashSetContains() => _set.Contains(_miss);
}