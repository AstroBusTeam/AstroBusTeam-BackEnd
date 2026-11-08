namespace AstroBusTeamBackEnd.Operations.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a vehicle position
/// </summary>
public record VehiclePositionResource(
    int Id,
    int VehicleId,
    int? ShiftId,
    double Latitude,
    double Longitude,
    double SpeedKmh,
    DateTime RecordedAt);
