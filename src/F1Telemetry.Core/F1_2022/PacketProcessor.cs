using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using F1Telemetry.Core.Abstractions;
using F1Telemetry.Core.F1_2022.Packets;
using Microsoft.Extensions.Logging;

namespace F1Telemetry.Core.F1_2022;

public class PacketProcessor : IPacketProcessor, IPacketObservable
{
    private readonly Subject<IPacket> _subject;

    private readonly IConnectableObservable<IPacket> _connectable;

    private readonly ILogger<PacketProcessor> _logger;

    public PacketProcessor(ILogger<PacketProcessor> logger)
    {
        _logger = logger;
        _subject = new();
        _connectable = _subject.ObserveOn(Scheduler.Default).Publish();
    }

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
            case (sbyte)PacketId.LapData:
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
            var motionData = reader.GetPacketMotionData(header);
            _subject.OnNext(motionData);
        }
        catch (PacketException ex)
        {
            _logger.LogError(ex, "Handling of motion data failed");
        }
    }

    public IObservable<T> Observe<T>()
    {
        return _connectable.OfType<T>();
    }
}
