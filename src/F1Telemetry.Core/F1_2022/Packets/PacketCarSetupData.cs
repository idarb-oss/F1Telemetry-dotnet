using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents the data for a Car Setup
/// </summary>
public record CarSetupData
{
    /// <summary>
    /// Front wing aero
    /// </summary>
    public byte FrontWing { get; init; }

    /// <summary>
    /// Rear wing aero
    /// </summary>
    public byte RearWing { get; init; }

    /// <summary>
    /// Differential adjustment on throttle (percentage)
    /// </summary>
    public byte OnThrottle { get; init; }

    /// <summary>
    /// Differential adjustment off throttle (percentage)
    /// </summary>
    public byte OffThrottle { get; init; }

    /// <summary>
    /// Front camber angle (suspension geometry)
    /// </summary>
    public float FrontCamber { get; init; }

    /// <summary>
    /// Rear camber angle (suspension geometry)
    /// </summary>
    public float RearCamber { get; init; }

    /// <summary>
    /// Front toe angle (suspension geometry)
    /// </summary>
    public float FrontToe { get; init; }

    /// <summary>
    /// Rear toe angle (suspension geometry)
    /// </summary>
    public float RearToe { get; init; }

    /// <summary>
    /// Front suspension
    /// </summary>
    public byte FrontSuspension { get; init; }

    /// <summary>
    /// Rear suspension
    /// </summary>
    public byte RearSuspension { get; init; }

    /// <summary>
    /// Front anti-roll bar
    /// </summary>
    public byte FrontAntiRollBar { get; init; }

    /// <summary>
    /// Front anti-roll bar
    /// </summary>
    public byte RearAntiRollBar { get; init; }

    /// <summary>
    /// Front ride height
    /// </summary>
    public byte FrontSuspensionHeight { get; init; }

    /// <summary>
    /// Rear ride height
    /// </summary>
    public byte RearSuspensionHeight { get; init; }

    /// <summary>
    /// Brake pressure (percentage)
    /// </summary>
    public byte BrakePressure { get; init; }

    /// <summary>
    /// Brake bias (percentage)
    /// </summary>
    public byte BrakeBias { get; init; }

    /// <summary>
    /// Rear left tyre pressure (PSI)
    /// </summary>
    public float RearLeftTyrePressure { get; init; }

    /// <summary>
    /// Rear right tyre pressure (PSI)
    /// </summary>
    public float RearRightTyrePressure { get; init; }

    /// <summary>
    /// Front left tyre pressure (PSI)
    /// </summary>
    public float FrontLeftTyrePressure { get; init; }

    /// <summary>
    /// Front right tyre pressure (PSI)
    /// </summary>
    public float FrontRightTyrePressure { get; init; }

    /// <summary>
    /// Ballast
    /// </summary>
    public byte Ballast { get; init; }

    /// <summary>
    /// Fuel load
    /// </summary>
    public float FuelLoad { get; init; }
}

/// <summary>
/// This packet details the car setups for each vehicle in the session. Note that in multiplayer games,
/// other player cars will appear as blank, you will only be able to see your car setup and AI cars.
///
/// Frequency: 2 per second
/// Version: 1
/// </summary>
public record PacketCarSetupData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Setup data for the cars - size 22
    /// </summary>
    public CarSetupData[] CarSetups { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketCarSetupDataExtensions
{
    private static CarSetupData GetCarSetupData(this BinaryReader reader)
    {
        return new CarSetupData
        {
            FrontWing = reader.ReadByte(),
            RearWing = reader.ReadByte(),
            OnThrottle = reader.ReadByte(),
            OffThrottle = reader.ReadByte(),
            FrontCamber = reader.ReadSingle(),
            RearCamber = reader.ReadSingle(),
            FrontToe = reader.ReadSingle(),
            RearToe = reader.ReadSingle(),
            FrontSuspension = reader.ReadByte(),
            RearSuspension = reader.ReadByte(),
            FrontAntiRollBar = reader.ReadByte(),
            RearAntiRollBar = reader.ReadByte(),
            FrontSuspensionHeight = reader.ReadByte(),
            RearSuspensionHeight = reader.ReadByte(),
            BrakePressure = reader.ReadByte(),
            BrakeBias = reader.ReadByte(),
            RearLeftTyrePressure = reader.ReadSingle(),
            RearRightTyrePressure = reader.ReadSingle(),
            FrontLeftTyrePressure = reader.ReadSingle(),
            FrontRightTyrePressure = reader.ReadSingle(),
            Ballast = reader.ReadByte(),
            FuelLoad = reader.ReadByte(),

        };
    }

    private static CarSetupData[] GetCarSetupDatas(this BinaryReader reader)
    {
        var data = new CarSetupData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetCarSetupData();
        }

        return data;
    }

    /// <summary>
    /// Parse the packet of car setup data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketCarSetupData"/></returns>
    /// <exception cref="PacketException"></exception>
    public static PacketCarSetupData GetCarSetupData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            return new PacketCarSetupData
            {
                Header = header,
                CarSetups = reader.GetCarSetupDatas()
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse car setup data", e);
        }
    }
}
