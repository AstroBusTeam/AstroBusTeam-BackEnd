using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a create notification channel resource into a command
/// </summary>
public static class CreateNotificationChannelCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static CreateNotificationChannelCommand ToCommandFromResource(CreateNotificationChannelResource resource) =>
        new CreateNotificationChannelCommand(
            resource.Code);
}
