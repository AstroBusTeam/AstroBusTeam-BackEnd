namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a vehicle
/// </summary>
public record VehicleResource(
    int Id,
    string Code,
    string Plate,
    string Model,
    int Capacity,
    string Status,
    double CurrentLatitude,
    double CurrentLongitude,
    DateTime? LastPositionAt,
    DateTimeOffset? CreatedAt);
