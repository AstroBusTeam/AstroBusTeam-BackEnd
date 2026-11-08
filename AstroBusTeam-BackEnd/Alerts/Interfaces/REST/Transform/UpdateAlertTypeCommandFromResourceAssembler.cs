using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update alert type resource into a command
/// </summary>
public static class UpdateAlertTypeCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateAlertTypeCommand ToCommandFromResource(int id, UpdateAlertTypeResource resource) =>
        new UpdateAlertTypeCommand(
            id,
            resource.Name,
            resource.DefaultPriority,
            resource.IsPanic);
}
