using F1Telemetry.Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace F1Telemetry.Core.F1_2022;

/// <summary>
/// Extensions for Service Collections
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the F1 Telemetry to the collection with standard configurations
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to add F1 Telemetry to</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddF1Telemetry(this IServiceCollection services)
    {
        services.AddHostedService<TelemetryClient>();
        services.AddSingleton<PacketProcessor>();
        services.AddSingleton<IPacketProcessor>(provider => provider.GetRequiredService<PacketProcessor>());
        services.AddSingleton<IPacketObservable>(provider => provider.GetRequiredService<PacketProcessor>());
        services.Configure<UdpClientOptions>(options => { });

        return services;
    }

    /// <summary>
    /// Add the F1 Telemetry to the collection with the given option factory
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to add F1 Telemetry to</param>
    /// <param name="optionFactory">Options to use for the UDP client</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddF1Telemetry(this IServiceCollection services,
        Action<UdpClientOptions> optionFactory)
    {
        services.AddHostedService<TelemetryClient>();
        services.AddSingleton<PacketProcessor>();
        services.AddSingleton<IPacketProcessor>(provider => provider.GetRequiredService<PacketProcessor>());
        services.AddSingleton<IPacketObservable>(provider => provider.GetRequiredService<PacketProcessor>());

        services.Configure(optionFactory);

        return services;
    }
}
