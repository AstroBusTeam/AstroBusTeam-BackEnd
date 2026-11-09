using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create alert type resource into a command
/// </summary>
public static class CreateAlertTypeCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateAlertTypeCommand ToCommandFromResource(CreateAlertTypeResource resource) =>
        new CreateAlertTypeCommand(
            resource.Name,
            resource.DefaultPriority ?? "medium",
            resource.IsPanic ?? false);
}
