namespace F1Telemetry.Core.Abstractions;

public interface IPacketObservable
{
    IObservable<T> Observe<T>();
}
