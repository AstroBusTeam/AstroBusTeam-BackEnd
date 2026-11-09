namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to create an alert
/// </summary>
/// <remarks>
///     The code is generated when it is omitted, the attempts default to 1, the status to <c>pending</c> and the
///     creation time to the local time of the server.
/// </remarks>
public record CreateAlertResource(
    int VehicleId,
    int AlertTypeId,
    int ReceiverId,
    double Latitude,
    double Longitude,
    int? DriverId = null,
    int? ShiftId = null,
    string? Code = null,
    int? Attempts = null,
    string? Status = null,
    DateTime? CreatedAt = null,
    DateTime? ConfirmedAt = null,
    int? ConfirmedBy = null,
    string? Notes = null);
