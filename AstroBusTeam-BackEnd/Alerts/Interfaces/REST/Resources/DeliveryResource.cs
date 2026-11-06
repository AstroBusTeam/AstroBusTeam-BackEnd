namespace SecurityBus.API.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data provided by the server about a delivery.
/// </summary>
/// <param name="Id">The server-generated ID of the delivery</param>
/// <param name="RecipientName">Name of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="AlertType">Type of the alert delivered</param>
/// <param name="Priority">Message priority</param>
/// <param name="Status">Delivery status (sent or failed)</param>
/// <param name="SentAt">Date and time of the delivery</param>
public record DeliveryResource(
    int Id,
    string RecipientName,
    string Channel,
    string AlertType,
    string Priority,
    string Status,
    DateTime SentAt);
