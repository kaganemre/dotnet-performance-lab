namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class EnumerableAnyCountBenchmarks
{
    // Added Where to force a lazy IEnumerable pipeline.
    // This prevents ICollection-based optimizations and
    // forces Count() to enumerate the entire sequence.
    // Represents LINQ to Objects scenarios (e.g. cache or in-memory results).
    private readonly IEnumerable<int> _queryResults =
        Enumerable.Range(0, 1_000_000)
                  .Where(x => x >= 0);
    
    [Benchmark]
    public bool Any() => _queryResults.Any();

    [Benchmark]
    public bool CountGreaterThanZero() => _queryResults.Count() > 0;
}