using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to create a control center
/// </summary>
public record CreateControlCenterResource(
    [Required(ErrorMessage = "RequiredControlCenterCode")]
    string Code,
    [Required(ErrorMessage = "RequiredControlCenterName")]
    string Name,
    [Required(ErrorMessage = "RequiredControlCenterZone")]
    string Zone,
    bool? Active = null);
