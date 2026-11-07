using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to update a alert. The identifier is taken from the route.
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
public record UpdateAlertResource(
    [Required(ErrorMessage = "RequiredAlertCode")]
    string Code,
    int VehicleId,
    [Required(ErrorMessage = "RequiredAlertType")]
    string Type,
    [Required(ErrorMessage = "RequiredAlertReceiver")]
    string Receiver,
    int Attempts,
    [Required(ErrorMessage = "RequiredAlertStatus")]
    string Status,
    DateTime CreatedAt,
    double Latitude,
    double Longitude);
