using AstroBusTeamBackEnd.Alerts.Domain.Model.ValueObjects;

namespace AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;

/// <summary>
///     Command to create a delivery
/// </summary>
/// <param name="RecipientName">Name of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="AlertType">Type of the alert delivered</param>
/// <param name="Priority">Message priority</param>
/// <param name="Status">Delivery status (sent or failed)</param>
/// <param name="SentAt">Date and time of the delivery</param>
public record CreateDeliveryCommand(string RecipientName, string Channel, string AlertType, string Priority, DeliveryStatus Status, DateTime SentAt);
