using F1Telemetry.Core.F1_2022.Records;

namespace F1Telemetry.Core.Abstractions;

public interface IPacketProcessor
{
    void ProcessPacket(byte[] data);
}
