using System.Globalization;

namespace DotNetPerformanceLab.StringBenchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class StringFormattingBenchmarks
{
    private decimal _price;

    [GlobalSetup]
    public void Setup()
    {
        _price = 1234.5678m;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
    }

    [Benchmark]
    public string StringFormatWithFormat()
        => string.Format("Price: {0:0.00}", _price);

    [Benchmark]
    public string InterpolatedWithFormat()
        => $"Price: {_price:0.00}";
}