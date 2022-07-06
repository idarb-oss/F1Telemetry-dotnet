namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Extensions for an ByteArray
/// </summary>
public static class ByteArrayExtensions
{
    /// <summary>
    /// Convert the byte array to an BinaryReader
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static BinaryReader Reader(this byte[] data)
    {
        return new BinaryReader(new MemoryStream(data));
    }
}

/// <summary>
/// Extensions for Binary Readers
/// </summary>
public static class BinaryReaderExtensions
{
    /// <summary>
    /// Get the header from the UDP packet
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public static PacketHeader GetPacketHeader(this BinaryReader reader)
    {
        return new PacketHeader()
        {
            PacketFormat = reader.ReadUInt16(),
            MajorVersion = reader.ReadSByte(),
            MinorVersion = reader.ReadSByte(),
            PacketVersion = reader.ReadSByte(),
            PacketId = reader.ReadSByte(),
            SessionUID = reader.ReadByte(),
            SessionTime = reader.ReadSingle(),
            FrameIdentifier = reader.ReadUInt32(),
            PlayerCarIndex = reader.ReadSByte(),
            SecondaryPlayerCarIndex = reader.ReadSByte()
        };
    }
}
