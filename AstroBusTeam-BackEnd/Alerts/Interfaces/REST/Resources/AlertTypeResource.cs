namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a alert type
/// </summary>
public record AlertTypeResource(
    int Id,
    string Name,
    string DefaultPriority,
    bool IsPanic);
