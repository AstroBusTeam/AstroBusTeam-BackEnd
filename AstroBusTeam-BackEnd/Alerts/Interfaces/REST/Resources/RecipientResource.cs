namespace SecurityBus.API.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data provided by the server about a recipient.
/// </summary>
/// <param name="Id">The server-generated ID of the recipient</param>
/// <param name="Name">Recipient name</param>
/// <param name="Role">Role of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="Active">Whether the recipient receives alerts</param>
public record RecipientResource(
    int Id,
    string Name,
    string Role,
    string Channel,
    bool Active);
