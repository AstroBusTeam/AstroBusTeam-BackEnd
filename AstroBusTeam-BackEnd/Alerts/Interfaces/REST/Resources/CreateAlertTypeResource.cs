using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a alert type
/// </summary>
public record CreateAlertTypeResource(
    [Required(ErrorMessage = "RequiredAlertTypeName")]
    string Name,
    string? DefaultPriority = null,
    bool? IsPanic = null);
