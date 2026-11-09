using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to update an alert
/// </summary>
public record UpdateAlertResource(
    [Required(ErrorMessage = "RequiredAlertCode")]
    string Code,
    int VehicleId,
    int AlertTypeId,
    int ReceiverId,
    int Attempts,
    [Required(ErrorMessage = "RequiredAlertStatus")]
    string Status,
    DateTime CreatedAt,
    double Latitude,
    double Longitude,
    int? DriverId = null,
    int? ShiftId = null,
    DateTime? ConfirmedAt = null,
    int? ConfirmedBy = null,
    string? Notes = null);
