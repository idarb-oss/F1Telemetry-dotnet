namespace F1Telemetry.Core.F1_2022.Records;

/// <summary>
/// Represents the motion data for one car
/// </summary>
public record CarMotionData()
{
    /// <summary>
    /// World space X position
    /// </summary>
    public float WorldPositionX { get; init; }

    /// <summary>
    /// World space Y position
    /// </summary>
    public float WorldPositionY { get; init; }

    /// <summary>
    /// World space Z position
    /// </summary>
    public float WorldPositionZ { get; init; }

    /// <summary>
    /// Velocity in world space X
    /// </summary>
    public float WorldVelocityX { get; init; }

    /// <summary>
    /// Velocity in world space Y
    /// </summary>
    public float WorldVelocityY { get; init; }

    /// <summary>
    /// Velocity in world space Z
    /// </summary>
    public float WorldVelocityZ { get; init; }

    /// <summary>
    /// World space forward X direction (normalised)
    /// </summary>
    public ushort WorldForwardDirX { get; init; }

    /// <summary>
    /// World space forward Y direction (normalised)
    /// </summary>
    public ushort WorldForwardDirY { get; init; }

    /// <summary>
    /// World space forward Z direction (normalised)
    /// </summary>
    public ushort WorldForwardDirZ { get; init; }

    /// <summary>
    /// World space right X direction (normalised)
    /// </summary>
    public ushort WorldRightDirX { get; init; }

    /// <summary>
    /// World space right Y direction (normalised)
    /// </summary>
    public ushort WorldRightDirY { get; init; }

    /// <summary>
    /// World space right Z direction (normalised)
    /// </summary>
    public ushort WorldRightDirZ { get; init; }

    /// <summary>
    /// Lateral G-Force component
    /// </summary>
    public float ForceLateral { get; init; }

    /// <summary>
    /// Longitudinal G-Force component
    /// </summary>
    public float ForceLongitudinal { get; init; }

    /// <summary>
    /// Vertical G-Force component
    /// </summary>
    public float ForceVertical { get; init; }

    /// <summary>
    /// Yaw angle in radians
    /// </summary>
    public float Yaw { get; init; }

    /// <summary>
    /// Pitch angle in radians
    /// </summary>
    public float Pitch { get; init; }

    /// <summary>
    /// Roll angle in radians
    /// </summary>
    public float Roll { get; init; }
}

/// <summary>
/// Represents the Motion Data Packet from the F1 Game
/// </summary>
public record PacketMotionData
{
    /// <summary>
    /// Header Data
    /// </summary>
    public PacketHeader PacketHeader { get; init; }

    /// <summary>
    /// Data for all cars on track
    /// </summary>
    public CarMotionData[] CarMotionData { get; init; }

    /// <summary>
    /// All wheel arrays have the following order: RL, RR, FL, FR
    /// </summary>
    public float[] SuspensionPosition { get; init; }

    /// <summary>
    /// RL, RR, FL, FR
    /// </summary>
    public float[] SuspensionVelocity { get; init; }

    /// <summary>
    /// RL, RR, FL, FR
    /// </summary>
    public float[] SuspensionAcceleration { get; init; }

    /// <summary>
    /// Speed of each wheel4
    /// </summary>
    public float[] WheelSpeed { get; init; }

    /// <summary>
    /// Slip ratio for each wheel
    /// </summary>
    public float[] WheelSlip { get; init; }

    /// <summary>
    /// Velocity in local space
    /// </summary>
    public float LocalVelocityX { get; init; }

    /// <summary>
    /// Velocity in local space
    /// </summary>
    public float LocalVelocityY { get; init; }

    /// <summary>
    /// Velocity in local space
    /// </summary>
    public float LocalVelocityZ { get; init; }

    /// <summary>
    /// Angular velocity x-component
    /// </summary>
    public float AngularVelocityX { get; init; }

    /// <summary>
    /// Angular velocity y-component
    /// </summary>
    public float AngularVelocityY { get; init; }

