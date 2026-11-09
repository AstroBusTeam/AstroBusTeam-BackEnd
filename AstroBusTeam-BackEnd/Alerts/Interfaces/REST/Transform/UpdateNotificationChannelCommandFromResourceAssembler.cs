using AstroBusTeamBackEnd.Alerts.Domain.Model.Commands;
using AstroBusTeamBackEnd.Alerts.Interfaces.REST.Resources;

namespace AstroBusTeamBackEnd.Alerts.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an update notification channel resource into a command
/// </summary>
public static class UpdateNotificationChannelCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a command.
    /// </summary>
    public static UpdateNotificationChannelCommand ToCommandFromResource(int id, UpdateNotificationChannelResource resource) =>
        new UpdateNotificationChannelCommand(
            id,
            resource.Code);
}
