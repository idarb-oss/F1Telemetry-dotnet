using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// The different codes representing the type of event arriving
/// </summary>
public static class EventStringCodes
{
    /// <summary>
    /// Sent when the session starts
    /// </summary>
    public static readonly char[] SessionStarted = new[] {'S', 'S', 'T', 'A'};

    /// <summary>
    /// Sent when the session ends
    /// </summary>
    public static readonly char[] SessionEnded = new[] {'S', 'E', 'N', 'D'};

    /// <summary>
    /// When a driver achieves the fastest lap
    /// </summary>
    public static readonly char[] FastestLap = new[] {'F', 'T', 'L', 'P'};

    /// <summary>
    /// When a driver retires
    /// </summary>
    public static readonly char[] Retirement = new[] {'R', 'T', 'M', 'T'};

    /// <summary>
    /// Race control have enabled DRS
    /// </summary>
    public static readonly char[] DrsEnabled = new[] {'D', 'R', 'S', 'E'};

    /// <summary>
    /// Race control have disabled DRS
    /// </summary>
    public static readonly char[] DrsDisabled = new[] {'D', 'R', 'S', 'D'};

    /// <summary>
    /// Your team mate has entered the pits
    /// </summary>
    public static readonly char[] TeamMateInPits = new[] {'T', 'M', 'P', 'T'};

    /// <summary>
    /// The chequered flag has been waved
    /// </summary>
    public static readonly char[] ChequeredFlag = new[] {'C', 'H','Q','F'};

    /// <summary>
    /// The race winner is announced
    /// </summary>
    public static readonly char[] RaceWinner = new[] {'R','C','W','N'};

    /// <summary>
    /// A penalty has been issued – details in event
    /// </summary>
    public static readonly char[] PenaltyIssued = new[] {'P','E','N','A'};

    /// <summary>
    /// Speed trap has been triggered by fastest speed
    /// </summary>
    public static readonly char[] SpeedTrapTriggered = new[] {'S','P','T','P'};

    /// <summary>
    /// Start lights – number shown
    /// </summary>
    public static readonly char[] StartLights = new[] {'S','T','L','G'};

    /// <summary>
    /// Lights out
    /// </summary>
    public static readonly char[] LightsOut = new[] {'L','G','O','T'};

    /// <summary>
    /// Drive through penalty served
    /// </summary>
    public static readonly char[] DriveThroughServed = new[] {'D','T','S','V'};

    /// <summary>
    /// Stop go penalty served
    /// </summary>
    public static readonly char[] StopGoServed = new[] {'S','G','S','V'};

    /// <summary>
    /// Flashback activated
    /// </summary>
    public static readonly char[] Flashback = new[] {'F','L','B','K'};

    /// <summary>
    /// Button status changed
    /// </summary>
    public static readonly char[] ButtonStatus = new[] {'B','U','T','N'};
}

/// <summary>
/// When a driver achieves the fastest lap
/// </summary>
public record FastestLap(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of car achieving fastest lap
    /// </summary>
    public byte VehicleIdx { get; init; }

    /// <summary>
    /// Lap time is in seconds
    /// </summary>
    public float LapTime { get; init; }
}

/// <summary>
/// When a driver retires
/// </summary>
public record Retirement(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of car retiring
    /// </summary>
    public byte VehicleIdx { get; init; }
}

/// <summary>
/// Your team mate has entered the pits
/// </summary>
public record TeamMateInPits(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of team mate
    /// </summary>
    public byte VehicleIdx { get; init; }
}

/// <summary>
/// The race winner is announced
/// </summary>
public record RaceWinner(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of the race winner
    /// </summary>
    public byte VehicleIdx { get; init; }
}

/// <summary>
/// A penalty has been issued – details in event
/// </summary>
public record Penalty(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Penalty type – see Appendices
    /// </summary>
    public byte PenaltyType { get; init; }

    /// <summary>
    /// Infringement type – see Appendices
    /// </summary>
    public byte InfringementType { get; init; }

    /// <summary>
    /// Vehicle index of the car the penalty is applied to
    /// </summary>
    public byte VehicleIdx { get; init; }

    /// <summary>
    /// Vehicle index of the other car involved
    /// </summary>
    public byte OtherVehicleIdx { get; init; }

    /// <summary>
    /// Time gained, or time spent doing action in seconds
    /// </summary>
    public byte Time { get; init; }

    /// <summary>
    /// Lap the penalty occurred on
    /// </summary>
    public byte LapNum { get; init; }

    /// <summary>
    /// Number of places gained by this
    /// </summary>
    public byte PlacesGained { get; init; }
}

/// <summary>
/// Speed trap has been triggered by fastest speed
/// </summary>
public record SpeedTrap(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of the vehicle triggering speed trap
    /// </summary>
    public byte VehicleIdx { get; init; }

    /// <summary>
    /// Top speed achieved in kilometres per hour
    /// </summary>
    public float Speed { get; init; }

    /// <summary>
    /// Overall fastest speed in session = 1, otherwise 0
    /// </summary>
    public byte IsOverallFastestInSession { get; init; }

    /// <summary>
    /// Fastest speed for driver in session = 1, otherwise 0
    /// </summary>
    public byte IsDriverFastestInSession { get; init; }

    /// <summary>
    /// Vehicle index of the vehicle that is the fastest in this session
    /// </summary>
    public byte FastestVehicleIdxInSession { get; init; }

    /// <summary>
    /// Speed of the vehicle that is the fastest in this session
    /// </summary>
    public byte FastestSpeedInSession { get; init; }
}

