using BenchmarkDotNet.Jobs;

namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[DisassemblyDiagnoser(maxDepth: 5, exportCombinedDisassemblyReport: true, exportDiff: true, printSource: true)]
[MemoryDiagnoser]
public class ListOrderByMinByBenchmarks
{
    private List<User> _users = null!;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);

        _users = Enumerable.Range(1, 10_000)
            .Select(_ => new User(Guid.NewGuid(), $"User_{random.Next(0, 1_000_000):D6}"))
            .ToList();
    }

    [Benchmark]
    public User OrderByFirst() => _users.OrderBy(x => x.Name).First();

    [Benchmark]
    public User MinBy() => _users.MinBy(x => x.Name)!;
}

public sealed record User(Guid Id, string Name);