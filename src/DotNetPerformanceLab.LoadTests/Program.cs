using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetPerformanceLab.LoadTests.BookStoreApi;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddHttpClient("BookStoreApi", client =>
        {
           client.BaseAddress = new Uri("http://localhost:5184");
        });
    })
    .Build();

var factory = host.Services.GetRequiredService<IHttpClientFactory>();
await BookStoreLoadTest.RunAsync(factory);