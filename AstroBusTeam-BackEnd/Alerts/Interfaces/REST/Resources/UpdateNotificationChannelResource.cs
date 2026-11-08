using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a notification channel
/// </summary>
public record UpdateNotificationChannelResource(
    [Required(ErrorMessage = "RequiredNotificationChannelCode")]
    string Code);
