using F1Telemetry.Core.F1_2022.Packets;

namespace F1Telemetry.Core.Abstractions;

public interface IPacket
{
    PacketHeader Header { get; }
}
