using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Represents the data required to update a recipient. The identifier is taken from the route.
/// </summary>
/// <param name="Name">Recipient name</param>
/// <param name="Role">Role of the recipient</param>
/// <param name="Channel">Notification channel</param>
/// <param name="Active">Whether the recipient receives alerts</param>
public record UpdateRecipientResource(
    [Required(ErrorMessage = "RequiredRecipientName")]
    string Name,
    [Required(ErrorMessage = "RequiredRecipientRole")]
    string Role,
    [Required(ErrorMessage = "RequiredRecipientChannel")]
    string Channel,
    bool Active);
