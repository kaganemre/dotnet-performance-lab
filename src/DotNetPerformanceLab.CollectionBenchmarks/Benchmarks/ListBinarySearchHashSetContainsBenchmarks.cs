using BenchmarkDotNet.Configs;

namespace DotNetPerformanceLab.CollectionBenchmarks.Benchmarks;

// Miss case: List<string>.BinarySearch vs HashSet<string>.Contains.
// The keys are e-mail-like strings of 23 characters, the same length as the searched value.
// The list is already sorted ordinally, so no sorting step is needed.
// Building the collections (Setup) is not part of the measurement.
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByParams)]
[DisassemblyDiagnoser(printSource: true)]
public class ListBinarySearchHashSetContainsBenchmarks
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

        _list = new List<string>(data);    // Already sorted (ordinal)
        _set = new HashSet<string>(data);  // Default string comparer (ordinal)
        _miss = "user-999999@example.com"; // Same length, not in the collection
    }

    // BinarySearch returns an index, or a negative value if the item isn't found.
    // Comparing it with 0 gives a bool, so it answers the same question as Contains.
    [Benchmark]
    public bool ListBinarySearch() => _list.BinarySearch(_miss, StringComparer.Ordinal) >= 0;

    [Benchmark]
    public bool HashSetContains() => _set.Contains(_miss);
}