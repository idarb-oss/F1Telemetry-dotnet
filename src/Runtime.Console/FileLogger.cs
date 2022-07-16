using F1Telemetry.Core.Abstractions;

namespace Runtime.Console;

/// <summary>
/// Log the raw F1 Telemetry data to a file
/// </summary>
public class FileLogger
{
    private readonly IPacketObservable _observable;

    /// <summary>
    /// Creates a new file logger for the binary data
    /// </summary>
    /// <param name="observable"></param>
    public FileLogger(IPacketObservable observable)
    {
        _observable = observable;
    }

}
