using SecurityBus.API.Alerts.Domain.Model.ValueObjects;

namespace SecurityBus.API.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to delete a alert
/// </summary>
/// <param name="Id">The identifier of the alert to delete</param>
public record DeleteAlertCommand(int Id);
