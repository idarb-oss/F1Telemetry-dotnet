namespace F1Telemetry.Core.Abstractions;

/// <summary>
/// Processor that can process new raw packet data from the F1 Game
/// </summary>
public interface IPacketProcessor
{
    /// <summary>
    /// Trigger processing of new telemetry data
    /// </summary>
    /// <param name="data"></param>
    void ProcessPacket(byte[] data);
}
