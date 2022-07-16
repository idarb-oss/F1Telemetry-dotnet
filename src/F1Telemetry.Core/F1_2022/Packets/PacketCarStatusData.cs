using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents an car status data
/// </summary>
public record CarStatusData
{
    /// <summary>
    /// Traction control - 0 = off, 1 = medium, 2 = full
    /// </summary>
    public byte TractionControl { get; init; }

    /// <summary>
    /// 0 (off) - 1 (on)
    /// </summary>
    public byte AntiLockBrakes { get; init; }

    /// <summary>
    /// Fuel mix - 0 = lean, 1 = standard, 2 = rich, 3 = max
    /// </summary>
    public byte FuelMix { get; init; }

    /// <summary>
    /// Front brake bias (percentage)
    /// </summary>
    public byte FrontBrakeBias { get; init; }

    /// <summary>
    /// Pit limiter status - 0 = off, 1 = on
    /// </summary>
    public byte PitLimiterStatus { get; init; }

    /// <summary>
    /// Current fuel mass
    /// </summary>
    public float FuelInTank { get; init; }

    /// <summary>
    /// Fuel capacity
    /// </summary>
    public float FuelCapacity { get; init; }

    /// <summary>
    /// Fuel remaining in terms of laps (value on MFD)
    /// </summary>
    public float FuelRemainingLaps { get; init; }

    /// <summary>
    /// Cars max RPM, point of rev limiter
    /// </summary>
    public ushort MaxRPM { get; init; }

    /// <summary>
    /// Cars idle RPM
    /// </summary>
    public ushort IdleRPM { get; init; }

    /// <summary>
    /// Maximum number of gears
    /// </summary>
    public byte MaxGears { get; init; }

    /// <summary>
    /// 0 = not allowed, 1 = allowed
    /// </summary>
    public byte DrsAllowed { get; init; }

    /// <summary>
    /// 0 = DRS not available, non-zero - DRS will be available
    /// </summary>
    public ushort DrsActivationDistance { get; init; }

    /// <summary>
    /// F1 Modern - 16 = C5, 17 = C4, 18 = C3, 19 = C2, 20 = C1, 7 = inter, 8 = wet, F1 Classic - 9 = dry, 10 = wet,
    /// F2 – 11 = super soft, 12 = soft, 13 = medium, 14 = hard, 15 = wet
    /// </summary>
    public byte ActualTyreCompound { get; init; }

    /// <summary>
    /// F1 visual (can be different from actual compound)
    ///
    /// 16 = soft, 17 = medium, 18 = hard, 7 = inter, 8 = wet
    ///
    /// F1 Classic – same as above
    ///
    /// F2 ‘19, 15 = wet, 19 – super soft, 20 = soft, 21 = medium , 22 = hard
    /// </summary>
    public byte VisualTyreCompound { get; init; }

    /// <summary>
    /// Age in laps of the current set of tyres
    /// </summary>
    public byte TyresAgeLaps { get; init; }

    /// <summary>
    /// -1 = invalid/unknown, 0 = none, 1 = green, 2 = blue, 3 = yellow, 4 = red
    /// </summary>
    public sbyte VehicleFiaFlags { get; init; }

    /// <summary>
    /// ERS energy store in Joules
    /// </summary>
    public float ErsStoreEnergy { get; init; }

    /// <summary>
    /// ERS deployment mode, 0 = none, 1 = medium, 2 = hotlap, 3 = overtake
    /// </summary>
    public byte ErsDeployMode { get; init; }

    /// <summary>
    /// ERS energy harvested this lap by MGU-K
    /// </summary>
    public float ErsHarvestedThisLapMGUK { get; init; }

    /// <summary>
    /// ERS energy harvested this lap by MGU-H
    /// </summary>
    public float ErsHarvestedThisLapMGUH { get; init; }

    /// <summary>
    /// ERS energy deployed this lap
    /// </summary>
    public float ErsDeployedThisLap { get; init; }

    /// <summary>
    /// Whether the car is paused in a network game
    /// </summary>
    public byte NetworkPaused { get; init; }
}

/// <summary>
/// This packet details car statuses for all the cars in the race.
///
/// Frequency: Rate as specified in menus
/// Version: 1
/// </summary>
public record PacketCarStatusData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// All status data from the cars - max size 22
    /// </summary>
    public CarStatusData[] CarStatusData { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketCarStatusDataExtensions
{
    private static CarStatusData GetCarStatusData(this BinaryReader reader)
    {
        return new CarStatusData
        {
            TractionControl = reader.ReadByte(),
            AntiLockBrakes = reader.ReadByte(),
            FuelMix = reader.ReadByte(),
            FrontBrakeBias = reader.ReadByte(),
            PitLimiterStatus = reader.ReadByte(),
            FuelInTank = reader.ReadSingle(),
            FuelCapacity = reader.ReadSingle(),
            FuelRemainingLaps = reader.ReadSingle(),
            MaxRPM = reader.ReadUInt16(),
            IdleRPM = reader.ReadUInt16(),
            MaxGears = reader.ReadByte(),
            DrsAllowed = reader.ReadByte(),
            DrsActivationDistance = reader.ReadUInt16(),
            ActualTyreCompound = reader.ReadByte(),
            VisualTyreCompound = reader.ReadByte(),
            TyresAgeLaps = reader.ReadByte(),
            VehicleFiaFlags = reader.ReadSByte(),
            ErsStoreEnergy = reader.ReadSingle(),
            ErsDeployMode = reader.ReadByte(),
            ErsHarvestedThisLapMGUK = reader.ReadSingle(),
            ErsHarvestedThisLapMGUH = reader.ReadSingle(),
            ErsDeployedThisLap = reader.ReadSingle(),
            NetworkPaused = reader.ReadByte()
        };
    }

    private static CarStatusData[] GetCarStatusDatas(this BinaryReader reader)
    {
        var data = new CarStatusData[22];

        for (var i = 0; i < 22 ; i++)
        {
            data[i] = reader.GetCarStatusData();
        }

        return data;
    }

    /// <summary>
    /// Parse the packet of car status data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketCarStatusData"/></returns>
    /// <exception cref="PacketException"></exception>
    public static PacketCarStatusData GetCarStatusData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketCarStatusData
            {
                Header = header,
                CarStatusData = reader.GetCarStatusDatas()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse car status data", e);
        }
    }
}
