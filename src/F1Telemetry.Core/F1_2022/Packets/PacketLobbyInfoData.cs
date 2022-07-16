using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents lobby info data
/// </summary>
public record LobbyInfoData
{
    /// <summary>
    /// Whether the vehicle is AI (1) or Human (0) controlled
    /// </summary>
    public byte AiControlled { get; init; }

    /// <summary>
    /// Team id - see appendix (255 if no team currently selected)
    /// </summary>
    public byte TeamId { get; init; }

    /// <summary>
    /// Nationality of the driver
    /// </summary>
    public byte Nationality { get; init; }

    /// <summary>
    /// Name of participant in UTF-8 format – null terminated
    /// Will be truncated with ... (U+2026) if too long
    /// </summary>
    public char[] Name { get; init; }

    /// <summary>
    /// Car number of the player
    /// </summary>
    public byte CarNumber { get; init; }

    /// <summary>
    /// 0 = not ready, 1 = ready, 2 = spectating
    /// </summary>
    public byte ReadyStatus { get; init; }
}

/// <summary>
/// This packet details the players currently in a multiplayer lobby. It details each player’s selected car,
/// any AI involved in the game and also the ready status of each of the participants.
///
/// Frequency: Two every second when in the lobby
/// Version: 1
/// </summary>
public record PacketLobbyInfoData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public  PacketHeader Header { get; init; }

    /// <summary>
    /// Number of players in the lobby data
    /// </summary>
    public byte NumPlayers { get; init; }

    /// <summary>
    /// Collection of <see cref="LobbyInfoData"/> for players - max size 22
    /// </summary>
    public LobbyInfoData[] LobbyPlayers { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketLobbyInfoDataExtensions
{
    private static char[] GetName(this BinaryReader reader)
    {
        var data = new char[48];

        for (var i = 0; i < 48; i++)
        {
            data[i] = reader.ReadChar();
        }

        return data;
    }
    private static LobbyInfoData GetlobbyInfoData(this BinaryReader reader)
    {
        return new LobbyInfoData
        {
            AiControlled = reader.ReadByte(),
            TeamId = reader.ReadByte(),
            Nationality = reader.ReadByte(),
            Name = reader.GetName(),
            CarNumber = reader.ReadByte(),
            ReadyStatus = reader.ReadByte()
        };
    }

    private static LobbyInfoData[] GetLobbyInfoDatas(this BinaryReader reader)
    {
        var data = new LobbyInfoData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetlobbyInfoData();
        }

        return data;
    }

    /// <summary>
    /// Parse the packet of car telemetry data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketCarTelemetryData"/></returns>
    /// <exception cref="PacketException"></exception>
    public static PacketLobbyInfoData GetLobbyInfoData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketLobbyInfoData
            {
                Header = header,
                NumPlayers = reader.ReadByte(),
                LobbyPlayers = reader.GetLobbyInfoDatas()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse lobby info data", e);
        }
    }
}
