using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a notification channel
/// </summary>
public record CreateNotificationChannelResource(
    [Required(ErrorMessage = "RequiredNotificationChannelCode")]
    string Code);
