using System.ComponentModel.DataAnnotations;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

/// <summary>
///     Resource to update a control center
/// </summary>
public record UpdateControlCenterResource(
    [Required(ErrorMessage = "RequiredControlCenterCode")]
    string Code,
    [Required(ErrorMessage = "RequiredControlCenterName")]
    string Name,
    [Required(ErrorMessage = "RequiredControlCenterZone")]
    string Zone,
    bool Active);
