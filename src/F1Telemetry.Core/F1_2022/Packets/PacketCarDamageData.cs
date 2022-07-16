using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents the data for damaged cars
/// </summary>
public record CarDamageData
{
    /// <summary>
    /// Tyre wear (percentage)
    /// </summary>
    public float[] TyresWear { get; init; }

    /// <summary>
    /// Tyre damage (percentage)
    /// </summary>
    public byte[] TyresDamage{ get; init; }

    /// <summary>
    /// Brakes damage (percentage)
    /// </summary>
    public byte[] BrakesDamage{ get; init; }

    /// <summary>
    /// Front left wing damage (percentage)
    /// </summary>
    public byte FrontLeftWingDamage{ get; init; }

    /// <summary>
    /// Front right wing damage (percentage)
    /// </summary>
    public byte FrontRightWingDamage{ get; init; }

    /// <summary>
    /// Rear wing damage (percentage)
    /// </summary>
    public byte RearWingDamage{ get; init; }

    /// <summary>
    /// Floor damage (percentage)
    /// </summary>
    public byte FloorDamage{ get; init; }

    /// <summary>
    /// Diffuser damage (percentage)
    /// </summary>
    public byte DiffuserDamage { get; init; }

    /// <summary>
    /// Sidepod damage (percentage)
    /// </summary>
    public byte SidepodDamage{ get; init; }

    /// <summary>
    /// Indicator for DRS fault, 0 = OK, 1 = fault
    /// </summary>
    public byte DrsFault{ get; init; }

    /// <summary>
    /// Indicator for ERS fault, 0 = OK, 1 = fault
    /// </summary>
    public byte ErsFault{ get; init; }

    /// <summary>
    /// Gear box damage (percentage)
    /// </summary>
    public byte GearBoxDamage{ get; init; }

    /// <summary>
    /// Engine damage (percentage)
    /// </summary>
    public byte EngineDamage{ get; init; }

    /// <summary>
    /// Engine wear MGU-H (percentage)
    /// </summary>
    public byte EngineMGUHWear{ get; init; }

    /// <summary>
    /// Engine wear ES (percentage)
    /// </summary>
    public byte EngineESWear{ get; init; }

    /// <summary>
    /// Engine wear CE (percentage)
    /// </summary>
    public byte EngineCEWear{ get; init; }

    /// <summary>
    /// Engine wear ICE (percentage)
    /// </summary>
    public byte EngineICEWear{ get; init; }

    /// <summary>
    /// Engine wear MGU-K (percentage)
    /// </summary>
    public byte EngineMGUKWear{ get; init; }

    /// <summary>
    /// Engine wear TC (percentage)
    /// </summary>
    public byte EngineTCWear{ get; init; }

    /// <summary>
    /// Engine blown, 0 = OK, 1 = fault
    /// </summary>
    public byte EngineBlown{ get; init; }

    /// <summary>
    /// Engine seized, 0 = OK, 1 = fault
    /// </summary>
    public byte EngineSeized { get; init; }
}

/// <summary>
/// This packet details car damage parameters for all the cars in the race.
///
/// Frequency: 2 per second
/// Version: 1
/// </summary>
public record PacketCarDamageData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Collection of <see cref="CarDamageData"/> - max size 22
    /// </summary>
    public CarDamageData[] CarDamageData { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketCarDamageDataExtensions
{
    private static float[] GetTyresWear(this BinaryReader reader)
    {
        var data = new float[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    private static byte[] GetTyresDamage(this BinaryReader reader)
    {
        var data = new byte[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }

    private static byte[] GetBrakesDamage(this BinaryReader reader)
    {
        var data = new byte[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadByte();
        }

        return data;
    }
    private static CarDamageData GetCarDamageData(this BinaryReader reader)
    {
        return new CarDamageData
        {
            TyresWear = reader.GetTyresWear(),
            TyresDamage = reader.GetTyresDamage(),
            BrakesDamage = reader.GetBrakesDamage(),
            FrontLeftWingDamage = reader.ReadByte(),
            FrontRightWingDamage = reader.ReadByte(),
            RearWingDamage = reader.ReadByte(),
            FloorDamage = reader.ReadByte(),
            DiffuserDamage = reader.ReadByte(),
            SidepodDamage = reader.ReadByte(),
            DrsFault = reader.ReadByte(),
            ErsFault = reader.ReadByte(),
            GearBoxDamage = reader.ReadByte(),
            EngineDamage = reader.ReadByte(),
            EngineMGUHWear = reader.ReadByte(),
            EngineESWear = reader.ReadByte(),
            EngineCEWear = reader.ReadByte(),
            EngineICEWear = reader.ReadByte(),
            EngineMGUKWear = reader.ReadByte(),
            EngineTCWear = reader.ReadByte(),
            EngineBlown = reader.ReadByte(),
            EngineSeized = reader.ReadByte()
        };
    }

    private static CarDamageData[] GetCarDamageDatas(this BinaryReader reader)
    {
        var data = new CarDamageData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetCarDamageData();
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
    public static PacketCarDamageData GetCarDamageData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketCarDamageData
            {
                Header = header,
                CarDamageData = reader.GetCarDamageDatas()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse car telemetry data", e);
        }
    }
}
