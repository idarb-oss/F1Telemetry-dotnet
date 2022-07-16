using F1Telemetry.Core.Abstractions;

namespace F1Telemetry.Core.F1_2022.Packets;

/// <summary>
/// Represents an marshal zone
/// </summary>
public record MarshalZone
{
    /// <summary>
    /// Fraction (0..1) of way through the lap the marshal zone starts
    /// </summary>
    public float ZoneStart { get; init; }

    /// <summary>
    /// // -1 = invalid/unknown, 0 = none, 1 = green, 2 = blue, 3 = yellow, 4 = red
    /// </summary>
    public sbyte ZoneFlag { get; init; }
}

/// <summary>
/// Represents an weather forecast
/// </summary>
public record WeatherForecastSample
{
    /// <summary>
    /// 0 = unknown, 1 = P1, 2 = P2, 3 = P3, 4 = Short P, 5 = Q1,  6 = Q2, 7 = Q3, 8 = Short Q, 9 = OSQ,
    /// 10 = R, 11 = R2, 12 = R3, 13 = Time Trial
    /// </summary>
    public byte SessionType { get; init; }

    /// <summary>
    /// Time in minutes the forecast is for
    /// </summary>
    public byte TimeOffset { get; init; }

    /// <summary>
    /// Weather - 0 = clear, 1 = light cloud, 2 = overcast, 3 = light rain, 4 = heavy rain, 5 = storm
    /// </summary>
    public byte Weather { get; init; }

    /// <summary>
    /// Track temp. in degrees Celsius
    /// </summary>
    public sbyte TrackTemperature { get; init; }

    /// <summary>
    /// Track temp. change – 0 = up, 1 = down, 2 = no change
    /// </summary>
    public sbyte TrackTemperatureChange { get; init; }

    /// <summary>
    /// Air temp. in degrees celsius
    /// </summary>
    public sbyte AirTemperature { get; init; }

    /// <summary>
    /// Air temp. change – 0 = up, 1 = down, 2 = no change
    /// </summary>
    public sbyte AirTemperatureChange { get; init; }

    /// <summary>
    /// Rain percentage (0-100)
    /// </summary>
    public byte RainPercentage { get; init; }
}

/// <summary>
/// The session packet includes details about the current session in progress.
///
/// Frequency: 2 per second
/// </summary>
public record PacketSessionData() : IPacket
{
    /// <summary>
    /// The header packet arriving with the data
    /// </summary>
    public PacketHeader Header { get; init; }

    /// <summary>
    /// Weather - 0 = clear, 1 = light cloud, 2 = overcast, 3 = light rain, 4 = heavy rain, 5 = storm
    /// </summary>
    public byte Weather { get; init; }

    /// <summary>
    /// Track temp. in degrees celsius
    /// </summary>
    public sbyte TrackTemperature { get; init; }

    /// <summary>
    /// Air temp. in degrees celsius
    /// </summary>
    public sbyte AirTemperature { get; init; }

    /// <summary>
    /// Total number of laps in this race
    /// </summary>
    public byte TotalLaps { get; init; }

    /// <summary>
    /// Track length in metres
    /// </summary>
    public ushort TrackLength { get; init; }

    /// <summary>
    /// 0 = unknown, 1 = P1, 2 = P2, 3 = P3, 4 = Short P, 5 = Q1, 6 = Q2, 7 = Q3, 8 = Short Q, 9 = OSQ, 10 = R,
    /// 11 = R2, 12 = R3, 13 = Time Trial
    /// </summary>
    public byte SessionType { get; init; }

    /// <summary>
    /// -1 for unknown, see appendix
    /// </summary>
    public sbyte TrackId { get; init; }

    /// <summary>
    /// Formula, 0 = F1 Modern, 1 = F1 Classic, 2 = F2, 3 = F1 Generic, 4 = Beta, 5 = Supercars,
    /// 6 = Esports, 7 = F2 2021
    /// </summary>
    public byte Formula { get; init; }

    /// <summary>
    /// Time left in session in seconds
    /// </summary>
    public ushort SessionTimeLeft { get; init; }

    /// <summary>
    /// Session duration in seconds
    /// </summary>
    public ushort SessionDuration { get; init; }

    /// <summary>
    /// Pit speed limit in kilometres per hour
    /// </summary>
    public byte PitSpeedLimit { get; init; }

    /// <summary>
    /// Whether the game is paused – network game only
    /// </summary>
    public byte GamePaused { get; init; }

    /// <summary>
    /// Whether the player is spectating
    /// </summary>
    public byte IsSpectating { get; init; }

    /// <summary>
    /// Index of the car being spectated
    /// </summary>
    public byte SpectatorCarIndex { get; init; }

    /// <summary>
    /// SLI Pro support, 0 = inactive, 1 = active
    /// </summary>
    public byte SliProNativeSupport { get; init; }

