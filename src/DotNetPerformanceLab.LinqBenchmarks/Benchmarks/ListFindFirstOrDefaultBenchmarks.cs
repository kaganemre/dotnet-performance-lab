using BenchmarkDotNet.Jobs;

namespace DotNetPerformanceLab.LinqBenchmarks.Benchmarks;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class ListFindFirstOrDefaultBenchmarks
{
    private List<Book> _books = null!;
    private const int Count = 10_000;

    [GlobalSetup]
    public void Setup()
    {
        _books = Enumerable.Range(1, Count)
            .Select(i => new Book
            {
                Id = i,
                Title = $"Book {i}"
            })
            .ToList();
    }

    [Benchmark]
    public Book? Find() => _books.Find(x => x.Id == Count);

    [Benchmark]
    public Book? FirstOrDefault() => _books.FirstOrDefault(x => x.Id == Count);
}

public sealed class Book
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
}