    /// <summary>
    /// Angular velocity z-component
    /// </summary>
    public float AngularVelocityZ { get; init; }

    /// <summary>
    /// Angular velocity x-component
    /// </summary>
    public float AngularAccelerationX { get; init; }

    /// <summary>
    /// Angular velocity y-component
    /// </summary>
    public float AngularAccelerationY { get; init; }

    /// <summary>
    /// Angular velocity z-component
    /// </summary>
    public float AngularAccelerationZ { get; init; }

    /// <summary>
    /// Current front wheels angle in radians
    /// </summary>
    public float FrontWheelsAngle { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Motion Data packets
/// </summary>
public static class MotionDataExtensions
{
    private static CarMotionData GetCarMotionData(this BinaryReader reader)
    {
        return new CarMotionData
        {
            WorldPositionX = reader.ReadSingle(),
            WorldPositionY = reader.ReadSingle(),
            WorldPositionZ = reader.ReadSingle(),
            WorldVelocityX = reader.ReadSingle(),
            WorldVelocityY = reader.ReadSingle(),
            WorldVelocityZ = reader.ReadSingle(),
            WorldForwardDirX = reader.ReadUInt16(),
            WorldForwardDirY = reader.ReadUInt16(),
            WorldForwardDirZ = reader.ReadUInt16(),
            WorldRightDirX = reader.ReadUInt16(),
            WorldRightDirY = reader.ReadUInt16(),
            WorldRightDirZ = reader.ReadUInt16(),
            ForceLateral = reader.ReadSingle(),
            ForceLongitudinal = reader.ReadSingle(),
            ForceVertical = reader.ReadSingle(),
            Yaw = reader.ReadSingle(),
            Pitch = reader.ReadSingle(),
            Roll = reader.ReadSingle()
        };
    }

    private static CarMotionData[] GetCarMotionDataArray(this BinaryReader reader)
    {
        var data = new CarMotionData[22];

        for (var i = 0; i < 22; i++)
        {
            data[i] = reader.GetCarMotionData();
        }

        return data;
    }

    private static float[] GetSuspensionPos(this BinaryReader reader)
    {
        var data = new float[4];

        for (var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    private static float[] GetSuspensionVelocity(this BinaryReader reader)
    {
        var data = new float[4];

        for(var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    private static float[] GetSuspensionAcceleration(this BinaryReader reader)
    {
        var data = new float[4];

        for(var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    private static float[] GetWheelSpeed(this BinaryReader reader)
    {
        var data = new float[4];

        for(var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    private static float[] GetWheelSlip(this BinaryReader reader)
    {
        var data = new float[4];

        for(var i = 0; i < 4; i++)
        {
            data[i] = reader.ReadSingle();
        }

        return data;
    }

    /// <summary>
    /// Parse the packet data to Motion Data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns></returns>
    public static PacketMotionData GetPacketMotionData(this BinaryReader reader, PacketHeader header)
    {
        return new PacketMotionData
        {
            PacketHeader = header,
            CarMotionData = reader.GetCarMotionDataArray(),
            SuspensionPosition = reader.GetSuspensionPos(),
            SuspensionVelocity = reader.GetSuspensionVelocity(),
            SuspensionAcceleration = reader.GetSuspensionAcceleration(),
            WheelSpeed = reader.GetWheelSpeed(),
            WheelSlip = reader.GetWheelSlip(),
            LocalVelocityX = reader.ReadSingle(),
            LocalVelocityY = reader.ReadSingle(),
            LocalVelocityZ = reader.ReadSingle(),
            AngularVelocityX = reader.ReadSingle(),
            AngularVelocityY = reader.ReadSingle(),
            AngularVelocityZ = reader.ReadSingle(),
            AngularAccelerationX = reader.ReadSingle(),
            AngularAccelerationY = reader.ReadSingle(),
            AngularAccelerationZ = reader.ReadSingle(),
            FrontWheelsAngle = reader.ReadSingle()
        };
    }
}
