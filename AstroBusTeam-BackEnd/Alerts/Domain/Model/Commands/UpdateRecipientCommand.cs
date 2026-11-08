namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to update a recipient. A null <c>ChannelIds</c> keeps the current channels.
/// </summary>
public record UpdateRecipientCommand(int Id, string Name, string Role, string? Email, string? Phone, int? ControlCenterId, bool Active, IReadOnlyCollection<int>? ChannelIds);
