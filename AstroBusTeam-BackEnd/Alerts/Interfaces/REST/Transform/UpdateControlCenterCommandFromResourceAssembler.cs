using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update control center resource into a command
/// </summary>
public static class UpdateControlCenterCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateControlCenterCommand ToCommandFromResource(int id, UpdateControlCenterResource resource) =>
        new UpdateControlCenterCommand(
            id,
            resource.Code,
            resource.Name,
            resource.Zone,
            resource.Active);
}
