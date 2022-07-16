using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents history data from the lap
/// </summary>
public record LapHistoryData
{
    /// <summary>
    /// Lap time in milliseconds
    /// </summary>
    public uint LapTimeInMS { get; init; }

    /// <summary>
    /// Sector 1 time in milliseconds
    /// </summary>
    public ushort Sector1TimeInMS { get; init; }

    /// <summary>
    /// Sector 2 time in milliseconds
    /// </summary>
    public ushort Sector2TimeInMS { get; init; }

    /// <summary>
    /// Sector 3 time in milliseconds
    /// </summary>
    public ushort Sector3TimeInMS { get; init; }

    /// <summary>
    /// 0x01 bit set-lap valid, 0x02 bit set-sector 1 valid, 0x04 bit set-sector 2 valid, 0x08 bit set-sector 3 valid
    /// </summary>
    public byte LapValidBitFlags { get; init; }
}

/// <summary>
/// Represents history data for the tyre stint
/// </summary>
public record TyreStintHistoryData
{
    /// <summary>
    /// Lap the tyre usage ends on (255 of current tyre)
    /// </summary>
    public byte EndLap { get; init; }

    /// <summary>
    /// Actual tyres used by this driver
    /// </summary>
    public byte TyreActualCompound { get; init; }

    /// <summary>
    /// Visual tyres used by this driver
    /// </summary>
    /// <returns></returns>
    public byte TyreVisualCompound { get; init; }
}

/// <summary>
/// This packet contains lap times and tyre usage for the session. This packet works slightly differently to other
/// packets. To reduce CPU and bandwidth, each packet relates to a specific vehicle and is sent every 1/20 s, and the
/// vehicle being sent is cycled through. Therefore in a 20 car race you should receive an update for each vehicle
/// at least once per second.
///
/// Note that at the end of the race, after the final classification packet has been sent, a final bulk update of
/// all the session histories for the vehicles in that session will be sent.
///
/// Frequency: 20 per second but cycling through cars
/// Version: 1
/// </summary>
public record PacketSessionHistoryData : IPacket
{
    /// <summary>
    /// Header
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Index of the car this lap data relates to
    /// </summary>
    public byte CarIdx { get; init; }

    /// <summary>
    /// Num laps in the data (including current partial lap)
    /// </summary>
    public byte NumLaps { get; init; }

    /// <summary>
    /// Number of tyre stints in the data
    /// </summary>
    public byte NumTyreStints { get; init; }

    /// <summary>
    /// Lap the best lap time was achieved on
    /// </summary>
    public byte BestLapTimeLapNum { get; init; }

    /// <summary>
    /// Lap the best Sector 1 time was achieved on
    /// </summary>
    public byte BestSector1LapNum { get; init; }

    /// <summary>
    /// Lap the best Sector 2 time was achieved on
    /// </summary>
    public byte BestSector2LapNum { get; init; }

    /// <summary>
    /// Lap the best Sector 3 time was achieved on
    /// </summary>
    public byte BestSector3LapNum { get; init; }

    /// <summary>
    /// 100 laps of data max - size 100
    /// </summary>
    public LapHistoryData LapHistoryData { get; init; }

    /// <summary>
    /// History from the tyre stints - size 8
    /// </summary>
    public TyreStintHistoryData TyreStintsHistoryData { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketSessionHistoryDataExtensions
{
    private static LapHistoryData GetLapHistoryData(this BinaryReader reader)
    {
        return new LapHistoryData
        {
            LapTimeInMS = reader.ReadUInt32(),
            Sector1TimeInMS = reader.ReadUInt16(),
            Sector2TimeInMS = reader.ReadUInt16(),
            Sector3TimeInMS = reader.ReadUInt16(),
            LapValidBitFlags = reader.ReadByte()
        };
    }

    private static TyreStintHistoryData GetTyreStintHistoryData(this BinaryReader reader)
    {
        return new TyreStintHistoryData
        {
            EndLap = reader.ReadByte(),
            TyreActualCompound = reader.ReadByte(),
            TyreVisualCompound = reader.ReadByte()
        };
    }

    private static LapHistoryData[] GetLapHistoryDatas(this BinaryReader reader)
    {
        var data = new LapHistoryData[100];

        for (var i = 0; i < 100; i++)
        {
            data[i] = reader.GetLapHistoryData();
        }

        return data;
    }

    private static TyreStintHistoryData[] GetTyreStintHistoryDatas(this BinaryReader reader)
    {
        var data = new TyreStintHistoryData[8];

        for (var i = 0; i < 8; i++)
        {
            data[i] = reader.GetTyreStintHistoryData();
        }

        return data;
    }

    /// <summary>
    /// Parse the packet of session history data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketCarTelemetryData"/></returns>
    /// <exception cref="PacketException">Parsing error</exception>
    public static PacketSessionHistoryData GetPacketSessionHistoryData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketSessionHistoryData
            {
                Header = header,
                CarIdx = reader.ReadByte(),
                NumLaps = reader.ReadByte(),
                NumTyreStints = reader.ReadByte(),
                BestLapTimeLapNum = reader.ReadByte(),
                BestSector1LapNum = reader.ReadByte(),
                BestSector2LapNum = reader.ReadByte(),
                BestSector3LapNum = reader.ReadByte(),
                LapHistoryData = reader.GetLapHistoryData(),
                TyreStintsHistoryData = reader.GetTyreStintHistoryData()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse session history data", e);
        }
    }
}
