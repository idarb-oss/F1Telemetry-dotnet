using F1Telemetry.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace F1Telemetry.Core.F1_2022;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddF1Telemetry(this IServiceCollection services)
    {
        services.AddHostedService<TelemetryClient>();
        services.AddSingleton<PacketProcessor>();
        services.AddSingleton<IPacketProcessor>(provider => provider.GetRequiredService<PacketProcessor>());
        services.AddSingleton<IPacketObservable>(provider => provider.GetRequiredService<PacketProcessor>());

        return services;
    }

    public static IServiceCollection AddF1Telemetry(this IServiceCollection services, UdpClientOptions options)
    {
        return services;
    }

    public static IServiceCollection AddF1Telemetry(this IServiceCollection services,
        Action<UdpClientOptions> optionFactory)
    {
        return services;
    }
}
