namespace F1Telemetry.Core.Abstractions;

public interface IPacketProcessor
{
    void ProcessPacket(byte[] data);
}