    /// <summary>
    /// Number of marshal zones to follow
    /// </summary>
    public byte NumMarshalZones { get; init; }

    /// <summary>
    /// List of marshal zones – max 21
    /// </summary>
    public MarshalZone[] MarshalZones { get; init; }

    /// <summary>
    /// 0 = no safety car, 1 = full, 2 = virtual, 3 = formation lap
    /// </summary>
    public byte SafetyCarStatus { get; init; }

    /// <summary>
    /// 0 = offline, 1 = online
    /// </summary>
    public byte NetworkGame { get; init; }

    /// <summary>
    /// Number of weather samples to follow
    /// </summary>
    public byte NumWeatherForecastSamples { get; init; }

    /// <summary>
    /// Array of weather forecast samples - max 51
    /// </summary>
    public WeatherForecastSample[] WeatherForecastSamples { get; init; }

    /// <summary>
    /// 0 = Perfect, 1 = Approximate
    /// </summary>
    public byte ForecastAccuracy { get; init; }

    /// <summary>
    /// AI Difficulty rating – 0-110
    /// </summary>
    public byte AiDifficulty { get; init; }

    /// <summary>
    /// Identifier for season - persists across saves
    /// </summary>
    public uint SeasonLinkIdentifier { get; init; }

    /// <summary>
    /// Identifier for weekend - persists across saves
    /// </summary>
    public uint WeekendLinkIdentifier { get; init; }

    /// <summary>
    /// Identifier for session - persists across saves
    /// </summary>
    public uint SessionLinkIdentifier { get; init; }

    /// <summary>
    /// Ideal lap to pit on for current strategy (player)
    /// </summary>
    public byte PitStopWindowIdealLap { get; init; }

    /// <summary>
    /// Latest lap to pit on for current strategy (player)
    /// </summary>
    public byte PitStopWindowLatestLap { get; init; }

    /// <summary>
    /// Predicted position to rejoin at (player)
    /// </summary>
    public byte PitStopRejoinPosition { get; init; }

    /// <summary>
    /// 0 = off, 1 = on
    /// </summary>
    public byte SteeringAssist { get; init; }

    /// <summary>
    /// 0 = off, 1 = low, 2 = medium, 3 = high
    /// </summary>
    public byte BrakingAssist { get; init; }

    /// <summary>
    ///  1 = manual, 2 = manual and suggested gear, 3 = auto
    /// </summary>
    public byte GearboxAssist { get; init; }

    /// <summary>
    /// 0 = off, 1 = on
    /// </summary>
    public byte PitAssist { get; init; }

    /// <summary>
    /// 0 = off, 1 = on
    /// </summary>
    public byte PitReleaseAssist { get; init; }

    /// <summary>
    /// 0 = off, 1 = on
    /// </summary>
    public byte ERSAssist { get; init; }

    /// <summary>
    /// 0 = off, 1 = on
    /// </summary>
    public byte DRSAssist { get; init; }

    /// <summary>
    /// 0 = off, 1 = corners only, 2 = full
    /// </summary>
    public byte DynamicRacingLine { get; init; }

    /// <summary>
    /// 0 = 2D, 1 = 3D
    /// </summary>
    public byte DynamicRacingLineType { get; init; }

    /// <summary>
    /// Game mode id - see appendix
    /// </summary>
    public byte GameMode { get; init; }

    /// <summary>
    /// Ruleset - see appendix
    /// </summary>
    public byte RuleSet { get; init; }

    /// <summary>
    /// Local time of day - minutes since midnight
    /// </summary>
    public uint TimeOfDay { get; init; }

    /// <summary>
    /// 0 = None, 2 = Very Short, 3 = Short, 4 = Medium, 5 = Medium Long, 6 = Long, 7 = Full
    /// </summary>
    public byte SessionLength { get; init; }
}

/// <summary>
/// Extension methods for <see cref="BinaryReader"/> to handle Motion Data packets
/// </summary>
public static class PacketSessionDataExtensions
{
    private static (byte total, MarshalZone[] marshalZones) GetMarshalZones(this BinaryReader reader)
    {
        var total = reader.ReadByte();

        var data = new MarshalZone[total];
        for (var i = 0; i < total; i++)
        {
            data[i] = new MarshalZone
            {
                ZoneStart = reader.ReadSingle(),
                ZoneFlag = reader.ReadSByte()
            };
        }

        return (total, data);
    }

    private static (byte total, WeatherForecastSample[] weatherForecastSamples) GetWeatherForecastSamples(this BinaryReader reader)
    {
        var total = reader.ReadByte();

        var data = new WeatherForecastSample[total];
        for (var i = 0; i < total; i++)
        {
            data[i] = new WeatherForecastSample
            {
                SessionType = reader.ReadByte(),
                TimeOffset = reader.ReadByte(),
                Weather = reader.ReadByte(),
                TrackTemperature = reader.ReadSByte(),
                TrackTemperatureChange = reader.ReadSByte(),
                AirTemperature = reader.ReadSByte(),
                AirTemperatureChange = reader.ReadSByte(),
                RainPercentage = reader.ReadByte()
            };
        }

        return (total, data);
    }

