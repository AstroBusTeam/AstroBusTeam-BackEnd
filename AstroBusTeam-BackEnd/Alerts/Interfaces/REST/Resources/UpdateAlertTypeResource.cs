using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a alert type
/// </summary>
public record UpdateAlertTypeResource(
    [Required(ErrorMessage = "RequiredAlertTypeName")]
    string Name,
    [Required(ErrorMessage = "RequiredAlertTypeDefaultPriority")]
    string DefaultPriority,
    bool IsPanic);
