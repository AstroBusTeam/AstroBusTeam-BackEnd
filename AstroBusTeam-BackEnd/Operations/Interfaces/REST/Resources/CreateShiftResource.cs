namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a shift
/// </summary>
/// <remarks>
///     The optional values are completed with defaults: the date and start time with the local time of the server,
///     the numbers with zero and the status with <c>active</c>.
/// </remarks>
public record CreateShiftResource(
    int AssignmentId,
    int DriverId,
    int VehicleId,
    int RouteId,
    DateOnly? ShiftDate = null,
    DateTime? StartTime = null,
    DateTime? EndTime = null,
    double? DistanceKm = null,
    int? Passengers = null,
    decimal? Revenue = null,
    string? Status = null,
    double? AverageSpeedKmh = null,
    double? MaxSpeedKmh = null,
    int? OverspeedEvents = null);
