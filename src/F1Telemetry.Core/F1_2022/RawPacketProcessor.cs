using System.Reactive.Linq;
using System.Reactive.Subjects;
using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022;

/// <summary>
/// Used send out raw telemetry data
/// </summary>
public class RawPacketProcessor : IPacketProcessor, IPacketObservable
{
    private readonly Subject<byte[]> _subject;

    /// <summary>
    /// Create a new <see cref="RawPacketProcessor"/>
    /// </summary>
    public RawPacketProcessor()
    {
        _subject = new();
    }

    /// <inheritdoc />
    public void ProcessPacket(byte[] data)
    {
        _subject.OnNext(data);
    }

    /// <inheritdoc />
    public IObservable<T> Observe<T>() where T : IPacket
    {
        return _subject.OfType<T>();
    }
}
