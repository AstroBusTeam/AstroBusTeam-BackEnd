namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data provided by the server about a alert.
/// </summary>
/// <param name="Id">The server-generated ID of the alert</param>
/// <param name="Code">Alert code (generated when omitted)</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="Type">Alert type</param>
/// <param name="Receiver">Receiver of the alert</param>
/// <param name="Attempts">Delivery attempts (1 to 3)</param>
/// <param name="Status">Alert status (pending, confirmed or critical)</param>
/// <param name="CreatedAt">Date and time when the alert occurred</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record AlertResource(
    int Id,
    string Code,
    int VehicleId,
    string Type,
    string Receiver,
    int Attempts,
    string Status,
    DateTime CreatedAt,
    double Latitude,
    double Longitude);
