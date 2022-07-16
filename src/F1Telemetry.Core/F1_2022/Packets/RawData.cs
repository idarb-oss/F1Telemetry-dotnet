using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents Raw binary data split up into header and the data packet it self
/// </summary>
public record RawData : IPacket
{
    /// <summary>
    /// Binary data representing the telemetry header
    /// </summary>
    public byte[] Header { get; init; }

    /// <summary>
    /// Binary data representing the telemetry data
    /// </summary>
    public byte[] PacketData { get; init; }
}
