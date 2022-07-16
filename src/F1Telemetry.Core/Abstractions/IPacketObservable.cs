namespace F1Telemetry.Core.Abstractions;

/// <summary>
/// Used to be able to observe for packet types
/// </summary>
public interface IPacketObservable
{
    /// <summary>
    /// Observe for given packet type
    /// </summary>
    /// <typeparam name="T">Packet type to observe</typeparam>
    /// <returns>Observable that can be subscribed for packet type data</returns>
    IObservable<T> Observe<T>() where T : IPacket;
}
