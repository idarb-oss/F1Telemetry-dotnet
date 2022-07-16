namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Thrown where there are an error during packet parsing
/// </summary>
public class PacketException : Exception
{
    /// <summary>
    /// Packet parsing error with a message
    /// </summary>
    /// <param name="message"></param>
    public PacketException(string message) : base(message) { }

    /// <summary>
    /// Packet parsing error with the underlying exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public PacketException(string message, Exception innerException) : base(message, innerException) { }
}
