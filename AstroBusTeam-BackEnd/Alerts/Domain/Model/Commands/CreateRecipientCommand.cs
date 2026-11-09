namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a recipient
/// </summary>
public record CreateRecipientCommand(string Name, string Role, string? Email, string? Phone, int? ControlCenterId, bool Active, IReadOnlyCollection<int> ChannelIds);
