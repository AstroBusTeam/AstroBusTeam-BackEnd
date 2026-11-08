using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a delivery
/// </summary>
public record CreateDeliveryResource(
    int AlertId,
    int RecipientId,
    int ChannelId,
    int AlertTypeId,
    [Required(ErrorMessage = "RequiredDeliveryPriority")]
    string Priority,
    string? Status = null,
    DateTime? SentAt = null,
    string? ErrorMessage = null);