    /// <summary>
    /// Parse the packet of session data
    /// </summary>
    /// <param name="reader"><see cref="BinaryReader"/> with the UDP packet data</param>
    /// <param name="header">The header from the received packet</param>
    /// <returns>Return a new <see cref="PacketSessionData"/></returns>
    /// <exception cref="PacketException"></exception>
    public static PacketSessionData GetPacketSessionData(this BinaryReader reader, PacketHeader header)
    {
        try
        {
            var weather = reader.ReadByte();
            var trackTemperature = reader.ReadSByte();
            var airTemperature = reader.ReadSByte();
            var totalLaps = reader.ReadByte();
            var trackLength = reader.ReadUInt16();
            var sessionType = reader.ReadByte();
            var trackId = reader.ReadSByte();
            var formula = reader.ReadByte();
            var sessionTimeLeft = reader.ReadUInt16();
            var sessionDuration = reader.ReadUInt16();
            var pitSpeedLimit = reader.ReadByte();
            var gamePaused = reader.ReadByte();
            var isSpectating = reader.ReadByte();
            var spectatorCarIndex = reader.ReadByte();
            var sliProNativeSupport = reader.ReadByte();
            var (numMarshalZones, marshalZones) = reader.GetMarshalZones();
            var safetyCarStatus = reader.ReadByte();
            var networkGame = reader.ReadByte();
            var (numWeatherForecastSamples, weatherForecastSamples) = reader.GetWeatherForecastSamples();
            var forecastAccuracy = reader.ReadByte();
            var aiDifficulty = reader.ReadByte();
            var seasonLinkIdentifier = reader.ReadUInt32();
            var weekendLinkIdentifier = reader.ReadUInt32();
            var sessionLinkIdentifier = reader.ReadUInt32();
            var pitStopWindowIdealLap = reader.ReadByte();
            var pitStopWindowLatestLap = reader.ReadByte();
            var pitStopRejoinPosition = reader.ReadByte();
            var steeringAssist = reader.ReadByte();
            var brakingAssist = reader.ReadByte();
            var gearboxAssist = reader.ReadByte();
            var pitAssist = reader.ReadByte();
            var pitReleaseAssist = reader.ReadByte();
            var ersAssist = reader.ReadByte();
            var drsAssist = reader.ReadByte();
            var dynamicRacingLine = reader.ReadByte();
            var dynamicRacingLineType = reader.ReadByte();
            var gameMode = reader.ReadByte();
            var ruleSet = reader.ReadByte();
            var timeOfDay = reader.ReadByte();
            var sessionLength = reader.ReadByte();

            return new PacketSessionData
            {
                Header = header,
                Weather = weather,
                TrackTemperature = trackTemperature,
                AirTemperature = airTemperature,
                TotalLaps = totalLaps,
                TrackLength = trackLength,
                SessionType = sessionType,
                TrackId = trackId,
                Formula = formula,
                SessionTimeLeft = sessionTimeLeft,
                SessionDuration = sessionDuration,
                PitSpeedLimit = pitSpeedLimit,
                GamePaused = gamePaused,
                IsSpectating = isSpectating,
                SpectatorCarIndex = spectatorCarIndex,
                SliProNativeSupport = sliProNativeSupport,
                NumMarshalZones = numMarshalZones,
                MarshalZones = marshalZones,
                SafetyCarStatus = safetyCarStatus,
                NetworkGame = networkGame,
                NumWeatherForecastSamples = numWeatherForecastSamples,
                WeatherForecastSamples = weatherForecastSamples,
                ForecastAccuracy = forecastAccuracy,
                AiDifficulty = aiDifficulty,
                SeasonLinkIdentifier = seasonLinkIdentifier,
                WeekendLinkIdentifier = weekendLinkIdentifier,
                SessionLinkIdentifier = sessionLinkIdentifier,
                PitStopWindowIdealLap = pitStopWindowIdealLap,
                PitStopWindowLatestLap = pitStopWindowLatestLap,
                PitStopRejoinPosition = pitStopRejoinPosition,
                SteeringAssist = steeringAssist,
                BrakingAssist = brakingAssist,
                GearboxAssist = gearboxAssist,
                PitAssist = pitAssist,
                PitReleaseAssist = pitReleaseAssist,
                ERSAssist = ersAssist,
                DRSAssist = drsAssist,
                DynamicRacingLine = dynamicRacingLine,
                DynamicRacingLineType = dynamicRacingLineType,
                GameMode = gameMode,
                RuleSet = ruleSet,
                TimeOfDay = timeOfDay,
                SessionLength = sessionLength
            };
        }
        catch (Exception ex)
        {
            throw new PacketException("Could not parse session data", ex);
        }
    }
}
