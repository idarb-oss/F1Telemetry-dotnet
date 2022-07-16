namespace F1Telemetry.Core.Abstractions;

/// <summary>
/// Marker interface for events arriving with the event packets
/// </summary>
public interface IEventDataDetails
{
    /// <summary>
    /// The type of the event created
    /// </summary>
    public Type EventType { get; init; }
}
