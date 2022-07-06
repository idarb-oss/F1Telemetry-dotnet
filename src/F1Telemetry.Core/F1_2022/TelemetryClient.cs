using System.Net.Sockets;
using F1Telemetry.Core.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace F1Telemetry.Core.F1_2022;

/// <summary>
/// Implement and Telemetry UDP server for the 2022 version of te F1 game
/// </summary>
public class TelemetryClient : BackgroundService
{
    private readonly IPacketProcessor _processor;

    private readonly ILogger<TelemetryClient> _logger;

    private readonly UdpClient _udpClient;

    /// <summary>
    /// Construct and background service to receive UDP data from the F1 2022 game.
    /// </summary>
    /// <param name="options"></param>
    /// <param name="processor"></param>
    /// <param name="logger"></param>
    public TelemetryClient(IOptions<UdpClientOptions> options, IPacketProcessor processor, ILogger<TelemetryClient> logger)
    {
        _processor = processor;
        _logger = logger;
        _udpClient = new UdpClient(options.Value.Port);
    }

    /// <inheritdoc />
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return ReceivePackets(stoppingToken);
    }

    private async Task ReceivePackets(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(5000);

            try
            {
                var data = await _udpClient.ReceiveAsync(cts.Token);
                _processor.ProcessPacket(data.Buffer);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation(ex, "No data received for 5000ms");
            }
        }
    }
}
