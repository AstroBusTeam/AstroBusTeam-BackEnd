using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to create a delivery.
/// </summary>
/// <param name="RecipientName">Name of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="AlertType">Type of the alert delivered</param>
/// <param name="Priority">Message priority</param>
/// <param name="Status">Delivery status (sent or failed)</param>
/// <param name="SentAt">Date and time of the delivery</param>
public record CreateDeliveryResource(
    [Required(ErrorMessage = "RequiredDeliveryRecipientName")]
    string RecipientName,
    [Required(ErrorMessage = "RequiredDeliveryChannel")]
    string Channel,
    [Required(ErrorMessage = "RequiredDeliveryAlertType")]
    string AlertType,
    [Required(ErrorMessage = "RequiredDeliveryPriority")]
    string Priority,
    string? Status = null,
    DateTime? SentAt = null);