/// <summary>
/// Start lights – number shown
/// </summary>
public record StartLights(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Number of lights showing
    /// </summary>
    public byte NumLights { get; init; }
}

/// <summary>
/// Drive through penalty served
/// </summary>
public record DriveThroughPenaltyServed(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of the vehicle serving drive through
    /// </summary>
    public byte VehicleIdx { get; init; }
}

/// <summary>
/// Stop go penalty served
/// </summary>
public record StopGoPenaltyServed(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Vehicle index of the vehicle serving stop go
    /// </summary>
    public byte VehicleIdx { get; init; }
}

/// <summary>
/// Flashback activated
/// </summary>
public record Flashback(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Frame identifier flashed back to
    /// </summary>
    public uint FlashbackFrameIdentifier { get; init; }

    /// <summary>
    /// Session time flashed back to
    /// </summary>
    public float FlashbackSessionTime { get; init; }
}

/// <summary>
/// Button status changed
/// </summary>
public record Buttons(Type EventType) : IEventDataDetails
{
    /// <summary>
    /// Bit flags specifying which buttons are being pressed currently - see appendices
    /// </summary>
    public uint ButtonStatus { get; init; }
}


/// <summary>
/// This packet gives details of events that happen during the course of a session.
///
/// Frequency: When the event occurs
/// Version: 1
/// </summary>
public record PacketEventData : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Event string code <see cref="EventStringCode"/>
    /// </summary>
    public char[] EventStringCode { get; init; }

    /// <summary>
    /// Event details - should be interpreted differently for each type
    /// </summary>
    public IEventDataDetails EventDataDetails { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Event Data packets
/// </summary>
public static class PacketEventDataExtensions
{
    private static IEventDataDetails GetEventDataDetails(this BinaryReader reader, char[] eventStringCode)
    {
        switch (eventStringCode)
        {
            case var value when value == EventStringCodes.SessionStarted:
                break;
            case var value when value == EventStringCodes.SessionEnded:
                break;
            case var value when value == EventStringCodes.LightsOut:
                break;
            case var value when value == EventStringCodes.DrsEnabled:
                break;
            case var value when value == EventStringCodes.DrsDisabled:
                break;
            case var value when value == EventStringCodes.ChequeredFlag:
                break;
            case var value when value == EventStringCodes.FastestLap:
                return new FastestLap(typeof(FastestLap))
                {
                    VehicleIdx = reader.ReadByte(),
                    LapTime = reader.ReadSingle()
                };
            case var value when value == EventStringCodes.Retirement:
                return new Retirement(typeof(Retirement))
                {
                    VehicleIdx = reader.ReadByte()
                };
            case var value when value == EventStringCodes.TeamMateInPits:
                return new TeamMateInPits(typeof(TeamMateInPits))
                {
                    VehicleIdx = reader.ReadByte()
                };
            case var value when value == EventStringCodes.RaceWinner:
                return new RaceWinner(typeof(RaceWinner))
                {
                    VehicleIdx = reader.ReadByte()
                };
            case var value when value == EventStringCodes.PenaltyIssued:
                return new Penalty(typeof(Penalty))
                {
                    PenaltyType = reader.ReadByte(),
                    InfringementType = reader.ReadByte(),
                    VehicleIdx = reader.ReadByte(),
                    OtherVehicleIdx = reader.ReadByte(),
                    Time = reader.ReadByte(),
                    LapNum = reader.ReadByte(),
                    PlacesGained = reader.ReadByte()
                };
            case var value when value == EventStringCodes.SpeedTrapTriggered:
                return new SpeedTrap(typeof(SpeedTrap))
                {
                    VehicleIdx = reader.ReadByte(),
                    Speed = reader.ReadSingle(),
                    IsOverallFastestInSession = reader.ReadByte(),
                    IsDriverFastestInSession = reader.ReadByte(),
                    FastestVehicleIdxInSession = reader.ReadByte(),
                    FastestSpeedInSession = reader.ReadByte()
                };
            case var value when value == EventStringCodes.StartLights:
                return new StartLights(typeof(StartLights))
                {
                    NumLights = reader.ReadByte()
                };
            case var value when value == EventStringCodes.DriveThroughServed:
                return new DriveThroughPenaltyServed(typeof(DriveThroughPenaltyServed))
                {
                    VehicleIdx = reader.ReadByte()
                };
            case var value when value == EventStringCodes.StopGoServed:
                return new StopGoPenaltyServed(typeof(StopGoPenaltyServed))
                {
                    VehicleIdx = reader.ReadByte()
                };
            case var value when value == EventStringCodes.Flashback:
                return new Flashback(typeof(Flashback))
                {
                    FlashbackFrameIdentifier = reader.ReadUInt32(),
                    FlashbackSessionTime = reader.ReadSingle()
                };
            case var value when value == EventStringCodes.ButtonStatus:
                return new Buttons(typeof(Buttons))
                {
                    ButtonStatus = reader.ReadUInt32()
                };
            default:
                throw new PacketException("Received unknown event data");
        }

        return null;
    }

    /// <summary>
    /// Parse the packet of event data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketEventData"/></returns>
    /// <exception cref="PacketException"></exception>
    public static PacketEventData GetPacketEventData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            var eventStringCode = reader.ReadChars(4);
            var eventDataDetails = reader.GetEventDataDetails(eventStringCode);

            return new PacketEventData
            {
                Header = header,
                EventStringCode = reader.ReadChars(4),
                EventDataDetails = eventDataDetails
            };
        }
        catch (Exception e)
        {
            throw new PacketException("Could not parse event data", e);
        }
    }
}
