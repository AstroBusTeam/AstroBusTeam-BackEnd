namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a shift
/// </summary>
public record ShiftResource(
    int Id,
    int AssignmentId,
    int DriverId,
    int VehicleId,
    int RouteId,
    DateOnly ShiftDate,
    DateTime StartTime,
    DateTime? EndTime,
    double DistanceKm,
    int Passengers,
    decimal Revenue,
    string Status,
    double AverageSpeedKmh,
    double MaxSpeedKmh,
    int OverspeedEvents);
