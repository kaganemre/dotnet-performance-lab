using BenchmarkDotNet.Jobs;

namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[DisassemblyDiagnoser(printSource: true)]
[MemoryDiagnoser]
public class ListOrderByMinByBenchmarks
{
    private List<Product> _products = null!;

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);

        _products = Enumerable.Range(1, 10_000)
            .Select(_ => new Product(Guid.NewGuid(), random.Next(1, 10_000_000) / 100m))
            .ToList();
    }

    [Benchmark]
    public Product OrderByFirst() => _products.OrderBy(x => x.Price).First();

    [Benchmark]
    public Product MinBy() => _products.MinBy(x => x.Price)!;
}

public sealed record Product(Guid Id, decimal Price);