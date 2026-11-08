namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a control center
/// </summary>
public record ControlCenterResource(
    int Id,
    string Code,
    string Name,
    string Zone,
    bool Active);
