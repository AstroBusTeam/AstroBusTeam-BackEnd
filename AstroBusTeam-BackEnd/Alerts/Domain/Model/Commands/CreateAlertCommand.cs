using SecurityBus.API.Alerts.Domain.Model.ValueObjects;

namespace SecurityBus.API.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a alert
/// </summary>
/// <param name="Code">Alert code (generated when omitted)</param>
/// <param name="VehicleId">Vehicle ID</param>
/// <param name="Type">Alert type</param>
/// <param name="Receiver">Receiver of the alert</param>
/// <param name="Attempts">Delivery attempts (1 to 3)</param>
/// <param name="Status">Alert status (pending, confirmed or critical)</param>
/// <param name="OccurredAt">Date and time when the alert occurred</param>
/// <param name="Latitude">Latitude</param>
/// <param name="Longitude">Longitude</param>
public record CreateAlertCommand(AlertCode Code, int VehicleId, string Type, string Receiver, int Attempts, AlertStatus Status, DateTime OccurredAt, double Latitude, double Longitude);
