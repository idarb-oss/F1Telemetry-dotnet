using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Data representing an participant
/// </summary>
public record ParticipantData
{
    /// <summary>
    /// Whether the vehicle is AI (1) or Human (0) controlled
    /// </summary>
    public byte AiControlled { get; init; }

    /// <summary>
    /// Driver id - see appendix, 255 if network human
    /// </summary>
    public byte DriverId { get; init; }

    /// <summary>
    /// Network id – unique identifier for network players
    /// </summary>
    public byte NetworkId { get; init; }

    /// <summary>
    /// Team id - see appendix
    /// </summary>
    public byte TeamId { get; init; }

    /// <summary>
    /// My team flag – 1 = My Team, 0 = otherwise
    /// </summary>
    public byte MyTeam { get; init; }

    /// <summary>
    /// Race number of the car
    /// </summary>
    public byte RaceNumber { get; init; }

    /// <summary>
    /// Nationality of the driver
    /// </summary>
    public byte Nationality { get; init; }

    /// <summary>
    /// Name of participant in UTF-8 format – null terminated
    /// Will be truncated with … (U+2026) if too long
    /// </summary>
    public char[] Name { get; init; }

    /// <summary>
    /// The player's UDP setting, 0 = restricted, 1 = public
    /// </summary>
    public byte YourTelemetry { get; init; }
}

/// <summary>
/// This is a list of participants in the race. If the vehicle is controlled by AI, then the name will be the
/// driver name. If this is a multiplayer game, the names will be the Steam Id on PC, or the LAN name if appropriate.
///
/// N.B. on Xbox One, the names will always be the driver name, on PS4 the name will be the LAN name if playing
/// a LAN game, otherwise it will be the driver name.
///
/// The array should be indexed by vehicle index.
///
/// Frequency: Every 5 seconds
/// Version: 1
/// </summary>
public record PacketParticipantsData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Number of active cars in the data – should match number of cars on HUD
    /// </summary>
    public byte NumActiveCars { get; init; }

    /// <summary>
    /// The data for each participants - size up to 22
    /// </summary>
    public ParticipantData[] Participants { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Participants Data packets
/// </summary>
public static class PacketParticipantsDataExtensions
{
    private static ParticipantData GetParticipantData(this BinaryReader reader)
    {
        return new ParticipantData
        {
            AiControlled = reader.ReadByte(),
            DriverId = reader.ReadByte(),
            NetworkId = reader.ReadByte(),
            TeamId = reader.ReadByte(),
            MyTeam = reader.ReadByte(),
            RaceNumber = reader.ReadByte(),
            Nationality = reader.ReadByte(),
            Name = reader.ReadChars(48),
            YourTelemetry = reader.ReadByte()
        };
    }

    private static ParticipantData[] GetParticipantDatas(this BinaryReader reader, int numActiveCars)
    {
        var data = new List<ParticipantData>();

        for (var i = 0; i < numActiveCars; i++)
        {
            data.Add(reader.GetParticipantData());
        }

        return data.ToArray();
    }

    /// <summary>
    /// Parse the packet of event data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketParticipantsData"/></returns>
    /// <exception cref="PacketException"></exception>
    public static PacketParticipantsData GetParticipantsData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            var numActiveCars = reader.ReadByte();
            return new PacketParticipantsData
            {
                Header = header,
                NumActiveCars = numActiveCars,
                Participants = reader.GetParticipantDatas(numActiveCars)
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse Participant data", e);
        }
    }
}
