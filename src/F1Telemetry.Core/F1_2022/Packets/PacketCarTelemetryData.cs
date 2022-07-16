using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents an car telemetry data
/// </summary>
public record CarTelemetryData
{
    /// <summary>
    /// Speed of car in kilometres per hour
    /// </summary>
    public ushort Speed { get; init; }

    /// <summary>
    /// Amount of throttle applied (0.0 to 1.0)
    /// </summary>
    public float Throttle { get; init; }

    /// <summary>
    /// Steering (-1.0 (full lock left) to 1.0 (full lock right))
    /// </summary>
    public float Steer { get; init; }

    /// <summary>
    /// Amount of brake applied (0.0 to 1.0)
    /// </summary>
    public float Brake { get; init; }

    /// <summary>
    /// Amount of clutch applied (0 to 100)
    /// </summary>
    public byte Clutch { get; init; }

    /// <summary>
    /// Gear selected (1-8, N=0, R=-1)
    /// </summary>
    public sbyte Gear { get; init; }

    /// <summary>
    /// Engine RPM
    /// </summary>
    public ushort EngineRpm { get; init; }

    /// <summary>
    /// 0 = off, 1 = on
    /// </summary>
    public byte Drs { get; init; }

    /// <summary>
    /// Rev lights indicator (percentage)
    /// </summary>
    public byte RevLightsPercent { get; init; }

    /// <summary>
    /// Rev lights (bit 0 = leftmost LED, bit 14 = rightmost LED)
    /// </summary>
    public ushort RevLightsBitValue { get; init; }

    /// <summary>
    /// Brakes temperature (celsius)
    /// </summary>
    public ushort[] BrakesTemperature { get; init; }

    /// <summary>
    /// Tyres surface temperature (celsius)
    /// </summary>
    public byte[] TyresSurfaceTemperature { get; init; }

    /// <summary>
    /// Tyres inner temperature (celsius)
    /// </summary>
    public byte[] TyresInnerTemperature { get; init; }

    /// <summary>
    /// Engine temperature (celsius)
    /// </summary>
    public ushort EngineTemperature { get; init; }

    /// <summary>
    /// Tyres pressure (PSI)
    /// </summary>
    public float[] TyresPressure { get; init; }

    /// <summary>
    /// Driving surface, see appendices
    /// </summary>
    public byte[] SurfaceType { get; init; }
}

/// <summary>
/// This packet details telemetry for all the cars in the race. It details various values that would be
/// recorded on the car such as speed, throttle application, DRS etc. Note that the rev light configurations
/// are presented separately as well and will mimic real life driver preferences.
///
/// Frequency: Rate as specified in menus
/// Version: 1
/// </summary>
public record PacketCarTelemetryData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Array of telemetry data from the cars
    /// </summary>
    public CarTelemetryData[] CarTelemetryData { get; init; }

    /// <summary>
    /// Index of MFD panel open - 255 = MFD closed
    ///
    /// Single player, race – 0 = Car setup, 1 = Pits, 2 = Damage, 3 =  Engine, 4 = Temperatures
    ///
    /// May vary depending on game mode
    /// </summary>
    public byte MfdPanelIndex { get; init; }

    /// <summary>
    /// Index of MFD panel open - 255 = MFD closed
    ///
    /// Single player, race – 0 = Car setup, 1 = Pits, 2 = Damage, 3 =  Engine, 4 = Temperatures
    ///
    /// May vary depending on game mode
    /// </summary>
    public byte MfdPanelIndexSecondaryPlayer { get; init; }

    /// <summary>
    /// Suggested gear for the player (1-8)
    /// 0 if no gear suggested
    /// </summary>
    public sbyte SuggestedGear { get; init; }
}


/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketCarTelemetryDataExtensions
{
    private static ushort[] GetBrakesTemperature(this BinaryReader reader)
    {
        var data = new ushort[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadUInt16();
        }

        return data;
    }

    private static byte[] GetTyresSurfaceTemperature(this BinaryReader reader)
    {
        var data = new byte[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }

    private static byte[] GetTyresInnerTemperature(this BinaryReader reader)
    {
        var data = new byte[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }

    private static float[] GetTyresPressure(this BinaryReader reader)
    {
        var data = new float[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    private static byte[] GetSurfaceType(this BinaryReader reader)
    {
        var data = new byte[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }
    private static CarTelemetryData GetCarTelemetryData(this BinaryReader reader)
    {
        return new CarTelemetryData
        {
            Speed = reader.ReadUInt16(),
            Throttle = reader.ReadSingle(),
            Steer = reader.ReadSingle(),
            Brake = reader.ReadSingle(),
            Clutch = reader.ReadByte(),
            Gear = reader.ReadSByte(),
            EngineRpm = reader.ReadUInt16(),
            Drs = reader.ReadByte(),
            RevLightsPercent = reader.ReadByte(),
            RevLightsBitValue = reader.ReadUInt16(),
            BrakesTemperature = reader.GetBrakesTemperature(),
            TyresSurfaceTemperature = reader.GetTyresSurfaceTemperature(),
            TyresInnerTemperature = reader.GetTyresInnerTemperature(),
            EngineTemperature = reader.ReadUInt16(),
            TyresPressure = reader.GetTyresPressure(),
            SurfaceType = reader.GetSurfaceType()
        };
    }

    private static CarTelemetryData[] GetTelemetryDatas(this BinaryReader reader)
    {
        var data = new CarTelemetryData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetCarTelemetryData();
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
    public static PacketCarTelemetryData GetCarTelemetryData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketCarTelemetryData
            {
                Header = header,
                CarTelemetryData = reader.GetTelemetryDatas(),
                MfdPanelIndex = reader.ReadByte(),
                MfdPanelIndexSecondaryPlayer = reader.ReadByte(),
                SuggestedGear = reader.ReadSByte()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse car telemetry data", e);
        }
    }
}
