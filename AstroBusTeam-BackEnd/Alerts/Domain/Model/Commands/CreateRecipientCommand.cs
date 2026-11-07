using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a recipient
/// </summary>
/// <param name="Name">Recipient name</param>
/// <param name="Role">Role of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="Active">Whether the recipient receives alerts</param>
public record CreateRecipientCommand(string Name, string Role, string Channel, bool Active);
