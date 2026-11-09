namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents an alert
/// </summary>
public record AlertResource(
    int Id,
    string Code,
    int VehicleId,
    int? DriverId,
    int? ShiftId,
    int AlertTypeId,
    int ReceiverId,
    int Attempts,
    string Status,
    DateTime CreatedAt,
    double Latitude,
    double Longitude,
    DateTime? ConfirmedAt,
    int? ConfirmedBy,
    string? Notes);
