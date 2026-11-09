using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create control center resource into a command
/// </summary>
public static class CreateControlCenterCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateControlCenterCommand ToCommandFromResource(CreateControlCenterResource resource) =>
        new CreateControlCenterCommand(
            resource.Code,
            resource.Name,
            resource.Zone,
            resource.Active ?? true);
}
