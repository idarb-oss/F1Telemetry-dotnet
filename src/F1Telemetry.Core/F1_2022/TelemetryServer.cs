using System.Net.Sockets;
using F1Telemetry.Core.F1_2022.Records;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace F1Telemetry.Core.F1_2022;

/// <summary>
/// Implement and Telemetry UDP server for the 2022 version of te F1 game
/// </summary>
public class TelemetryServer : BackgroundService
{
    private readonly ILogger<TelemetryServer> _logger;

    private readonly UdpClient _udpClient;

    /// <summary>
    /// Construct and background service to receive UDP data from the F1 2022 game.
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    public TelemetryServer(IOptions<UdpServerOptions> options, ILogger<TelemetryServer> logger)
    {
        _logger = logger;
        _udpClient = new(options.Value.Port);
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

                var reader = new BinaryReader(new MemoryStream(data.Buffer));
                var header = reader.GetPacketHeader();

                _logger.LogDebug("Header: {Header}", header);

                switch (header.PacketId)
                {
                    case (sbyte)PacketId.Motion:
                        var motionData = reader.GetPacketMotionData(header);
                        _logger.LogInformation("MotionData: {MotionData}", motionData);
                        break;
                    default:
                        _logger.LogWarning("Can not handle data with packet id: {Id}", header.PacketId.ToString());
                        break;
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation("No data received for 5000ms");
            }
        }
    }
}
