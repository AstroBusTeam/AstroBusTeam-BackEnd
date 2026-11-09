using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create an alert
/// </summary>
public record CreateAlertCommand(AlertCode Code, int VehicleId, int? DriverId, int? ShiftId, int AlertTypeId, int ReceiverId, int Attempts, AlertStatus Status, DateTime CreatedAt, double Latitude, double Longitude, DateTime? ConfirmedAt, int? ConfirmedBy, string? Notes);
