namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a alert type
/// </summary>
public record CreateAlertTypeCommand(string Name, string DefaultPriority, bool IsPanic);
