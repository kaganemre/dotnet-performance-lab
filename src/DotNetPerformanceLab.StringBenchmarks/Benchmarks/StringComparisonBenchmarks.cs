namespace DotNetPerformanceLab.StringBenchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class StringComparisonBenchmarks
{
    private string textA = null!;
    private string textB = null!;

    [GlobalSetup]
    public void Setup()
    {
        var baseString = new string('a', 1000);
        textA = baseString.ToUpperInvariant();
        textB = baseString.ToLowerInvariant();
    }

    [Benchmark(Baseline = true)]
    public bool ToLower() 
        => textA.ToLower() == textB.ToLower();

    [Benchmark]
    public bool EqualsOrdinalIgnoreCase() 
        => string.Equals(textA, textB, StringComparison.OrdinalIgnoreCase);
}