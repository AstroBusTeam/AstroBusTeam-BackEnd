namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to update a alert type
/// </summary>
public record UpdateAlertTypeCommand(int Id, string Name, string DefaultPriority, bool IsPanic);
