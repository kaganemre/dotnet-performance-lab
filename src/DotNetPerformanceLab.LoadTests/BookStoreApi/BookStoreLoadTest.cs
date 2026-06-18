using System.Text.Json;
using NBomber.Contracts.Stats;
using NBomber.CSharp;
using NBomber.Http.CSharp;

namespace DotNetPerformanceLab.LoadTests.BookStoreApi;

public static class BookStoreLoadTest
{
    private const string BooksEndpoint = "/api/books";

    public static async Task RunAsync(IHttpClientFactory factory)
    {
        var httpClient = factory.CreateClient("BookStoreApi");

        Console.WriteLine("Fetching all books to collect IDs...");

        var bookIds = await FetchAllBookIdsAsync(httpClient);

        if (bookIds.Count == 0)
        {
            throw new InvalidOperationException("No books were found. Load test cannot continue");
        }

        Console.WriteLine($"{bookIds.Count} book IDs collected.");

        var getAllScenario = Scenario.Create("get_all_books", async _ =>
        {
            var request = Http.CreateRequest("GET", BooksEndpoint);
            var response = await Http.Send(httpClient, request);

            return response.IsError ? Response.Fail() : Response.Ok();
        })
        .WithWarmUpDuration(TimeSpan.FromSeconds(5))
        .WithLoadSimulations(
            Simulation.Inject(rate: 10, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1))
        );

        var getByIdScenario = Scenario.Create("get_book_by_id", async _ =>
        {
            var randomId = bookIds[Random.Shared.Next(bookIds.Count)];
            var request = Http.CreateRequest("GET", $"{BooksEndpoint}/{randomId}");
            var response = await Http.Send(httpClient, request);

            return response.IsError ? Response.Fail() : Response.Ok();
        })
        .WithWarmUpDuration(TimeSpan.FromSeconds(5))
        .WithLoadSimulations(
            Simulation.Inject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromMinutes(1))
        );

        NBomberRunner
            .RegisterScenarios(getAllScenario, getByIdScenario)
            .WithReportFormats(ReportFormat.Csv, ReportFormat.Html)
            .Run();
    }

    private static async Task<List<Guid>> FetchAllBookIdsAsync(HttpClient httpClient)
    {
        var response = await httpClient.GetAsync(BooksEndpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var books = JsonSerializer.Deserialize<List<BookResponse>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return books?.Select(b => b.Id).ToList() ?? [];
    }

    private sealed record BookResponse(Guid Id, string Title, string Author, decimal Price, int Stock);
}