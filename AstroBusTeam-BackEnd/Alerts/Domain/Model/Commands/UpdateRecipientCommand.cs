using SecurityBus.API.Alerts.Domain.Model.ValueObjects;

namespace SecurityBus.API.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to update a recipient
/// </summary>
/// <param name="Id">The identifier of the recipient</param>
/// <param name="Name">Recipient name</param>
/// <param name="Role">Role of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="Active">Whether the recipient receives alerts</param>
public record UpdateRecipientCommand(int Id, string Name, string Role, string Channel, bool Active);
