using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a recipient. When <c>ChannelIds</c> is omitted the current channels are kept.
/// </summary>
public record UpdateRecipientResource(
    [Required(ErrorMessage = "RequiredRecipientName")]
    string Name,
    [Required(ErrorMessage = "RequiredRecipientRole")]
    string Role,
    bool Active,
    string? Email = null,
    string? Phone = null,
    int? ControlCenterId = null,
    int[]? ChannelIds = null);
