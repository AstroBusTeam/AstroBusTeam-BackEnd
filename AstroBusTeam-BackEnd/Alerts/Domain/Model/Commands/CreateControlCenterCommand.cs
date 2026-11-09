namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a control center
/// </summary>
public record CreateControlCenterCommand(string Code, string Name, string Zone, bool Active);
