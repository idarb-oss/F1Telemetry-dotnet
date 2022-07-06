namespace F1Telemetry.Core.F1_2022;

/// <summary>
/// Options for the UDP server
/// </summary>
public record UdpClientOptions
{
    /// <summary>
    /// The port to listen to for data
    /// </summary>
    public int Port { get; init; } = 2077;

    /// <summary>
    /// Wait for data for timeout
    /// </summary>
    public int Timeout { get; init; } = 5000;
}
