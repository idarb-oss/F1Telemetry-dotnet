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
