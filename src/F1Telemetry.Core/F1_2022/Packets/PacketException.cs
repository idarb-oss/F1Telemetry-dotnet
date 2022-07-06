namespace F1Telemetry.Core.F1_2022.Packets;

public class PacketException : Exception
{
    public PacketException(string message, Exception innerException) : base(message, innerException) { }
}
