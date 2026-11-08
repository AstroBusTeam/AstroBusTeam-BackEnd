namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource that represents a delivery
/// </summary>
public record DeliveryResource(
    int Id,
    int AlertId,
    int RecipientId,
    int ChannelId,
    int AlertTypeId,
    string Priority,
    string Status,
    DateTime SentAt,
    string? ErrorMessage);
