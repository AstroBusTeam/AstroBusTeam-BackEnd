namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to update a control center
/// </summary>
public record UpdateControlCenterCommand(int Id, string Code, string Name, string Zone, bool Active);
