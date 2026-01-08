using System.Text;

namespace DotNetPerformanceLab.StringBenchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class StringConcatBenchmarks
{
    private readonly int IterationCount = 1_000;

    [Benchmark(Baseline = true)]
    public string StringPlusEquals()
    {
        var result = string.Empty;

        for (var i = 0; i < IterationCount; i++)
        {
            result += i;
        }

        return result;
    }

    [Benchmark]
    public string StringBuilderAppend()
    {
        StringBuilder sb = new();

        for (var i = 0; i < IterationCount; i++)
        {
            sb.Append(i);
        }

        return sb.ToString();
    }
}
