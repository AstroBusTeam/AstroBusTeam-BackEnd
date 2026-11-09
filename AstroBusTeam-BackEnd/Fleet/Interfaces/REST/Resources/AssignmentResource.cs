namespace AstroBusTeamBackEnd.Fleet.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents an assignment
/// </summary>
public record AssignmentResource(
    int Id,
    int DriverId,
    int VehicleId,
    int RouteId,
    string StartTime,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    string Status,
    int? AssignedBy);
