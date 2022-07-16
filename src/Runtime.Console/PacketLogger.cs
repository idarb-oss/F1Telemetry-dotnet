using F1Telemetry.Core.Abstractions;
using F1Telemetry.Core.F1_2022.Packets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Runtime.Console;

/// <summary>
/// Basic logger task listening for packet data to log
/// </summary>
public class PacketLogger : BackgroundService
{
    private readonly IPacketObservable _packetObservable;
    private readonly ILogger<PacketLogger> _logger;


    /// <inheritdoc />
    public PacketLogger(IPacketObservable packetObservable, ILogger<PacketLogger> logger)
    {
        _packetObservable = packetObservable;
        _logger = logger;
    }

    /// <inheritdoc />
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _packetObservable.Observe<PacketMotionData>().Subscribe(data => _logger.LogInformation("{MotionData}", data.ToString()));
        }, stoppingToken);
    }
}
