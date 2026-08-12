# .NET Performance Lab

![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4?logo=dotnet)
![BenchmarkDotNet](https://img.shields.io/badge/BenchmarkDotNet-Performance-blue)
![License](https://img.shields.io/badge/License-MIT-green)

A collection of performance benchmarks built with **BenchmarkDotNet**.

This repository explores the performance characteristics of different .NET APIs, language features, and implementation approaches through reproducible benchmarks.

Many benchmark scenarios are executed across **.NET 8**, **.NET 9**, and **.NET 10**, making it easy to observe runtime improvements, memory allocation changes, and behavioural differences between framework versions.

> **Measure first. Optimise second.**

Rather than relying on assumptions, these benchmarks provide measurable results that help developers make informed, evidence-based decisions.

> [!TIP]
> This repository benchmarks identical scenarios across multiple .NET versions, making it easier to observe runtime improvements, JIT optimisations, and memory allocation changes introduced with each release.

---

## Features

- Performance benchmarks powered by BenchmarkDotNet
- Comparisons across .NET 8, .NET 9 and .NET 10
- LINQ performance experiments
- String performance experiments
- Memory allocation analysis
- Runtime and code generation comparisons
- Easy to extend with new benchmark scenarios

---

## Project Structure

```text
src
├── DotNetPerformanceLab.LinqBenchmarks
└── DotNetPerformanceLab.StringBenchmarks
```

---

## Benchmark Projects

### LINQ Benchmarks

Current benchmark scenarios include:

- `List.Exists()` vs `Enumerable.Any()`
- `List.Find()` vs `Enumerable.FirstOrDefault()`
- `Enumerable.Any()` vs `Enumerable.Count()`
- `List.Contains()` with `Enumerable.Any()`
- `OrderBy().First()` vs `MinBy()`

### String Benchmarks

Current benchmark scenarios include:

- String comparison
- String concatenation
- String formatting

---

## Sample Benchmark Results

The following benchmark compares `List.Contains()` with `Enumerable.Any()` across **.NET 8**, **.NET 9**, and **.NET 10**.

It illustrates the significant LINQ performance improvements introduced in newer .NET releases while showing that `List.Contains()` remains the fastest option for direct lookups.

```text
| Method       | Job       | Runtime   | Mean      | Error     | StdDev    | Code Size | Allocated |
|------------- |---------- |---------- |----------:|----------:|----------:|----------:|----------:|
| ListContains | .NET 8.0  | .NET 8.0  |  2.369 μs | 0.0146 μs | 0.0129 μs |     137 B |         - |
| LinqAny      | .NET 8.0  | .NET 8.0  | 27.278 μs | 0.1298 μs | 0.1151 μs |     834 B |      40 B |
| ListContains | .NET 9.0  | .NET 9.0  |  2.124 μs | 0.0368 μs | 0.0344 μs |     137 B |         - |
| LinqAny      | .NET 9.0  | .NET 9.0  |  7.053 μs | 0.0234 μs | 0.0219 μs |     648 B |         - |
| ListContains | .NET 10.0 | .NET 10.0 |  2.222 μs | 0.0280 μs | 0.0234 μs |     332 B |         - |
| LinqAny      | .NET 10.0 | .NET 10.0 |  7.039 μs | 0.0237 μs | 0.0221 μs |     573 B |         - |
```

### Key Takeaways

- `List.Contains()` is consistently the fastest approach in this benchmark.
- `Enumerable.Any()` is approximately **4× faster** in .NET 9 and .NET 10 than in .NET 8.
- The allocation observed in .NET 8 is eliminated in .NET 9 and .NET 10.
- The results demonstrate the continuous performance improvements delivered in recent .NET releases.

---

## Technologies

- .NET 8
- .NET 9
- .NET 10
- BenchmarkDotNet

---

## Running Benchmarks

Clone the repository:

```bash
git clone https://github.com/kaganemre/dotnet-performance-lab.git
```

Navigate to the benchmark project you want to run:

```bash
cd src/DotNetPerformanceLab.LinqBenchmarks
```

Run all benchmarks:

```bash
dotnet run -c Release
```

If the project targets multiple frameworks, you can run a specific .NET version by using the `-f` option:

```bash
dotnet run -c Release -f net8.0
```

```bash
dotnet run -c Release -f net9.0
```

```bash
dotnet run -c Release -f net10.0
```

BenchmarkDotNet generates detailed reports including:

- Execution time
- Memory allocations
- Code size
- Standard deviation
- Statistical analysis
- Runtime information
- Environment details

---

## Why This Repository?

Performance optimisation should always be guided by measurements rather than assumptions.

This repository demonstrates how different APIs, language features, and implementation approaches behave across multiple .NET versions through repeatable benchmarks.

Whether you are comparing LINQ methods, evaluating string operations, or exploring framework improvements, the results are intended to support learning, experimentation, and evidence-based optimisation.

---

## Contributing

Contributions are welcome.

If you have ideas for new benchmark scenarios or performance experiments, feel free to open an issue or submit a pull request.

---

## Licence

This project is licensed under the MIT Licence.