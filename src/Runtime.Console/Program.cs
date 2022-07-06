using F1Telemetry.Core.F1_2022;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureAppConfiguration(config =>
    {
    })
    .ConfigureLogging(builder =>
    {
        builder.AddJsonConsole();
    })
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddF1Telemetry();
    })
    .UseConsoleLifetime()
    .Build();

host.Run();
