namespace F1Telemetry.Core.F1_2022.Records;

/// <summary>
/// Represents the different packet types received from F1
/// </summary>
public enum PacketId : sbyte
{
    /// <summary>
    /// Contains all motion data for player’s car – only sent while player is in control
    /// </summary>
    Motion,

    /// <summary>
    /// Data about the session – track, time left
    /// </summary>
    Session,

    /// <summary>
    /// Data about all the lap times of cars in the session
    /// </summary>
    LapData,

    /// <summary>
    /// Various notable events that happen during a session
    /// </summary>
    Event,

    /// <summary>
    /// List of participants in the session, mostly relevant for multiplayer
    /// </summary>
    Participants,

    /// <summary>
    /// Packet detailing car setups for cars in the race
    /// </summary>
    CarSetups,

    /// <summary>
    /// Telemetry data for all cars
    /// </summary>
    CarTelemetry,

    /// <summary>
    /// Status data for all cars
    /// </summary>
    CarStatus,

    /// <summary>
    /// Final classification confirmation at the end of a race
    /// </summary>
    FinalClassification,

    /// <summary>
    /// Information about players in a multiplayer lobby
    /// </summary>
    LobbyInfo,

    /// <summary>
    /// Damage status for all cars
    /// </summary>
    CarDamage,

    /// <summary>
    /// Lap and tyre data for session
    /// </summary>
    SessionHistory
}
