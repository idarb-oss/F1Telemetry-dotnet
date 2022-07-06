namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents the header of an F1 UDP package
/// </summary>
public record PacketHeader()
{
    /// <summary>
    /// The format (2022)
    /// </summary>
    public ushort PacketFormat { get; init; }

    /// <summary>
    /// Game major version - "X.00"
    /// </summary>
    public sbyte MajorVersion { get; init; }

    /// <summary>
    /// Game minor version - "1.XX"
    /// </summary>
    public sbyte MinorVersion { get; init; }

    /// <summary>
    /// Version of this packet type, all start from 1
    /// </summary>
    public sbyte PacketVersion { get; init; }

    /// <summary>
    /// Identifier for the packet type, see below
    /// </summary>
    public sbyte PacketId { get; init; }

    /// <summary>
    /// Unique identifier for the session
    /// </summary>
    public ulong SessionUID { get; init; }

    /// <summary>
    /// Session timestamp
    /// </summary>
    public float SessionTime { get; init; }

    /// <summary>
    /// Identifier for the frame the data was retrieved on
    /// </summary>
    public uint FrameIdentifier { get; init; }

    /// <summary>
    /// Index of player's car in the array
    /// </summary>
    public sbyte PlayerCarIndex { get; init; }

    /// <summary>
    /// Index of secondary player's car in the array (split screen).
    ///
    /// 255 if no second player
    /// </summary>
    public sbyte SecondaryPlayerCarIndex { get; init; }
}