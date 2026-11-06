using System.ComponentModel.DataAnnotations;

namespace SecurityBus.API.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to create a alert.
/// </summary>
/// <param name="Code">Alert code (generated when omitted)</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="Type">Alert type</param>
/// <param name="Receiver">Receiver of the alert</param>
/// <param name="Attempts">Delivery attempts (1 to 3)</param>
/// <param name="Status">Alert status (pending, confirmed or critical)</param>
/// <param name="CreatedAt">Date and time when the alert occurred</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record CreateAlertResource(
    int VehicleId,
    [Required(ErrorMessage = "RequiredAlertType")]
    string Type,
    [Required(ErrorMessage = "RequiredAlertReceiver")]
    string Receiver,
    double Latitude,
    double Longitude,
    string? Code = null,
    int? Attempts = null,
    string? Status = null,
    DateTime? CreatedAt = null);
