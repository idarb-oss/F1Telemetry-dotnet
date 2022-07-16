using System.Reactive.Linq;
using System.Reactive.Subjects;
using F1Telemetry.Core.Abstractions;
using F1Telemetry.Core.F1_2022.Packets;
using Microsoft.Extensions.Logging;

namespace F1Telemetry.Core.F1_2022;

/// <summary>
/// Processor of telemetry packets that can be observed for new data
/// </summary>
public class PacketProcessor : IPacketProcessor, IPacketObservable
{
    private readonly Subject<IPacket> _subject;

    private readonly ILogger<PacketProcessor> _logger;

    /// <summary>
    /// Construct new <see cref="PacketProcessor"/>
    /// </summary>
    /// <param name="logger">The logger to use</param>
    public PacketProcessor(ILogger<PacketProcessor> logger)
    {
        _logger = logger;
        _subject = new();
    }

    /// <inheritdoc />
    public void ProcessPacket(byte[] data)
    {
        var reader = new BinaryReader(new MemoryStream(data));
        var header = reader.GetPacketHeader();

        _logger.LogDebug("Header: {Header}", header);

        switch (header.PacketId)
        {
            case (sbyte)PacketId.Motion:
                HandleMotionData(reader, header);
                break;
            case (sbyte)PacketId.Session:
                HandleSessionData(reader, header);
                break;
            case (sbyte)PacketId.LapData:
                HandleLapData(reader, header);
                break;
            case (sbyte)PacketId.Event:
            case (sbyte)PacketId.Participants:
            case (sbyte)PacketId.CarSetups:
            case (sbyte)PacketId.CarTelemetry:
            case (sbyte)PacketId.CarStatus:
            case (sbyte)PacketId.FinalClassification:
            case (sbyte)PacketId.LobbyInfo:
            case (sbyte)PacketId.CarDamage:
            case (sbyte)PacketId.SessionHistory:
                _logger.LogInformation("Packet with Id {Id} is not supported yet", header.PacketId.ToString());
                break;
            default:
                _logger.LogWarning("Can not handle data with packet id: {Id}", header.PacketId.ToString());
                break;
        }
    }

    private void HandleMotionData(BinaryReader reader, PacketHeader header)
    {
        try
        {
            var data = reader.GetPacketMotionData(header);
            _subject.OnNext(data);
        }
        catch (PacketException ex)
        {
            _logger.LogError(ex, "Handling of motion data failed");
        }
    }

    private void HandleSessionData(BinaryReader reader, PacketHeader header)
    {
        try
        {
            var data = reader.GetPacketSessionData(header);
            _subject.OnNext(data);
        }
        catch (PacketException ex)
        {
            _logger.LogError(ex, "Handling of session data failed");
        }
    }

    private void HandleLapData(BinaryReader reader, PacketHeader header)
    {
        try
        {
            var data = reader.GetPacketLapData(header);
            _subject.OnNext(data);
        }
        catch (PacketException ex)
        {
            _logger.LogError(ex, "Handling of lap data failed");
        }
    }

    /// <inheritdoc />
    public IObservable<T> Observe<T>() where T : IPacket
    {
        return _subject.OfType<T>();
    }
}
