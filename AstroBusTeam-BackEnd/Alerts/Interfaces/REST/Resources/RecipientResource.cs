namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a recipient
/// </summary>
public record RecipientResource(
    int Id,
    string Name,
    string Role,
    string? Email,
    string? Phone,
    int? ControlCenterId,
    bool Active,
    IReadOnlyCollection<int> ChannelIds);
