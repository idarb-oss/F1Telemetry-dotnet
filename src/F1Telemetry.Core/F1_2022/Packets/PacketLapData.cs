using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Data representing a car on the track
/// </summary>
public record LapData
{
    /// <summary>
    /// Last lap time in milliseconds
    /// </summary>
    public uint LastLapTimeInMs { get; init; }

    /// <summary>
    /// Current time around the lap in milliseconds
    /// </summary>
    public uint CurrentLapTimeInMs { get; init; }

    /// <summary>
    /// Sector 1 time in milliseconds
    /// </summary>
    public ushort Sector1TimeInMs { get; init; }

    /// <summary>
    /// Sector 2 time in milliseconds
    /// </summary>
    public ushort Sector2TimeInMs { get; init; }

    /// <summary>
    /// Distance vehicle is around current lap in metres – could
    /// </summary>
    public float LapDistance { get; init; }

    /// <summary>
    /// Total distance travelled in session in metres – could
    /// </summary>
    public float TotalDistance { get; init; }

    /// <summary>
    /// Delta in seconds for safety car
    /// </summary>
    public float SafetyCarDelta { get; init; }

    /// <summary>
    /// Car race position
    /// </summary>
    public byte CarPosition { get; init; }

    /// <summary>
    /// Current lap number
    /// </summary>
    public byte CurrentLapNum { get; init; }

    /// <summary>
    /// 0 = none, 1 = pitting, 2 = in pit area
    /// </summary>
    public byte PitStatus { get; init; }

    /// <summary>
    /// Number of pit stops taken in this race
    /// </summary>
    public byte NumPitStops { get; init; }

    /// <summary>
    /// 0 = sector1, 1 = sector2, 2 = sector3
    /// </summary>
    public byte Sector { get; init; }

    /// <summary>
    /// Current lap invalid - 0 = valid, 1 = invalid
    /// </summary>
    public byte CurrentLapInvalid { get; init; }

    /// <summary>
    /// Accumulated time penalties in seconds to be added
    /// </summary>
    public byte Penalties { get; init; }

    /// <summary>
    /// Accumulated number of warnings issued
    /// </summary>
    public byte Warnings { get; init; }

    /// <summary>
    /// Num drive through pens left to serve
    /// </summary>
    public byte NumUnservedDriveThroughPens { get; init; }

    /// <summary>
    /// Num stop go pens left to serve
    /// </summary>
    public byte NumUnservedStopGoPens { get; init; }

    /// <summary>
    /// Grid position the vehicle started the race in
    /// </summary>
    public byte GridPosition { get; init; }

    /// <summary>
    /// Status of driver - 0 = in garage, 1 = flying lap
    /// </summary>
    public byte DriverStatus { get; init; }

    /// <summary>
    /// Result status - 0 = invalid, 1 = inactive, 2 = active, Result status - 0 = invalid, 1 = inactive, 2 = active,
    /// 6 = not classified, 7 = retired
    /// </summary>
    public byte ResultStatus { get; init; }

    /// <summary>
    /// Pit lane timing, 0 = inactive, 1 = active
    /// </summary>
    public byte PitLaneTimerActive { get; init; }

    /// <summary>
    /// If active, the current time spent in the pit lane in ms
    /// </summary>
    public ushort PitLaneTimeInLaneInMs { get; init; }

    /// <summary>
    /// Time of the actual pit stop in ms
    /// </summary>
    public ushort PitStopTimerInMs { get; init; }

    /// <summary>
    /// Whether the car should serve a penalty at this stop
    /// </summary>
    public byte PitStopShouldServePen { get; init; }
}

/// <summary>
/// The lap data packet gives details of all the cars in the session.
///
/// Frequency: Rate as specified in menus
/// </summary>
public record PacketLapData : IPacket
{

    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Lap data for all cars on track - 22
    /// </summary>
    public LapData[] LapData { get; init; }

    /// <summary>
    /// Index of Personal Best car in time trial (255 if invalid)
    /// </summary>
    public byte TimeTrialPbCarIdx { get; init; }

    /// <summary>
    /// Index of Rival car in time trial (255 if invalid)
    /// </summary>
    public byte TimeTrialRivalCarIdx { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Lap Data packets
/// </summary>
public static class PacketLapDataExtensions
{
    private static LapData GetLapData(this BinaryReader reader)
    {
        return new LapData
        {
            LastLapTimeInMs = reader.ReadUInt32(),
            CurrentLapTimeInMs = reader.ReadUInt32(),
            Sector1TimeInMs = reader.ReadUInt16(),
            Sector2TimeInMs = reader.ReadUInt16(),
            LapDistance = reader.ReadSingle(),
            TotalDistance = reader.ReadSingle(),
            SafetyCarDelta = reader.ReadSingle(),
            CarPosition = reader.ReadByte(),
            CurrentLapNum = reader.ReadByte(),
            PitStatus = reader.ReadByte(),
            NumPitStops = reader.ReadByte(),
            Sector = reader.ReadByte(),
            CurrentLapInvalid = reader.ReadByte(),
            Penalties = reader.ReadByte(),
            Warnings = reader.ReadByte(),
            NumUnservedDriveThroughPens = reader.ReadByte(),
            NumUnservedStopGoPens = reader.ReadByte(),
            GridPosition = reader.ReadByte(),
            DriverStatus = reader.ReadByte(),
            ResultStatus = reader.ReadByte(),
            PitLaneTimerActive = reader.ReadByte(),
            PitLaneTimeInLaneInMs = reader.ReadUInt16(),
            PitStopTimerInMs = reader.ReadUInt16(),
            PitStopShouldServePen = reader.ReadByte()
        };
    }

    private static LapData[] GetLapDataArray(this BinaryReader reader)
    {
        var data = new LapData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetLapData();
        }

        return data;
    }

    /// <summary>
    /// Parse packet data to Lap Data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Returns a new <see cref="PacketLapData"/></returns>
    /// <exception cref="PacketException">Exception if there is an parsing error</exception>
    public static PacketLapData GetPacketLapData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketLapData
            {
                Header = header,
                LapData = reader.GetLapDataArray(),
                TimeTrialPbCarIdx = reader.ReadByte(),
                TimeTrialRivalCarIdx = reader.ReadByte()
            };
        }
        catch (Exception ex)
        {
            throw new PacketException("Could not parse lap data", ex);
        }
    }
}
