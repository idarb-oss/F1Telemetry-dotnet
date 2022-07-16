using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents an final classification data
/// </summary>
public record FinalClassificationData
{
    /// <summary>
    /// Finishing position
    /// </summary>
    public byte Position { get; init; }

    /// <summary>
    /// Number of laps completed
    /// </summary>
    public byte NumLaps { get; init; }

    /// <summary>
    /// Grid position of the car
    /// </summary>
    public byte GridPosition { get; init; }

    /// <summary>
    /// Number of points scored
    /// </summary>
    public byte Points { get; init; }

    /// <summary>
    /// Number of pit stops made
    /// </summary>
    public byte NumPitStops { get; init; }

    /// <summary>
    /// Result status - 0 = invalid, 1 = inactive, 2 = active, 3 = finished, 4 = didnotfinish, 5 = disqualified,
    /// 6 = not classified, 7 = retired
    /// </summary>
    public byte ResultStatus { get; init; }

    /// <summary>
    /// Best lap time of the session in milliseconds
    /// </summary>
    public uint BestLapTimeInMS { get; init; }

    /// <summary>
    /// Total race time in seconds without penalties
    /// </summary>
    public double TotalRaceTime { get; init; }

    /// <summary>
    /// Total penalties accumulated in seconds
    /// </summary>
    public byte PenaltiesTime { get; init; }

    /// <summary>
    /// Number of penalties applied to this driver
    /// </summary>
    public byte NumPenalties { get; init; }

    /// <summary>
    /// Number of tyres stints up to maximum
    /// </summary>
    public byte NumTyreStints { get; init; }

    /// <summary>
    /// Actual tyres used by this driver - size 8
    /// </summary>
    public byte[] TyreStintsActual { get; init; }

    /// <summary>
    /// Visual tyres used by this driver - size 8
    /// </summary>
    public byte[] TyreStintsVisual { get; init; }

    /// <summary>
    /// The lap number stints end on - size 8
    /// </summary>
    public byte[] TyreLapNumberStints { get; init; }
}

/// <summary>
/// This packet details the final classification at the end of the race, and the data will match with the
/// post race results screen. This is especially useful for multiplayer games where it is not always possible
/// to send lap times on the final frame because of network delay.
///
/// Frequency: Once at the end of a race
/// Version: 1
/// </summary>
public record PacketFinalClassificationData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Number of cars in the final classification
    /// </summary>
    public byte NumCars { get; init; }

    /// <summary>
    /// Collection of classification data - max size 22
    /// </summary>
    public FinalClassificationData[] ClassificationData { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketFinalClassificationDataExtensions
{
    private static byte[] GetTyresStintsActual(this BinaryReader reader)
    {
        var data = new byte[8];

        for (var i = 0; i < 8; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }

    private static byte[] GetTyresStingsVisual(this BinaryReader reader)
    {
        var data = new byte[8];

        for (var i = 0; i < 8; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }

    private static byte[] GetTyreLapNumberStints(this BinaryReader reader)
    {
        var data = new byte[8];

        for (var i = 0; i < 8; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }

    private static FinalClassificationData GetFinalClassificationData(this BinaryReader reader)
    {
        return new FinalClassificationData
        {
            Position = reader.ReadByte(),
            NumLaps = reader.ReadByte(),
            GridPosition = reader.ReadByte(),
            Points = reader.ReadByte(),
            NumPitStops = reader.ReadByte(),
            ResultStatus = reader.ReadByte(),
            BestLapTimeInMS = reader.ReadUInt32(),
            TotalRaceTime = reader.ReadDouble(),
            PenaltiesTime = reader.ReadByte(),
            NumPenalties = reader.ReadByte(),
            NumTyreStints = reader.ReadByte(),
            TyreStintsActual = reader.GetTyresStintsActual(),
            TyreStintsVisual = reader.GetTyresStingsVisual(),
            TyreLapNumberStints = reader.GetTyreLapNumberStints()
        };
    }

    private static FinalClassificationData[] GetFinalClassificationDatas(this BinaryReader reader)
    {
        var data = new FinalClassificationData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetFinalClassificationData();
        }

        return data;
    }

    /// <summary>
    /// Parse the packet of final classification data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketFinalClassificationData"/></returns>
    /// <exception cref="PacketException">When the parsing fails</exception>
    public static PacketFinalClassificationData GetFinalClassificationData(this BinaryReader reader,
        PacketHeader header)
    {
        try
        {
            return new PacketFinalClassificationData
            {
                Header = header,
                NumCars = reader.ReadByte(),
                ClassificationData = reader.GetFinalClassificationDatas()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse final classification data", e);
        }
    }
}
